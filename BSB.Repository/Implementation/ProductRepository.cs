using BSB.Data;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<Product> AddProduct(Product p)
        {
            this._context.Add(p);
            await _context.SaveChangesAsync();

            return p;
        }

        public async Task<Product> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> EditProduct(Product p)
        {
            this._context.Update(p);
            await _context.SaveChangesAsync();

            return p;
        }


        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetAllBuy()
        {
            return await _context.Products.Where(x=>x.IsForBuy).ToListAsync();
        }

        public async Task<List<string>> GetAllGenres()
        {
            List<string> res = new List<string>();

            foreach(var book in await this.GetAll())
            {
                if (!res.Contains(book.Genre))
                    res.Add(book.Genre);
            }

            return res;
        }
       
        public async Task<List<Product>> GetAllRent()
        {
            return await _context.Products.Where(x => !x.IsForBuy).ToListAsync();
        }

        public async Task<Product> GetProduct(Guid id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Product>> GroupByGenres(string Genre)
        {
            return await _context.Products
                .Where(x => x.Genre.Equals(Genre)).ToListAsync();
        }

        public void Insert(ProductInShoppingCart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");  
            }
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Insert(ProductInOrder entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Add(entity);
            _context.SaveChanges();
        }


    }
}
