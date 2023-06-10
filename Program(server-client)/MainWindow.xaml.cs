using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace Program_server_client_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UdpClient udpClient = new UdpClient();
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("10.7.13.11"), 3344);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PushButton_Click(object sender, RoutedEventArgs e)
        {
            if(type.Text != null)
            {
                byte[] data = Encoding.UTF8.GetBytes(type.Text);
                udpClient.Send(data, data.Length, endPoint);
                IPEndPoint serverEndPoint = null;
                byte[] response = udpClient.Receive(ref serverEndPoint);
                string responseMessage = Encoding.UTF8.GetString(response);
                responseText.Text += responseMessage + " " + DateTime.Now.ToShortTimeString() + "\n";
            }
        }
    }
}
