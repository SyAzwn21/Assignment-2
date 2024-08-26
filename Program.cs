using System;

namespace StudentManagement
{
    class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }

        public Student(int id, string name, int age, double grade)
        {
            ID = id;
            Name = name;
            Age = age;
            Grade = grade;
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"ID:{ID}/nName:{Name}/nAge:{Age}/nGrade:{Grade}");
        }

        public bool IsPassing()
        {
            if (Grade >= 60)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            int option = 0;
            Student[] students = new Student[50];
            int studCount = 0;
            string gradePass;


            Console.WriteLine("Choose task option:");


            do
            {
                Console.WriteLine("1:Add a New Student");
                Console.WriteLine("2:View All Students");
                Console.WriteLine("3:Search for a Student by ID");
                Console.WriteLine("4:Remove a Student by ID");
                Console.WriteLine("5:Update a Student's Grade");
                Console.WriteLine("6:Exit");
                Console.WriteLine("\nEnter task option:");
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        if (studCount < 50)
                        {
                            Console.Write("Enter ID:");
                            string inputid = Console.ReadLine();
                            int id;
                            if (!int.TryParse(inputid, out id) || id <= 0)
                            {
                                Console.WriteLine("Invalid input. Please enter a numeric ID greater than 0.");
                                continue;
                            }

                            Console.Write("Enter Name:");
                            string inputname = Console.ReadLine();
                            string name = CapitalizeWord(inputname);
                            if (int.TryParse(name, out _) || double.TryParse(name, out _))
                            {
                                Console.WriteLine("Invalid input. Please enter a non-numeric name.");
                                continue;
                            }

                            Console.Write("Enter Age:");
                            string inputage = Console.ReadLine();
                            int age;
                            if (!int.TryParse(inputage, out age) || age < 5)
                            {
                                Console.WriteLine("Invalid input. Please enter a numeric age greater than 0.");
                                continue;
                            }

                            Console.Write("Enter Grade:");
                            string inputgrade = Console.ReadLine();
                            double grade;
                            if (!double.TryParse(inputgrade, out grade) || grade < 0)
                            {
                                Console.WriteLine("Invalid input. Please enter a numeric grade greater than 0.");
                                continue;
                            }
                            students[studCount] = new Student(id, name, age, grade);
                            Console.WriteLine("Student added successfully!");
                            studCount++;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Cannot accept more students");
                            break;
                        }
                    case 2:
                        Console.WriteLine("All Student Details");
                        double sum = 0;
                        double avgGrade = 0;
                        Console.WriteLine("\nDo you want to sort students by their grades in descending order? (yes/no): ");
                        string sortResponse = Console.ReadLine();


                        int passingCount = 0;
                        for (int i = 0; i < studCount; i++)
                        {
                            sum += students[i].Grade;

                            if (sortResponse.ToLower() == "yes")
                            {
                                var sortedStudents = students.Take(studCount)
                                        .OrderByDescending(s => s.Grade)
                                        .ToArray();

                                bool pass = sortedStudents[i].IsPassing();
                                if (pass == true)
                                {
                                    gradePass = "Passing";
                                    passingCount++;

                                }
                                else
                                {
                                    gradePass = "Failing";
                                }
                                Console.WriteLine($"ID: {sortedStudents[i].ID}, Name: {sortedStudents[i].Name}, Age: {sortedStudents[i].Age}, Grade: {sortedStudents[i].Grade} - {gradePass}");

                            }
                            else
                            {
                                bool pass = students[i].IsPassing();
                                if (pass == true)
                                {
                                    gradePass = "Passing";
                                    passingCount++;

                                }
                                else
                                {
                                    gradePass = "Failing";
                                }
                                Console.WriteLine($"ID: {students[i].ID}, Name: {students[i].Name}, Age: {students[i].Age}, Grade: {students[i].Grade} - {gradePass}");

                            }
                        }
                        avgGrade = sum / studCount;

                        Console.WriteLine($"\nAverage Grade of All Students: {avgGrade:F2}");
                        Console.WriteLine($"Number of Passing Students: {passingCount}");


                        break;
                    case 3:
                        Console.Write("Insert ID:");
                        int searchId = int.Parse(Console.ReadLine());

                        for (int i = 0; i < studCount; i++)
                        {
                            if (students[i].ID == searchId)
                            {
                                bool pass = students[i].IsPassing();
                                if (pass == true)
                                {
                                    gradePass = "Passing";

                                }
                                else
                                {
                                    gradePass = "Failing";
                                }
                                Console.WriteLine($"ID: {students[i].ID}, Name: {students[i].Name}, Age: {students[i].Age}, Grade: {students[i].Grade} - {gradePass}");
                                break;
                            }
                        }
                        Console.WriteLine("Id not exist");
                        break;
                    case 4:
                        Console.Write("Enter the student ID to remove: ");
                        int removeId = int.Parse(Console.ReadLine());

                        for (int i = 0; i < studCount; i++)
                        {
                            if (students[i].ID == removeId)
                            {
                                for (int j = i; j < studCount - 1; j++)
                                {
                                    students[j] = students[j + 1];
                                }
                                students[studCount - 1] = null; //
                                studCount--;
                                Console.WriteLine("Student removed successfully.");
                                break;
                            }
                        }
                        Console.WriteLine("Id not exist");

                        break;
                    case 5:
                        Console.Write("Enter the student ID to update grade: ");
                        int updateId = int.Parse(Console.ReadLine());

                        for (int i = 0; i < studCount; i++)
                        {
                            if (students[i].ID == updateId)
                            {
                                Console.Write("Enter the new grade: ");
                                double newGrade = double.Parse(Console.ReadLine());
                                students[i].Grade = newGrade;
                                Console.WriteLine("Student's grade update successfully.");
                                break;
                            }
                        }
                        Console.WriteLine("Id not exist");

                        break;
                    case 6:
                        Console.Write("Goodbye!");
                        break;
                    default:
                        Console.Write("Invalid option.");
                        break;
                }

            } while (option != 6);
        }

        static string CapitalizeWord(string input)
        {
            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }

            return string.Join(" ", words);
        }
    }
}