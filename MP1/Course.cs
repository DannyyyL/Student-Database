using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP1
{
    class Course
    {
        private string courseName;
        private string courseCode;
        private double mark;

        //CONSTRUCTORS
        public Course()
        {
            courseName = "None";
            courseCode = "N/A";
            mark = 0;
        }

        public Course(string courseName, string courseCode, double mark)
        {
            this.courseName = courseName;
            this.courseCode = courseCode;
            this.mark = mark;
        }

        //ACCESSORS
        public double GetMark()
        {
            return mark;
        }

        public string GetCourseName()
        {
            return courseName;
        }

        public string GetFullCourseData(int columnWidth)
        {
            string formattedString;

            formattedString = "¦" + courseName.PadRight(columnWidth, ' ') + "¦" + courseCode.PadRight(columnWidth, ' ') + "¦" + 
                                    Convert.ToString(mark + "%").PadRight(columnWidth, ' ');

            return formattedString;
        }
    }
}
