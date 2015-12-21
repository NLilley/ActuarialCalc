using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ActuarialCalc
{
   
    public partial class ConnectionDetails : Window
    {
        MainWindow referenceToMainWindow;

        public ConnectionDetails(MainWindow refer)
        {
            referenceToMainWindow = refer;
            InitializeComponent();

        }

        private void Connect_Button_Click(object sender, RoutedEventArgs e)
        {
            referenceToMainWindow.controller = new MySQLController(Server_TextBox.Text,Username_TextBox.Text,Password_TextBox.Text,Database_TextBox.Text);
            referenceToMainWindow.getMySQLTables();
            
            this.Close();
        }

    }
}
