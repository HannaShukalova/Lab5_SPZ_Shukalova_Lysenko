using System.Windows;
using System.Windows.Controls;

namespace Lab5_team1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataContainer DataContainer { get; private set; } = new DataContainer();

        public MainWindow()
        {
            InitializeComponent();
            DataContainer = TryFindResource("dataContainer") as DataContainer;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) => DataContainer.LoadDataFromJSON(DataContainer);
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => DataContainer.SaveDataToJSON(DataContainer);

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
                cbCurrentGroup.SelectedIndex = cbCurrentGroup.Items.Count - 1;
            }
        }
        private void btDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            if (cbCurrentGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Группа не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            for (int i = 0; i < DataContainer.Students.Count; )
            {
                if (DataContainer.Students[i].StudentGroupID == (cbCurrentGroup.SelectedItem as Group).GroupID)
                {
                    DataContainer.RemoveStudent(DataContainer.Students[i]);
                }
                else
                    i++;
            }

            DataContainer.RemoveGroup(cbCurrentGroup.SelectedItem as Group);
            cbCurrentGroup.SelectedIndex = cbCurrentGroup.Items.Count - 1;
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
                (cbCurrentGroup.SelectedItem as Group).ChangeGroupName(groupChange.group.Name);
                cbCurrentGroup.Items.Refresh();
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
                studentChange.student.ChangeGroup((cbCurrentGroup.SelectedItem as Group).GroupID);
                DataContainer.AddStudent(studentChange.student);
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

            DataContainer.RemoveStudent(dbStudents.SelectedItem as Student);
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
                (dbStudents.SelectedItem as Student).Rename(studentChange.student.FirstName, studentChange.student.Patronymic, studentChange.student.LastName);
                cbCurrentGroup.SelectedIndex = -1;
                dbStudents.Items.Refresh();
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
                foreach (var item in DataContainer.Groups)
                {
                    if (item.Name == studentChange.NewGroupName)
                    {
                        (dbStudents.SelectedItem as Student).ChangeGroup(item.GroupID);
                        MessageBox.Show("Группа успешно изменена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        cbCurrentGroup.SelectedIndex = -1;
                        dbStudents.Items.Refresh();
                        return;
                    }
                }
                MessageBox.Show("Указанное имя группы не существует", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
