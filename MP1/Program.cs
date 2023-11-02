//Author: Dan Lichtin
//File Name: Program.cs
//Project Name: PASS1
//Creation Date: October 2, 2022
//Modified Date: October -, 2022
//Description: Creating a student manager program
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP1
{
    class Program
    {
        const int ADD_STUDENT = 1;
        const int REMOVE_STUDENT = 2;
        const int ADD_COURSE = 3;
        const int REMOVE_COURSE = 4;
        const int DISPLAY_ALL_STUDENT = 5;
        const int DISPLAY_STUDENT = 6;
        const int EXIT = 7;

        private const int COL_WIDTH = 18;

        //Course names
        private static string[] courseNames = new string[]
        {
            "Math","Computer Science","Physics","English","French","History","Geography","Drama","Phys. Ed.","Fashion", "Art","Philosophy"
        };
        //Course codes
        private static string[] courseCodes = new string[]
        {
            "MHF4U","ICS4U","SPH4U","ENG4U","FSF4U","CHY4U","CGW4U","ADA4M","PPL4O","HNB4M","AVI4M","HZT4U"
        };

        private static string studentTable = CenteredString("Last Name".PadRight(COL_WIDTH, ' ') + "¦" + "First Name".PadRight(COL_WIDTH, ' ') +
                              "¦" + "Student Number".PadRight(COL_WIDTH, ' ') + "¦" + "Course Count".PadRight(COL_WIDTH, ' ') + "¦" + "Average".PadRight(COL_WIDTH, ' '), true) + " ";

        private static string courseTable = CenteredString("¦" + "Course Name".PadRight(COL_WIDTH, ' ') + "¦" + "Course Code".PadRight(COL_WIDTH, ' ') +
                              "¦" + "Mark".PadRight(COL_WIDTH, ' '), true) + " ";

        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            //TESTING 
            students.Add(new Student("Bart", "Simpson", 4));
            students.Add(new Student("Lisa", "Simpson", 8));
            students.Add(new Student("Montgomery", "Burns", 6));

            students[0].AddCourse(new Course(courseNames[0], courseCodes[0], 87.3));
            students[0].AddCourse(new Course(courseNames[6], courseCodes[6], 65.2));
            students[0].AddCourse(new Course(courseNames[11], courseCodes[11], 91.3));
            students[0].AddCourse(new Course(courseNames[2], courseCodes[2], 75.9));
            students[1].AddCourse(new Course(courseNames[6], courseCodes[6], 98.2));
            students[1].AddCourse(new Course(courseNames[11], courseCodes[11], 91.3));
            students[1].AddCourse(new Course(courseNames[0], courseCodes[0], 94.3));
            students[1].AddCourse(new Course(courseNames[7], courseCodes[7], 93.2));
            students[1].AddCourse(new Course(courseNames[11], courseCodes[11], 91.3));
            students[1].AddCourse(new Course(courseNames[8], courseCodes[8], 42.2));
            students[2].AddCourse(new Course(courseNames[1], courseCodes[1], 99.3));
            students[2].AddCourse(new Course(courseNames[9], courseCodes[9], 97.4));

            students.RemoveAt(2);
            students.Add(new Student("Frank", "Grimes", 2));
            students[2].AddCourse(new Course(courseNames[0], courseCodes[0], 68.3));
            students[2].AddCourse(new Course(courseNames[4], courseCodes[4], 72.9));
            //TESTING

            bool managerRunning = true;

            while (managerRunning)
            {
                DisplayMenu();
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case ADD_STUDENT:
                        students.Add(CreateStudent());
                        break;
                    case REMOVE_STUDENT:
                        students.RemoveAt(ChooseStudent());
                        break;
                    case ADD_COURSE:
                        AddCourse(ChooseStudent());
                        break;
                    case REMOVE_COURSE:
                        RemoveCourse(ChooseStudent());
                        break;
                    case DISPLAY_STUDENT:
                        DisplayStudentComplexInfo();
                        Console.ReadLine();
                        break;
                    case DISPLAY_ALL_STUDENT:
                        DisplayAllStudentsBasicInfo();
                        Console.ReadLine();
                        break;
                    case EXIT:
                        managerRunning = false;
                        break;
                    default:
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n" + CenteredString("► Invalid Number :(", true) + " ");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }

                Console.Clear();
            }
        }

        private static void DisplayMenu()
        {
            string[] options = new string[]
            {
                "1. Add a student","2. Remove a student","3. Add a course","4. Remove a course","5. Basic info of all students","6. Detailed info of a student","7. Exit"
            };
            string optionPrompt = "Choose an option: ";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(CenteredString(" _____ _         _         _      _____                          ", true));
            Console.WriteLine(CenteredString("|   __| |_ _ _ _| |___ ___| |_   |     |___ ___ ___ ___ ___ ___  ", true));
            Console.WriteLine(CenteredString("|__   |  _| | | . | -_|   |  _|  | | | | .'|   | .'| . | -_|  _| ", true));
            Console.WriteLine(CenteredString("|_____|_| |___|___|___|_|_|_|    |_|_|_|__,|_|_|__,|_  |___|_|   ", true));
            Console.WriteLine(CenteredString("                                                   |___|         ", true) + "\n");

            Console.ForegroundColor = ConsoleColor.White;
            //Looping through the options string; center and then write
            for (int i = 0; i < options.Length; i++)
            {
                options[i] = CenteredString(options[i], true);
                Console.WriteLine(options[i]);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n" + CenteredString(optionPrompt, false));
            Console.ResetColor();
        }

        //Call to center a string
        //Called once in student class
        //IF TRUE THEN CURSORS ENDS ON A NEW LINE
        //IF FALSE CURSORS ENDS ON THE SAME LINE
        public static string CenteredString(string sentence, bool padFull)
        {
            string centerSentence;
            int center = (Console.WindowWidth - sentence.Length) / 2 + sentence.Length;
            if (padFull)
            {
                centerSentence = sentence.PadLeft(center, ' ').PadRight(Console.WindowWidth - 1, ' ');
            }
            else
            {
                centerSentence = sentence.PadLeft(center, ' ');
            }
            return centerSentence;
        }

        //Call to make sure a inputed number is in range
        private static int GetNumInRange(string request, int bottom, int top)
        {
            int num = top + 1;

            while (num < bottom || num > top)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(request);
                Console.ResetColor();
                Console.Write(CenteredString(" ", false));

                if (Int32.TryParse(Console.ReadLine(), out num) == false || num < bottom || num > top)
                {
                    //Bad input, reset and ask again
                    num = top + 1;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n" + CenteredString("► Invalid Input: Enter any button to retry", true) + " ");
                    Console.ResetColor();
                    Console.ReadLine();
                }
            }

            return num;
        }

        private static Student CreateStudent()
        {
            string firstName;
            string lastName;
            int maxCourses;

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(CenteredString("► Whats the student's first name?", true) + " ");
            Console.ResetColor();
            Console.Write(CenteredString("", false));
            firstName = Console.ReadLine();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n" + CenteredString("► And their last name?", true) + " ");
            Console.ResetColor();
            Console.Write(CenteredString("", false));
            lastName = Console.ReadLine();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n" + CenteredString("► # of allowed courses?", true) + " ");
            Console.ResetColor();
            Console.Write(CenteredString("", false));
            maxCourses = Convert.ToInt32(Console.ReadLine());

            return new Student(firstName, lastName, maxCourses);
        }

        private static int ChooseStudent()
        {
            int chosenStudent;

            DisplayAllStudentsBasicInfo();
            Console.WriteLine("");
            chosenStudent = GetNumInRange(CenteredString("► Choose a student", true) + " ", 1, students.Count);

            return chosenStudent - 1;
        }

        private static void DisplayAllStudentsBasicInfo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write(studentTable);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("▬");
            }
            Console.ResetColor();
            for (int i = 0; i < students.Count; i++)
            {
                Console.Write(CenteredString((i + 1) + ") " + students[i].GetStudentBasicData(COL_WIDTH), true) + " ");
            }
        }

        private static void DisplayStudentComplexInfo()
        {
            int index;

            index = ChooseStudent();
            Console.Clear();

            //Writing student section
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(studentTable);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("▬");
            }
            Console.ResetColor();
            Console.Write(CenteredString(students[index].GetStudentBasicData(COL_WIDTH), true));

            //Writing course section
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n\n" + courseTable);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("▬");
            }
            Console.ResetColor();
            Console.Write(CenteredString(students[index].GetStudentCourseData(COL_WIDTH), true));

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n\n" + CenteredString("► Enter any button to continue", true) + " ");
            Console.ResetColor();
        }

        /// START
        /// Course Managment
        private static void AddCourse(int idx)
        {
            int chosenCourse;
            double mark = 0;
            bool validInput = false;

            chosenCourse = PickNewCourse();

            while (validInput == false)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\n" + CenteredString("► Choose a mark", true) + " ");
                Console.ResetColor();
                Console.Write(CenteredString("", false));
                mark = Convert.ToDouble(Console.ReadLine());
                if (0 < mark && mark < 101)
                {
                    validInput = true;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n" + CenteredString("► Invalid Input: Enter any button to retry", true) + " ");
                    Console.ResetColor();
                    Console.ReadLine();
                }
            }

            if (students[idx].AddCourse(new Course(courseNames[chosenCourse], courseCodes[chosenCourse], mark)))
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n" + CenteredString("► Valid Student: Course Added", true) + " ");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n" + CenteredString("► Invalid Student: Max Number Of Courses Added OR Course Already Added", true) + " ");
            }

            Console.ResetColor();
            Console.ReadLine();
        }

        private static int PickNewCourse()
        {
            int chosenCourse;

            Console.Clear();
            for (int i = 0; i < courseNames.Length; i++)
            {
                Console.Write(CenteredString((i + 1) + ". " + courseNames[i], true) + " ");
            }
            chosenCourse = GetNumInRange(CenteredString("► Choose a course", true) + " ", 1, courseNames.Length);

            return chosenCourse - 1;
        }

        private static void RemoveCourse(int idx)
        {
            List<string> courses = students[idx].GetCoursesNames();
            if (courses.Count == 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n" + CenteredString("► Invalid Student: No Courses To Remove", true) + " ");
                Console.ResetColor();
                Console.ReadLine();
            }
            else
            {
                int chosenCourse = ChooseCourse(courses);
                students[idx].RemoveCourse(courses[chosenCourse]);
            }
        }

        private static int ChooseCourse(List<string> courses)
        {
            int chosenCourse = 0;

            Console.Clear();
            for (int i = 0; i < courses.Count; i++)
            {
                Console.Write(CenteredString((i + 1) + ". " + courses[i], true) + " ");
            }
            chosenCourse = GetNumInRange(CenteredString("► Choose a course", true) + " ", 1, courses.Count);

            return chosenCourse - 1;
        }
        /// FINISH
    }
}