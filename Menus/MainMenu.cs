using MyApp.Repositories;

namespace MyApp.Menus;

public class MainMenu
{
    private readonly StudentsMenu _studentsMenu;
    private readonly TeachersMenu _teachersMenu;
    private readonly CoursesMenu _coursesMenu;
    private readonly EnrollmentsMenu _enrollmentsMenu;

    public MainMenu(
        StudentRepository studentRepo,
        TeacherRepository teacherRepo,
        CourseRepository courseRepo,
        EnrollmentRepository enrollmentRepo)
    {
        _studentsMenu    = new StudentsMenu(studentRepo);
        _teachersMenu    = new TeachersMenu(teacherRepo);
        _coursesMenu     = new CoursesMenu(courseRepo);
        _enrollmentsMenu = new EnrollmentsMenu(enrollmentRepo);
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n=== MAIN MENU ===");
            Console.WriteLine("1) Students");
            Console.WriteLine("2) Teachers");
            Console.WriteLine("3) Courses");
            Console.WriteLine("4) Enrollments");
            Console.WriteLine("0) Exit");
            Console.Write("Choice: ");
            var ch = Console.ReadLine();

            switch (ch)
            {
                case "1": _studentsMenu.Run(); break;
                case "2": _teachersMenu.Run(); break;
                case "3": _coursesMenu.Run(); break;
                case "4": _enrollmentsMenu.Run(); break;
                case "0": return;
                default: Console.WriteLine("Unknown option."); break;
            }
        }
    }
}
