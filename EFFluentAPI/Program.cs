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

            var query2 =
                from c in Context.Courses
                where c.Level == CourseLevel.Beginner && c.Author.ID == 1
                select c;

            foreach(var c in query2)
                Console.WriteLine(c.Name);

            Console.ReadLine();
        }
    }
}
