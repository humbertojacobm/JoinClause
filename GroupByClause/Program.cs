using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupByClause
{

    class Program
    {
        public class Student
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public List<int> Scores;
        }

        public static List<Student> GetStudents()
        {
            // Use a collection initializer to create the data source. Note that each element
            //  in the list contains an inner sequence of scores.
            List<Student> students = new List<Student>
            {
            new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 72, 81, 60}},
            new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
            new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {99, 89, 91, 95}},
            new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {72, 81, 65, 84}},
            new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {97, 89, 85, 82}}
            };

            return students;
        }
        static void Main(string[] args)
        {
              Program app = new Program();
              app.GroupByBool();
              app.GroupByNumericRange();
              app.GroupByChar();
              app.GroupByCharAdvanced();
        }
        void GroupByBool()
        {
            List<Student> students = GetStudents();

            var booleanGroupQuery = 
                from student in students
                group student by student.Scores.Average () >= 80;

            Console.WriteLine("");
            Console.WriteLine("GroupByBool");
            foreach (var studentGroup in booleanGroupQuery)
            {
                Console.WriteLine(studentGroup.Key == true ? "High averages" : "Low averages");
                foreach (var student in studentGroup)
                {
                    Console.WriteLine("   {0}, {1}:{2}", student.Last, student.First, student.Scores.Average());
                }
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            //Console.ReadKey();  
        }
        void GroupByNumericRange()
        {
            var students = GetStudents();

            var studentQuery = 
                from student in students
                let avg = (int)student.Scores.Average()
                group student by (avg/10) into g
                orderby g.Key
                select g;
            // Execute the query.
            Console.WriteLine("");
            Console.WriteLine("GroupByNumericRange");
            foreach (var studentGroup in studentQuery)
            {
                int temp = studentGroup.Key * 10;
                Console.WriteLine("Students with an average between {0} and {1}", temp, temp + 10);
                foreach (var student in studentGroup)
                {
                    Console.WriteLine("   {0}, {1}:{2}", student.Last, student.First, student.Scores.Average());
                }
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            //Console.ReadKey();                
        }

        void GroupByChar()
        {
            string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };

            var wordGroups = 
                from w in words
                group w by w[0]
                ;

            Console.WriteLine("");
            Console.WriteLine("GroupByChar");                
            // Execute the query.
            foreach (var wordGroup in wordGroups)
            {
                Console.WriteLine("Words that start with the letter '{0}':", wordGroup.Key);
                foreach (var word in wordGroup)
                {
                    Console.WriteLine(word);
                }
            }

            // Keep the console window open in debug mode
            Console.WriteLine("Press any key to exit.");            
        }
        void GroupByCharAdvanced()
        {
            string[] words2 = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese", "elephant", "umbrella", "anteater" };
            var wordGroups2 = 
                from w in words2
                group w by w[0] into grps 
                where (grps.Key == 'a' ||
                    grps.Key == 'e' ||
                    grps.Key == 'i' ||
                    grps.Key == 'o' ||
                    grps.Key == 'u'
                )
                select grps;
            Console.WriteLine("");
            Console.WriteLine("GroupByCharAdvanced");                  
            // Execute the query.
            foreach (var wordGroup in wordGroups2)
            {
                Console.WriteLine("Groups that start with a vowel: {0}", wordGroup.Key);
                foreach (var word in wordGroup)
                {
                    Console.WriteLine("   {0}", word);
                }
            }

            // Keep the console window open in debug mode
            Console.WriteLine("Press any key to exit.");
                                    
        }
    }
}
