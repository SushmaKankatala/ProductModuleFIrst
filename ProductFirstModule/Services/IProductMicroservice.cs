using Microsoft.AspNetCore.Mvc;
using ProductFirstModule.Database;
using ProductFirstModule.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductFirstModule.Services
{
    public interface IProductMicroservice
    {
        Task<Product> ProductById(int id);
        List<Product> GetProducts();
        Task<Product> SearchProductByName(string name);
        Task<bool> AddProductRating(Rating rating);
    }
}
