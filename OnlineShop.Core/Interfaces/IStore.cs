using System;
using System.Collections.Generic;
using System.Text;

using OnlineShop.DataAccessLayer.Entities;

namespace OnlineShop.Core.Interfaces
{
    public interface IStore
    {
        Store GetStore(int id);

        bool UpdateStore(int id, string name, string tel, string address, string des, string logo);

        List<Category> GetCategoriesByNullParent();

        #region StoreCategory

        void AddStoreCategory(StoreCategory storeCategory);
        void UpdateStoreCategory(int id, int categoryid, string img);
        void DeleteStoreCategory(int id);
        List<StoreCategory> GetStoreCategoriesByStoreId(int id);
        StoreCategory GetStoreCategory(int id);

        #endregion

        #region Brand

        void AddBrand(Brand brand);

        List<Brand> GetBrands(int id);

        List<Brand> AllBrands();

        bool ExistsBrand(string name);

        #endregion

        #region Product

        void AddProduct(Product product);

        void DeleteProduct(int id);

        void UpdateProduct(int id, int brandid, int catid, string name, string img, int price, int delprice, int exists, bool notshow, string des);

        Product GetProduct(int id);

        List<Product> GetProducts(int id);

        List<Category> GetCategories(int id);

        Category GetCategory(int id);

        List<FieldCategory> GetFieldCategories(int id);

        //int GetProductSeen(int id);

        #endregion


    }
}
