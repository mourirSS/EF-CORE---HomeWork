using MyApp.Models;
using MyApp.Repositories;

namespace MyApp.Menus;

public class TeachersMenu
{
    private readonly TeacherRepository _repo;
    public TeachersMenu(TeacherRepository repo) => _repo = repo;

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n--- Teachers ---");
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
                        var list = _repo.GetAllTeachers();
                        if (list.Count == 0) { Console.WriteLine("No teachers."); break; }
                        foreach (var t in list)
                            Console.WriteLine($"{t.TeacherId}\t{t.FullName}\t{t.Email}");
                        break;

                    case "2":
                        Console.Write("Full name: ");
                        var name = Console.ReadLine() ?? "";
                        Console.Write("Email (can be empty): ");
                        var email = Console.ReadLine();
                        var ins = _repo.Create(new Teacher { FullName = name, Email = email });
                        Console.WriteLine(ins == 1 ? "Inserted." : "Nothing inserted.");
                        break;

                    case "3":
                        Console.Write("TeacherId to update: ");
                        if (!int.TryParse(Console.ReadLine(), out var uid)) { Console.WriteLine("Invalid id."); break; }
                        Console.Write("New full name: ");
                        var newName = Console.ReadLine() ?? "";
                        Console.Write("New email: ");
                        var newEmail = Console.ReadLine();
                        var upd = _repo.Update(new Teacher { TeacherId = uid, FullName = newName, Email = newEmail });
                        Console.WriteLine(upd == 1 ? "Updated." : "Not found.");
                        break;

                    case "4":
                        Console.Write("TeacherId to delete: ");
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
