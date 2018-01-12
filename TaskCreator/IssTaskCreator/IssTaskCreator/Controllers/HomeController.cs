using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IssTaskCreator.Models;
using IssTaskCreator.Repository;
using System;

namespace IssTaskCreator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("CreateTask");
        }


        [HttpGet]
        public ActionResult CreateTask()
        {
            ViewBag.Message = "Create Task";

            return View();
        }
        [HttpPost]
        public ActionResult CreateTaskPost()
        {
            var taskModel = new TaskModel();
            taskModel.TaskName = Request.Form["taskName"];
            taskModel.StressorOccurrence = float.Parse(Request.Form["stressorOccurrence"]) / 100;
            taskModel.Transformations = new List<Transformation>();
            for (var i = 0; i < 6; i++)
            {
                var ot = "OT_" + i;
                var rf = "RF_" + i;
                var x = "X_" + i;
                var y = "Y_" + i;
                var z = "Z_" + i;
                if (string.IsNullOrWhiteSpace(Request.Form[ot]))
                {
                    break;
                }
                var transformation = new Transformation()
                {
                    OccurenceTime = float.Parse(Request.Form[ot]),
                    rotationFactor = float.Parse(Request.Form[rf]),
                    xTranslation = float.Parse(Request.Form[x]),
                    yTranslation = float.Parse(Request.Form[y]),
                    zTranslation = float.Parse(Request.Form[z]),
                };
                taskModel.Transformations.Add(transformation);
            }
            var repo = new TaskRepository();
            var success = repo.Save(taskModel);


            return View("CreateTask");
        }
    }
}