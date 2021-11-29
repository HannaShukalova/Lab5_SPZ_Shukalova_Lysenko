using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows;
using System;

namespace Lab5_team1
{
    public class DataContainer : INotifyPropertyChanged
    {
        public ObservableCollection<Student> _students = new ObservableCollection<Student>();
        public ObservableCollection<Group> _groups = new ObservableCollection<Group>();

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

        public void AddGroup(Group group) => Groups.Add(group);
        public void RemoveGroup(Group group) => Groups.Remove(group);
        public void AddStudent(Student student) => Students.Add(student);
        public void RemoveStudent(Student student) => Students.Remove(student);

        public void SortStudentsByLastName() => Students = new ObservableCollection<Student>(Students.OrderBy(x => x.LastName));
        public void SortStudentsByGroupID() => Students = new ObservableCollection<Student>(Students.OrderBy(x => x.StudentGroupID));

        protected void OnPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static void LoadDataFromJSON(DataContainer dataContainer)
        {
            try
            {
                var jsonGrps = new DataContractJsonSerializer(typeof(ObservableCollection<Group>));
                var fileGrps = new FileStream("Groups.json", FileMode.Open);
                dataContainer.Groups = (ObservableCollection<Group>)jsonGrps.ReadObject(fileGrps);
                fileGrps.Close();

                var jsonSts = new DataContractJsonSerializer(typeof(ObservableCollection<Student>));
                var fileSts = new FileStream("Students.json", FileMode.Open);
                dataContainer.Students = (ObservableCollection<Student>)jsonSts.ReadObject(fileSts);
                fileSts.Close();

                MessageBox.Show("Данные о группах успешно загружены с файла", "Инфо", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных \"{ex.Message}\" о группах с файла", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                dataContainer = new DataContainer();
            }

            try
            {
                var jsonSts = new DataContractJsonSerializer(typeof(ObservableCollection<Student>));
                var fileSts = new FileStream("Students.json", FileMode.Open);
                dataContainer.Students = (ObservableCollection<Student>)jsonSts.ReadObject(fileSts);
                fileSts.Close();

                MessageBox.Show("Данные о студентах успешно загружены с файла", "Инфо", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных \"{ex.Message}\" о студентах с файла", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                dataContainer = new DataContainer();
            }
        }
        public static void SaveDataToJSON(DataContainer dataContainer)
        {
            try
            {
                var jsonGrps = new DataContractJsonSerializer(typeof(ObservableCollection<Group>));
                var fileGrps = new FileStream("Groups.json", FileMode.OpenOrCreate);
                jsonGrps.WriteObject(fileGrps, dataContainer.Groups);
                fileGrps.Close();

                var jsonSts = new DataContractJsonSerializer(typeof(ObservableCollection<Student>));
                var fileSts = new FileStream("Students.json", FileMode.OpenOrCreate);
                jsonSts.WriteObject(fileSts, dataContainer.Students);
                fileSts.Close();

                MessageBox.Show("Данные успешно сериализованы в файл", "Инфо", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сериализации данных \"{ex.Message}\" в файл", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
