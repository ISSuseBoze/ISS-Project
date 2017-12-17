using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using SimulationMonitor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimulationMonitor.FileControl
{
    public class ExcelWriter
    {
        private string filePath;


        private XLWorkbook workbook;

        public ExcelWriter(string filePath)
        {
            this.filePath = filePath;
        }


        public void writeTaskResults(TaskResult taskResult)
        {

            string tempPath = System.IO.Path.GetTempFileName();
            tempPath = tempPath.Replace(".tmp", ".xlsx");

            System.IO.File.WriteAllBytes(tempPath, Properties.Resources.Task_Result_template);

            workbook = new XLWorkbook(tempPath);
            
            
            IXLWorksheet worksheet = workbook.Worksheets.First();

            IXLTable table = worksheet.Cell(1, 1).InsertTable<TimeCycleResult>(taskResult.TimeCycleResults);

            worksheet.Cell(1, 6).SetValue<int>(taskResult.TaskDuration);


            workbook.SaveAs(filePath);

            System.IO.File.Delete(tempPath);
        }
    }
}
