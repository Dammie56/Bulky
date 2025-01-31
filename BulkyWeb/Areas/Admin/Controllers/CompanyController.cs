namespace BulkyBookWeb.Controllers
{
    using BulkyBook.DataAccess.Repository.IRepository;
    using BulkyBook.Models;
    using Microsoft.AspNetCore.Mvc;

    namespace BulkyBookWeb.Areas.Admin.Controllers
    {
        [Area("Admin")]
        //[Authorize(Roles = SD.Role_Admin)]
        public class CompanyController : Controller
        {
            private readonly IUnitOfWork _unitOfWork;
            public CompanyController(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;

            }
            public IActionResult Index()
            {
                List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

                return View(objCompanyList);
            }

            public IActionResult Upsert(int? id)
            {

                if (id == null || id == 0)
                {
                    //Create
                    return View(new Company());
                }
                else
                {
                    //update
                    Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                    return View(companyObj);
                }
            }

            [HttpPost]
            public IActionResult Upsert(Company companyObj)
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


                    if (companyObj.Id == 0)
                    {
                        _unitOfWork.Company.Add(companyObj);
                    }
                    else
                    {
                        _unitOfWork.Company.Update(companyObj);
                    }

                    _unitOfWork.Save();
                    TempData["Success"] = "Company Created Successfully";
                    return RedirectToAction("Index");
                }
                else
                {

                    return View(companyObj);
                }

            }
            //if (ModelState.IsValid)
            //{
            //    _unitOfWork.Company.Add(obj.Company);
            //    _unitOfWork.Save();
            //    TempData["Success"] = "Company Created Successfully";
            //    return RedirectToAction("Index");
            //}

            //public IActionResult Edit(int? id)
            //{
            //    if (id == null || id == 0)
            //    {
            //        return NotFound();
            //    }

            //    Company companyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
            //    //Company companyFromDb = _db.Categories.FirstOrDefault(u =>u.Id ==id); This is a link Operator
            //    //Company companyFromDb = _db.Categories.where(u =>u.Id ==id).FirstOrDefault
            //    if (companyFromDb == null)
            //    {
            //        return NotFound();

            //    }

            //    return View(companyFromDb);



            //}

            //[HttpPost]
            //public IActionResult Edit(Company obj)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        _unitOfWork.Company.Update(obj);
            //        _unitOfWork.Save();
            //        TempData["Success"] = "Company Updated Successfully";
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

            //    Company companyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
            //    if (companyFromDb == null)
            //    {
            //        return NotFound();

            //    }

            //    return View(companyFromDb);



            //}

            //[HttpPost, ActionName("Delete")]
            //public IActionResult DeletePOST(int? id)
            //{
            //    Company? obj = _unitOfWork.Company.Get(u => u.Id == id);
            //    if (obj == null)
            //    {
            //        return NotFound();
            //    }

            //    _unitOfWork.Company.Remove(obj);
            //    _unitOfWork.Save();
            //    TempData["Success"] = "Company Deleted Successfully";
            //    return RedirectToAction("Index");

            //}


            #region API CALLS

            [HttpGet]
            public IActionResult GetAll()
            {
                List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
                return Json(new { data = objCompanyList });
            }

            [HttpDelete]
            public IActionResult Delete(int? id)
            {
                var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
                if (CompanyToBeDeleted == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                _unitOfWork.Company.Remove(CompanyToBeDeleted);
                _unitOfWork.Save();

                return Json(new { success = true, message = "Deleted successfully" });
            }


            #endregion
        }
    }

}
