using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Services;
using OnlineShop.Core.Classes;
using OnlineShop.Core.ViewModels;

using OnlineShop.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;
using System.IO;

namespace OnlineShop.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {

        private IAdmin _admin;

        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }


        public IActionResult Setting()
        {
            Setting setting = _admin.GetSetting();

            ViewBag.MyMessage = false;

            if (setting != null)
            {
                AdminSettingViewModel viewModel = new AdminSettingViewModel()
                {
                    MailAddress = setting.MailAddress,
                    MailPassword = setting.MailPassword,
                    SiteDesc = setting.SiteDesc,
                    SiteKeys = setting.SiteKeys,
                    SiteName = setting.SiteName,
                    SmsApi = setting.SmsApi,
                    SmsSender = setting.SmsSender
                };

                return View(viewModel);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Setting(AdminSettingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (_admin.ExistsSetting())
                {
                    _admin.UpdateSetting(viewModel.SiteName, viewModel.SiteDesc, viewModel.SiteDesc, viewModel.SmsApi, viewModel.SmsSender, viewModel.MailAddress, viewModel.MailPassword);
                }
                else
                {
                    Setting setting = new Setting()
                    {
                        MailAddress = viewModel.MailAddress,
                        MailPassword = viewModel.MailPassword,
                        SiteDesc = viewModel.SiteDesc,
                        SiteKeys = viewModel.SiteKeys,
                        SiteName = viewModel.SiteName,
                        SmsApi = viewModel.SmsApi,
                        SmsSender = viewModel.SmsSender
                    };

                    _admin.InsertSetting(setting);
                }

                ViewBag.MyMessage = true;
            }

            return View(viewModel);
        }

        public IActionResult Permissions(int? page)
        {
            //List<Permission> permissions = _admin.GetPermissions();

            int pageSize = 5;
            var pageNumber = page ?? 1;
            var permissions = _admin.GetPermissions().ToPagedList(pageNumber, pageSize);

            ViewBag.MyModels = permissions;

            return View(permissions);
        }

        public IActionResult AddPermission()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPermission(PermissionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Permission permission = new Permission()
                {
                    Name = viewModel.Name
                };

                _admin.InsertPermission(permission);

                return RedirectToAction(nameof(Permissions));
            }

            return View(viewModel);
        }

        public IActionResult EditPermission(int id)
        {
            Permission permission = _admin.GetPermission(id);

            PermissionViewModel viewModel = new PermissionViewModel()
            {
                Name = permission.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditPermission(int id, PermissionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _admin.UpdatePermission(id, viewModel.Name);

                return RedirectToAction(nameof(Permissions));
            }

            return View(viewModel);
        }

        public IActionResult DeletePermission(int? id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeletePermission(int id)
        {
            if (ModelState.IsValid)
            {
                _admin.DeletePermission(id);

                return RedirectToAction(nameof(Permissions));
            }

            return View();
        }

        public IActionResult Categories()
        {
            List<Category> categories = _admin.GetCategories();

            return View(categories);
        }
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category()
                {
                    ParentId = null,
                    Icon = viewModel.Icon,
                    Name = viewModel.Name
                };

                _admin.InsertCategory(category);

                return RedirectToAction(nameof(Categories));
            }

            return View(viewModel);
        }

        public IActionResult EditCategory(int id)
        {
            Category category = _admin.GetCategory(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult EditCategory(int id, Category model)
        {
            if (ModelState.IsValid)
            {
                _admin.UpdateCategory(id, model.Name, model.Icon);

                int? parentID = _admin.GetCategoryParentId(id);

                if (parentID != null)
                {
                    Category category1 = _admin.GetCategory((int)parentID);

                    if (category1.ParentId == null)
                    {
                        return Redirect("/Admin/SubCategories/" + parentID);
                    }
                    else
                    {
                        parentID = _admin.GetCategoryParentId((int)parentID);

                        Category category2 = _admin.GetCategory((int)parentID);

                        if (category2.ParentId == null)
                        {
                            return Redirect("/Admin/SubCategories/" + parentID);
                        }
                        else
                        {
                            parentID = _admin.GetCategoryParentId((int)parentID);

                            return Redirect("/Admin/SubCategories/" + parentID);
                        }
                    }
                }
                else
                {
                    return Redirect("/Admin/SubCategories/" + id);
                }
            }

            return View(model);
        }

        public IActionResult DeleteCategory(int? id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            DeleteCascadeCategory(id);

            return RedirectToAction(nameof(Categories));


        }

        private void DeleteCascadeCategory(int id)
        {
            List<Category> categories = _admin.GetCategoriesByParentId(id);

            if (categories.Count > 0)
            {
                foreach (var item in categories)
                {
                    DeleteCascadeCategory(item.Id);
                }
            }

            _admin.DeleteCategory(id);
        }
        public IActionResult SubCategories(int id)
        {
            List<Category> categories = _admin.GetSubCategories();

            ViewBag.MyId = id;

            return View(categories);
        }

        public IActionResult CreateSubCategory(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSubCategory(int id, SubCategoriesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                Category category = new Category()
                {
                    Icon = null,
                    Name = viewModel.Name,
                    ParentId = id
                };

                _admin.InsertCategory(category);

                int? parentID = _admin.GetCategoryParentId(id);

                if (parentID != null)
                {
                    Category category1 = _admin.GetCategory((int)parentID);

                    if (category1.ParentId == null)
                    {
                        return Redirect("/Admin/SubCategories/" + parentID);
                    }
                    else
                    {
                        parentID = _admin.GetCategoryParentId((int)parentID);

                        return Redirect("/Admin/SubCategories/" + parentID);
                    }
                }
                else
                {
                    return Redirect("/Admin/SubCategories/" + id);
                }
            }

            return View(viewModel);
        }

        public IActionResult ShowStoreCategories()
        {
            List<StoreCategory> storeCategories = _admin.GetStoreCategories();

            return View(storeCategories);
        }
        public IActionResult EditStoreCategory(int id)
        {
            StoreCategory storeCategory = _admin.GetStoreCategory(id);

            UpdateStoreCategoryViewModel viewModel = new UpdateStoreCategoryViewModel()
            {
                Des = storeCategory.Des,
                Img = storeCategory.Img,
                IsActive = storeCategory.IsActive
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult EditStoreCategory(int id, UpdateStoreCategoryViewModel viewModel)
        {
            StoreCategory storeCategory = _admin.GetStoreCategory(id);

            if (ModelState.IsValid)
            {
                _admin.UpdateStoreCategory(id, viewModel.IsActive, viewModel.Des);

                return RedirectToAction(nameof(ShowStoreCategories));
            }

            UpdateStoreCategoryViewModel storeC = new UpdateStoreCategoryViewModel()
            {
                Des = storeCategory.Des,
                Img = storeCategory.Img,
                IsActive = storeCategory.IsActive
            };

            return View(storeC);
        }

        public IActionResult DeleteStoreCategory(int? id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteStoreCategory(int id)
        {
            _admin.DeleteStoreCategory(id);

            return RedirectToAction(nameof(ShowStoreCategories));
        }

        public IActionResult ShowBrands()
        {
            List<Brand> brands = _admin.GetBrands();

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
                            NotShow = viewModel.NotShow,
                            StoreId = null
                        };

                        _admin.AddBrand(brand);

                        return RedirectToAction(nameof(ShowBrands));
                    }
                }
                else
                {
                    Brand brand = new Brand()
                    {
                        Img = null,
                        Name = viewModel.Name,
                        NotShow = viewModel.NotShow,
                        StoreId = null
                    };

                    _admin.AddBrand(brand);

                    return RedirectToAction(nameof(ShowBrands));
                }
            }

            return View(viewModel);
        }

        public IActionResult EditBrand(int id)
        {
            Brand brand = _admin.GetBrand(id);

            AdminBrandViewModel viewModel = new AdminBrandViewModel()
            {
                Name = brand.Name,
                ImgName = brand.Img,
                NotShow = brand.NotShow
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditBrand(AdminBrandViewModel viewModel, int id)
        {
            if (ModelState.IsValid)
            {
                Brand brand = _admin.GetBrand(id);

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

                        _admin.UpdateBrand(id, viewModel.Name, viewModel.ImgName, viewModel.NotShow);

                        return RedirectToAction(nameof(ShowBrands));
                    }
                }
                else
                {
                    _admin.UpdateBrand(id, viewModel.Name, brand.Img, viewModel.NotShow);

                    return RedirectToAction(nameof(ShowBrands));
                }
            }

            return View(viewModel);
        }

        public IActionResult DetailsBrand(int id)
        {
            Brand brand = _admin.GetBrand(id);

            return View(brand);
        }

        public IActionResult DeleteBrand(int? id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteBrand(int id)
        {
            _admin.DeleteBrand(id);

            return RedirectToAction(nameof(ShowBrands));
        }

        public IActionResult CreateField()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateField(Field model)
        {
            Field field = new Field()
            {
                Name = model.Name
            };

            _admin.AddField(field);

            return RedirectToAction("ShowFields");
        }

        public IActionResult ShowFields()
        {
            List<Field> fields = _admin.GetFields();

            return View(fields);
        }

        public IActionResult EditField(int id)
        {
            Field field = _admin.GetField(id);

            return View(field);
        }

        [HttpPost]
        public IActionResult EditField(int id,Field model)
        {
            if (ModelState.IsValid)
            {
                _admin.UpdateField(id, model.Name);

                return RedirectToAction(nameof(ShowFields));
            }

            return View(model);
        }

        public IActionResult DeleteField(int? id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteField(int id)
        {
            _admin.DeleteField(id);

            return RedirectToAction(nameof(ShowFields));
        }

        public IActionResult FieldCategories(int id)
        {
            List<Field> fields = _admin.GetFields();
            ViewBag.CatId = id;
            return View(fields);
        }

        public IActionResult UpdateFieldCategory(int id, string result)
        {
            _admin.DeleteAll(id);

            int count = result.Length - 1;

            string strID = result.Remove(count, 1);
            char[] dash = new char[] { '-' };
            string[] strResult = strID.Split(dash);

            if (result != "0")
            {
                foreach (string item in strResult)
                {
                    FieldCategory fieldCategory = new FieldCategory()
                    {
                        CategoryId = id,
                        FieldId = Convert.ToInt32(item)
                    };

                    _admin.AddFieldCategory(fieldCategory);
                }
            }

            return RedirectToAction("Categories");
        }

    }
}
