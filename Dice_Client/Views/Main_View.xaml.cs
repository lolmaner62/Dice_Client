using Dice_Client.Classes;
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
            User_Label.Content = user;
        }
        
        private async void Generate(object sender, RoutedEventArgs e)
        {
            string id;
            id = await connection.GenerateSession(Username);
            if (id == null)
            {
                return;
            }
            else
            {
                ID_Label.Content = id;
            }
        }

        private async void Join(object sender, RoutedEventArgs e)
        {
            bool status = false;
            string id = ID_TextBox.Text;
            if (id != null && id.Length == 6)
            {
                 status = await connection.ConnectToSession(id, Username);
            }
            else
            {
                MessageBox.Show("ID must be 6 characters long!");
                return;
            }

            if (status)
            {
                MessageBox.Show("Connected to Session");

                ID_Label.Content = id;
                

            }
            else
            {
                MessageBox.Show("Invalid session ID or already in session");
            }

        }
        private void Window_Mousedown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();

            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
