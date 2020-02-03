using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Redpeper.Repositories.Order.Combos;

namespace Redpeper.Controllers
{
    public class ComboController : ControllerBase
    {
        private readonly IComboRepository _comboRepository;
        private readonly IComboDetailRepository _comboDetailRepository;

        public ComboController(IComboRepository comboRepository, IComboDetailRepository comboDetailRepository)
        {
            _comboRepository = comboRepository;
            _comboDetailRepository = comboDetailRepository;
        }


    }
}
