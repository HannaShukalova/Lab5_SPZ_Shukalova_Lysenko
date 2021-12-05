using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lab7_team1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        OracleConnection con;
        void Connect()
        {
            con = new OracleConnection();
            con.ConnectionString = "User Id=<username>;Password=<password>;Data Source=<datasource>";
            con.Open();
            Console.WriteLine("Connected to Oracle" + con.ServerVersion);
        }

        void Close()
        {
            con.Close();
            con.Dispose();
        }
    }
}
