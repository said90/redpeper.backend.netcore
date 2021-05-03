using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Redpeper.Dto;

namespace Redpeper.Services.Inventory.Templates
{
    public class InventoryExcelTemplate
    {
        public Byte[] GenerateExcelReport(List<InventoryDto> data)
        {
            byte[] fileContents;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Inventario Actual");

                worksheet.Cells[1, 1, 1, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1].Value = "Insumo";
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 2].Value = "Cantidad";
                worksheet.Cells[1, 2].Style.Font.Bold = true;
                worksheet.Cells[2, 1,2,2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, 2, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Row(1).Height = 25;
                worksheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Column(1).Width = 45;
                worksheet.Column(2).Width = 22;
                worksheet.View.FreezePanes(2, 1);


                for (int i = 0; i < data.Count; i++)
                {
                    worksheet.Row(i + 2).Height = 20;
                    worksheet.Cells[i + 2, 1, i + 2, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 2, 1, i + 2, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 2, 1, i + 2, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 2, 1, i + 2, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 2, 1].Value = data[i].Supply;
                    worksheet.Cells[i + 2, 2].Value = data[i].Qty;
                }

                worksheet.Cells.AutoFitColumns();
                fileContents = package.GetAsByteArray();
            }

            return fileContents;
        }
    }
}