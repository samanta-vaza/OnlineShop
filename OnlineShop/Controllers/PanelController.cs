using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Services;
using OnlineShop.Core.Classes;
using OnlineShop.Core.ViewModels;

using OnlineShop.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {

        private IUser _user;
        private IStore _store;

        private PersianCalendar pc = new PersianCalendar();

        public PanelController(IUser user, IStore store)
        {
            _user = user;
            _store = store;
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Activate()
        {
            ViewBag.IsOK = false;

            return View();
        }

        [HttpPost]
        public IActionResult Activate(StoreActivateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;

                Store store = _user.GetUserStore(username);

                if (_user.ExistsMobileActivate(username, viewModel.MobileCode))
                {
                    if (_user.ExistsMailActivate(username, viewModel.MailCode))
                    {

                        _user.ActiveMobileNumber(username);
                        _user.ActiveMailAddress(store.Mail);

                        ViewBag.IsOK = true;
                    }
                    else
                    {
                        ModelState.AddModelError("MailCode", "کد فعالسازی شما اشتباه است");

                        ViewBag.IsOK = false;
                    }
                }
                else
                {
                    ModelState.AddModelError("MobileCode", "کد فعالسازی شما اشتباه است");

                    ViewBag.IsOK = false;
                }
            }

            return View(viewModel);
        }

        public IActionResult Edit()
        {
            Store store = _store.GetStore(_user.GetUserStore(User.Identity.Name).UserId);

            StorePropertyViewModel viewModel = new StorePropertyViewModel()
            {
                Address = store.Address,
                Des = store.Des,
                LogoName = store.Logo,
                Name = store.Name,
                Tel = store.Tel
            };

            ViewBag.MyStatus = false;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(StorePropertyViewModel viewModel)
        {
            Store store = _store.GetStore(_user.GetUserStore(User.Identity.Name).UserId);

            if (ModelState.IsValid)
            {
                if (viewModel.LogoImg == null)
                {
                    bool isUpdate = _store.UpdateStore(store.UserId, viewModel.Name, viewModel.Tel, viewModel.Address, viewModel.Des, store.Logo);

                    if (isUpdate)
                    {
                        ViewBag.MyStatus = true;
                        return View(viewModel);
                    }
                }
                else
                {
                    if (Path.GetExtension(viewModel.LogoImg.FileName) != ".jpg")
                    {
                        ModelState.AddModelError("LogoImg", "فایل با پسوند jpg بارگزاری شود");

                        ViewBag.MyStatus = false;
                    }
                    else
                    {
                        string filePath = "";
                        viewModel.LogoName = CodeGenerators.FileCode() + Path.GetExtension(viewModel.LogoImg.FileName);
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users/stores/", viewModel.LogoName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            viewModel.LogoImg.CopyTo(stream);
                        }

                        bool isUpdate = _store.UpdateStore(store.UserId, viewModel.Name, viewModel.Tel, viewModel.Address, viewModel.Des, viewModel.LogoName);

                        if (isUpdate)
                        {
                            ViewBag.MyStatus = true;
                            return View(viewModel);
                        }
                    }
                }
            }

            ViewBag.MyStatus = false;
            return View(viewModel);
        }
        public IActionResult ShowStoreCategory()
        {
            Store store = _store.GetStore(_user.GetUserStore(User.Identity.Name).UserId);

            List<StoreCategory> storeCategories = _store.GetStoreCategoriesByStoreId(store.UserId);

            return View(storeCategories);
        }

        public IActionResult CreateStoreCategory()
        {
            ViewBag.CategoryId = new SelectList(_store.GetCategoriesByNullParent(), "Id", "Name");

            return View();
        }
        [HttpPost]
        public IActionResult CreateStoreCategory(StoreCatgoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Store store = _store.GetStore(_user.GetUserStore(User.Identity.Name).UserId);

                if (viewModel.Img != null)
                {
                    if (Path.GetExtension(viewModel.Img.FileName) != ".jpg")
                    {
                        ModelState.AddModelError("Img", "فایل با پسوند jpg بارگزاری شود");
                    }
                    else
                    {
                        string filePath = "";
                        viewModel.ImgName = CodeGenerators.FileCode() + Path.GetExtension(viewModel.Img.FileName);
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users/stores/", viewModel.ImgName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            viewModel.Img.CopyTo(stream);
                        }

                        StoreCategory storeCategory = new StoreCategory()
                        {
                            CategoryId = viewModel.CategoryId,
                            Date = pc.GetYear(DateTime.Now).ToString("0000") + "/" + pc.GetMonth(DateTime.Now).ToString("00") +
                             "/" + pc.GetDayOfMonth(DateTime.Now).ToString("00"),
                            Des = null,
                            Img = viewModel.ImgName,
                            IsActive = false,
                            UserId = store.UserId,
                            Time = pc.GetHour(DateTime.Now).ToString("00") + ":" + pc.GetMinute(DateTime.Now).ToString("00")
                        };

                        _store.AddStoreCategory(storeCategory);

                        return RedirectToAction(nameof(ShowStoreCategory));
                    }
                }
                else
                {
                    StoreCategory storeCategory = new StoreCategory()
                    {
                        CategoryId = viewModel.CategoryId,
                        Date = pc.GetYear(DateTime.Now).ToString("0000") + "/" + pc.GetMonth(DateTime.Now).ToString("00") +
                             "/" + pc.GetDayOfMonth(DateTime.Now).ToString("00"),
                        Des = null,
                        Img = null,
                        IsActive = false,
                        UserId = store.UserId,
                        Time = pc.GetHour(DateTime.Now).ToString("00") + ":" + pc.GetMinute(DateTime.Now).ToString("00")
                    };

                    _store.AddStoreCategory(storeCategory);

                    return RedirectToAction(nameof(ShowStoreCategory));
                }
            }

            return View(viewModel);
        }

        public IActionResult EditStoreCategory(int id)
        {
            StoreCategory storeCategory = _store.GetStoreCategory(id);

            ViewBag.CategoryId = new SelectList(_store.GetCategoriesByNullParent(), "Id", "Name", storeCategory.CategoryId);

            StoreCatgoryViewModel viewModel = new StoreCatgoryViewModel()
            {
                CategoryId = storeCategory.CategoryId,
                ImgName = storeCategory.Img
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditStoreCategory(StoreCatgoryViewModel viewModel, int id)
        {
            if (ModelState.IsValid)
            {
                StoreCategory storeCategory = _store.GetStoreCategory(id);

                if (viewModel.Img != null)
                {
                    if (Path.GetExtension(viewModel.Img.FileName) != ".jpg")
                    {
                        ModelState.AddModelError("Img", "فایل با پسوند jpg بارگزاری شود");
                    }
                    else
                    {
                        string filePath = "";
                        viewModel.ImgName = CodeGenerators.FileCode() + Path.GetExtension(viewModel.Img.FileName);
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users/stores/", viewModel.ImgName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            viewModel.Img.CopyTo(stream);
                        }

                        _store.UpdateStoreCategory(id, viewModel.CategoryId, viewModel.ImgName);

                        return RedirectToAction(nameof(ShowStoreCategory));
                    }
                }
                else
                {
                    _store.UpdateStoreCategory(id, viewModel.CategoryId, storeCategory.Img);

                    return RedirectToAction(nameof(ShowStoreCategory));
                }
            }

            return View(viewModel);
        }

        public IActionResult DetailsStoreCategory(int id)
        {
            StoreCategory storeCategory = _store.GetStoreCategory(id);

            return View(storeCategory);
        }

        public IActionResult DeleteStoreCategory(int? id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteStoreCategory(int id)
        {
            if (ModelState.IsValid)
            {
                _store.DeleteStoreCategory(id);

                return RedirectToAction(nameof(ShowStoreCategory));
            }

            return View();
        }

        public IActionResult ShowBrands()
        {
            string username = User.Identity.Name;

            Store store = _user.GetUserStore(username);

            List<Brand> brands = _store.GetBrands(store.UserId);

            return View(brands);
        }

        public IActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBrand(AdminBrandViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;

                Store store = _user.GetUserStore(username);

                if (!_store.ExistsBrand(viewModel.Name))
                {
                    if (viewModel.Img != null)
                    {
                        if (Path.GetExtension(viewModel.Img.FileName) != ".jpg")
                        {
                            ModelState.AddModelError("Img", "فایل با پسوند jpg بارگزاری شود");
                        }
                        else
                        {
                            string filePath = "";
                            viewModel.ImgName = CodeGenerators.FileCode() + Path.GetExtension(viewModel.Img.FileName);
                            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/brands/", viewModel.ImgName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                viewModel.Img.CopyTo(stream);
                            }

                            Brand brand = new Brand()
                            {
                                Img = viewModel.ImgName,
                                Name = viewModel.Name,
                                NotShow = true,
                                StoreId = store.UserId
                            };

                            _store.AddBrand(brand);

                            return RedirectToAction(nameof(ShowBrands));
                        }
                    }
                    else
                    {
                        Brand brand = new Brand()
                        {
                            Img = null,
                            Name = viewModel.Name,
                            NotShow = true,
                            StoreId = store.UserId
                        };

                        _store.AddBrand(brand);

                        return RedirectToAction(nameof(ShowBrands));
                    }
                }
            }

            return View(viewModel);
        }

        public IActionResult ShowProducts()
        {
            string username = User.Identity.Name;

            Store store = _user.GetUserStore(username);

            List<Product> products = _store.GetProducts(store.UserId);

            return View(products);
        }

        public IActionResult DetailsProduct(int id)
        {
            Product product = _store.GetProduct(id);

            return View(product);
        }

        public IActionResult AddProduct()
        {
            Store store = _user.GetUserStore(User.Identity.Name);

            int id = _store.GetStoreCategoriesByStoreId(store.UserId).FirstOrDefault().Id;

            StoreCategory storeCategory = _store.GetStoreCategory(id);

            ViewBag.CategoryId = new SelectList(_store.GetCategories(storeCategory.CategoryId), "Id", "Name");
            ViewBag.BrandId = new SelectList(_store.AllBrands(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel viewModel)
        {
            string username = User.Identity.Name;

            Store store = _user.GetUserStore(username);

            if (ModelState.IsValid)
            {
                if (viewModel.Img != null)
                {
                    if (Path.GetExtension(viewModel.Img.FileName) != ".jpg")
                    {
                        ModelState.AddModelError("Img", "فایل با پسوند jpg بارگزاری شود");
                    }
                    else
                    {
                        string filePath = "";
                        viewModel.ImgName = CodeGenerators.FileCode() + Path.GetExtension(viewModel.Img.FileName);
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products/", viewModel.ImgName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            viewModel.Img.CopyTo(stream);
                        }

                        Product product = new Product()
                        {
                            Img = viewModel.ImgName,
                            BrandId = viewModel.BrandId,
                            CategoryId = viewModel.CategoryId,
                            Date = pc.GetYear(DateTime.Now).ToString("0000") + "/" + pc.GetMonth(DateTime.Now).ToString("00") +
                         "/" + pc.GetDayOfMonth(DateTime.Now).ToString("00"),
                            Time = pc.GetHour(DateTime.Now).ToString("00") + ":" + pc.GetMinute(DateTime.Now).ToString("00"),
                            DeletePrice = viewModel.DeletePrice,
                            Exist = viewModel.Exist,
                            Des = viewModel.Des,
                            Name = viewModel.Name,
                            NotShow = viewModel.NotShow,
                            Price = viewModel.Price,
                            StoreId = store.UserId
                        };

                        _store.AddProduct(product);

                        return RedirectToAction(nameof(ShowProducts));
                    }
                }
            }

            int id = _store.GetStoreCategoriesByStoreId(store.UserId).FirstOrDefault().Id;

            StoreCategory storeCategory = _store.GetStoreCategory(id);

            ViewBag.CategoryId = new SelectList(_store.GetCategories(storeCategory.CategoryId), "Id", "Name", viewModel.CategoryId);
            ViewBag.BrandId = new SelectList(_store.AllBrands(), "Id", "Name", viewModel.BrandId);

            return View(viewModel);
        }

        public IActionResult EditProduct(int id)
        {
            Store store = _user.GetUserStore(User.Identity.Name);

            int cid = _store.GetStoreCategoriesByStoreId(store.UserId).FirstOrDefault().Id;

            StoreCategory storeCategory = _store.GetStoreCategory(cid);

            ViewBag.CategoryId = new SelectList(_store.GetCategories(storeCategory.CategoryId), "Id", "Name");
            ViewBag.BrandId = new SelectList(_store.AllBrands(), "Id", "Name");

            Product product = _store.GetProduct(id);

            ProductViewModel viewModel = new ProductViewModel()
            {
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                DeletePrice = product.DeletePrice,
                Exist = product.Exist,
                Des = product.Des,
                ImgName = product.Img,
                Name = product.Name,
                NotShow = product.NotShow,
                Price = product.Price
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductViewModel viewModel, int id)
        {
            string username = User.Identity.Name;

            Store store = _user.GetUserStore(username);

            if (ModelState.IsValid)
            {
                Product product = _store.GetProduct(id);

                if (viewModel.Img != null)
                {
                    if (Path.GetExtension(viewModel.Img.FileName) != ".jpg")
                    {
                        ModelState.AddModelError("Img", "فایل با پسوند jpg بارگزاری شود");
                    }
                    else
                    {
                        string filePath = "";
                        viewModel.ImgName = CodeGenerators.FileCode() + Path.GetExtension(viewModel.Img.FileName);
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products/", viewModel.ImgName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            viewModel.Img.CopyTo(stream);
                        }

                        _store.UpdateProduct(id, viewModel.BrandId, viewModel.CategoryId, viewModel.Name,
                            viewModel.ImgName, viewModel.Price, viewModel.DeletePrice, viewModel.Exist, viewModel.NotShow, viewModel.Des);

                        return RedirectToAction(nameof(ShowProducts));
                    }
                }
                else
                {
                    _store.UpdateProduct(id, viewModel.BrandId, viewModel.CategoryId, viewModel.Name,
                            product.Img, viewModel.Price, viewModel.DeletePrice, viewModel.Exist, viewModel.NotShow, viewModel.Des);

                    return RedirectToAction(nameof(ShowProducts));
                }
            }

            int cid = _store.GetStoreCategoriesByStoreId(store.UserId).FirstOrDefault().Id;

            StoreCategory storeCategory = _store.GetStoreCategory(cid);

            ViewBag.CategoryId = new SelectList(_store.GetCategories(storeCategory.CategoryId), "Id", "Name", viewModel.CategoryId);
            ViewBag.BrandId = new SelectList(_store.AllBrands(), "Id", "Name", viewModel.BrandId);

            return View(viewModel);
        }

        public IActionResult DeleteProduct(int? id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {
                _store.DeleteProduct(id);

                return RedirectToAction(nameof(ShowProducts));
            }

            return View();
        }

        public IActionResult CreateGallery(int id)
        {
            ViewBag.MyId = id;

            return View();
        }
    }
}