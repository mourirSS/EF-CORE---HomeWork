using Microsoft.Data.SqlClient;
using MyApp.Models;

namespace MyApp.Repositories;

public class CourseRepository
{
    private const string connectionString = @"Server=ITPC6\MSSQLSERVER01;Database=MyDatabase;
    Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

    // READ
    public List<Course> GetAllCourses()
    {
        const string sql = @"
        SELECT CourseId, CourseName, Credits, TeacherId 
        FROM Courses 
        ORDER BY CourseName;";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var command = new SqlCommand(sql, connection);
        var reader = command.ExecuteReader();

        var list = new List<Course>();
        while (reader.Read())
        {
            list.Add(new Course
            {
                CourseId = reader.GetInt32(0),
                CourseName = reader.GetString(1),
                Credits = reader.GetInt32(2),
                TeacherId = reader.GetInt32(3)
            });
        }
        return list;
    }

    // CREATE
    public int Create(Course course)
    {
        string sql = $@"
            INSERT INTO Courses (CourseName, Credits, TeacherId)
            VALUES ('{course.CourseName}', {course.Credits}, {course.TeacherId});";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand(sql, connection);
        int affectedRowsCount = command.ExecuteNonQuery(); 
        return affectedRowsCount;
    }
    
    // UPDATE
    public int Update(Course course)
    {
        string sql = $@"
            UPDATE Courses
            SET CourseName = '{course.CourseName}',
            Credits    = {course.Credits},
            TeacherId  = {course.TeacherId}
            WHERE CourseId = {course.CourseId};";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand(sql, connection);
        int affectedRowsCount = command.ExecuteNonQuery();   
        return affectedRowsCount;
    }

    // DELETE
    public int Delete(int courseId)
    {
        string sql = $@"
        DELETE FROM Enrollments 
        WHERE CourseId = {courseId};
        DELETE FROM Courses 
        WHERE CourseId = {courseId};";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand(sql, connection);
        int affectedRowsCount = command.ExecuteNonQuery();   
        return affectedRowsCount;
    }
}
