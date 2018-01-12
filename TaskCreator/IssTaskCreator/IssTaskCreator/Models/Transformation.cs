using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssTaskCreator.Models
{
    public class Transformation
    {
        public float OccurenceTime { get; set; }
        public float xTranslation { get; set; }
        public float yTranslation { get; set; }
        public float zTranslation { get; set; }
        public float rotationFactor { get; set; }
    }
}