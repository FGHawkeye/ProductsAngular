using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService
    {
        private ProductDbContext context;

        public ProductService(ProductDbContext context)
        {
            this.context = context;
        }

        public List<ProductDto> GetProducts()
        {
            var products = context.Product.Join(
                            context.ProductCategory,
                            product => product.ProductID,
                            productCategory => productCategory.ProductID,
                            (product, productCategory) => new { product, productCategory })
                           .Join(
                                context.Category,
                                combined => combined.productCategory.CategoryID,
                                category => category.CategoryID,
                                (combined, category) => new ProductDto
                                {
                                    Product = combined.product,
                                    Category = category
                                }).ToList();
            return products;

        }

        public void AddCategory(Category category)
        {
            context.Category.Add(category);
            context.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return context.Category.ToList();
        }

        public void AddProduct(ProductDto product)
        {
            context.Product.Add(product.Product);
            context.ProductCategory.Add(new ProductCategory() { CategoryID = product.Category.CategoryID, Product = product.Product});
            context.SaveChanges();
        }

        public void UpdateProduct(ProductDto product)
        {
            var uProduct = context.Product.Find(product.Product.ProductID);
            var uProductCategory = context.ProductCategory.Where(pc => pc.ProductID == product.Product.ProductID).FirstOrDefault();
            if(uProduct != null && uProductCategory != null)
            {
                uProduct.Name = product.Product.Name;
                uProduct.Description = product.Product.Description;
                uProduct.Image = product.Product.Image;
                context.Remove(uProductCategory);
                context.Add(new ProductCategory() { ProductID = product.Product.ProductID , CategoryID = product.Category.CategoryID });
                context.SaveChanges();
            }

        }

        public void DeleteProduct(ProductDto product)
        {
            context.Product.Remove(product.Product);
            context.ProductCategory.Remove(new ProductCategory() { ProductID = product.Product.ProductID, CategoryID = product.Category.CategoryID });
            context.SaveChanges();
        }
    }
}
