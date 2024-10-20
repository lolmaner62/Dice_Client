using Dice_Client.Classes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dice_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServerConnection connection;
        bool status;
        public MainWindow()
        {
            InitializeComponent();
            connection = ServerConnection.GetInstance();
            TestCon();

        }
        private async void TestCon()
        {
              
            if (await connection.Test())
            {
                status_label.Content = "OK";
                status = true;

            }
            else
            {
                status = false;
                status_label.Foreground = new SolidColorBrush(Colors.Red);
                status_label.Content = "NONE";
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
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUser.Text;
            string pass = txtPass.Password;
            if(!status)
            {
                MessageBox.Show($"No connection with server");
                return;
            }
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Username or password mustn't be empty");
                return;
            }
            bool isValid = await connection.LoginAsync(user, pass);
            if (isValid)
            {
                Main_View view = new Main_View(user);
                view.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials");
                txtPass.Clear();
                txtUser.Clear();
                txtUser.Focus();
            }

        }
    }
}