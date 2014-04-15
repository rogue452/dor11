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
    /// Interaction logic for ManagerContactsGUI.xaml
    /// </summary>
    public partial class ManagerContactsGUI : Window
    {
        string costid;
        string cosName;
        string cosADDs;

        public ManagerContactsGUI(string costid, string cosName, string cosADDs)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.costid = costid;
            this.cosName = cosName;
            this.cosADDs = cosADDs;
            try
            {
                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                MySqlConn.Open();
                string Query1 = ("select contactid as `מספר איש קשר`,contactName as `שם איש קשר` ,contactEmail as `אימייל איש קשר` ,contactPhone as `טלפון איש קשר` ,contactDepartment as `מחלקת איש קשר` from costumers  where costumerid='" + costid + "'");
                MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                MSQLcrcommand1.ExecuteNonQuery();
                MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                DataTable dt = new DataTable("contacts");
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
           
                    ExportToTXT();
                }





            private void ExportToTXT()
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.FileName = "רשימת אנשי קשר"; // Default file name
                    dialog.DefaultExt = ".text"; // Default file extension
                    dialog.Filter = "Text documents (.txt)|*.txt";  //EXcel documents (.xlsx)|*.xlsx";    // Filter files by extension 

                    // Show save file dialog box
                    Nullable<bool> result = dialog.ShowDialog();

                    // Process save file dialog box results 
                    if (result == true)
                    {
                        dataGrid1.SelectAllCells();
                        dataGrid1.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                        ApplicationCommands.Copy.Execute(null, dataGrid1);
                        String result1 = (string)Clipboard.GetData(DataFormats.Text);
                        dataGrid1.UnselectAllCells();
                        string saveto = dialog.FileName;
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@saveto,false,Encoding.Default);
                
                        file.WriteLine(result1.Replace("‘,’", "‘ ‘"));
                        file.Close();
                        file.Dispose();
                        // Save document 
                        MessageBox.Show("                                                                             !קובץ הטקסט נשמר\n\n           :כדי לפתוח באקסל מומלץ להשתמש ב''פתיחה באמצעות'' ולבחור ב\n\n                                                 ''Microsoft Excel''");
                    }
                }


    



        private void FirstNameSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                MySqlConn.Open();
                String searchkey = this.FirstNameSearchTextBox.Text;
                string Query1 = ("select contactid as `מספר איש קשר`,contactName as `שם איש קשר` ,contactEmail as `אימייל איש קשר` ,contactPhone as `טלפון איש קשר` ,contactDepartment as `מחלקת איש קשר` from costumers where contactName Like '%" + searchkey + "%' and costumerid='" + costid + "'");
                MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                MSQLcrcommand1.ExecuteNonQuery();
                MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                DataTable dt = new DataTable("contacts");
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
              //  string Connectionstring = " Server=localhost;Database=project; UId=root;Password=1234;";
              //  MySqlConnection MySqlConn = new MySqlConnection(Connectionstring);
                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                MySqlConn.Open();
                String searchidkey = this.IDSearchTextBox.Text;
                string Query1 = ("select contactid as `מספר איש קשר`,contactName as `שם איש קשר` ,contactEmail as `אימייל איש קשר` ,contactPhone as `טלפון איש קשר` ,contactDepartment as `מחלקת איש קשר` from costumers where contactid Like '%" + searchidkey + "%'  and costumerid='" + costid + "'");
                MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                MSQLcrcommand1.ExecuteNonQuery();
                MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                DataTable dt = new DataTable("contacts");
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
            ManagerAddContactsGUI MACG = new ManagerAddContactsGUI(costid, cosName, cosADDs);
            MACG.Show();
            this.Close();
        }




        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {   
                try
                {
                    System.Collections.IList rows = dataGrid1.SelectedItems;
                    DataRowView row = (DataRowView)dataGrid1.SelectedItems[0];
                    if (MessageBox.Show("?האם אתה בטוח שברצונך למחוק איש קשר זה", "וידוא מחיקה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //do no stuff
                    }
                    else // if the user clicked on "Yes" so he wants to Delete.
                    {
                        // this will give us the first colum of the selected row in the DataGrid.
                        
                        string selected = row["מספר איש קשר"].ToString();
                        // MessageBox.Show("" + selected + "");
                        try
                        {
                            MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                            MySqlConn.Open();
                            string Query1 = "delete from costumers  where costumerid='" + costid + "' and contactid='" + selected + "'";
                            MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                            MSQLcrcommand1.ExecuteNonQuery();
                            MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                            MySqlConn.Close();
                            MessageBox.Show("!איש הקשר נמחק מהמערכת");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        try
                        {
                            MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                            MySqlConn.Open();
                            string Query1 = ("select contactid as `מספר איש קשר`,contactName as `שם איש קשר` ,contactEmail as `אימייל איש קשר` ,contactPhone as `טלפון איש קשר` ,contactDepartment as `מחלקת איש קשר` from costumers  where costumerid='" + costid + "'");
                            MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                            MSQLcrcommand1.ExecuteNonQuery();
                            MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                            DataTable dt = new DataTable("contacts");
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
            catch { MessageBox.Show("לא נבחר איש קשר למחיקה"); }
            
        }//end function







        // go to previous screen.
        private void Back_Btn_Click(object sender, RoutedEventArgs e)
        {
            ManagerCusGui MCG = new ManagerCusGui();
            MCG.Show();
            this.Close();
        }






        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    DataRowView row = (DataRowView)dataGrid1.SelectedItems[0];
                    string selected = row["מספר איש קשר"].ToString();
                    // MessageBox.Show(""+selected+ "");
                        if (MessageBox.Show("?האם אתה בטוח שברצונך לעדכן איש קשר זה", "וידוא עדכון", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        {
                            //dont do stuff
                        }
                        else // if the user clicked on "Yes" so he wants to Update.
                        {
                            //checking if the email intered correctlly.
                            if ((Regex.IsMatch(row["אימייל איש קשר"].ToString(), @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")))
                            {
                                string contactName = row["שם איש קשר"].ToString();
                                string contactEmail = row["אימייל איש קשר"].ToString();
                                string contactPhone = row["טלפון איש קשר"].ToString();
                                string contactDepartment = row["מחלקת איש קשר"].ToString();

                                try
                                {

                                    MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                                    MySqlConn.Open();
                                    string Query1 = "update costumers set contactName='" + contactName + "',contactEmail='" + contactEmail + "',contactPhone='" + contactPhone + "',contactDepartment='" + contactDepartment + "' where costumerid='" + costid + "' and contactid='" + selected + "'";
                                    MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                                    MSQLcrcommand1.ExecuteNonQuery();
                                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                                    MySqlConn.Close();
                                    MessageBox.Show("!פרטי איש הקשר עודכנו");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                try
                                {
                                    MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                                    MySqlConn.Open();
                                    string Query1 = ("select contactid as `מספר איש קשר`,contactName as `שם איש קשר` ,contactEmail as `אימייל איש קשר` ,contactPhone as `טלפון איש קשר` ,contactDepartment as `מחלקת איש קשר` from costumers  where costumerid='" + costid + "'");
                                    MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                                    MSQLcrcommand1.ExecuteNonQuery();
                                    MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                                    DataTable dt = new DataTable("contacts");
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
                            else MessageBox.Show("כתובת האימייל שהזנת לא תקינה");

                        }


                    
                
            }
            catch { MessageBox.Show("לא נבחר איש קשר לעדכון "); }
        }



        private void Grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "מספר איש קשר")
            {
                // e.Cancel = true;   // For not to include 
                 e.Column.IsReadOnly = true; // Makes the column as read only
            }

        }





    }

}