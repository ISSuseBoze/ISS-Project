using Newtonsoft.Json;
using SimulationMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;

namespace SimulationMonitor.FileControl
{
    public class SimulationJSONReader
    {
        private string filePath;

        public SimulationJSONReader(string filePath)
        {
            this.filePath = filePath;
        }

        public List<SimulationTask> getTasks()
        {
            List<SimulationTask> tasks = new List<SimulationTask>();
            string fileText = System.IO.File.ReadAllText(filePath);

           
            var js = new JavaScriptSerializer();
            var d = js.Deserialize<dynamic>(fileText);
            
            

            int taskNum = 1;
            foreach(object task in d["tasks"])
            {
                tasks.Add(new SimulationTask(taskNum, JsonConvert.SerializeObject(task, Formatting.Indented)));
                taskNum++;
            }

            return tasks;
        }
    }
}
