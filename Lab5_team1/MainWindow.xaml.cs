using System.Windows;
using System.Windows.Controls;

namespace Lab7_team1
{
    public partial class MainWindow : Window
    {
        public DataContainer DataContainer { get; private set; } = new DataContainer();

        public MainWindow()
        {
            InitializeComponent();
            DataContainer = TryFindResource("dataContainer") as DataContainer;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) => DataContainer.LoadDataFromDatabase(DataContainer);

        private void btAllGroups_Click(object sender, RoutedEventArgs e) => cbCurrentGroup.SelectedIndex = -1;

        private void cbCurrentGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCurrentGroup.SelectedIndex < 0)
            {
                dbStudents.Items.Refresh();
                return;
            }

            for (int i = 0; i < dbStudents.Items.Count; i++)
            {
                var row = dbStudents.ItemContainerGenerator.ContainerFromItem(dbStudents.Items[i]) as DataGridRow;

                if (row != null)
                {
                    if ((cbCurrentGroup.SelectedItem as Group).GroupID == DataContainer.Students[i].StudentGroupID)
                    {
                        row.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        row.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void btAddGroup_Click(object sender, RoutedEventArgs e)
        {
            GroupChange groupChange = new GroupChange();

            groupChange.ShowDialog();

            if (groupChange.group != null)
            {
                DataContainer.AddGroup(groupChange.group);
                DataContainer.Groups = new DBConnection().getAllGroups();
            }
        }
        private void btDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            if (cbCurrentGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Группа не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataContainer.RemoveStudentByGroupId((cbCurrentGroup.SelectedItem as Group).GroupID);
            DataContainer.RemoveGroup(cbCurrentGroup.SelectedItem as Group);

            DataContainer.Groups = new DBConnection().getAllGroups();
            DataContainer.Students = new DBConnection().getAllStudents();
        }
        private void btRenameGroup_Click(object sender, RoutedEventArgs e)
        {
            if (cbCurrentGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Группа не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            GroupChange groupChange = new GroupChange();
            groupChange.ShowDialog();

            if (groupChange.group != null)
            {
                DataContainer.RenameGroup(cbCurrentGroup.SelectedItem as Group, groupChange.group.Name);
                DataContainer.Groups = new DBConnection().getAllGroups();
            }
        }

        private void btAddSt_Click(object sender, RoutedEventArgs e)
        {
            if (cbCurrentGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Группа не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StudentChange studentChange = new StudentChange();
            studentChange.ShowDialog();

            if (studentChange.student != null)
            {
                Student newStudent = studentChange.student as Student;
                newStudent.StudentGroupID = (cbCurrentGroup.SelectedItem as Group).GroupID;
                DataContainer.AddStudent(newStudent);
                DataContainer.Students = new DBConnection().getAllStudents();
            }
        }
        private void btDeleteSt_Click(object sender, RoutedEventArgs e)
        {
            if (cbCurrentGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Группа не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dbStudents.SelectedIndex < 0)
            {
                MessageBox.Show("Студент не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataContainer.RemoveStudentById(dbStudents.SelectedItem as Student);
            DataContainer.Students = new DBConnection().getAllStudents();
        }

        private void btRenameSt_Click(object sender, RoutedEventArgs e)
        {
            if (cbCurrentGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Группа не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dbStudents.SelectedIndex < 0)
            {
                MessageBox.Show("Студент не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StudentChange studentChange = new StudentChange();
            studentChange.txStAge.IsEnabled = false;
            studentChange.ShowDialog();

            if (studentChange.student != null)
            {
                DataContainer.ChangeStudentById(studentChange.student, (dbStudents.SelectedItem as Student).StudentID);
                DataContainer.Students = new DBConnection().getAllStudents();
            }
        }

        private void btChangeStGroup_Click(object sender, RoutedEventArgs e)
        {
            if (cbCurrentGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Группа не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dbStudents.SelectedIndex < 0)
            {
                MessageBox.Show("Студент не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StudentChange studentChange = new StudentChange();
            studentChange.txStFirstName.IsEnabled = false;
            studentChange.txStPatronymic.IsEnabled = false;
            studentChange.txStLastName.IsEnabled = false;
            studentChange.txStAge.IsEnabled = false;
            studentChange.txGroupName.IsEnabled = true;
            studentChange.ShowDialog();

            if (studentChange.student != null)
            {
                Group group = DataContainer.GetGroupByName(studentChange.NewGroupName);

                if (group.Name.Equals(studentChange.NewGroupName))
                {
                    DataContainer.ChangeStudentGroupById(group.GroupID, (dbStudents.SelectedItem as Student).StudentID);
                    MessageBox.Show("Группа успешно изменена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    DataContainer.Students = new DBConnection().getAllStudents();
                    return;
                }
                else
                {
                    MessageBox.Show("Указанное имя группы не существует", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
