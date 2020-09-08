using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EFFluentAPI.Model;

namespace EFFluentAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var Context = new FluentAPIContext();

            //LINQ Syntax
            var query = 
                from c in Context.Courses
                where c.Name.Contains("C#") //Remove this line to display all courses
                orderby c.Name
                select c;

            foreach(var c in query)
                Console.WriteLine(c.Name);

            Console.WriteLine("\n\n\n");

            //Extension Methods            
            var courses = Context.Courses
                .Where(c => c.Name.Contains("C#"))
                .OrderBy(c => c.Name);

            foreach(var d in courses)
                Console.WriteLine(d.Name);

            Console.WriteLine("\n\n\n");

            //LINQ Syntaxes
            //Restrictions
            var RestrictionsQuery =
                from c in Context.Courses
                where c.Level == CourseLevel.Beginner && c.Author.ID == 1
                select c;
            foreach(var c in RestrictionsQuery)
                Console.WriteLine(c.Name);

            Console.WriteLine("\n\n\n");

            //Ordering
            var OrderingQuery = from c in Context.Courses
                         orderby c.Level descending, c.Name
                         select c;
            foreach(var c in OrderingQuery)
                Console.WriteLine("{0}\t{1}\t{2}", c.ID, c.Level, c.Name);

            Console.WriteLine("\n\n\n");

            //Projection
            var ProjectionQuery = from c in Context.Courses
                         select new
                         {
                             Name = c.Name,
                             Author = c.Author.Name
                         };
            foreach(var c in ProjectionQuery)
                Console.WriteLine("{0}\t{1}", c.Author, c.Name);

            Console.WriteLine("\n\n\n");

            //Grouping
            var GroupQuery = from c in Context.Courses
                             group c by c.Author.Name
                             into g
                             select g;
            foreach (var group in GroupQuery)
            {
                Console.WriteLine("{0}\t({1})", group.Key, group.Count());//Aggregate Function

                foreach(var course in group)
                    Console.WriteLine("\t{0}", course.Name);
            }

            Console.WriteLine("\n\n\n");

            //Joining
            //Inner Join
            var InnerJoinQuery = from c in Context.Courses
                            join a in Context.Authors
                            on c.AuthorID equals a.ID
                            select new
                            {
                                CourseName = c.Name, AuthorName = a.Name
                            };
            foreach(var c in InnerJoinQuery)
                Console.WriteLine("{0}\t{1}", c.AuthorName, c.CourseName);

            Console.WriteLine("\n");

            //Group Join
            var GroupJoinQuery = from a in Context.Authors
                                 join c in Context.Courses
                                 on a.ID equals c.AuthorID
                                 into g
                                 select new
                                 {
                                     AuthorName = a.Name,
                                     //Courses = g.,
                                     NumberOfCourses = g.Count()
                                 };
            foreach (var x in GroupJoinQuery)            
                Console.WriteLine("{0}\t{1}", x.AuthorName, x.NumberOfCourses);
            //foreach(var y in )
            //    Console.WriteLine("\t{2}", x.NumberOfCourses);

            Console.WriteLine("\n");

            //Cross Join
            var CrossJoinQuery = from a in Context.Authors
                                 from c in Context.Courses
                                 select new
                                 {
                                     AuthorName = a.Name,
                                     CourseName = c.Name
                                 };
            foreach(var c in CrossJoinQuery)
                Console.WriteLine("{0} - {1}", c.AuthorName, c.CourseName);

            Console.ReadKey();
        }
    }
}
