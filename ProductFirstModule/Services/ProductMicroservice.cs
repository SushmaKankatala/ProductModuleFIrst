using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductFirstModule.Database;
using ProductFirstModule.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductFirstModule.Services
{
    public class ProductMicroservice : IProductMicroservice
    {
        private readonly ProductDbContext _db;
        public ProductMicroservice(ProductDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddProductRating(Rating rating)
        {
            Product product = await _db.Product_s.Where(s => s.Id == rating.ProductId).FirstOrDefaultAsync();
            if (product == null)
            {
                return false;
            }
            Rating rating1 = await _db.Ratings.Where(s => s.UserId == rating.UserId && s.ProductId == rating.ProductId).FirstOrDefaultAsync();
            if (rating1 == null)
            {
                _db.Ratings.Add(rating);
            }
            else
            {
                rating1.ProductRating = rating.ProductRating;
                _db.Ratings.Update(rating1);
            }
            await _db.SaveChangesAsync();
            product.Rating = (product.Rating + rating.ProductRating) / (_db.Ratings.Where(s => s.ProductId == rating.ProductId).Count() + 1);

            _db.Product_s.Update(product);
            await _db.SaveChangesAsync();
            return true;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = _db.Product_s.ToList();
            return products;
        }

        public async Task<Product> ProductById(int id)
        {
            Product product = await _db.Product_s.Where(s=>s.Id==id).FirstOrDefaultAsync();
            return product;
        }

        public async Task<Product> SearchProductByName(string name)
        {
            Product product = await _db.Product_s.Where(s => s.Name.ToLower().Contains(name.ToLower())).FirstOrDefaultAsync();
            return product;
        }
    }
}
