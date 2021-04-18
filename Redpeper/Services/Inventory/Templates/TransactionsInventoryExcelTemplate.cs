using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Redpeper.Dto;

namespace Redpeper.Services.Inventory.Templates
{
    public class TransactionsInventoryExcelTemplate
    {
        public Byte[] GenerateExcelReport(List<InventoryTransactionDto> data, DateTime date)
        {
            byte[] fileContents;
            using (var package = new ExcelPackage())
            {
                var dataGruped = data.GroupBy(x => x.Supply).Select(y => new InventoryDto
                {
                    Supply = y.First().Supply,
                    Qty = y.Sum(z => z.Qty)
                }).OrderBy(x => x.Supply).ToList();

                var worksheet = package.Workbook.Worksheets.Add($"{date.Date:dd_MM_yyyy}");

                worksheet.Cells[1, 1, 1, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, 6].Merge = true;
                worksheet.Cells[1, 1, 1, 6].Value = $"Detalle de transacciones {date.Date:dd/MM/yyyy}";
                worksheet.Cells[1, 1, 1, 6].Style.Font.Bold = true;


                worksheet.Cells[1, 7, 1, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 7, 1, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 7, 1, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 7, 1, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 7, 1, 9].Merge = true;
                worksheet.Cells[1, 7, 1, 9].Value = $"Consolidado de Insumos {date.Date:dd/MM/yyyy}";
                worksheet.Cells[1, 7, 1, 9].Style.Font.Bold = true;


                worksheet.Cells[2, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1].Value = "Fecha";

                worksheet.Cells[2, 1].Style.Font.Bold = true;

                worksheet.Cells[2, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 2].Value = "Tipo de Transaccion";
                worksheet.Cells[2, 2].Style.Font.Bold = true;

                worksheet.Cells[2, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 3].Value = "Numero de Transaccion";
                worksheet.Cells[2, 3].Style.Font.Bold = true;

                worksheet.Cells[2, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 4].Value = "Insumo";
                worksheet.Cells[2, 4].Style.Font.Bold = true;

                worksheet.Cells[2, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 5].Value = "Cantidad";
                worksheet.Cells[2, 5].Style.Font.Bold = true;
                
                worksheet.Cells[2, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 6].Value = "Justificacion";
                worksheet.Cells[2, 6].Style.Font.Bold = true;

                worksheet.Cells[2, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 8].Value = "Insumo";
                worksheet.Cells[2, 8].Style.Font.Bold = true;


                worksheet.Cells[2, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[2, 9].Value = "Total";
                worksheet.Cells[2, 9].Style.Font.Bold = true;


                worksheet.Row(1).Height = 35;
                worksheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Row(2).Height = 35;
                worksheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Row(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                worksheet.Row(1).Height = 35;
                worksheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Row(2).Height = 35;
                worksheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Row(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                worksheet.Column(1).Width = 22;
                worksheet.Column(2).Width = 22;
                worksheet.Column(3).Width = 25;
                worksheet.Column(4).Width = 40;
                worksheet.Column(5).Width = 22;
                worksheet.Column(6).Width = 30;

                worksheet.Column(7).Width = 20;

                worksheet.Column(8).Width = 40;
                worksheet.Column(9).Width = 30;


                var inventoryTransactionDtos = data.OrderBy(x => x.Supply).ToList();
                for (int i = 0; i < inventoryTransactionDtos.Count; i++)
                {
                    worksheet.Row(i + 3).Height = 25;
                    worksheet.Row(i + 3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Row(i + 3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[i + 3, 1, i + 3, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 1, i + 3, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


                    worksheet.Cells[i + 3, 1].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[i + 3, 1].Value = inventoryTransactionDtos[i].Date;
                    worksheet.Cells[i + 3, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[i + 3, 2].Value = inventoryTransactionDtos[i].TransactionType;
                    worksheet.Cells[i + 3, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[i + 3, 3].Value = inventoryTransactionDtos[i].TransationNumber;
                    worksheet.Cells[i + 3, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[i + 3, 4].Value = inventoryTransactionDtos[i].Supply;
                    worksheet.Cells[i + 3, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                    worksheet.Cells[i + 3, 5].Value = inventoryTransactionDtos[i].Qty;
                    worksheet.Cells[i + 3, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[i + 3, 6].Value = inventoryTransactionDtos[i].Comments;
                    worksheet.Cells[i + 3, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 6].Style.WrapText = true;
                }

                for (int i = 0; i < dataGruped.Count; i++)
                {
                    worksheet.Cells[i + 3, 8].Value = dataGruped[i].Supply;
                    worksheet.Cells[i + 3, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[i + 3, 9].Value = dataGruped[i].Qty;
                    worksheet.Cells[i + 3, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }

                fileContents = package.GetAsByteArray();
            }

            return fileContents;
        }
    }
}