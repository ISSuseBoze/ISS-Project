using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationMonitor.Model
{
    public class TimeCycleResult
    {
        public int TimeCycleNumber { get; set; }
        public int NumberOfControlChanges { get; set; }
        public int NumberOfStressors { get; set; }

        public TimeCycleResult(int timeCycleNumber, int numberOfControlChanges, int numberOfStressors)
        {
            this.TimeCycleNumber = timeCycleNumber;
            this.NumberOfControlChanges = numberOfControlChanges;
            this.NumberOfStressors = numberOfStressors;
        }
    }
}
