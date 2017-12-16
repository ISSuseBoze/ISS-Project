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

namespace TcpTestClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Client client;

		public MainWindow()
		{
			client = new Client("127.0.0.1", 1111);

			InitializeComponent();
		}

		private void btn_send_Click(object sender, RoutedEventArgs e)
		{
			string message = tbx_message.Text;

			client.sendMessage(message);
		}
	}
}
