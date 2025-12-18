using WebAppMVC.Models;

namespace WebAppMVC.ViewModels
{
    public class CourseEnrollmentViewModel
    {
        public Course Course { get; set; }
        public List<Student> Students { get; set; }

        // ID mahasiswa yang dipilih
        public List<int> SelectedStudentIds { get; set; }
    }
}
