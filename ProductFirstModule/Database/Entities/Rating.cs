using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductFirstModule.Database.Entities
{
    
    public class Rating
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public double ProductRating { get; set; }
    }
}
