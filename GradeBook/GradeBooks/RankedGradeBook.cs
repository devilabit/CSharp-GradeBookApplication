using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException();

            var sortedStudents = from student in Students
                            orderby student.AverageGrade descending
                            select student;

            // foreach (var s in sortedStudents)
            //     System.Console.WriteLine($"{s.Name} : {s.AverageGrade}");    

            // default to last rank position
            var rank = Students.Count;

            for (var i = 0; i < Students.Count; i++) 
            {
                if (averageGrade >= Students[i].AverageGrade)
                {
                    rank = i + 1;
                    break;
                }
            }

            var percentile = ((double) rank/Students.Count) * 100;
            
            //System.Console.WriteLine($"avgGrade {averageGrade} - pos = {rank} percentile = {percentile}");

            if (percentile <= 20)
                return 'A';
            else if (percentile <= 40)
                return 'B';
            else if (percentile <= 60)
                return 'C';
            else if (percentile <= 80)
                return 'D';
            else 
                return 'F';
            
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                System.Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }
            else
            {
                base.CalculateStatistics();
            }
        }

        public override void CalculateStudentStatistics(string name) 
        {
            if (Students.Count < 5)
            {
                System.Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }
            else
            {
                base.CalculateStudentStatistics(name);
            }
        }
    }
}