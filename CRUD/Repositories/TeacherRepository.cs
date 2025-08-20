using Microsoft.Data.SqlClient;
using MyApp.Models;

namespace MyApp.Repositories;

public class TeacherRepository
{
    private const string connectionString = @"Server=ITPC6\MSSQLSERVER01;Database=MyDatabase;
    Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

    //READ 
    public List<Teacher> GetAllTeachers()
    {
        const string sql = @"
        SELECT TeacherId, FullName, Email 
        FROM Teachers 
        ORDER BY FullName;";
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var command = new SqlCommand(sql, connection);
        var reader = command.ExecuteReader();

        var list = new List<Teacher>();
        while (reader.Read())
        {
            list.Add(new Teacher
            {
                TeacherId = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Email = reader.IsDBNull(2) ? null : reader.GetString(2)
            });
        }
        return list;
    }

    //CREATE
    public int Create(Teacher teacher)
    {
        string sql = $@"
        INSERT INTO Teachers (FullName, Email) 
        VALUES ('{teacher.FullName}', '{teacher.Email}');";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand(sql, connection);
        int affectedRowsCount = command.ExecuteNonQuery();

        return affectedRowsCount;
    }

    //UPDATE
    public int Update(Teacher teacher)
    {
        string sql = $@"
                UPDATE Teachers 
                SET FullName = '{teacher.FullName}', 
                Email = '{teacher.Email}' 
                WHERE TeacherId = {teacher.TeacherId};";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand(sql, connection);
        int affectedRowsCount = command.ExecuteNonQuery();
        return affectedRowsCount;
    }

    //DELETE
    public int Delete(int teacherId)
    {                   //этот стринг на работе помогли. в связях запутался.
        string sql = $@" 
        DELETE FROM Enrollments 
        WHERE CourseId IN (SELECT CourseId FROM Courses WHERE TeacherId = {teacherId});

        DELETE FROM Courses 
        WHERE TeacherId = {teacherId};

        DELETE FROM Teachers 
        WHERE TeacherId = {teacherId};";


        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand(sql, connection);
        int affectedRowsCount = command.ExecuteNonQuery();
        return affectedRowsCount;
    }
}