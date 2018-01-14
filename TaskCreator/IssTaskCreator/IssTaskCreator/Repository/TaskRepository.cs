using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using IssTaskCreator.Models;
using Newtonsoft.Json;

namespace IssTaskCreator.Repository
{

    public class TaskRepository
    {
        private readonly string dirName = "C:\\ISS";
        private readonly string fileName = "C:\\ISS\\tasks.json";
        public bool Save(TaskModel task)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    Directory.CreateDirectory(dirName);
                    File.Create(fileName);
                }

                var repoJson = File.ReadAllText(fileName);

                ListContainter container = new ListContainter();

                if (string.IsNullOrWhiteSpace(repoJson))
                {
                    container.tasks = new List<TaskModel>();
                    container.tasks.Add(task);
                }
                else
                {
                    container = JsonConvert.DeserializeObject<ListContainter>(repoJson);
                    container.tasks.Add(task);

                }

                File.WriteAllText(fileName, JsonConvert.SerializeObject(container, Formatting.Indented));
            }
            catch (Exception ex)
            {
                //Bude se handlalo sad je tu tek tolko, nekad zeza kad stvara file.
                throw;
            }
            return true;

        }
    }

    class ListContainter
    {
        public List<TaskModel> tasks { get; set; }
    }
}