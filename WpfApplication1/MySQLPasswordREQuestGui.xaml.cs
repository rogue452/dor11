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

namespace project
{
    /// <summary>
    /// Interaction logic for MySQLPasswordREQuestGui.xaml
    /// </summary>
    public partial class MySQLPasswordREQuestGui : Window
    {
        public MySQLPasswordREQuestGui()
        {
            InitializeComponent();
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            Login LI = new Login();
            LI.Show();
            this.Close();
        }

        private void Enter_button_Click(object sender, RoutedEventArgs e)
        {
            if (password.Password == "דב777בלום")
            {
                MySQLImpotrGui MSQLI = new MySQLImpotrGui();
                MSQLI.Show();
                this.Close();
            }
            else {
                MessageBox.Show("הסיסמה שהזנת לא נכונה"); 
                 }
        }
    }
}