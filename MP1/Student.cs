using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP1
{
    class Student
    {
        private static int totalStudentsAdded = 0;
        private string firstName;
        private string lastName;
        private int studentNum;
        private int maxCourses;
        private List<Course> courses = new List<Course>(); 

        //CONSTRUCTORS
        public Student()
        {
            firstName = "No";
            lastName = "name";
            maxCourses = 0;
        }

        public Student(string firstName, string lastName, int maxCourses)
        {
            totalStudentsAdded++;
            studentNum = totalStudentsAdded;
            this.firstName = firstName;
            this.lastName = lastName;
            this.maxCourses = maxCourses;
        }

        public bool AddCourse(Course course)
        {
            //Duplicates
            for (int i = 0; i < courses.Count; i++)
            {
                //If it finds the course return false and dont add the course
                if (course.GetCourseName().Equals(courses[i].GetCourseName()))
                {
                    return false;
                }
            }
            if (courses.Count < maxCourses)
            {
                courses.Add(course);
                return true;
            }

            return false;
        }

        public bool RemoveCourse(string courseName)
        {
            for (int i = 0; i < courses.Count; i++)
            {
                //If it finds the course return true and remove the course
                if (courseName.Equals(courses[i].GetCourseName()))
                {
                    courses.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        private double CalcAvg()
        {
            double avg = 0;
            List<double> marks = new List<double>();

            for (int i = 0; i < courses.Count; i++)
            {
                marks.Add(courses[i].GetMark());
                avg = avg + courses[i].GetMark();
            }
            avg = (avg / marks.Count);

            return avg;
        }

        public List<String> GetCoursesNames()
        {
            List<string> courseNames = new List<string>();

            for (int i = 0; i < courses.Count; i++)
            {
                courseNames.Add(courses[i].GetCourseName());
            }

            return courseNames;
        }

        public bool SpaceAvailable()
        {
            if (courses.Count < maxCourses)
            {
                return true;
            }

            return false;
        }

        public string GetStudentBasicData(int columnWidth)
        {
            string formattedString;
            int avg = (int)CalcAvg();

            if (avg == -2147483648)
            {
                avg = 0;
            }

            formattedString = lastName.PadRight(columnWidth, ' ') + "¦" + firstName.PadRight(columnWidth, ' ') + "¦" + Convert.ToString(studentNum).PadRight(columnWidth, ' ') 
                                        + "¦" + Convert.ToString(courses.Count).PadRight(columnWidth, ' ') + "¦" + Convert.ToString(avg + "%").PadRight(columnWidth, ' ');

            return formattedString;
        }

        public string GetStudentCourseData(int columnWidth)
        {
            string formattedString = "";

            for (int i = 0; i < courses.Count; i++)
            {
                formattedString += Program.CenteredString(courses[i].GetFullCourseData(columnWidth), true) + " ";
            }

            double avg = CalcAvg();

            if (courses.Count < 1)
            {
                avg = 0;
            }

            formattedString += ("AVERAGE: " + Math.Round(avg, 1) + "%").PadLeft((int)(GetStudentBasicData(columnWidth).Length / 1.25), ' ');

            return formattedString;
        }

    }
}
