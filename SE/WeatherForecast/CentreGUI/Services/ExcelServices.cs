using CentreGUI.Models;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentreGUI.Services
{
    public class ExcelServices
    {
        public static async void CreateFile(IEnumerable<Region> regions)
        {
            await Task.Yield();
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            DateTime currentDate = DateTime.Now;

            ws.Cells["1", "A"].Value = DateTime.Now;

            ws.Cells["2", "A"].Value = "Mã Trạm";
            ws.Cells["2", "B"].Value = "Tên Trạm";
            ws.Cells["2", "C"].Value = "Nhiệt Độ";
            ws.Cells["2", "D"].Value = "Độ Ẩm";
            ws.Cells["2", "E"].Value = "Áp Xuất";

            int i = 3;
            foreach (var r in regions ?? new List<Region>())
            {
                ws.Cells[i.ToString(), "A"].Value = r.RegionId;
                ws.Cells[i.ToString(), "B"].Value = r.Name;
                ws.Cells[i.ToString(), "C"].Value = double.IsNaN(r.Temperature) ? "NaN" : r.Temperature.ToString();
                ws.Cells[i.ToString(), "D"].Value = double.IsNaN(r.Humidity) ? "NaN" : r.Humidity.ToString();
                ws.Cells[i.ToString(), "E"].Value = double.IsNaN(r.Barometer) ? "NaN" : r.Barometer.ToString();
                i++;
            }
        }
    }
}