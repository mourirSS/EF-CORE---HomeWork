namespace MyApp.Models;
public class Student
{
    public int StudentId { get; set; }
    public required string FullName { get; set; }
    public DateTime BirthDate { get; set; }
}
