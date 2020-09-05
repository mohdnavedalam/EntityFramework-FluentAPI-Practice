using System.Collections.Generic;

namespace EFFluentAPI.Model
{
    public class Tag
    {
        public Tag()
        {
            Courses = new List<Course>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }
    }
}
