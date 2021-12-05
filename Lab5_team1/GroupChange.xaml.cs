using Oracle.ManagedDataAccess.Client;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace Lab7_team1
{
    public partial class GroupChange : Window
    {
        public Group group { get; private set; }
        public GroupChange() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            group = new Group();

            group.Name = txStFirstName.Text.Length == 0 ? group.Name : txStFirstName.Text;

            Close();
        }
    }
}