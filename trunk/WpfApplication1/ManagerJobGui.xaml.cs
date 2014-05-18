﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
using System.ComponentModel;
using Microsoft.Win32;
using System.Text.RegularExpressions;
namespace project
{
    /// <summary>
    /// Interaction logic for ManagerJobGui.xaml
    /// 
    /// </summary>
    public partial class ManagerJobGui : Window
    {
        DataTable dt = new DataTable("jobs");
        public ManagerJobGui()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            


            try
            {
                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                MySqlConn.Open();
                string Query1 = ("select jobid as `מספר עבודה`,costumerid as `מספר לקוח` ,job_status as `סטטוס עבודה`,jobdescription  as `תאור עבודה` ,startDate  as `תאריך התחלה`,expectedFinishDate as `תאריך סיום משוער` ,actualFinishDate as `תאריך סיום בפועל`  from jobs group by jobid");
                MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                MSQLcrcommand1.ExecuteNonQuery();
                MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                //DataTable dt = new DataTable("jobs");
                dt.Clear();
                mysqlDAdp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                mysqlDAdp.Update(dt);
                MySqlConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        private void TXTBtn_Click(object sender, RoutedEventArgs e)
        {

            ExportToExcel();
        }





        private void ExportToExcel()
        {
            try
            {
               
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = "רשימת עבודות" + " נכון לתאריך - " + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString(); ; // Default file name
                dialog.DefaultExt = ".xlsx"; // Default file extension
                dialog.Filter = "Microsoft Excel 2003 and above Documents (.xlsx)|*.xlsx";  // |Text documents (.txt)|*.txt| Filter files by extension 

                // Show save file dialog box
                Nullable<bool> result = dialog.ShowDialog();

                // Process save file dialog box results 
                if (result == true)
                {
                    string saveto = dialog.FileName;
                    bool success = CreateExcelFile.CreateExcelDocument(dt, saveto);
                    if (success)
                    {
                        MessageBox.Show(" נוצר בהצלחה Microsoft Excel -מסמך ה");
                    }
                    else { 
                            MessageBox.Show(" לא נוצר  Microsoft Excel -התרחשה שגיאה ולכן מסמך ה"); 
                         }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void JobIDSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                MySqlConn.Open();
                String searchkey = this.JobIDSearchTextBox.Text;
                string Query1 = ("select jobid as `מספר עבודה`,costumerid as `מספר לקוח` ,job_status as `סטטוס עבודה`,jobdescription  as `תאור עבודה` ,startDate  as `תאריך התחלה`,expectedFinishDate as `תאריך סיום משוער` ,actualFinishDate as `תאריך סיום בפועל` from project.jobs  where  jobid Like '%" + searchkey + "%' group by jobid");
                MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                MSQLcrcommand1.ExecuteNonQuery();
                MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                //DataTable dt = new DataTable("jobs");
                dt.Clear();
                mysqlDAdp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                mysqlDAdp.Update(dt);
                MySqlConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void IDSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                MySqlConn.Open();
                String searchidkey = this.IDSearchTextBox.Text;
                string Query1 = ("select jobid as `מספר עבודה`,costumerid as `מספר לקוח` ,job_status as `סטטוס עבודה`,jobdescription  as `תאור עבודה` ,startDate  as `תאריך התחלה`,expectedFinishDate as `תאריך סיום משוער` ,actualFinishDate as `תאריך סיום בפועל`  from project.jobs  where  costumerid Like '%" + searchidkey + "%' group by jobid");
                MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                MSQLcrcommand1.ExecuteNonQuery();
                MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                //DataTable dt = new DataTable("jobs");
                dt.Clear();
                mysqlDAdp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                mysqlDAdp.Update(dt);
                MySqlConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ADD_Btn_Click(object sender, RoutedEventArgs e)
        {
            ManagerAddNewJobGUI MACG = new ManagerAddNewJobGUI();
            MACG.Show();
            Login.close = 1;
            this.Close();
        }



        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dataGrid1.SelectedItems[0];
                if (MessageBox.Show("האם אתה בטוח שברצונך למחוק עבודה זו", "וידוא מחיקה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else // if the user clicked on "Yes" so he wants to Delete.
                {
                    // this will give us the first colum of the selected row in the DataGrid.

                    string selected = row["מספר עבודה"].ToString();
                    

                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        string Query1 = "delete from jobs where jobid='" + selected + "'";
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        MySqlConn.Close();
                        MessageBox.Show("! עבודה נמחקה מהמערכת");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        string Query1 = ("select jobid as `מספר עבודה`,costumerid as `מספר לקוח` ,job_status as `סטטוס עבודה`,jobdescription  as `תאור עבודה` ,startDate  as `תאריך התחלה`,expectedFinishDate as `תאריך סיום משוער` ,actualFinishDate as `תאריך סיום בפועל`  from jobs group by jobid");
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        //DataTable dt = new DataTable("jobs");
                        dt.Clear();
                        mysqlDAdp.Fill(dt);
                        dataGrid1.ItemsSource = dt.DefaultView;
                        mysqlDAdp.Update(dt);
                        MySqlConn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }//end else

            }//end try
            catch { MessageBox.Show("לא נבחרה עבודה למחיקה"); }

        }//end function



        // go to previous screen.
        private void Back_Btn_Click(object sender, RoutedEventArgs e)
        {
            ManagerGui MG = new ManagerGui();
            MG.Show();
            Login.close = 1;
            this.Close();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dataGrid1.SelectedItems[0];
                string jobid = row["מספר עבודה"].ToString();
                // MessageBox.Show("" + jobid + "");

                if (MessageBox.Show("?האם אתה בטוח שברצונך לעדכן עבודה זו", "וידוא עדכון", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //dont do stuff
                }
                else // if the user clicked on "Yes" so he wants to Update.
                {
                    string status = row["סטטוס עבודה"].ToString();
                    string description = row["תאור עבודה"].ToString();
                    if (row["תאריך התחלה"].ToString().Equals(""))
                    {
                        MessageBox.Show("!לא ניתן למחוק את תאריך ההתחלה");
                        return;
                    }
                    if (row["תאריך סיום משוער"].ToString().Equals(""))
                    {
                        MessageBox.Show("!לא ניתן למחוק את תאריך סיום משוער");
                        return;
                    }
                    string start_date =  Convert.ToDateTime(row["תאריך התחלה"].ToString()).ToString("yyyy-MM-dd");
                    string finish_date = Convert.ToDateTime(row["תאריך סיום משוער"].ToString()).ToString("yyyy-MM-dd");
                    string actual_finish_date;
                    if (row["תאריך סיום בפועל"].ToString().Equals(""))
                    {
                        try
                        {
                            MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                            MySqlConn.Open();
                            string Query1 = "update jobs set job_status='" + status + "',jobdescription='" + description + "',startDate='" + start_date + "',expectedFinishDate='" + finish_date + "',actualFinishDate=NULL where jobid='" + jobid + "'";
                            MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                            MSQLcrcommand1.ExecuteNonQuery();
                            MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                            MySqlConn.Close();
                            MessageBox.Show("!פרטי העבודה עודכנו");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        actual_finish_date = Convert.ToDateTime(row["תאריך סיום בפועל"].ToString()).ToString("yyyy-MM-dd");
                        try
                        {

                            MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                            MySqlConn.Open();
                            string Query1 = "update jobs set job_status='" + status + "',jobdescription='" + description + "',startDate='" + start_date + "',expectedFinishDate='" + finish_date + "',actualFinishDate='" + actual_finish_date + "' where jobid='" + jobid + "'";
                            MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                            MSQLcrcommand1.ExecuteNonQuery();
                            MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                            MySqlConn.Close();
                            MessageBox.Show("!פרטי העבודה עודכנו");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        string Query1 = ("select jobid as `מספר עבודה`,costumerid as `מספר לקוח` ,job_status as `סטטוס עבודה`,jobdescription  as `תאור עבודה` ,startDate  as `תאריך התחלה`,expectedFinishDate as `תאריך סיום משוער` ,actualFinishDate as `תאריך סיום בפועל`  from jobs group by jobid");
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        //DataTable dt = new DataTable("jobs");
                        dt.Clear();
                        mysqlDAdp.Fill(dt);
                        dataGrid1.ItemsSource = dt.DefaultView;
                        mysqlDAdp.Update(dt);
                        MySqlConn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            catch { MessageBox.Show("לא נבחרה עבודה לעדכון "); }
        }



        private void Grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "מספר לקוח" || e.Column.Header.ToString() == "מספר עבודה")
            {
                // e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read onl
            }



            //if the Column is a date Column then show me only the date
            if (e.Column.Header.ToString() == "תאריך התחלה")
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";

                /*
                // Create a new template column.
                DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
                templateColumn.CellTemplate = (DataTemplate)Resources["dueDateCellTemplate"];
                templateColumn.CellEditingTemplate = (DataTemplate)Resources["dueDateCellEditingTemplate"];
                e.Column = templateColumn;
                */



                DataGridTemplateColumn dgct = new DataGridTemplateColumn();
                dgct.Header = "תאריך התחלה";
                dgct.SortMemberPath = "תאריך התחלה";

                Binding b = new Binding("תאריך התחלה");
                b.StringFormat = "dd/MM/yyyy";

                #region Editing
                FrameworkElementFactory factory = new FrameworkElementFactory(typeof(DatePicker));
                factory.SetValue(DatePicker.SelectedDateProperty, b);
                DataTemplate cellEditingTemplate = new DataTemplate();
                cellEditingTemplate.VisualTree = factory;
                dgct.CellEditingTemplate = cellEditingTemplate;
                #endregion

                #region View
                FrameworkElementFactory sfactory = new FrameworkElementFactory(typeof(TextBlock));
                sfactory.SetValue(TextBlock.TextProperty, b);
                DataTemplate cellTemplate = new DataTemplate();
                cellTemplate.VisualTree = sfactory;
                dgct.CellTemplate = cellTemplate;
                #endregion
                e.Column = dgct;
            }

            if (e.Column.Header.ToString() == "תאריך סיום משוער" )
            {
                DataGridTemplateColumn dgct = new DataGridTemplateColumn();
                dgct.Header = "תאריך סיום משוער";
                dgct.SortMemberPath = "תאריך סיום משוער";

                Binding b = new Binding("תאריך סיום משוער");
                b.StringFormat = "dd/MM/yyyy";

                #region Editing
                FrameworkElementFactory factory = new FrameworkElementFactory(typeof(DatePicker));
                factory.SetValue(DatePicker.SelectedDateProperty, b);
                DataTemplate cellEditingTemplate = new DataTemplate();
                cellEditingTemplate.VisualTree = factory;
                dgct.CellEditingTemplate = cellEditingTemplate;
                #endregion

                #region View
                FrameworkElementFactory sfactory = new FrameworkElementFactory(typeof(TextBlock));
                sfactory.SetValue(TextBlock.TextProperty, b);
                DataTemplate cellTemplate = new DataTemplate();
                cellTemplate.VisualTree = sfactory;
                dgct.CellTemplate = cellTemplate;
                #endregion
                e.Column = dgct;
            }

            if (e.Column.Header.ToString() == "תאריך סיום בפועל")
            {
                DataGridTemplateColumn dgct = new DataGridTemplateColumn();
                dgct.Header = "תאריך סיום בפועל";
                dgct.SortMemberPath = "תאריך סיום בפועל";

                Binding b = new Binding("תאריך סיום בפועל");
                b.StringFormat = "dd/MM/yyyy";

                #region Editing
                FrameworkElementFactory factory = new FrameworkElementFactory(typeof(DatePicker));
                factory.SetValue(DatePicker.SelectedDateProperty, b);
                DataTemplate cellEditingTemplate = new DataTemplate();
                cellEditingTemplate.VisualTree = factory;
                dgct.CellEditingTemplate = cellEditingTemplate;
                #endregion

                #region View
                FrameworkElementFactory sfactory = new FrameworkElementFactory(typeof(TextBlock));
                sfactory.SetValue(TextBlock.TextProperty, b);
                DataTemplate cellTemplate = new DataTemplate();
                cellTemplate.VisualTree = sfactory;
                dgct.CellTemplate = cellTemplate;
                #endregion
                e.Column = dgct;
            }


            if (e.Column.Header.ToString() == "סטטוס עבודה")
            {

                string columnName1 = e.Column.Header.ToString();
                Dictionary<string, string> comboKey1 = new Dictionary<string, string>()
                    {
                        {"נרשמה","נרשמה"},
                        {"בעבודה","בעבודה"},
                        {"מושהת","מושהת"},
                        {"בוטלה","בוטלה"},
                        {"הסתיימה","הסתיימה"},
                    };
                DataGridTemplateColumn col11 = new DataGridTemplateColumn();
                col11.Header = columnName1;

                #region Editing
                FrameworkElementFactory factory11 = new FrameworkElementFactory(typeof(ComboBox));
                Binding b11 = new Binding(columnName1);
                b11.IsAsync = true;
                b11.Mode = BindingMode.TwoWay;
                factory11.SetValue(ComboBox.ItemsSourceProperty, comboKey1);
                factory11.SetValue(ComboBox.SelectedValuePathProperty, "Key");
                factory11.SetValue(ComboBox.DisplayMemberPathProperty, "Value");
                factory11.SetValue(ComboBox.SelectedValueProperty, b11);
                factory11.SetValue(ComboBox.SelectedItemProperty, col11);

                DataTemplate cellTemplate1 = new DataTemplate();
                cellTemplate1.VisualTree = factory11;
                col11.CellTemplate = cellTemplate1;
                col11.CellEditingTemplate = cellTemplate1;
                col11.IsReadOnly = false;
                col11.InvalidateProperty(ComboBox.SelectedValueProperty);
                #endregion

                #region View
                FrameworkElementFactory sfactory = new FrameworkElementFactory(typeof(TextBlock));
                sfactory.SetValue(TextBlock.TextProperty, b11);
                DataTemplate cellTemplate = new DataTemplate();
                cellTemplate.VisualTree = sfactory;
                col11.CellTemplate = cellTemplate;
                #endregion

                e.Column = col11;


            }
        }

        private void Contacts_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dataGrid1.SelectedItems[0];
                string selected = row["מספר לקוח"].ToString();
                string CosName = row["שם לקוח"].ToString();
                string cosADDs = row["כתובת לקוח"].ToString();
                ManagerContactsGUI MCG = new ManagerContactsGUI(selected, CosName, cosADDs);
                MCG.Show();
                Login.close = 1;
                this.Close();
            }
            catch { MessageBox.Show("לא נבחר לקוח"); }
        }


        //This function will set the 2 DatePickers to today and will reaload to the default datagrid.
        private void Refresh_button_Click(object sender, RoutedEventArgs e)
        {
            Start_datePicker.SelectedDate = DateTime.Today;
            End_datePicker.SelectedDate = DateTime.Today;
            try
            {
                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                MySqlConn.Open();
                string Query1 = ("select jobid as `מספר עבודה`,costumerid as `מספר לקוח` ,job_status as `סטטוס עבודה`,jobdescription  as `תאור עבודה` ,startDate  as `תאריך התחלה`,expectedFinishDate as `תאריך סיום משוער` ,actualFinishDate as `תאריך סיום בפועל`  from jobs group by jobid");
                MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                MSQLcrcommand1.ExecuteNonQuery();
                MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                dt.Clear();
                mysqlDAdp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                mysqlDAdp.Update(dt);
                MySqlConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        //This function will filter the startDate by date selected from 2 DatePickers.
        // if the dates were not right a message will be shown. 
        private void Filter_button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("" + Start_datePicker.Text + "");
            if (Start_datePicker.Text != "" && End_datePicker.Text != "")
            {
                String start, end;
                DateTime s = (DateTime)Convert.ToDateTime(Start_datePicker.Text);
                DateTime f = (DateTime)Convert.ToDateTime(End_datePicker.Text);
                TimeSpan ts = f - s;
                //MessageBox.Show("" + s + "");
                //MessageBox.Show("" + f + "");
                //MessageBox.Show("" + ts.Days + "");
                if (ts.Days >= 0)
                {
                    start = Convert.ToDateTime(Start_datePicker.Text).ToString("yyyy-MM-dd");
                    end = Convert.ToDateTime(End_datePicker.Text).ToString("yyyy-MM-dd");

                    string radio = "startDate";
                    if (ExpectedFinishDate_radioButton.IsChecked == true)
                    {
                        radio = "expectedFinishDate";
                    }
                    if (ActualFinishDate_radioButton.IsChecked == true)
                    {
                        radio = "actualFinishDate";
                    }
                    //MessageBox.Show("" + radio + "");
                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        string Query1 = ("SELECT jobid as `מספר עבודה`,costumerid as `מספר לקוח` ,job_status as `סטטוס עבודה`,jobdescription  as `תאור עבודה` ,startDate  as `תאריך התחלה`,expectedFinishDate as `תאריך סיום משוער` ,actualFinishDate as `תאריך סיום בפועל`  FROM jobs WHERE " + radio + " BETWEEN '" + start + "' AND '" + end + "'  group by jobid ");
                        //MessageBox.Show("" + Query1 + "");
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        dt.Clear();
                        mysqlDAdp.Fill(dt);
                        dataGrid1.ItemsSource = dt.DefaultView;
                        mysqlDAdp.Update(dt);
                        MySqlConn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("אסור שתאריך הסוף הנבחר יהיה לפני תאריך ההתחלה הנבחר");
                }
            }
            else 
            {
                if (Start_datePicker.Text == "" && End_datePicker.Text != "")
                {MessageBox.Show("לא נבחר תאריך התחלה לסינון");}
                if (Start_datePicker.Text != "" && End_datePicker.Text == "")
                { MessageBox.Show("לא נבחר תאריך סוף לסינון"); }
                if (Start_datePicker.Text == "" && End_datePicker.Text == "")
                { MessageBox.Show("לא נבחרו תאריכי התחלה וסוף לסינון "); }
            }

        }






        private void ViewJobIInfo_button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {                
                DataRowView row = (DataRowView)dataGrid1.SelectedItems[0];
                string selected = row["מספר עבודה"].ToString();
                ManagerJobInfoGui MJIG = new ManagerJobInfoGui(selected);
                MJIG.Show();
                Login.close = 1;
                this.Close();
            }//end try
            catch { MessageBox.Show("לא נבחרה עבודה לצפיה"); }
        }



        private void ADD_new_item_pattern_click(object sender, RoutedEventArgs e)
        {
            ManagerAddNewItemPatternGUI MANIG = new ManagerAddNewItemPatternGUI();
            MANIG.ShowDialog();
           // this.Close();

        }






        private void exit_clicked(object sender, CancelEventArgs e)
        {
            Console.WriteLine("" + Login.close);

            if (Login.close == 0) // then the user want to exit.
            {
                if (MessageBox.Show("?האם אתה בטוח שברצונך לצאת מהמערכת ", "וידוא יציאה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    e.Cancel = true; ; //don't exit.
                }
                else // if the user clicked on "Yes" so he wants to Update.
                {
                    // logoff user
                    try
                    {
                        string empid1 = Login.empid;
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        string Query1 = "update users set connected='לא מחובר' where empid='" + empid1 + "' ";
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        MySqlConn.Close();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    MessageBox.Show("               נותקת בהצלחה מהמערכת\n          תודה שהשתמשת במערכת קרוסר\n                          !להתראות");
                }
            }
            else
            {

            }
            Login.close = 0;
        }











    }

}







      
