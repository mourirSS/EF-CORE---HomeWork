using MyApp.Models;
using MyApp.Repositories;

namespace MyApp.Menus;

public class StudentsMenu
{
    private readonly StudentRepository _repo;
    public StudentsMenu(StudentRepository repo) => _repo = repo;

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n--- Students ---");
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
                        var list = _repo.GetAllStudents();
                        if (list.Count == 0) { Console.WriteLine("No students."); break; }
                        foreach (var s in list)
                            Console.WriteLine($"{s.StudentId}\t{s.FullName}\t{s.BirthDate:yyyy-MM-dd}");
                        break;

                    case "2":
                        Console.Write("Full name: ");
                        var name = Console.ReadLine() ?? "";
                        Console.Write("Birth date (yyyy-mm-dd): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out var bd))
                        { Console.WriteLine("Invalid date."); break; }
                        var ins = _repo.Create(new Student { FullName = name, BirthDate = bd });
                        Console.WriteLine(ins == 1 ? "Inserted." : "Nothing inserted.");
                        break;

                    case "3":
                        Console.Write("StudentId to update: ");
                        if (!int.TryParse(Console.ReadLine(), out var uid)) { Console.WriteLine("Invalid id."); break; }
                        Console.Write("New full name: ");
                        var newName = Console.ReadLine() ?? "";
                        Console.Write("New birth date (yyyy-mm-dd): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out var newBd))
                        { Console.WriteLine("Invalid date."); break; }
                        var upd = _repo.Update(new Student { StudentId = uid, FullName = newName, BirthDate = newBd });
                        Console.WriteLine(upd == 1 ? "Updated." : "Not found.");
                        break;

                    case "4":
                        Console.Write("StudentId to delete: ");
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
