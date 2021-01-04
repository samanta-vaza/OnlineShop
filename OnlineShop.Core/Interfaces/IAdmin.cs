using System;
using System.Collections.Generic;
using System.Text;


using OnlineShop.DataAccessLayer.Entities;

using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.Interfaces
{
    public interface IAdmin
    {

        #region For Setting

        void InsertSetting(Setting setting);

        void UpdateSetting(string name, string desc, string keys, string api, string sender, string mail, string password);

        bool ExistsSetting();

        Setting GetSetting();

        #endregion

        #region For Permission

        void InsertPermission(Permission permission);

        void UpdatePermission(int id, string name);

        void DeletePermission(int id);

        List<Permission> GetPermissions();

        Permission GetPermission(int id);

        #endregion

        #region Category

        void InsertCategory(Category category);

        void UpdateCategory(int id, string name, string icon);

        void UpdateSubCategory(int id, int parentid, string name);

        void DeleteCategory(int id);

        Category GetCategory(int id);

        List<Category> GetCategories();

        List<Category> GetSubCategories();

        int? GetCategoryParentId(int id);

        List<Category> GetCategoriesByParentId(int id);

        #endregion

        #region StoreCategory

        void UpdateStoreCategory(int id, bool isactive, string des);
        List<StoreCategory> GetStoreCategories();
        StoreCategory GetStoreCategory(int id);
        void DeleteStoreCategory(int id);

        #endregion

        #region Brand

        void AddBrand(Brand brand);

        void UpdateBrand(int id, string name, string img, bool notshow);

        void DeleteBrand(int id);

        Brand GetBrand(int id);

        List<Brand> GetBrands();

        List<Brand> GetBrandsByStoreId(int id);

        int GetBrandsNotShowCount();

        #endregion

        #region Field

        void AddField(Field field);

        void UpdateField(int id, string name);

        void DeleteField(int id);

        Field GetField(int id);

        List<Field> GetFields();

        #endregion

        #region FieldCategory

        void DeleteAll(int id);

        void AddFieldCategory(FieldCategory fieldCategory);

        List<FieldCategory> GetFieldCategories(int id);

        bool ExistsFieldCategory(int id, int catid);

        #endregion

       
    }
}
