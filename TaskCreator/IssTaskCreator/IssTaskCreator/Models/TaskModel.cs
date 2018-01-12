using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssTaskCreator.Models
{
    public class TaskModel
    {
        public string TaskName { get; set; }
        public float StressorOccurrence { get; set; }
        public List<Transformation> Transformations { get; set; }

    }
}