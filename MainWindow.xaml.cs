using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ActuarialCalc
{
   
     public partial class MainWindow : Window
    {
        LifeTable myTable = new LifeTable();

        
        public MySQLController controller = new MySQLController();


        public MainWindow()
        {
            InitializeComponent();


            //These functions have been called to perform some of the setup required to make the program work.
            //controller.createDB("ActuarialData");
            //controller.createTable("ELT15FEMALES");

            //DatabaseInitializer init = new DatabaseInitializer(controller);
            //init.fillTable(myTable);

            UseLocalData_CheckBox.IsChecked = true;

            getMySQLTables();

        }

        private void AnnuityButton_Click(object sender, RoutedEventArgs e)
        {
            if (UseLocalData_CheckBox.IsChecked == true)
            {
                if (File.Exists(Mortality_ComboBox.Text))
                {

                    myTable.tableBuilder(Mortality_ComboBox.Text);

                    try
                    {
                        Double CurrentAge = Convert.ToDouble(CurrentAge_TextBox.Text);
                        Double Duration = Convert.ToDouble(Duration_TextBox.Text);

                        // -1 is a flag for a full life annuity.
                        Duration = (Duration == -1) ? (myTable.maxAge - CurrentAge) : Duration;

                        Double DiscountRate = Convert.ToDouble(DiscountRate_TextBox.Text);
                        Double AmountPayable = Convert.ToDouble(AmountPayable_TextBox.Text);

                        AnnuityValue_TextBox.Text = Convert.ToString(ActuarialCalculator.calculateAnnuityInArrears(CurrentAge, Duration, DiscountRate, myTable, AmountPayable));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    MessageBox.Show("The mortaility table selected cannot be found.");
                }
            }
            else if (UseMySQLData_CheckBox.IsChecked == true)
            {
                try
                {
                    myTable.tableBuilderFromMYSQL(controller, DatabaseTables_ComboBox.Text);

                    Double CurrentAge = Convert.ToDouble(CurrentAge_TextBox.Text);
                    Double Duration = Convert.ToDouble(Duration_TextBox.Text);

                    // -1 is a flag for a full life annuity.
                    Duration = (Duration == -1) ? (myTable.maxAge - CurrentAge) : Duration;

                    Double DiscountRate = Convert.ToDouble(DiscountRate_TextBox.Text);
                    Double AmountPayable = Convert.ToDouble(AmountPayable_TextBox.Text);

                    AnnuityValue_TextBox.Text = Convert.ToString(ActuarialCalculator.calculateAnnuityInArrears(CurrentAge, Duration, DiscountRate, myTable, AmountPayable));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        private void AssuranceButton_Click(object sender, RoutedEventArgs e)
        {
            if (UseLocalData_CheckBox.IsChecked == true)
            {
                if (File.Exists(Mortality_ComboBox.Text))
                {

                    myTable.tableBuilder(Mortality_ComboBox.Text);

                    try
                    {
                        Double CurrentAge = Convert.ToDouble(CurrentAge_TextBox.Text);
                        Double Duration = Convert.ToDouble(Duration_TextBox.Text);

                        // -1 is a flag for a full life annuity.
                        Duration = (Duration == -1) ? (myTable.maxAge - CurrentAge) : Duration;

                        Double DiscountRate = Convert.ToDouble(DiscountRate_TextBox.Text);
                        Double AmountPayable = Convert.ToDouble(AmountPayable_TextBox.Text);

                        AssuranceValue_TextBox.Text = Convert.ToString(ActuarialCalculator.calculateAssurance(CurrentAge, Duration, DiscountRate, myTable, AmountPayable));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);

                    }
                }
                else
                {
                    MessageBox.Show("The mortaility table selected cannot be found.");
                }
            }
            else if (UseMySQLData_CheckBox.IsChecked == true)
            {

                try
                {

                    myTable.tableBuilderFromMYSQL(controller, DatabaseTables_ComboBox.Text);

                    Double CurrentAge = Convert.ToDouble(CurrentAge_TextBox.Text);
                    Double Duration = Convert.ToDouble(Duration_TextBox.Text);

                    // -1 is a flag for a full life annuity.
                    Duration = (Duration == -1) ? (myTable.maxAge - CurrentAge) : Duration;

                    Double DiscountRate = Convert.ToDouble(DiscountRate_TextBox.Text);
                    Double AmountPayable = Convert.ToDouble(AmountPayable_TextBox.Text);

                    AssuranceValue_TextBox.Text = Convert.ToString(ActuarialCalculator.calculateAssurance(CurrentAge, Duration, DiscountRate, myTable, AmountPayable));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                }

            }
        }

        private void AddTable_Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt) | *.txt";
            dlg.Title = "Please select a file containing a mortaility table.";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string fileName = dlg.FileName;
                Mortality_ComboBox.Items.Add(fileName);

            }
        }

        private void UseLocalData_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UseMySQLData_CheckBox.IsChecked = false;
        }

        private void UseMySQLData_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UseLocalData_CheckBox.IsChecked = false;
        }

        private void Connect_Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectionDetails popup = new ConnectionDetails(this);
            popup.Show();

        }

        public void getMySQLTables()
        {
            DatabaseTables_ComboBox.Items.Clear();

            try
            {
                List<List<String>> mySQLTables = new List<List<String>>();
                mySQLTables = controller.performQuery("SHOW TABLES FROM ACTUARIALDATA");

                for (int i = 0; i < mySQLTables.Count; i++)
                {
                    for (int j = 0; j < mySQLTables[i].Count; j++)
                    {
                        DatabaseTables_ComboBox.Items.Add(mySQLTables[i][j]);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (DatabaseTables_ComboBox.Items.Count == 0)
            {
                DatabaseTables_ComboBox.Items.Add("No MySQL Data Available");
                DatabaseTables_ComboBox.SelectedIndex = 0;

            }
            else
            {
                DatabaseTables_ComboBox.SelectedIndex = 0;
            }

        }
    }

}