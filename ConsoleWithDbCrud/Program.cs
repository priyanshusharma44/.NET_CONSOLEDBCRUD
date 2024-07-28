using System;
using System.Globalization;

namespace ConsoleWithDbCrud
{
    public class Program
    {
        static void Main(string[] args)
        {
            DataAccess DA = new DataAccess();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Student Management System");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddStudent(DA);
                            break;
                        case 2:
                            ViewStudents(DA);
                            break;
                        case 3:
                            UpdateStudent(DA);
                            break;
                        case 4:
                            DeleteStudent(DA);
                            break;
                        case 5:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void AddStudent(DataAccess DA)
        {
            Student s = new Student();

            Console.Write("Enter Id: ");
            s.Id = int.Parse(Console.ReadLine());

            Console.Write("Enter Name: ");
            s.Name = Console.ReadLine();

            Console.Write("Enter Address: ");
            s.Address = Console.ReadLine();

            Console.Write("Enter Gender (true for Male, false for Female): ");
            s.Gender = bool.Parse(Console.ReadLine());

            Console.Write("Enter Date of Birth (yyyy-MM-dd): ");
            s.DoB = DateOnly.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (DA.AddStudent(s))
                Console.WriteLine("INSERTED SUCCESSFULLY");
            else
                Console.WriteLine("FAILED");
        }

        static void ViewStudents(DataAccess DA)
        {
            List<Student> students = DA.GetStudentList();
            Console.WriteLine("Id\tName\t\tAddress\t\tGender\t\tDate of Birth");
            foreach (Student student in students)
            {
                Console.WriteLine($"{student.Id}\t{student.Name}\t\t{student.Address}\t\t{(student.Gender ? "Male" : "Female")}\t\t{student.DoB}");
            }
        }

        static void UpdateStudent(DataAccess DA)
        {
            Console.Write("Enter the ID of the student to update: ");
            int oldId = int.Parse(Console.ReadLine());

            Student s = new Student();

            Console.Write("Enter New Id: ");
            s.Id = int.Parse(Console.ReadLine());

            Console.Write("Enter New Name: ");
            s.Name = Console.ReadLine();

            Console.Write("Enter New Address: ");
            s.Address = Console.ReadLine();

            Console.Write("Enter New Gender (true for Male, false for Female): ");
            s.Gender = bool.Parse(Console.ReadLine());

            Console.Write("Enter New Date of Birth (yyyy-MM-dd): ");
            s.DoB = DateOnly.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (DA.UpdateStudent(s, oldId))
                Console.WriteLine("UPDATED SUCCESSFULLY");
            else
                Console.WriteLine("FAILED");
        }

        static void DeleteStudent(DataAccess DA)
        {
            Console.Write("Enter the ID of the student to delete: ");
            int id = int.Parse(Console.ReadLine());

            if (DA.DeleteStudent(id) > 0)
                Console.WriteLine("DELETED SUCCESSFULLY");
            else
                Console.WriteLine("FAILED");
        }
    }
}
