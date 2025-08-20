using Microsoft.Data.SqlClient;
using MyApp.Models;
namespace MyApp.Repositories;

public class StudentRepository
{
    private const string connectionString = @"Server=ITPC6\MSSQLSERVER01;Database=MyDatabase;
    Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

    //READ 
    public List<Student> GetAllStudents()
    {
        const string sql = @"
        SELECT StudentId, FullName, BirthDate 
        FROM Students 
        ORDER BY FullName";
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var command = new SqlCommand(sql, connection);
        var reader = command.ExecuteReader();

        var list = new List<Student>();
        while (reader.Read())
        {
            list.Add(new Student
            {
                StudentId = reader.GetInt32(0),
                FullName = reader.GetString(1),
                BirthDate = reader.GetDateTime(2)
            });
        }
        return list;
    }

    //CREATE
    public int Create(Student student)
    {
        string sql = $@"
        INSERT INTO Students (FullName, BirthDate) 
        VALUES ('{student.FullName}', '{student.BirthDate:yyyy-MM-dd}');";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand(sql, connection);
        int affectedRowsCount = command.ExecuteNonQuery();

        return affectedRowsCount;
    }

    //UPDATE
    public int Update(Student student)
    {
        string sql = $@"
        UPDATE Students 
        SET FullName = '{student.FullName}', 
        BirthDate = '{student.BirthDate:yyyy-MM-dd}' 
        WHERE StudentId = {student.StudentId};";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand(sql, connection);
        int affectedRowsCount = command.ExecuteNonQuery();
        return affectedRowsCount;
    }

    //DELETE
    public int Delete(int studentId)
    {
        string sql = $@"
        DELETE FROM Enrollments 
        WHERE StudentId = {studentId};
        DELETE FROM Students 
        WHERE StudentId = {studentId};";

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand(sql, connection);
        int affectedRowsCount = command.ExecuteNonQuery();
        return affectedRowsCount;
    }
}
