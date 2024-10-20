﻿using Dice_Client.Classes;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Dice_Client
{
    /// <summary>
    /// Interaction logic for Main_View.xaml
    /// </summary>
    public partial class Main_View : Window
    {
        string Username;
        ServerConnection connection;
        public Main_View(string user)
        {
            InitializeComponent();
            Username = user;
            connection = ServerConnection.GetInstance();
        }
    }
}
