using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Redpeper.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Sex { get; set; }
        public DateTime Birthdate { get; set; }
        public string Dui { get; set; }
        public string Nit { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
