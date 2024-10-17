using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();

            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order can not be same.");
            }
            /*if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value.");
            }*/

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id is null || id is 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb is null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated Successfully!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id is 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb is null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb is null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(categoryFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully!";

            return RedirectToAction("Index", "Category");
        }
    }
}
