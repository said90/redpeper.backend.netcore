﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Collection;
using Redpeper.Data;
using Redpeper.Extensions;
using Redpeper.Model;

namespace Redpeper.Repositories.InvoiceSupply
{
    public class SupplyInvoiceRepository :ISupplyInvoiceRepository
    {
        private readonly DataContext _dataContext;

        public SupplyInvoiceRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<SupplyInvoice>> GetAll()
        {
            return await _dataContext.SupplyInvoices.Include(x=>x.Details).ToListAsync();
        }

        public async Task<PagedList<SupplyInvoice>> GetPaginated(int pageNumber, int pageSize, string sort)
        {
            return await _dataContext.SupplyInvoices.ToPagedListAsync(pageNumber, pageSize, sort);

        }

        public async Task<SupplyInvoice> GetById(int id)
        {
            return await _dataContext.SupplyInvoices.Include(x => x.Details).FirstOrDefaultAsync(x=> x.Id ==id);
        }

        public async  Task<int> GetMaxInvoice()
        {
            return await _dataContext.SupplyInvoices.Select(x=>x.Id).MaxAsync();
        }

        public async Task<SupplyInvoice> GetByInvoiceNumber(string number)
        {
            return await _dataContext.SupplyInvoices.Include(x => x.Details).FirstOrDefaultAsync(x => x.InvoiceNumber == number);
        }

        public async Task<List<SupplyInvoice>> GetByProvider(int id)
        {
            return await _dataContext.SupplyInvoices.Include(x => x.Details).Where(x => x.ProviderId == id).ToListAsync();
        }

        public async Task<List<SupplyInvoice>> GetByDate(DateTime date)
        {
            return await _dataContext.SupplyInvoices.Include(x => x.Details).Where(x => x.EmissionDate >= date && x.EmissionDate <= date)
                .ToListAsync();
        }

        public async Task<List<SupplyInvoice>> GetByDateRange(DateTime initialDate, DateTime endDate)
        {
            return await _dataContext.SupplyInvoices.Include(x => x.Details).Where(x => x.EmissionDate >= initialDate && x.EmissionDate <= endDate)
                .ToListAsync();
        }

        public void Create(SupplyInvoice supplyInvoice)
        {
            _dataContext.SupplyInvoices.Add(supplyInvoice);
        }

        public void Update(SupplyInvoice supplyInvoice)
        {
            _dataContext.SupplyInvoices.Update(supplyInvoice);
        }

        public void Remove(SupplyInvoice supplyInvoice)
        {
            _dataContext.SupplyInvoices.Remove(supplyInvoice);
        }
    }
}
