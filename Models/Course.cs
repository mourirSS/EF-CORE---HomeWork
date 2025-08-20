namespace MyApp.Models;

public class Course
{
    public int CourseId { get; set; }
    public required string CourseName { get; set; }
    public int Credits { get; set; }
    public int TeacherId { get; set; }
}