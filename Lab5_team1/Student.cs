namespace Lab7_team1
{
    public class Student
    {
        public string FirstName { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public string LastName { get; set; } = "";
        private int _age;
        public int Age
        {
            get => _age;
            set => _age = (value > 0 && value < 100) ? value : 20;
        }
        public int StudentID { get; private set; }
        public int StudentGroupID { get; set; }

        // Student must belong to a group
        public Student(int studentID, string firstName = "No first name", string patronymic = "No patrinymic", string lastName = "No last name", int age = 20, int studentGroupID = 1)
        {
            FirstName = firstName;
            Patronymic = patronymic;
            LastName = lastName;
            Age = age;
            StudentID = studentID;
            StudentGroupID = studentGroupID;
        }
        public Student() { }
        public override string ToString() => $"Студент {FirstName} {Patronymic} {LastName}, ID <{StudentID}>, ID группы <{StudentGroupID}>, возвраст {Age})";
    }
}
