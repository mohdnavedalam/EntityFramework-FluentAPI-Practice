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

            Console.WriteLine("\n\n\n");

            //Extension Method Syntaxes
            //Restrictions
            var ExtRestrictionQuery = Context.Courses.Where(c => c.Level == (CourseLevel)1);
            foreach(var x in ExtRestrictionQuery)
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", x.Name, x.AuthorID, x.Level, x.FullPrice);

            Console.WriteLine("\n\n\n");

            //Ordering
            var ExtOrderingQuery = Context.Courses
                                   .Where(c => c.Level == (CourseLevel)2)
                                   .OrderBy(c => c.Level)
                                   .ThenByDescending(c => c.Name);
            foreach(var x in ExtOrderingQuery)
                Console.WriteLine("{0}\t{1}", x.Level, x.Name);

            Console.WriteLine("\n\n\n");

            //Projection
            var ExtProjectionQuery = Context.Courses
                                    .Where(c => c.Level == CourseLevel.Beginner)
                                    .Select(c => new { CourseName = c.Name, AuthorName = c.Author.Name });
            foreach(var x in ExtProjectionQuery)
                Console.WriteLine("{0}\t{1}", x.AuthorName, x.CourseName);

            Console.WriteLine("\n");

            var ExtProjectionQuery2 = Context.Courses
                                        .Where(c=>c.Level == CourseLevel.Intermediate)
                                      //.Where(c => c.Level == CourseLevel.Beginner && c.Name == "c#" || c.AuthorID == 2)
                                      .Select(c => c.Tags);
            foreach (var x in ExtProjectionQuery2)
            {
                foreach(var y in x)
                    Console.WriteLine(y.Name);
            }

            Console.WriteLine("\n");

            var ExtProjectionQuery3 = Context.Courses
                                        .SelectMany(c => c.Tags)
                                        .Distinct();//Set Operator
            foreach(var x in ExtProjectionQuery3)
                Console.WriteLine(x.Name);

            Console.WriteLine("\n\n\n");

            //Grouping
            var ExtGroupingQuery = Context.Courses.GroupBy(c => c.Level);
            foreach(var x in ExtGroupingQuery)
            {
                Console.WriteLine("Level - {0}", x.Key);
                foreach(var y in x)
                    Console.WriteLine("\t{0}\t{1}", y.FullPrice, y.Name);
            }

            Console.WriteLine("\n\n\n");

            //Joins
            //Inner Join
            var ExtInnerJoinQuery = Context.Courses
                                    .Join(Context.Authors,
                                    c => c.AuthorID,
                                    a => a.ID,
                                    (course, author) => new
                                    {
                                        CourseName = course.Name,
                                        AuthorName = author.Name
                                    });
            foreach(var x in ExtInnerJoinQuery)
                Console.WriteLine("{0}\t\t\t\t\t{1}", x.CourseName, x.AuthorName);

            Console.WriteLine("\n");

            //Group Join
            var ExtGroupJoinQuery = Context.Authors
                                    .GroupJoin(Context.Courses,
                                    a => a.ID,
                                    c => c.AuthorID,
                                    (author, course) => new
                                    {
                                        AuthorName = author.Name,
                                        Courses = course.Count()
                                    });
            foreach(var x in ExtGroupJoinQuery)
                Console.WriteLine("{0}\t{1}", x.AuthorName, x.Courses);

            Console.WriteLine("\n");

            var ExtGroupJoinQuery2 = Context.Authors
                                        .GroupJoin(Context.Courses,
                                        a => a.ID,
                                        c => c.AuthorID,
                                        (author, course) => new
                                        {
                                            Author = author.Name,
                                            Course = course
                                        });
            foreach(var f in ExtGroupJoinQuery2)
            {
                Console.WriteLine("Author : {0}", f.Author);

                foreach(var g in f.Course)
                    Console.WriteLine("\t{0}", g.Name);
            }

            Console.WriteLine("\n");

            //Cross Join
            var ExtCrossJoinQuery = Context.Authors.SelectMany(a => Context.Courses, (author, course) => new
            {
                AuthorName = author.Name,
                CourseName = course.Name
            });
            foreach(var x in ExtCrossJoinQuery)
                Console.WriteLine("{0}\t{1}", x.AuthorName, x.CourseName);

            Console.ReadKey();
        }
    }
}
