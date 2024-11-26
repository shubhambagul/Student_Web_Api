namespace Student_Web_Api.Model
{
    public class Student
    {
        public int studentId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string branch { get; set; }
        public float cgpa { get; set; }
    }
}
