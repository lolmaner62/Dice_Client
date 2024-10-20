using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Accessibility;
using Microsoft.AspNetCore.SignalR.Client;

namespace Dice_Client.Classes
{
    public class ServerConnection
    {
        private static ServerConnection _instance;

        private static readonly object _lock = new object();
        private HubConnection _connection;

        private  ServerConnection()
        {
            Connect();
        }
        public static ServerConnection GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ServerConnection();
                    }
                }
            }
            return _instance;
        }

        private async void Connect()
        {
            if (_connection == null)
            {
               
                try
                {
                    _connection = new HubConnectionBuilder()
                   .WithUrl("https://localhost:7215/gamehub")
                   .Build();
                    await _connection.StartAsync();
                }
                catch (Exception)
                {
                }
            }
        }
        public async Task<bool> Test()
        {
            bool output = false;
            try
            {
               
                if (await _connection.InvokeAsync<bool>("TestConnection"))
                {
                    output = true;
                }
            }
            catch (Exception)
            {
                output = false;
            }
            return output;
        }
        public async Task<bool> LoginAsync(string username, string password)
        {
            if (_connection != null && _connection.State == HubConnectionState.Connected)
            {
                try
                {
                    // Invoke the ValidateCredentials method on the server and return the result
                    bool isValid = await _connection.InvokeAsync<bool>("ValidateCredentials", username, password);
                    return isValid;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return false;
        }


        public void Disconnect()
        {
            
        }
    }

}
