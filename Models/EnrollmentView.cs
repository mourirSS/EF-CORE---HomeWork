namespace MyApp.Models;

public class EnrollmentView
{
    public int EnrollmentId { get; set; }
    public string StudentName { get; set; } = null!;
    public string CourseName { get; set; } = null!;
    public int Grade { get; set; }
    public string TeacherName { get; set; } = null!;
}
