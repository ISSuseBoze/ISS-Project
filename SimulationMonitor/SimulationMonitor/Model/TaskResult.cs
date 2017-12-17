using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimulationMonitor.Model
{
    public class TaskResult
    {
        public int TaskDuration { get; set; } //in seconds
        public List<TimeCycleResult> TimeCycleResults { get; set; }

        public string ShortDescription
        {
            get
            {
                return "Task runtime: " + TaskDuration.ToString();
            }
        }

        //TODO: add exception
        public TaskResult(string taskText)
        {
            string[] taskSplit = Regex.Split(taskText, "TIME ");//0-cycle data; 1-task duration
            TaskDuration = Convert.ToInt32(taskSplit[1]);

            TimeCycleResults = new List<TimeCycleResult>();
            foreach(string line in taskSplit[0].Split(':'))//only temp should be \n
            {
                if (string.IsNullOrEmpty(line))
                    break;
                string[] linesplit = line.Split(',');

                int cycleNumber = Convert.ToInt32(linesplit[0]);
                int numberOfControlChanges = Convert.ToInt32(linesplit[1]);
                int numberOfStressors = Convert.ToInt32(linesplit[2]);

                TimeCycleResults.Add(new TimeCycleResult(cycleNumber, numberOfControlChanges, numberOfStressors));
            }

            
        }

        public TaskResult(int taskDuration, List<TimeCycleResult> timeCycleResults)
        {
            this.TaskDuration = taskDuration;
            this.TimeCycleResults = timeCycleResults;
        }
    }
}
