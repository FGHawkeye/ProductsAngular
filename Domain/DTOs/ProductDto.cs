using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ProductDto
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
