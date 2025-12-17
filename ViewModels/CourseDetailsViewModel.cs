using WebAppMVC.Models;

namespace WebAppMVC.ViewModels
{
    public class CourseDetailsViewModel
    {
        public Course Course { get; set; }
        public List<Student> Students { get; set; }
    }
}
