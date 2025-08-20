namespace MyApp.Models;

public class Teacher
{
    public int TeacherId { get; set; }
    public required string FullName { get; set; }
    public string? Email { get; set; }
}
