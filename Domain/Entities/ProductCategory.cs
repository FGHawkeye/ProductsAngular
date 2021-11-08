using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ProductCategory
    {
        [Key]
        public int ProductID { get; set; }
        [Key]
        public int CategoryID { get; set; }

        public Product Product { get; set; }

    }
}
