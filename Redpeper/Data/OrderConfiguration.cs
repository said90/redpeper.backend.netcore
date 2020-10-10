using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Redpeper.Model;

namespace Redpeper.Data
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //Table Name
            builder.ToTable("Order");
            
            //Primary Key
            builder.HasKey(o => o.Id);

            //relationships
            builder.HasMany(o => o.OrderDetails);
        }
    }
}
