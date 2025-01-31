using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers
{
    using BulkyBook.DataAccess.Repository.IRepository;
    using BulkyBook.Models;
    using Microsoft.AspNetCore.Mvc;

    namespace BulkyBookWeb.Areas.Admin.Controllers
    {
        [Area("Admin")]
        //[Authorize(Roles = SD.Role_Admin)]
        public class ProductController : Controller
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IWebHostEnvironment _webHostEnvironment;
            public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
            {
                _unitOfWork = unitOfWork;
                _webHostEnvironment = webHostEnvironment;
            }
            public IActionResult Index()
            {
                List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

                return View(objProductList);
            }

            public IActionResult Upsert(int? id)
            {
                ProductVM productVM = new()
                {
                    CategoryList = _unitOfWork.Category.GetAll().Select(u =>
                        new SelectListItem
                        {
                            Text = u.Name,
                            Value = u.Id.ToString()
                        }),
                    Product = new Product()
                };
                if (id == null || id == 0)
                {
                    //Create
                    return View(productVM);
                }
                else
                {
                    //update
                    productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                    return View(productVM);
                }
            }

            [HttpPost]
            public IActionResult Upsert(ProductVM productVm, IFormFile? file)
            {
                //if (obj.Name == obj.DisplayOrder.ToString())
                //{
                //    ModelState.AddModelError("name", "Display Order cannot exactly match the Name");
                //}
                ////if (obj.Name.ToLower() == "test")
                ////{
                ////    ModelState.AddModelError("", "test is an invalid value");
                ////}
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = Path.Combine(wwwRootPath, @"images\product");
                        if (!string.IsNullOrEmpty(productVm.Product.ImageUrl))
                        {
                            // delete old image
                            var oldImagePath =
                                Path.Combine(wwwRootPath, productVm.Product.ImageUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);

                            }

                        }

                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        productVm.Product.ImageUrl = @"\images\product\" + fileName;
                    }

                    if (productVm.Product.Id == 0)
                    {
                        _unitOfWork.Product.Add(productVm.Product);
                    }
                    else
                    {
                        _unitOfWork.Product.Update(productVm.Product);
                    }

                    _unitOfWork.Save();
                    TempData["Success"] = "Product Created Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    productVm.CategoryList = _unitOfWork.Category.GetAll().Select(u =>
                        new SelectListItem
                        {
                            Text = u.Name,
                            Value = u.Id.ToString()
                        });
                    return View(productVm);
                }

            }
            //if (ModelState.IsValid)
            //{
            //    _unitOfWork.Product.Add(obj.Product);
            //    _unitOfWork.Save();
            //    TempData["Success"] = "Product Created Successfully";
            //    return RedirectToAction("Index");
            //}

            //public IActionResult Edit(int? id)
            //{
            //    if (id == null || id == 0)
            //    {
            //        return NotFound();
            //    }

            //    Product productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //    //Product productFromDb = _db.Categories.FirstOrDefault(u =>u.Id ==id); This is a link Operator
            //    //Product productFromDb = _db.Categories.where(u =>u.Id ==id).FirstOrDefault
            //    if (productFromDb == null)
            //    {
            //        return NotFound();

            //    }

            //    return View(productFromDb);



            //}

            //[HttpPost]
            //public IActionResult Edit(Product obj)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        _unitOfWork.Product.Update(obj);
            //        _unitOfWork.Save();
            //        TempData["Success"] = "Product Updated Successfully";
            //        return RedirectToAction("Index");
            //    }

            //    return View();
            //}

            //public IActionResult Delete(int? id)
            //{
            //    if (id == null || id == 0)
            //    {
            //        return NotFound();
            //    }

            //    Product productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //    if (productFromDb == null)
            //    {
            //        return NotFound();

            //    }

            //    return View(productFromDb);



            //}

            //[HttpPost, ActionName("Delete")]
            //public IActionResult DeletePOST(int? id)
            //{
            //    Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            //    if (obj == null)
            //    {
            //        return NotFound();
            //    }

            //    _unitOfWork.Product.Remove(obj);
            //    _unitOfWork.Save();
            //    TempData["Success"] = "Product Deleted Successfully";
            //    return RedirectToAction("Index");

            //}


            #region API CALLS

            [HttpGet]
            public IActionResult GetAll()
            {
                List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
                return Json(new { data = objProductList });
            }

            [HttpDelete]
            public IActionResult Delete(int? id)
            {
                var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
                if (productToBeDeleted == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                var oldImagePath =
                    Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);

                }

                _unitOfWork.Product.Remove(productToBeDeleted);
                _unitOfWork.Save();

                return Json(new { success = true, message = "Deleted successfully" });
            }


            #endregion
        }
    }

}
