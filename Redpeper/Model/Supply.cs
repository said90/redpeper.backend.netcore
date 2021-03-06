﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class Supply
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Presentation { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public double MinimumQty { get; set; }
    }
}
