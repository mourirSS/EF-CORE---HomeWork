using MyApp.Models;
using MyApp.Repositories;

namespace MyApp.Menus;

public class EnrollmentsMenu
{
    private readonly EnrollmentRepository _repo;
    public EnrollmentsMenu(EnrollmentRepository repo) => _repo = repo;

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n--- Enrollments ---");
            Console.WriteLine("1) List");
            Console.WriteLine("2) Update");
            Console.WriteLine("3) Delete");
            Console.WriteLine("4) List detailed  (Student · Course · Teacher)");
            Console.WriteLine("0) Back");
            Console.Write("Choice: ");
            var ch = Console.ReadLine();
            if (ch == "0") return;

            try
            {
                switch (ch)
                {
                    case "1":
                    {
                        var list = _repo.GetAllEnrollments();
                        if (list.Count == 0) { Console.WriteLine("No enrollments."); break; }
                        foreach (var e in list)
                            Console.WriteLine($"{e.EnrollmentId}\tStudent:{e.StudentId}\tCourse:{e.CourseId}\tGrade:{e.Grade}");
                        break;
                    }

                    case "2":
                    {
                        Console.Write("EnrollmentId to update: ");
                        if (!int.TryParse(Console.ReadLine(), out var eid)) { Console.WriteLine("Invalid id."); break; }
                        Console.Write("New StudentId (FK): ");
                        if (!int.TryParse(Console.ReadLine(), out var nsid)) { Console.WriteLine("Invalid student id."); break; }
                        Console.Write("New CourseId (FK): ");
                        if (!int.TryParse(Console.ReadLine(), out var ncid)) { Console.WriteLine("Invalid course id."); break; }
                        Console.Write("New Grade (int): ");
                        if (!int.TryParse(Console.ReadLine(), out var ngrade)) { Console.WriteLine("Invalid grade."); break; }

                        var upd = _repo.Update(new Enrollment { EnrollmentId = eid, StudentId = nsid, CourseId = ncid, Grade = ngrade });
                        Console.WriteLine(upd == 1 ? "Updated." : "Not found.");
                        break;
                    }

                    case "3":
                    {
                        Console.Write("EnrollmentId to delete: ");
                        if (!int.TryParse(Console.ReadLine(), out var did)) { Console.WriteLine("Invalid id."); break; }
                        var del = _repo.Delete(did);
                        Console.WriteLine(del >= 1 ? "Deleted." : "Not found.");
                        break;
                    }

                    case "4":
                    {
                        var detailed = _repo.GetAllDetailed(); // метод с JOIN, как писали ранее
                        if (detailed.Count == 0) { Console.WriteLine("No enrollments."); break; }
                        foreach (var d in detailed)
                            Console.WriteLine($"{d.EnrollmentId}\t{d.StudentName}\t{d.CourseName}\tGrade:{d.Grade}\tTeacher:{d.TeacherName}");
                        break;
                    }

                    default:
                        Console.WriteLine("Unknown option.");
                        break;
                }
            }
            catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
        }
    }
}
