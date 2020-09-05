using System.Collections.Generic;

namespace EFFluentAPI.Model
{
    public class Course
    {
        public Course()
        {
            Tags = new List<Tag>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CourseLevel Level { get; set; }  //Navigation Property
        public float FullPrice { get; set; }
        public int AuthorID { get; set; }
        public Author Author { get; set; }  //Navigation Property
        public Cover Cover { get; set; }    //Navigation Property
        public IList<Tag> Tags { get; set; }    //Navigation Property
    }
}
