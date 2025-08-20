using MyApp.Models;
using MyApp.Repositories;

namespace MyApp.Menus;

public class CoursesMenu
{
    private readonly CourseRepository _repo;
    public CoursesMenu(CourseRepository repo) => _repo = repo;

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n--- Courses ---");
            Console.WriteLine("1) List");
            Console.WriteLine("2) Create");
            Console.WriteLine("3) Update");
            Console.WriteLine("4) Delete");
            Console.WriteLine("0) Back");
            Console.Write("Choice: ");
            var ch = Console.ReadLine();
            if (ch == "0") return;

            try
            {
                switch (ch)
                {
                    case "1":
                        var list = _repo.GetAllCourses();
                        if (list.Count == 0) { Console.WriteLine("No courses."); break; }
                        foreach (var c in list)
                            Console.WriteLine($"{c.CourseId}\t{c.CourseName}\tCredits:{c.Credits}\tTeacherId:{c.TeacherId}");
                        break;

                    case "2":
                        Console.Write("Course name: ");
                        var name = Console.ReadLine() ?? "";
                        Console.Write("Credits (int): ");
                        if (!int.TryParse(Console.ReadLine(), out var cr)) { Console.WriteLine("Invalid credits."); break; }
                        Console.Write("TeacherId (FK): ");
                        if (!int.TryParse(Console.ReadLine(), out var tid)) { Console.WriteLine("Invalid teacher id."); break; }
                        var ins = _repo.Create(new Course { CourseName = name, Credits = cr, TeacherId = tid });
                        Console.WriteLine(ins == 1 ? "Inserted." : "Nothing inserted.");
                        break;

                    case "3":
                        Console.Write("CourseId to update: ");
                        if (!int.TryParse(Console.ReadLine(), out var cid)) { Console.WriteLine("Invalid id."); break; }
                        Console.Write("New course name: ");
                        var newName = Console.ReadLine() ?? "";
                        Console.Write("New credits (int): ");
                        if (!int.TryParse(Console.ReadLine(), out var ncr)) { Console.WriteLine("Invalid credits."); break; }
                        Console.Write("New TeacherId (FK): ");
                        if (!int.TryParse(Console.ReadLine(), out var ntid)) { Console.WriteLine("Invalid teacher id."); break; }
                        var upd = _repo.Update(new Course { CourseId = cid, CourseName = newName, Credits = ncr, TeacherId = ntid });
                        Console.WriteLine(upd == 1 ? "Updated." : "Not found.");
                        break;

                    case "4":
                        Console.Write("CourseId to delete: ");
                        if (!int.TryParse(Console.ReadLine(), out var did)) { Console.WriteLine("Invalid id."); break; }
                        var del = _repo.Delete(did);
                        Console.WriteLine(del >= 1 ? "Deleted." : "Not found.");
                        break;

                    default: Console.WriteLine("Unknown option."); break;
                }
            }
            catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
        }
    }
}
