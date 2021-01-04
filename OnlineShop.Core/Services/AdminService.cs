using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OnlineShop.Core.Interfaces;
using OnlineShop.DataAccessLayer.Context;
using OnlineShop.DataAccessLayer.Entities;

using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Core.Services
{
    public class AdminService : IAdmin
    {
        private DatabaseContext _context;
        public AdminService(DatabaseContext context)
        {
            _context = context;
        }

        public void AddBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public void AddField(Field field)
        {
            _context.Fields.Add(field);
            _context.SaveChanges();
        }

        public void AddFieldCategory(FieldCategory fieldCategory)
        {
            _context.FieldCategories.Add(fieldCategory);
            _context.SaveChanges();
        }

        public void DeleteAll(int id)
        {
            List<FieldCategory> fieldCategories = _context.FieldCategories.Where(f => f.CategoryId == id).ToList();

            _context.FieldCategories.RemoveRange(fieldCategories);
            _context.SaveChanges();
        }

        public void DeleteBrand(int id)
        {
            Brand brand = _context.Brands.Find(id);

            _context.Brands.Remove(brand);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            //List<Category> categories = _context.Categories.Where(c => c.ParentId == id).ToList();
            //_context.Categories.RemoveRange(categories);
            //_context.SaveChanges();

            Category category = _context.Categories.Find(id);

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public void DeleteField(int id)
        {
            Field field = _context.Fields.Find(id);

            _context.Fields.Remove(field);
            _context.SaveChanges();
        }

        public void DeletePermission(int id)
        {
            Permission permission = _context.Permissions.Find(id);

            _context.Permissions.Remove(permission);
            _context.SaveChanges();
        }

        public void DeleteStoreCategory(int id)
        {
            StoreCategory storeCategory = _context.StoreCategories.Find(id);

            _context.StoreCategories.Remove(storeCategory);
            _context.SaveChanges();
        }

        public bool ExistsFieldCategory(int id, int catid)
        {
            return _context.FieldCategories.Any(f => f.FieldId == id && f.CategoryId == catid);
        }

        public bool ExistsSetting()
        {
            // return _context.Setting.Any();
            Setting setting = _context.Settings.FirstOrDefault();

            if (setting == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Brand GetBrand(int id)
        {
            return _context.Brands.Find(id);
        }

        public List<Brand> GetBrands()
        {
            return _context.Brands.Include(b => b.Store).OrderByDescending(b => b.Id).ToList();
        }

        public List<Brand> GetBrandsByStoreId(int id)
        {
            return _context.Brands.Where(b => b.StoreId == id).OrderByDescending(b => b.Id).ToList();
        }

        public int GetBrandsNotShowCount()
        {
            return _context.Brands.Where(b => b.NotShow == true).ToList().Count();
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.Where(c => c.ParentId == null).ToList();
        }

        public List<Category> GetCategoriesByParentId(int id)
        {
            return _context.Categories.Include(c => c.Parent).Where(c => c.ParentId == id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Include(c => c.Parent).FirstOrDefault(c => c.Id == id);
        }

        public int? GetCategoryParentId(int id)
        {
           
            return _context.Categories.FirstOrDefault(c => c.Id == id).ParentId;
        }

        public Field GetField(int id)
        {
            return _context.Fields.Find(id);
        }

        public List<FieldCategory> GetFieldCategories(int id)
        {
            return _context.FieldCategories.Where(f => f.CategoryId == id).ToList();
        }

        public List<Field> GetFields()
        {
            return _context.Fields.OrderBy(f => f.Name).ToList();
        }

        public Permission GetPermission(int id)
        {
            return _context.Permissions.Find(id);
        }

        public List<Permission> GetPermissions()
        {
            return _context.Permissions.OrderBy(p => p.Name).ToList();
        }

        public Setting GetSetting()
        {
            return _context.Settings.FirstOrDefault();
        }

        public List<StoreCategory> GetStoreCategories()
        {
            return _context.StoreCategories.Include(s => s.Category).Include(s => s.Store).Where(s => s.IsActive == false).OrderByDescending(s => s.Id).ToList();
        }

        public StoreCategory GetStoreCategory(int id)
        {
            return _context.StoreCategories.Find(id);
        }

        public List<Category> GetSubCategories()
        {
            return _context.Categories.ToList();
        }

        public void InsertCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void InsertPermission(Permission permission)
        {
            _context.Permissions.Add(permission);
            _context.SaveChanges();
        }

        public void InsertSetting(Setting setting)
        {
            _context.Settings.Add(setting);
            _context.SaveChanges();
        }

        public void UpdateBrand(int id, string name, string img, bool notshow)
        {
            Brand brand = _context.Brands.Find(id);

            brand.Name = name;
            brand.Img = img;
            brand.NotShow = notshow;

            _context.SaveChanges();
        }

        public void UpdateCategory(int id, string name, string icon)
        {
            Category category = _context.Categories.Find(id);

            category.Name = name;
            category.Icon = icon;

            _context.SaveChanges();
        }

        public void UpdateField(int id, string name)
        {
            Field field = _context.Fields.Find(id);

            field.Name = name;

            _context.SaveChanges();
        }

        public void UpdatePermission(int id, string name)
        {
            Permission permission = _context.Permissions.Find(id);

            permission.Name = name;

            _context.SaveChanges();
        }

        public void UpdateSetting(string name, string desc, string keys, string api, string sender, string mail, string password)
        {
            Setting setting = _context.Settings.FirstOrDefault();

            setting.SiteName = name;
            setting.SiteDesc = desc;
            setting.SiteKeys = keys;
            setting.SmsApi = api;
            setting.SmsSender = sender;
            setting.MailAddress = mail;
            setting.MailPassword = password;

            _context.SaveChanges();
        }

        public void UpdateStoreCategory(int id, bool isactive, string des)
        {
            StoreCategory storeCategory = _context.StoreCategories.Find(id);

            storeCategory.IsActive = isactive;
            storeCategory.Des = des;

            _context.SaveChanges();
        }

        public void UpdateSubCategory(int id, int parentid, string name)
        {

            Category category = _context.Categories.Find(id);

            category.Name = name;
            category.ParentId = parentid;

            _context.SaveChanges();
        }
    }
}
