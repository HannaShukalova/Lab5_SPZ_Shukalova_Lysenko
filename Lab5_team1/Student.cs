using System;
using System.Runtime.Serialization;

namespace Lab5_team1
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public string FirstName { get; set; } = "";
        [DataMember]
        public string Patronymic { get; set; } = "";
        [DataMember]
        public string LastName { get; set; } = "";
        private int _age;
        [DataMember]
        public int Age
        {
            get => _age;
            set => _age = (value > 0 && value < 100) ? value : 20;
        }
        [DataMember]
        public string StudentID { get; private set; } = "";
        [DataMember]
        public string StudentGroupID { get; private set; } = "";

        // Student must belong to a group
        public Student(string studentGroupID, string firstName = "No first name", string patronymic = "No patrinymic", string lastName = "No last name", int age = 20)
        {
            FirstName = firstName;
            Patronymic = patronymic;
            LastName = lastName;
            Age = age;
            StudentID = Guid.NewGuid().ToString();
            StudentGroupID = studentGroupID;
        }
        public void Rename(string firstName = "No first name", string patronymic = "No patrinymic", string lastName = "No last name")
        {
            FirstName = firstName;
            Patronymic = patronymic;
            LastName = lastName;
        }
        public void ChangeGroup(string newGroupID) => StudentGroupID = newGroupID;
        public override string ToString() => $"Студент {FirstName} {Patronymic} {LastName}, ID <{StudentID}>, ID группы <{StudentGroupID}>, возвраст {Age})";
    }
}
