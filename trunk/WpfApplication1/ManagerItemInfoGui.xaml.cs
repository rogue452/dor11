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
    public partial class ManagerItemInfoGui : Window
    {
        DataTable dt = new DataTable("iteminfo");
        string itemID, jobID;
        public ManagerItemInfoGui(string itemID1, string jobID1)
        {
            this.itemID = itemID1;
            this.jobID = jobID1;
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            label1.Content = jobID;
            label4.Content = itemID;
            refreashandclear();
/*
            try
            {
                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                MySqlConn.Open();
                string Query1 = ("SELECT jobs.itemNum as `מספר פריט בסט`, inTheGroup as `ייחשב בקבוצה`, jobs.itemDescription as `תיאור פריט`,jobs.itemStatus  as `סטטוס הפריט`, jobs.itemStageOrder as `מספר השלב הנוכחי`,stageName  as `שם השלב הנוכחי` ,item.stage_discription as `תאור השלב הנוכחי`,itemToFixStageOrder as `מספר השלב שבו זוהה כתקול (אם זוהה)`  FROM jobs,item WHERE jobs.itemid=item.itemid and jobs.itemStageOrder=item.itemStageOrder and jobs.itemStatus=item.itemStatus and jobs.jobid='" + jobID + "' and jobs.itemid='" + itemID + "' ");
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
 * */
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
                dialog.FileName = " רשימת פריטים לעבודה מספר " + jobID + " נכון לתאריך - " + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString(); ; // Default file name
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
                    else
                    {
                        MessageBox.Show(" לא נוצר  Microsoft Excel -התרחשה שגיאה ולכן מסמך ה");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void refreashandclear()
        {
            try
            {
                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                MySqlConn.Open();
                string Query1 = ("SELECT jobs.itemNum as `מספר פריט בסט`, inTheGroup as `ייחשב בקבוצה`, jobs.itemDescription as `תיאור פריט`,jobs.itemStatus  as `סטטוס הפריט`, jobs.itemStageOrder as `מספר השלב הנוכחי`,stageName  as `שם השלב הנוכחי` ,item.stage_discription as `תאור השלב הנוכחי`,itemToFixStageOrder as `מספר השלב שבו זוהה כתקול (אם זוהה)`  FROM jobs,item WHERE jobs.itemid=item.itemid and jobs.itemStageOrder=item.itemStageOrder and jobs.itemStatus=item.itemStatus and jobs.jobid='" + jobID + "' and jobs.itemid='" + itemID + "' ");
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
            ItemIDSearch_TextBox.Clear();
            StageNameSearchTextBox.Clear();
        }

        private void ItemIDSearch_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            try
            {
                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                MySqlConn.Open();
                String searchkey = this.ItemIDSearch_TextBox.Text;
                string Query1 = ("SELECT jobs.itemNum as `מספר פריט בסט`, inTheGroup as `ייחשב בקבוצה`, jobs.itemDescription as `תיאור פריט`,jobs.itemStatus  as `סטטוס הפריט`, jobs.itemStageOrder as `מספר השלב הנוכחי`,stageName  as `שם השלב הנוכחי` ,item.stage_discription as `תאור השלב הנוכחי`,itemToFixStageOrder as `מספר השלב שבו זוהה כתקול (אם זוהה)`  FROM jobs,item WHERE jobs.itemid=item.itemid and jobs.itemStageOrder=item.itemStageOrder and jobs.itemStatus=item.itemStatus and jobs.jobid='" + jobID + "' and jobs.itemid='" + itemID + "' and jobs.itemNum Like '%" + searchkey + "%'");
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
            StageNameSearchTextBox.Clear();

        }

        private void StageNameSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            try
            {
                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                MySqlConn.Open();
                String searchNamekey = this.StageNameSearchTextBox.Text;
                string Query1 = ("SELECT jobs.itemNum as `מספר פריט בסט`, inTheGroup as `ייחשב בקבוצה`, jobs.itemDescription as `תיאור פריט`,jobs.itemStatus  as `סטטוס הפריט`, jobs.itemStageOrder as `מספר השלב הנוכחי`,stageName  as `שם השלב הנוכחי` ,item.stage_discription as `תאור השלב הנוכחי`,itemToFixStageOrder as `מספר השלב שבו זוהה כתקול (אם זוהה)`   FROM jobs,item WHERE jobs.itemid=item.itemid and jobs.itemStageOrder=item.itemStageOrder and jobs.itemStatus=item.itemStatus and jobs.jobid='" + jobID + "'and jobs.itemid='" + itemID + "' and item.stageName Like '%" + searchNamekey + "%'");
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
            ItemIDSearch_TextBox.Clear();
        }





        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //System.Collections.IList rows = dataGrid1.SelectedItems;
                DataRowView row = (DataRowView)dataGrid1.SelectedItems[0];
                if (MessageBox.Show("?האם אתה בטוח שברצונך למחוק פריט זה", "וידוא מחיקה", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return;
                }
                else // if the user clicked on "Yes" so he wants to Delete.
                {
                    string itemnum = row["מספר פריט בסט"].ToString();
                    int thecountinitemid = 0;
                    int thecountinjobid = 0;

                    // see if this is the last item in this itemid
                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        string Query1 = "select COUNT(itemNum) from jobs where itemid='" + itemID + "' and   jobid='" + jobID + "' ";
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        MySqlDataReader dr = MSQLcrcommand1.ExecuteReader();
                        
                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                thecountinitemid = dr.GetInt32(0);
                            }

                        }
                        MySqlConn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("נפל בשורה מספר 222");
                        MessageBox.Show(ex.Message);
                    }
                    Console.WriteLine("שורה  225 thecountinitemid=" + thecountinitemid);
                    if (thecountinitemid > 1)
                    {
                        try
                        {
                            MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                            MySqlConn.Open();
                            string Query1 = "DELETE FROM jobs WHERE jobid='" + jobID + "' and itemid='" + itemID + "'and itemNum='" + itemnum + "'";
                            MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                            MSQLcrcommand1.ExecuteNonQuery();
                            MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                            MySqlConn.Close();
                            refreashandclear();
                            MessageBox.Show("!הפריט נמחק מהעבודה", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                
                    // if it is the last from the itemid then check if this is the last from the jobid.
                    else 
                    {
                        try
                        {
                            MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                            MySqlConn.Open();
                            string Query1 = "select COUNT(itemNum) from jobs where jobid='" + jobID + "' ";
                            MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                            MSQLcrcommand1.ExecuteNonQuery();
                            MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                            MySqlDataReader dr = MSQLcrcommand1.ExecuteReader();

                            while (dr.Read())
                            {
                                if (!dr.IsDBNull(0))
                                {
                                    thecountinjobid = dr.GetInt32(0);
                                }

                            }
                            MySqlConn.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("נפל בשורה מספר 272");
                            MessageBox.Show(ex.Message);
                        }
                        Console.WriteLine("שורה  275 thecountinjobid=" + thecountinjobid);
                        if (thecountinjobid > 1)
                        {
                            try
                            {
                                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                                MySqlConn.Open();
                                string Query1 = "DELETE FROM jobs WHERE jobid='" + jobID + "' and itemid='" + itemID + "'and itemNum='" + itemnum + "'";
                                MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                                MSQLcrcommand1.ExecuteNonQuery();
                                MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                                MySqlConn.Close();
                                refreashandclear();
                                MessageBox.Show("!הפריט נמחק מהעבודה", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                                ManagerJobInfoGui MJIG = new ManagerJobInfoGui(jobID);
                                MJIG.Show();
                                Login.close = 1;
                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else // if this is the last item in this job then just update the job status to בוטלה
                        {
                            try
                            {
                                string Query1 = "UPDATE jobs SET job_status='בוטלה' WHERE jobid='" + jobID + "'";
                                MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                                MySqlConn.Open();
                                MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                                MSQLcrcommand1.ExecuteNonQuery();
                                MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                                MySqlConn.Close();
                                MessageBox.Show("בגלל שזהו הפריט האחרון בעבודה\n .הפריט לא נמחק אך סטטוס העבודה שונה לבוטלה", "שים לב - שינוי סטטוס עבודה", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
 
                        }

                    }// end else -  if it is the last from the itemid then check if this is the last from the jobid.
                    

                    
                }//end else

            }//end try
            catch 
            {
                MessageBox.Show("לא נבחר פריט למחיקה", "שים לב", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }//end function



        // go to previous screen.
        private void Back_Btn_Click(object sender, RoutedEventArgs e)
        {
            ManagerJobInfoGui MJIG = new ManagerJobInfoGui(jobID);
            MJIG.Show();
            Login.close = 1;
            this.Close();
        }



        //////////////////////
        //////////////////////
        // This func is not complete and will be changed 
        /////////////////////
        /////////////////////
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dataGrid1.SelectedItems[0];
                string itemnum = row["מספר פריט בסט"].ToString();

                if (MessageBox.Show("?האם אתה בטוח שברצונך לעדכן פריט זה", "וידוא עדכון", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                { }
                else // if the user clicked on "Yes" so he wants to Update.
                {
                    string status = row["סטטוס הפריט"].ToString();
                    string itemdes = row["תיאור פריט"].ToString();
                    string isIn = row["ייחשב בקבוצה"].ToString();
                    // check if the item status pattern exist.
               /*     try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        string Query1 = ("SELECT item.itemStatus FROM item WHERE item.itemid='" + itemID + " AND itemStatus='" + status + " ");
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        MySqlDataReader dr = MSQLcrcommand1.ExecuteReader();
                        int exist = 0;
                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                exist=1;
                            }
                        }
                        MySqlConn.Close();
                        if (exist == 0)
                        {
                            MessageBox.Show("!לא נוצרו עדיין שלבים עבור הסטטוס שבחרת - סטטוס לא עודכן");
                            // need to do refreash here.
                            return;
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); return; }
                    */

                    //doing the update
                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        string Query1 = "update jobs set itemStatus='" + status + "', itemDescription='" + itemdes + "' , inTheGroup='" + isIn + "'  where jobid='" + jobID + "' and itemid='" + itemID + "'and itemNum='" + itemnum + "'";
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        MySqlConn.Close();


                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); return; }
                    refreashandclear();

                  /*  // refreash (need to change Query1)
                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        string Query1 = ("SELECT jobs.itemNum as `מספר פריט בסט`,jobs.itemStatus  as `סטטוס הפריט`, jobs.itemStageOrder as `מספר השלב הנוכחי`,stageName  as `שם השלב הנוכחי` ,item.stage_discription as `תאור השלב הנוכחי`,itemToFixStageOrder as `מספר השלב שבו זוהה כתקול (אם זוהה)`  FROM jobs,item WHERE jobs.itemid=item.itemid and jobs.itemStageOrder=item.itemStageOrder and jobs.itemStatus=item.itemStatus and jobs.jobid='" + jobID + "' and jobs.itemid='" + itemID + "' ");
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        dt.Clear();
                        mysqlDAdp.Fill(dt);
                        dataGrid1.ItemsSource = dt.DefaultView;
                        mysqlDAdp.Update(dt);
                        MySqlConn.Close();
                        MessageBox.Show ("סטטוס התעדכן");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }*/
                }
            }
            catch { MessageBox.Show("לא נבחר פריט לעדכון "); }
        }
            
    

        private void Grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "שם השלב הנוכחי" || e.Column.Header.ToString() == "מספר השלב הנוכחי" || e.Column.Header.ToString() == "מספר פריט" || e.Column.Header.ToString() == "מספר פריט בסט" || e.Column.Header.ToString() == "תאור השלב הנוכחי" || e.Column.Header.ToString() == "מספר השלב שבו זוהה כתקול (אם זוהה)")
            {
                // e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }

            if (e.Column.Header.ToString() == "סטטוס הפריט")
            {
                string columnName = e.Column.Header.ToString();
                Dictionary<string, string> comboKey = new Dictionary<string, string>()
                    {
                        {"רישום","רישום"},
                        {"בעבודה","בעבודה"},
                        {"תיקון","תיקון"},
                        {"פסול","פסול"},
                        {"גמר ייצור","גמר ייצור"},
                        {"הסתיים","הסתיים"},
                    };
                DataGridTemplateColumn col1 = new DataGridTemplateColumn();
                col1.Header = columnName;
                col1.SortMemberPath = columnName;
                #region Editing
                FrameworkElementFactory factory1 = new FrameworkElementFactory(typeof(ComboBox));
                Binding b1 = new Binding(columnName);
                b1.IsAsync = true;
                b1.Mode = BindingMode.TwoWay;
                factory1.SetValue(ComboBox.ItemsSourceProperty, comboKey);
                factory1.SetValue(ComboBox.SelectedValuePathProperty, "Key");
                factory1.SetValue(ComboBox.DisplayMemberPathProperty, "Value");
                factory1.SetValue(ComboBox.SelectedValueProperty, b1);
                factory1.SetValue(ComboBox.SelectedItemProperty, col1);

                DataTemplate cellTemplate1 = new DataTemplate();
                cellTemplate1.VisualTree = factory1;
                col1.CellTemplate = cellTemplate1;
                col1.CellEditingTemplate = cellTemplate1;
                col1.IsReadOnly = false;
                col1.InvalidateProperty(ComboBox.SelectedValueProperty);
                #endregion

                #region View
                FrameworkElementFactory sfactory = new FrameworkElementFactory(typeof(TextBlock));
                sfactory.SetValue(TextBlock.TextProperty, b1);
                DataTemplate cellTemplate = new DataTemplate();
                cellTemplate.VisualTree = sfactory;
                col1.CellTemplate = cellTemplate;
                #endregion

                e.Column = col1;
            }

            if (e.Column.Header.ToString() == "ייחשב בקבוצה")
            {
                string columnName = e.Column.Header.ToString();
                Dictionary<string, string> comboKey = new Dictionary<string, string>()
                    {
                        {"כן","כן"},
                        {"לא","לא"},
                    };
                DataGridTemplateColumn col1 = new DataGridTemplateColumn();
                col1.Header = columnName;
                col1.SortMemberPath = columnName;
                #region Editing
                FrameworkElementFactory factory1 = new FrameworkElementFactory(typeof(ComboBox));
                Binding b1 = new Binding(columnName);
                b1.IsAsync = true;
                b1.Mode = BindingMode.TwoWay;
                factory1.SetValue(ComboBox.ItemsSourceProperty, comboKey);
                factory1.SetValue(ComboBox.SelectedValuePathProperty, "Key");
                factory1.SetValue(ComboBox.DisplayMemberPathProperty, "Value");
                factory1.SetValue(ComboBox.SelectedValueProperty, b1);
                factory1.SetValue(ComboBox.SelectedItemProperty, col1);

                DataTemplate cellTemplate1 = new DataTemplate();
                cellTemplate1.VisualTree = factory1;
                col1.CellTemplate = cellTemplate1;
                col1.CellEditingTemplate = cellTemplate1;
                col1.IsReadOnly = false;
                col1.InvalidateProperty(ComboBox.SelectedValueProperty);
                #endregion

                #region View
                FrameworkElementFactory sfactory = new FrameworkElementFactory(typeof(TextBlock));
                sfactory.SetValue(TextBlock.TextProperty, b1);
                DataTemplate cellTemplate = new DataTemplate();
                cellTemplate.VisualTree = sfactory;
                col1.CellTemplate = cellTemplate;
                #endregion

                e.Column = col1;
            }
        }

       

        private void Item_Stages_button_Click(object sender, RoutedEventArgs e)
        {
                ManagerGeneralItemStagesGui MGIG = new ManagerGeneralItemStagesGui(itemID);
                MGIG.Owner = this;
                MGIG.ShowDialog();
        }





        private void Add_Existing_button_Click(object sender, RoutedEventArgs e)
        {
            ManagerAddExistingItemGui MAEIG = new ManagerAddExistingItemGui(jobID);
            MAEIG.Show();
            Login.close = 1;
            this.Close();
        }



        private void NextStage_button_Click(object sender, RoutedEventArgs e)
        {
            string next_stage="";
            string status = "";
            string next_status = "";
            string itemToFixStageOrder = "";
            bool last = false;
            string Query1 = "";
            try
            {
                DataRowView row = (DataRowView)dataGrid1.SelectedItems[0];
                if (MessageBox.Show("?האם אתה בטוח שברצונך לקדם פריט זה", "וידוא קידום", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    refreashandclear();
                    return; //do no stuff
                }
                else // if the user clicked on "Yes" so he wants to Delete.
                {

                    string curr = row["מספר השלב הנוכחי"].ToString();
                    string itemnum =row["מספר פריט בסט"].ToString();


                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        Query1 = "select itemStatus , itemToFixStageOrder from jobs where itemid='" + itemID + "' and   jobid='" + jobID + "' and itemNum= '" + itemnum + "'     ";
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        MySqlDataReader dr = MSQLcrcommand1.ExecuteReader();

                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                status = dr.GetString(0);
                                itemToFixStageOrder = dr.GetString(1);
                            }
                           
                        }

                        MySqlConn.Close();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("נפל בשורה מספר 404");
                        MessageBox.Show(ex.Message);
                    }


                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        Query1 = "select MIN(itemStageOrder) from item where itemid='" + itemID + "' and   itemStageOrder>'" + curr + "' and itemStatus= '" + status + "'     ";
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        MySqlDataReader dr = MSQLcrcommand1.ExecuteReader();
                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                next_stage = dr.GetString(0);
                            }
                            else 
                            {
                                last = true;
                                if (status=="רישום")
                                {
                                    next_status = "בעבודה";
                                   next_stage = "1";
                                }
                                if (status == "בעבודה")
                                {
                                    next_status = "גמר ייצור";
                                   next_stage = "0";
                                    
                                }
                                if (status == "גמר ייצור")
                                {
                                    next_status = "הסתיים";
                                    next_stage = "1";
                                    
                                }
                                if (status == "תיקון")
                                {
                                    if (itemToFixStageOrder != "0")
                                    {
                                        next_status = "בעבודה";
                                        next_stage = itemToFixStageOrder;
                                    }
                                    if (itemToFixStageOrder == "0")
                                    {
                                        next_status = "גמר ייצור";
                                        next_stage = itemToFixStageOrder;
                                    }
                                }

                                if (status == "הסתיים")
                                {
                                    MessageBox.Show(".כבר בוצעו כל השלבים הקיימים עבור פריט זה", "שים לב", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    MySqlConn.Close();
                                    refreashandclear();
                                    return;
                                }
                               
                                if (status == "פסול")
                                {
                                    MessageBox.Show(".עבור פריט בסטטוס פסול עליך לשנות סטטוס ידנית", "שים לב", MessageBoxButton.OK, MessageBoxImage.Error);
                                    MySqlConn.Close();
                                    refreashandclear();
                                    return;
                                    
                                }

                            } // end else of if (!dr.IsDBNull(0))
                        } // end while (dr.Read())

                        MySqlConn.Close();

                    } // end try
                    catch (Exception ex)
                    {
                        Console.WriteLine("נפל בשורה מספר 550");
                        MessageBox.Show(ex.Message);
                    }

                    if (last == true)
                    {
                        Query1 = "update jobs set itemStatus='" + next_status + "' , itemStageOrder='" + next_stage + "' where jobid='" + jobID + "' and itemid='" + itemID + "'and itemNum='" + itemnum + "'";
                    }
                    else 
                    {
                        Query1 = "update jobs set itemStageOrder='" + next_stage + "' where jobid='" + jobID + "' and itemid='" + itemID + "'and itemNum='" + itemnum + "'";
                    }
                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        //string Query1 = "update jobs set itemStageOrder='" + next_stage + "' where jobid='" + jobID + "' and itemid='" + itemID + "'and itemNum='" + itemnum + "'";
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        MySqlConn.Close();


                    }
                    catch (Exception ex) 
                    {
                        MessageBox.Show(ex.Message);
                        Console.WriteLine("נפל בשורה מספר 458");
                        return; 
                    }
                    if (last == true)
                    {
                        MessageBox.Show(".הפריט קודם לסטטוס הבא", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                        refreashandclear();
                        return;
                    }

                    MessageBox.Show(".הפריט קודם לשלב הבא", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information); 
                    refreashandclear();
   
                } // end of else // if the user clicked on "Yes" so he wants to Delete.

            }
            catch 
            {
                MessageBox.Show(".לא נבחר פריט לקדם", "שים לב", MessageBoxButton.OK, MessageBoxImage.Error); 
                refreashandclear(); 
                return;
            }

        }

        private void PrevStage_button_Click(object sender, RoutedEventArgs e)
        {

            string prev_stage = "";
            string status = "";
            string prev_status = "";
            string itemToFixStageOrder = "";
            string workLast = "";
            bool first = false;
            string Query1 = "";
            try
            {
                DataRowView row = (DataRowView)dataGrid1.SelectedItems[0];
                if (MessageBox.Show("?האם אתה בטוח שברצונך להחזיר פריט זה", "וידוא קידום", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    refreashandclear();
                    return; //do no stuff
                }
                else // if the user clicked on "Yes" so he wants to Delete.
                {

                    string curr = row["מספר השלב הנוכחי"].ToString();
                    string itemnum = row["מספר פריט בסט"].ToString();


                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        Query1 = "select itemStatus , itemToFixStageOrder from jobs where itemid='" + itemID + "' and   jobid='" + jobID + "' and itemNum= '" + itemnum + "'     ";
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        MySqlDataReader dr = MSQLcrcommand1.ExecuteReader();

                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                status = dr.GetString(0);
                                itemToFixStageOrder = dr.GetString(1);
                            }

                        }

                        MySqlConn.Close();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("נפל בשורה מספר 404");
                        MessageBox.Show(ex.Message);
                    }


                    if (status == "גמר ייצור")
                    {// get the last stage of Work status.
                        try
                        {
                            MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                            MySqlConn.Open();
                            Query1 = "select MAX(itemStageOrder) from item where itemid='" + itemID + "' and itemStatus= 'בעבודה' ";
                            MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                            MSQLcrcommand1.ExecuteNonQuery();
                            MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                            MySqlDataReader dr = MSQLcrcommand1.ExecuteReader();

                            while (dr.Read())
                            {
                                if (!dr.IsDBNull(0))
                                {
                                    workLast = dr.GetString(0);
                                }

                            }

                            MySqlConn.Close();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("נפל בשורה מספר 669");
                            MessageBox.Show(ex.Message);
                        }
                    }

                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        Query1 = "select MAX(itemStageOrder) from item where itemid='" + itemID + "' and   itemStageOrder<'" + curr + "' and itemStatus= '" + status + "'     ";
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        MySqlDataReader dr = MSQLcrcommand1.ExecuteReader();
                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                prev_stage = dr.GetString(0);
                            }
                            else
                            {
                                first = true;
                                if (status == "רישום")
                                {
                                    MessageBox.Show(".הפריט כבר נמצא בשלב הראשון הקיים", "שים לב", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    MySqlConn.Close();
                                    refreashandclear();
                                    return;
                                }

                                if (status == "בעבודה")
                                {
                                    prev_status = "רישום";
                                    prev_stage = "2";

                                }

                                if (status == "גמר ייצור")
                                {
                                        prev_status = "בעבודה";
                                        prev_stage = workLast;

                                }

                                if (status == "תיקון")
                                {
                                    if (itemToFixStageOrder != "0")
                                    {
                                        prev_status = "בעבודה";
                                        prev_stage = itemToFixStageOrder;
                                    }
                                    if (itemToFixStageOrder == "0")
                                    {
                                        prev_status = "גמר ייצור";
                                        prev_stage = itemToFixStageOrder ;
                                    }
                                }

                                if (status == "הסתיים")
                                {
                                    prev_status = "גמר ייצור";
                                    prev_stage = "1";
                                    
                                }

                                if (status == "פסול")
                                {
                                    MessageBox.Show(".עבור פריט בסטטוס פסול עליך לשנות סטטוס ידנית", "שים לב", MessageBoxButton.OK, MessageBoxImage.Error);
                                    MySqlConn.Close();
                                    refreashandclear();
                                    return;

                                }

                            } // end else of if (!dr.IsDBNull(0))
                        } // end while (dr.Read())

                        MySqlConn.Close();

                    } // end try
                    catch (Exception ex)
                    {
                        Console.WriteLine("נפל בשורה מספר 744");
                        MessageBox.Show(ex.Message);
                    }

                    if (first == true)
                    {
                        Query1 = "update jobs set itemStatus='" + prev_status + "' , itemStageOrder='" + prev_stage + "' where jobid='" + jobID + "' and itemid='" + itemID + "'and itemNum='" + itemnum + "'";
                    }
                    else
                    {
                        Query1 = "update jobs set itemStageOrder='" + prev_stage + "' where jobid='" + jobID + "' and itemid='" + itemID + "'and itemNum='" + itemnum + "'";
                    }
                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        //string Query1 = "update jobs set itemStageOrder='" + next_stage + "' where jobid='" + jobID + "' and itemid='" + itemID + "'and itemNum='" + itemnum + "'";
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        MySqlConn.Close();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        Console.WriteLine("נפל בשורה מספר 458");
                        return;
                    }
                    if (first == true)
                    {
                        MessageBox.Show(".הפריט הוחזר לסטטוס הקודם", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                        refreashandclear();
                        return;
                    }

                    MessageBox.Show(".הפריט הוחזר לשלב הקודם", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                    refreashandclear();

                } // end of else // if the user clicked on "Yes" so he wants to Delete.

            }
            catch
            {
                MessageBox.Show(".לא נבחר פריט להחזרה", "שים לב", MessageBoxButton.OK, MessageBoxImage.Error);
                refreashandclear();
                return;
            }

            /*
            string prev_stage = "";
            string status = "";
            try
            {
                DataRowView row = (DataRowView)dataGrid1.SelectedItems[0];
                if (MessageBox.Show("?האם אתה בטוח שברצונך לחזור שלב  אחורה בפריט זה", "וידוא חזרת שלב", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return; //do no stuff
                }
                else // if the user clicked on "Yes" so he wants to Delete.
                {
                    // this will give us the first colum of the selected row in the DataGrid.

                    // string status = row["סטטוס הפריט"].ToString();
                    string curr = row["מספר השלב הנוכחי"].ToString();
                    string itemnum = row["מספר פריט בסט"].ToString();
                    // MessageBox.Show("" + status + "");

                    try
                    {
                        MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                        MySqlConn.Open();
                        string Query1 = "select itemStatus from jobs where itemid='" + itemID + "' and   jobid='" + jobID + "' and itemNum= '" + itemnum + "'     ";
                        MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                        MSQLcrcommand1.ExecuteNonQuery();
                        MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                        MySqlDataReader dr = MSQLcrcommand1.ExecuteReader();

                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                            {
                                status = dr.GetString(0);
                            }

                        }

                        MySqlConn.Close();
                        // MessageBox.Show("!הלקוח נמחק מהמערכת");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("נפסל בשורה מספר 542");
                        MessageBox.Show(ex.Message);
                    }


                        try
                        {
                            MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                            MySqlConn.Open();
                            string Query1 = "select MAX(itemStageOrder) from item where itemid='" + itemID + "' and   itemStageOrder<'" + curr + "' and itemStatus= '" + status + "'     ";
                            MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                            MSQLcrcommand1.ExecuteNonQuery();
                            MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);


                            MySqlDataReader dr = MSQLcrcommand1.ExecuteReader();

                            while (dr.Read())
                            {
                                if (!dr.IsDBNull(0))
                                {
                                    prev_stage = dr.GetString(0);
                                }
                                else { MessageBox.Show("הפריט כבר נמצא בשלב הראשוני ביותר"); return; }
                            }

                            MySqlConn.Close();
                            // MessageBox.Show("!הלקוח נמחק מהמערכת");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("נפסל בשורה מספר 578");
                            MessageBox.Show(ex.Message);
                        }




                        try
                        {
                            MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                            MySqlConn.Open();
                            string Query1 = "update jobs set itemStageOrder='" + prev_stage + "' where jobid='" + jobID + "' and itemid='" + itemID + "'and itemNum='" + itemnum + "'";
                            MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                            MSQLcrcommand1.ExecuteNonQuery();
                            MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                            MySqlConn.Close();


                        }
                        catch (Exception ex) 
                        { 
                            MessageBox.Show(ex.Message);
                            Console.WriteLine("נפסל בשורה מספר 600");
                            return; 
                        }



                        try
                        {
                            MySqlConnection MySqlConn = new MySqlConnection(Login.Connectionstring);
                            MySqlConn.Open();
                            string Query1 = ("SELECT jobs.itemNum as `מספר פריט בסט`,jobs.itemStatus  as `סטטוס הפריט`, jobs.itemStageOrder as `מספר השלב הנוכחי`,stageName  as `שם השלב הנוכחי` ,item.stage_discription as `תאור השלב הנוכחי`,itemToFixStageOrder as `מספר השלב שבו זוהה כתקול (אם זוהה)`  FROM jobs,item WHERE jobs.itemid=item.itemid and jobs.itemStageOrder=item.itemStageOrder and jobs.itemStatus=item.itemStatus and jobs.jobid='" + jobID + "' and jobs.itemid='" + itemID + "' ");
                            MySqlCommand MSQLcrcommand1 = new MySqlCommand(Query1, MySqlConn);
                            MSQLcrcommand1.ExecuteNonQuery();
                            MySqlDataAdapter mysqlDAdp = new MySqlDataAdapter(MSQLcrcommand1);
                            dt.Clear();
                            mysqlDAdp.Fill(dt);
                            dataGrid1.ItemsSource = dt.DefaultView;
                            mysqlDAdp.Update(dt);
                            MySqlConn.Close();
                            MessageBox.Show("הפריט הוחזר לשלב הקודם");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("נפסל בשורה מספר 623");
                            MessageBox.Show(ex.Message);
                        }


                  //  }// end of if בעבודה
                }



            }
            catch { MessageBox.Show("לא נבחר פריט להחזרת שלב"); return; }


            */



        }

        private void exit_clicked(object sender, CancelEventArgs e)
        {
            Console.WriteLine("" + Login.close);

            if (Login.close == 0) // then the user want to exit.
            {
                if (MessageBox.Show("?האם אתה בטוח שברצונך לצאת מהמערכת ", "וידוא יציאה", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
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
