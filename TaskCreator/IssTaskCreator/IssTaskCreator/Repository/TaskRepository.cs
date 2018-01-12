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

                List<TaskModel> repo;

                if (string.IsNullOrWhiteSpace(repoJson))
                {
                    repo = new List<TaskModel>();
                    repo.Add(task);
                }
                else
                {
                    repo = JsonConvert.DeserializeObject<List<TaskModel>>(repoJson);
                    repo.Add(task);
                }

                File.WriteAllText(fileName, JsonConvert.SerializeObject(repo, Formatting.Indented));
            }
            catch (Exception ex)
            {
                //Bude se handlalo sad je tu tek tolko, nekad zeza kad stvara file.
                throw;
            }
            return true;

        }
    }
}