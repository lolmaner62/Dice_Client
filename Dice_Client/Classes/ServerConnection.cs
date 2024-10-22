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
        private bool InSession = false;
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

        private async Task Connect()
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
        public async Task<string> GenerateSession(string user)
        {
            string output;
            if (!InSession)
            {
                try
                {
                    InSession = true;
                    output = await _connection.InvokeAsync<string>("GenerateSession", user);
                    return output;
                }
                catch (Exception ex)
                {
                    InSession = false;
                    MessageBox.Show($"Failed to generate session {ex.Message}");
                    return null;
                }
                
            }
            else
            {
                MessageBox.Show("Already in Session");
                return null;
            }
            
        }
        public async Task<bool> ConnectToSession(string id, string user)
        {
            if (!InSession)
            {
                bool output = await _connection.InvokeAsync<bool>("JoinSession", id, user);
                if (output)
                {
                    InSession = true;
                }
                return output;
            }
            else
            {
                return false;
            }
        }
        public void Disconnect()
        {
            
        }
    }

}
