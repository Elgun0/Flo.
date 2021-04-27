using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService
    {
        private EcommerceContext _context;
        public CategoryService(EcommerceContext context)
        {
            _context = context;
        }
        public List<Category> GetCatagories()
        {
            return _context.Categories.ToList();
        }
        public Category GetProductByID(int id)
        {
            return _context.Categories.FirstOrDefault(x=>x.ID==id);
        }
    }
}
