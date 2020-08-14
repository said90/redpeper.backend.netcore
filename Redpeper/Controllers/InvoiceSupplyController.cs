using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Newtonsoft.Json;
using Redpeper.Collection;
using Redpeper.Dto;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Repositories.InvoiceSupply;
using Redpeper.Services.Inventory;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceSupplyController : ControllerBase
    {
      
        private readonly IInventoryService _inventoryService;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceSupplyController( IUnitOfWork unitOfWork,
             IInventoryService inventoryService)
        {
            _unitOfWork = unitOfWork;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplyInvoice>>> GetAll()
        {
            return await _unitOfWork.SupplyInvoiceRepository.GetAllIncludingDetails();
        }

        // [HttpGet]
        // public async Task<PagedList<SupplyInvoice>> GetPaginated(int page = 1, int size = 10, string sort = "")
        // {
        //     var result = await _supplyInvoiceRepository.GetPaginated(page, size, sort);
        //     var metadata = new
        //     {
        //         result.TotalCount,
        //         result.ItemPerPage,
        //         result.Page,
        //         result.TotalPages,
        //         result.HasMorePages,
        //         result.HasPrevPages,
        //         result.Sort
        //     };
        //     Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        //     return result;
        // }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<SupplyInvoice>> GetInvoiceById(int id)
        {
            return await _unitOfWork.SupplyInvoiceRepository.GetByIdIncludingDetails(id);
        }


        [HttpGet("[action]/{invoiceNumber}")]
        public async Task<ActionResult<SupplyInvoice>> GetInvoiceByNumber(string invoiceNumber)
        {
            return await _unitOfWork.SupplyInvoiceRepository.GetByInvoiceNumber(invoiceNumber);
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceSupplyDto>> Create(InvoiceSupplyDto invoice)
        {
            var inv = new SupplyInvoice
            {

                InvoiceNumber = invoice.InvoiceNumber,
                EmissionDate = invoice.EmissionDate,
                ProviderId = invoice.ProviderId,
                Total = invoice.Total,
                Iva = invoice.Iva
            };
            await _unitOfWork.SupplyInvoiceRepository.InsertTask(inv);
            await _unitOfWork.Commit();

            var invoiceNew = await _unitOfWork.SupplyInvoiceRepository.GetMaxInvoice();

            var details = invoice.Details.Select(x => new SupplyInvoiceDetail
            {
                SupplyInvoiceId = invoiceNew,
                SupplyId = x.SupplyId,
                ExpirationDate = x.ExpirationDate,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                Total = x.Total
            }).ToList();

            await _unitOfWork.SupplyInvoiceDetailRepository.InsertRangeTask(details);
            await _unitOfWork.Commit();
            await _inventoryService.AddInvoiceDetailToInventory(invoice);
            return invoice;
        }
    }
}