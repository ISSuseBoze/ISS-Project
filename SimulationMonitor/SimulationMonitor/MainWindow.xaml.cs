using SimulationMonitor.CustomEventArgs;
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

        public MainWindow()
        {
            SimulationTasks = new ObservableCollection<SimulationTask>();
            SimulationTasks.Add(new SimulationTask(1, "test1"));
            SimulationTasks.Add(new SimulationTask(2, "test2"));
            SimulationTasks.Add(new SimulationTask(3, "test3"));

            InitializeComponent();
        }

        private void mi_openSimConfig_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mi_saveSimResults_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mi_quit_Click(object sender, RoutedEventArgs e)
        {

        }

        //TODO: send selected task JSON view SimCtrl 
        private void btn_sendTask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_startStopServer_Click(object sender, RoutedEventArgs e)
        {
            if (!serverStarted)
            {
                simulatorController = new SimulatorController("127.0.0.1", 1111);
                serverStarted = true;
                btn_startStopServer.Content = "Stop Server";
                btn_sendTask.IsEnabled = true;

                simulatorController.OnServerStarted += eventHandle;
                simulatorController.OnServerStopped += eventHandle;
                simulatorController.OnListeningForSimulator += eventHandle;
                simulatorController.OnSimulatorAccepted += eventHandle;
                simulatorController.OnDataReceived += eventHandle;

                simulatorController.initiateServer();
            }
            else
            {
                serverStarted = false;
                btn_startStopServer.Content = "Start Server";
                btn_sendTask.IsEnabled = false;
                simulatorController.Dispose();
            }
        }

        //display JSON in TextBlock
        private void lst_tasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txb_selectedTaskDetails.Text = ((SimulationTask)lst_tasks.SelectedItem).TaskJSON;
        }

        private void eventHandle(object sender, SimulatorEventArgs e)
        {

        }
    }
}
