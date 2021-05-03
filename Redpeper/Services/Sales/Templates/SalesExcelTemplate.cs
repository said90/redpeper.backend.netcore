using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Redpeper.Dto;

namespace Redpeper.Services.Sales.Templates
{
    public class SalesExcelTemplate
    {
        public Byte[] GenerateExcelReport(List<OrderReportDto> data, DateTime date)
        {
            byte[] fileContents;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add($"{date.Date:dd_MM_yy}");

                worksheet.Cells[1, 1, 1, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1].Value = "Fecha";
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 2].Value = "Tipo de Orden";
                worksheet.Cells[1, 2].Style.Font.Bold = true;
                worksheet.Cells[1, 3].Value = "Numero de Orden";
                worksheet.Cells[1, 3].Style.Font.Bold = true;
                worksheet.Cells[1, 4].Value = "Cliente";
                worksheet.Cells[1, 4].Style.Font.Bold = true;
                worksheet.Cells[1, 5].Value = "Propina";
                worksheet.Cells[1, 5].Style.Font.Bold = true;
                worksheet.Cells[1, 6].Value = "Total";
                worksheet.Cells[1, 6].Style.Font.Bold = true;

                worksheet.Cells[2, 1, 2, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, 2, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, 2, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, 2, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Row(1).Height = 35;
                worksheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                worksheet.Column(1).Width = 25;
                worksheet.Column(2).Width = 30;
                worksheet.Column(3).Width = 30;
                worksheet.Column(4).Width = 30;
                worksheet.Column(5).Width = 30;
                worksheet.Column(6).Width = 30;
                worksheet.View.FreezePanes(2, 1);


                for (int i = 0; i < data.Count; i++)
                {
                    worksheet.Row(i + 2).Height = 30;
                    worksheet.Row(i + 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Row(i + 2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[i + 2, 1, i + 2, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 2, 1, i + 2, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 2, 1, i + 2, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 2, 1, i + 2, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 2, 1].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[i + 2, 1].Value = data[i].Date;
                    worksheet.Cells[i + 2, 2].Value = data[i].OrderType;
                    worksheet.Cells[i + 2, 3].Value = data[i].OrderNumber;
                    worksheet.Cells[i + 2, 4].Value = data[i].Customer;
                    worksheet.Cells[i + 2, 5].Style.Numberformat.Format = "$###,###,##0.00";
                    worksheet.Cells[i + 2, 5].Value = data[i].Tip ? data[i].Total * (decimal)0.10 : 0;
                    worksheet.Cells[i + 2, 6].Style.Numberformat.Format = "$###,###,##0.00";
                    worksheet.Cells[i + 2, 6].Value = data[i].Total;
                }
                worksheet.Cells[data.Count + 2, 1, data.Count + 2, 4].Merge = true;
                worksheet.Cells[data.Count + 2, 1, data.Count + 2, 5].Style.Border.Top.Style = ExcelBorderStyle.Double;
                worksheet.Cells[data.Count + 2, 1, data.Count + 2, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                worksheet.Cells[data.Count + 2, 1, data.Count + 2, 5].Style.Border.Left.Style = ExcelBorderStyle.Double;
                worksheet.Cells[data.Count + 2, 1, data.Count + 2, 5].Style.Border.Right.Style = ExcelBorderStyle.Double;

                worksheet.Cells[data.Count + 2, 1, data.Count + 2, 5].Style.Numberformat.Format = "$###,###,##0.00";
                worksheet.Cells[data.Count + 2, 1, data.Count + 2, 5].Value = data.Sum(x => x.Total * (decimal)0.10);
                worksheet.Cells[data.Count + 2, 1, data.Count + 2, 5].Style.Font.Bold = true;
                worksheet.Cells[data.Count + 2, 1, data.Count + 2, 5].Style.Font.Size = 16;
                worksheet.Cells[data.Count + 2, 1, data.Count + 2, 4].Value = "Total";

                worksheet.Cells[data.Count + 2, 6].Style.Border.Top.Style = ExcelBorderStyle.Double;
                worksheet.Cells[data.Count + 2, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                worksheet.Cells[data.Count + 2, 6].Style.Border.Left.Style = ExcelBorderStyle.Double;
                worksheet.Cells[data.Count + 2, 6].Style.Border.Right.Style = ExcelBorderStyle.Double;
                worksheet.Cells[data.Count + 2, 6].Style.Font.Bold = true;

                worksheet.Row(data.Count + 2).Height = 35;
                worksheet.Row(data.Count + 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Row(data.Count + 2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[data.Count + 2, 6].Style.Font.Size = 16;
                worksheet.Cells[data.Count + 3, 5, data.Count + 3, 6].Merge = true;

                worksheet.Cells[data.Count + 2, 6].Style.Numberformat.Format = "$###,###,##0.00";
                worksheet.Cells[data.Count + 2, 6].Value = data.Sum(x => x.Total);

                worksheet.Cells[data.Count + 3, 5, data.Count + 3, 6].Style.Font.Size = 16;

                worksheet.Cells[data.Count + 3, 5, data.Count + 3, 6].Style.Numberformat.Format = "$###,###,##0.00";
                worksheet.Cells[data.Count + 3, 5, data.Count + 3, 6].Style.Font.Bold = true;

                worksheet.Cells[data.Count + 3, 5, data.Count + 3, 6].Value = data.Sum(x => x.Total) + data.Sum(x => x.Total * (decimal)0.10);
                worksheet.Cells[data.Count + 3, 5, data.Count + 3, 6].Style.Border.Top.Style = ExcelBorderStyle.Double;
                worksheet.Cells[data.Count + 3, 5, data.Count + 3, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                worksheet.Cells[data.Count + 3, 5, data.Count + 3, 6].Style.Border.Left.Style = ExcelBorderStyle.Double;
                worksheet.Cells[data.Count + 3, 5, data.Count + 3, 6].Style.Border.Right.Style = ExcelBorderStyle.Double;
                worksheet.Row(data.Count + 3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Row(data.Count + 3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                fileContents = package.GetAsByteArray();
            }

            return fileContents;
        }
    }
}