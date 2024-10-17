using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

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
            List<Company> CompanyList = _unitOfWork.Company.GetAll().ToList();
            
            return View(CompanyList);
        }

        public IActionResult Upsert(int? id)
        {
            if(id is null || id is 0)
            {
                return View(new Company());
            }
            else
            {
                Company companyObj = _unitOfWork.Company.Get(x => x.Id == id);
                return View(companyObj);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
            if (ModelState.IsValid)
            {
                if(CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company Created Successfully!";
                return RedirectToAction("Index", "Company");
            }
            else
            {
                return View(CompanyObj);
            }
        }

        /*public IActionResult Edit(int? id)
        {
            if (id is null || id is 0)
            {
                return NotFound();
            }

            Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyFromDb is null)
            {
                return NotFound();
            }

            return View(CompanyFromDb);
        }

        [HttpPost]
        public IActionResult Edit(CompanyVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(obj.Company);
                _unitOfWork.Save();
                TempData["success"] = "Company Updated Successfully!";
                return RedirectToAction("Index", "Company");
            }
            return View();
        }*/

        /*public IActionResult Delete(int? id)
        {
            if (id is null || id is 0)
            {
                return NotFound();
            }

            Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyFromDb is null)
            {
                return NotFound();
            }

            return View(CompanyFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyFromDb is null)
            {
                return NotFound();
            }

            _unitOfWork.Company.Remove(CompanyFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Company Deleted Successfully!";

            return RedirectToAction("Index", "Company");
        }*/

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> CompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = CompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyTobeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            
            _unitOfWork.Company.Remove(CompanyTobeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully!" });
        }

        #endregion
    }
}
