using Microsoft.Win32;
using SimulationMonitor.CustomEventArgs;
using SimulationMonitor.FileControl;
using SimulationMonitor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SimulationMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //TODO: SimCtrl events
    public partial class MainWindow : Window
    {
        private SimulatorController simulatorController;

        private bool serverStarted = false;

        public ObservableCollection<SimulationTask> SimulationTasks { get; set; }

        public OpenFileDialog ofd_openConfigJSON;

        public MainWindow()
        {
            SimulationTasks = new ObservableCollection<SimulationTask>();

            InitializeComponent();
        }

        private void mi_openSimConfig_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "";

            ofd_openConfigJSON = new OpenFileDialog();
            ofd_openConfigJSON.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            ofd_openConfigJSON.Multiselect = false;

            if (ofd_openConfigJSON.ShowDialog() == true)
            {
                filePath = ofd_openConfigJSON.FileName;

                SimulationJSONReader reader = new SimulationJSONReader(filePath);

                SimulationTasks = new ObservableCollection<SimulationTask>(reader.getTasks());

                lst_tasks.ItemsSource = SimulationTasks;
            }
        }

        private void mi_saveSimResults_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mi_quit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        
        private void btn_sendTask_Click(object sender, RoutedEventArgs e)
        {
            if(lst_tasks.SelectedItem != null)
            {
                simulatorController.send_TASK(((SimulationTask)lst_tasks.SelectedItem).TaskJSON);
            }
        }

        private void btn_startStopServer_Click(object sender, RoutedEventArgs e)
        {
            if (!serverStarted)
            {
                simulatorController = new SimulatorController("127.0.0.1", 1111);
                serverStarted = true;
                btn_startStopServer.Content = "Stop Server";
                

                simulatorController.OnServerStarted += SimulatorController_OnServerStarted;
                simulatorController.OnServerStopped += SimulatorController_OnServerStopped;
                simulatorController.OnListeningForSimulator += SimulatorController_OnListeningForSimulator;
                simulatorController.OnSimulatorAccepted += SimulatorController_OnSimulatorAccepted;
                simulatorController.OnDataReceived += SimulatorController_OnDataReceived;

                simulatorController.initiateServer();
            }
            else
            {
                serverStarted = false;
                btn_startStopServer.Content = "Start Server";
                disableSimulatorControls();
                simulatorController.Dispose();
            }
        }

        private void SimulatorController_OnDataReceived(object sender, SimulatorEventArgs e)
        {
            txb_receivedData.Text += simulatorController.getMessage() + "\n";
        }

        private void SimulatorController_OnSimulatorAccepted(object sender, SimulatorEventArgs e)
        {
            txb_receivedData.Text += e.Message + "\n";
            enableSimulatorControls();
        }

        private void SimulatorController_OnListeningForSimulator(object sender, SimulatorEventArgs e)
        {
            txb_receivedData.Text += e.Message + "\n";
        }

        private void SimulatorController_OnServerStopped(object sender, SimulatorEventArgs e)
        {
            txb_receivedData.Text += e.Message + "\n";
        }

        private void SimulatorController_OnServerStarted(object sender, SimulatorEventArgs e)
        {
            txb_receivedData.Text += e.Message + "\n";
        }

        //display JSON in TextBlock
        private void lst_tasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lst_tasks.SelectedItem != null)
            {
                txb_selectedTaskDetails.Text = ((SimulationTask)lst_tasks.SelectedItem).TaskJSONBeautified;
            }
        }

        private void btn_sendStart_Click(object sender, RoutedEventArgs e)
        {
            simulatorController.send_START();
        }

        private void btn_sendHalt_Click(object sender, RoutedEventArgs e)
        {
            simulatorController.send_HALT();
        }

        private void enableSimulatorControls()
        {
            btn_sendTask.IsEnabled = true;
            btn_sendStart.IsEnabled = true;
            btn_sendHalt.IsEnabled = true;
        }

        private void disableSimulatorControls()
        {
            btn_sendTask.IsEnabled = false;
            btn_sendStart.IsEnabled = false;
            btn_sendHalt.IsEnabled = false;
        }
    }
}
