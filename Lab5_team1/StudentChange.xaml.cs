using System.Windows;

namespace Lab5_team1
{
    /// <summary>
    /// Interaction logic for StudentChange.xaml
    /// </summary>
    public partial class StudentChange : Window
    {
        public Student student { get; private set; }
        public string NewGroupName { get; private set; }

        public StudentChange() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            student = new Student("");

            student.FirstName = txStFirstName.Text.Length == 0 ? student.FirstName : txStFirstName.Text;
            student.Patronymic = txStPatronymic.Text.Length == 0 ? student.Patronymic : txStPatronymic.Text;
            student.LastName = txStLastName.Text.Length == 0 ? student.LastName : txStLastName.Text;
            if (int.TryParse(txStAge.Text, out int ageTmp))
                student.Age = ageTmp;
            NewGroupName = txGroupName.Text.Length == 0 ? student.StudentGroupID : txGroupName.Text;

            Close();
        }
    }
}
