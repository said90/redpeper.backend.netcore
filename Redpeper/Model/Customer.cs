﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Sex { get; set; }
        public DateTime Birthdate { get; set; }
        public string Dui { get; set; }
        public string Nit { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }

    }
}
