using Microsoft.Data.SqlClient;
using MyApp.Models;

namespace MyApp.Repositories;

public class EnrollmentRepository
{
    private const string connectionString = @"Server=ITPC6\MSSQLSERVER01;Database=MyDatabase;
    Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

    // READ
    public List<Enrollment> GetAllEnrollments()
    {
        const string sql = @"
        SELECT EnrollmentId, StudentId, CourseId, Grade 
        FROM Enrollments 
        ORDER BY EnrollmentId;";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var command = new SqlCommand(sql, connection);
        var reader = command.ExecuteReader();

        var list = new List<Enrollment>();
        while (reader.Read())
        {
            list.Add(new Enrollment
            {
                EnrollmentId = reader.GetInt32(0),
                StudentId = reader.GetInt32(1),
                CourseId = reader.GetInt32(2),
                Grade = reader.GetInt32(3)
            });
        }
        return list;
    }

    // UPDATE
    public int Update(Enrollment enrollment)
    {
        string sql = $@"
            UPDATE Enrollments
            SET StudentId = {enrollment.StudentId}, 
                CourseId  = {enrollment.CourseId}, 
                Grade = {enrollment.Grade}
            WHERE EnrollmentId = {enrollment.EnrollmentId};";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand(sql, connection);
        int affectedRowsCount = command.ExecuteNonQuery();
        return affectedRowsCount;
    }

    // DELETE
    public int Delete(int enrollmentId)
    {
        string sql = $@"
            DELETE FROM Enrollments 
            WHERE EnrollmentId = {enrollmentId};";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand(sql, connection);
        int affectedRowsCount = command.ExecuteNonQuery();
        return affectedRowsCount;
    }

    //GOOD VIEW (JOIN)
    public List<EnrollmentView> GetAllDetailed()
    {
        const string sql = @"
        SELECT 
            e.EnrollmentId,
            s.FullName   AS StudentName,
            c.CourseName AS CourseName,
            e.Grade,
            t.FullName   AS TeacherName
        FROM Enrollments e
        JOIN Students s ON s.StudentId = e.StudentId
        JOIN Courses  c ON c.CourseId  = e.CourseId
        JOIN Teachers t ON t.TeacherId = c.TeacherId
        ORDER BY e.EnrollmentId;";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var command = new SqlCommand(sql, connection);
        var reader  = command.ExecuteReader();

        var list = new List<EnrollmentView>();
        while (reader.Read())
        {
            list.Add(new EnrollmentView
            {
                EnrollmentId = reader.GetInt32(0),
                StudentName  = reader.GetString(1),
                CourseName   = reader.GetString(2),
                Grade        = reader.GetInt32(3),
                TeacherName  = reader.GetString(4)
            });
        }
        return list;
    }
}
