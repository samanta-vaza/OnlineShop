using System;
using System.Collections.Generic;
using System.Text;

using OnlineShop.Core.Interfaces;
using OnlineShop.DataAccessLayer.Entities;
using OnlineShop.DataAccessLayer.Context;

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OnlineShop.Core.Services
{
    public class StoreService : IStore
    {
        private DatabaseContext _context;

        public StoreService(DatabaseContext context)
        {
            _context = context;
        }

        public void AddBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void AddStoreCategory(StoreCategory storeCategory)
        {
            _context.StoreCategories.Add(storeCategory);
            _context.SaveChanges();
        }

        public List<Brand> AllBrands()
        {
            return _context.Brands.OrderBy(b => b.Name).ToList();
        }

        public void DeleteProduct(int id)
        {
            Product product = _context.Products.Find(id);

            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public void DeleteStoreCategory(int id)
        {
            StoreCategory storeCategory = _context.StoreCategories.Find(id);

            _context.StoreCategories.Remove(storeCategory);
            _context.SaveChanges();
        }

        public bool ExistsBrand(string name)
        {
            return _context.Brands.Any(b => b.Name == name);
        }

        public List<Brand> GetBrands(int id)
        {
            return _context.Brands.Where(b => b.StoreId == id).OrderByDescending(b => b.Id).ToList();
        }

        public List<Category> GetCategories(int id)
        {
            return _context.Categories.Where(c => c.Parent.ParentId == id).ToList();
        }

        public List<Category> GetCategoriesByNullParent()
        {
            return _context.Categories.Where(c => c.ParentId == null).OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Include(c => c.Parent).FirstOrDefault(c => c.Id == id);
        }

        public List<FieldCategory> GetFieldCategories(int id)
        {
            return _context.FieldCategories.Include(f => f.Field).Where(f => f.CategoryId == id).ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Include(p => p.Brand).Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProducts(int id)
        {
            return _context.Products.Where(p => p.StoreId == id).OrderByDescending(p => p.Id).ToList();
        }

        

        //public int GetProductSeen(int id)
        //{
        //    return _context.ProductSeens.Where(s => s.ProductId == id).ToList().Count();
        //}

        public Store GetStore(int id)
        {
            return _context.Stores.Include(s => s.User).FirstOrDefault(s => s.UserId == id);
        }

        public List<StoreCategory> GetStoreCategoriesByStoreId(int id)
        {
            return _context.StoreCategories.Include(s => s.Category).Where(s => s.UserId == id).OrderByDescending(s => s.Id).ToList();
        }

        public StoreCategory GetStoreCategory(int id)
        {
            return _context.StoreCategories.Find(id);
        }

        public void UpdateProduct(int id, int brandid, int catid, string name, string img, int price, int delprice, int exists, bool notshow, string des)
        {
            Product product = _context.Products.Find(id);

            product.BrandId = brandid;
            product.CategoryId = catid;
            product.DeletePrice = delprice;
            product.Price = price;
            product.Name = name;
            product.Img = img;
            product.NotShow = notshow;
            product.Exist = exists;
            product.Des = des;

            _context.SaveChanges();
        }

        public bool UpdateStore(int id, string name, string tel, string address, string des, string logo)
        {
            Store store = _context.Stores.Find(id);

            if (store != null)
            {
                store.Name = name;
                store.Tel = tel;
                store.Address = address;
                store.Des = des;
                store.Logo = logo;

                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateStoreCategory(int id, int categoryid, string img)
        {
            StoreCategory storeCategory = _context.StoreCategories.Find(id);

            storeCategory.CategoryId = categoryid;
            storeCategory.Img = img;
            storeCategory.IsActive = false;

            _context.SaveChanges();
        }
    }
}
