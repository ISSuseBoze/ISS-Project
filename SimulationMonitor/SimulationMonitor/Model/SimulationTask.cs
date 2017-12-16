using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationMonitor.Model
{
    public class SimulationTask
    {
        public int TaskNumber { get; set; }
        public string TaskJSON { get; set; }
        public string ShortName
        {
            get
            {
                return "Task " + TaskNumber.ToString();
            }

            private set
            {

            }
        }

        public SimulationTask(int taskNumber, string taskJSON)
        {
            this.TaskNumber = taskNumber;
            this.TaskJSON = taskJSON;
        }

        public string toString()
        {
            return "Task " + TaskNumber.ToString();
        }
    }
}
