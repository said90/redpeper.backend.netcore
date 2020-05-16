﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Repositories.Orders
{
    public interface IOrderRepository
    {
        Task<List<Model.Order>> GetAll();
        Task<Model.Order> GetById(int id);
        Task<Model.Order> GetByOrderNumber(string number);
        Task<List<Model.Order>> GetOrdersByStatus(string status);
        void Create(Model.Order order);
        void Update(Model.Order order);
        void Remove(Model.Order order);


    }
}