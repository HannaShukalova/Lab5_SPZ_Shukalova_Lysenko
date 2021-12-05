using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System;

namespace Lab7_team1
{
    public class DataContainer : INotifyPropertyChanged
    {
        public ObservableCollection<Student> _students = new ObservableCollection<Student>();
        public ObservableCollection<Group> _groups = new ObservableCollection<Group>();
        private static DBConnection db = new DBConnection();

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }
        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public DataContainer() { }
        public DataContainer(ObservableCollection<Group> groups, ObservableCollection<Student> students)
        {
            Groups = groups;
            Students = students;
        }

        public void AddGroup(Group group) => db.addNewGroup(group);
        public void RemoveGroup(Group group) => db.deleteGroupById(group.GroupID);
        public void AddStudent(Student student) => db.addNewStudent(student);
        public void RemoveStudentByGroupId(int groupId) => db.deleteStudentByGroupId(groupId);
        public void RemoveStudentById(Student student) => db.deleteStudentById(student.StudentID);
        public void ChangeStudentById(Student newStudent, int studentId) => db.changeStudentById(newStudent, studentId);
        public void ChangeStudentGroupById(int newGroupId, int studentId) => db.changeStudentGroupById(newGroupId, studentId);
        public Group GetGroupByName(string groupName) => db.getGroupByName(groupName);
        public void RenameGroup(Group group, string newName) => db.changeGroupById(group, newName);
        protected void OnPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static void LoadDataFromDatabase(DataContainer dataContainer)
        {
            try
            {
                dataContainer.Groups = db.getAllGroups();

                dataContainer.Students = db.getAllStudents();

                //MessageBox.Show("Данные о группах успешно загружены", "Инфо", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных \"{ex.Message}\" о группах", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
