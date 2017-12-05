using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TcpServer
{
    public partial class MainWindow : Window
    {
        private Server server;

        public MainWindow()
        {
            server = new Server("127.0.0.1", 1111);
            server.OnDataReceived += printRecievedDataToTxb;
            server.OnServerStarted += printServerCtrlMessageToTxb;
            server.OnServerStopped += printServerCtrlMessageToTxb;
            server.OnListeningForClient += printServerCtrlMessageToTxb;
            server.OnClientAccepted += printServerCtrlMessageToTxb;
            
            InitializeComponent();
        }

        #region PRINT EVENTS
        private void printRecievedDataToTxb(object sender, ServerEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                txb_receivedData.Text += e.Message + "\n";
            });
            
        }

        private void printServerCtrlMessageToTxb(object sender, ServerEventArgs e)
        {
            txb_receivedData.Text += e.Message + "\n";
        }
        #endregion

        #region BUTTON EVENTS
        private void btn_startServer_Click(object sender, EventArgs e)
        {
            server.startServer();
            show_btn_acceptClient();
            hide_btn_startServer();
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            server.closeConnection();
            show_btn_startServer();
            hide_btn_disconnect();
        }

        private void btn_acceptClient_Click(object sender, EventArgs e)
        {
            
            show_btn_disconnect();
            hide_btn_acceptClient();
            server.acceptClient().ContinueWith(x => {
                server.receiveSessionData();
            });
        }
        #endregion

        #region SHOW/HIDE BUTTONS

        private void show_btn_disconnect()
        {
            btn_disconnect.Visibility = Visibility.Visible;
            btn_disconnect.IsEnabled = true;
        }

        private void hide_btn_disconnect()
        {
            btn_disconnect.Visibility = Visibility.Hidden;
            btn_disconnect.IsEnabled = false;
        }

        private void show_btn_acceptClient()
        {
            btn_acceptClient.Visibility = Visibility.Visible;
            btn_acceptClient.IsEnabled = true;
        }

        private void hide_btn_acceptClient()
        {
            btn_acceptClient.Visibility = Visibility.Hidden;
            btn_acceptClient.IsEnabled = false;
        }

        private void show_btn_startServer()
        {
            btn_startServer.Visibility = Visibility.Visible;
            btn_startServer.IsEnabled = true;
        }

        private void hide_btn_startServer()
        {
            btn_startServer.Visibility = Visibility.Hidden;
            btn_startServer.IsEnabled = false;
        }
        #endregion
    }
}
