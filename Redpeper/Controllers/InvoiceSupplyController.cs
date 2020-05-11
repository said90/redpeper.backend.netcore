using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
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
        private readonly ISupplyInvoiceRepository _supplyInvoiceRepository;
        private readonly ISupplyInvoiceDetailRepository _supplyInvoiceDetailRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceSupplyController(ISupplyInvoiceRepository supplyInvoiceRepository, IUnitOfWork unitOfWork,
            ISupplyInvoiceDetailRepository supplyInvoiceDetailRepository, IInventoryService inventoryService)
        {
            _supplyInvoiceRepository = supplyInvoiceRepository;
            _unitOfWork = unitOfWork;
            _supplyInvoiceDetailRepository = supplyInvoiceDetailRepository;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplyInvoice>>> GetAll()
        {
            return await _supplyInvoiceRepository.GetAll();
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<SupplyInvoice>> GetInvoiceById(int id)
        {
            return await _supplyInvoiceRepository.GetById(id);
        }


        [HttpGet("[action]/{invoiceNumber}")]
        public async Task<ActionResult<SupplyInvoice>> GetInvoiceByNumber(string invoiceNumber)
        {
            return await _supplyInvoiceRepository.GetByInvoiceNumber(invoiceNumber);
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceSupplyDto>> Create(InvoiceSupplyDto invoice)
        {
            var inv = new SupplyInvoice
            {

                InvoiceNumber = invoice.InvoiceNumber,
                EmissionDate = invoice.EmissionDate,
                ProviderId = invoice.ProviderId,
                Total = invoice.Total
            };
            _supplyInvoiceRepository.Create(inv);
            await _unitOfWork.Commit();

            var invoiceNew = await _supplyInvoiceRepository.GetMaxInvoice();

            var details = invoice.Details.Select(x => new SupplyInvoiceDetail
            {
                SupplyInvoiceId = invoiceNew,
                SupplyId = x.SupplyId,
                ExpirationDate = x.ExpirationDate,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                Total = x.Total
            }).ToList();

            _supplyInvoiceDetailRepository.CreateRange(details);
            await _unitOfWork.Commit();
            await _inventoryService.AddInvoiceDetailToInventory(invoice);
            return invoice;
        }
    }
}