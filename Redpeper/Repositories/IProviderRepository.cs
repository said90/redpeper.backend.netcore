﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Collection;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public interface IProviderRepository :IRepository<Provider>
    {
        Task<List<Provider>> GetAllOrderById();
      
    }
}
