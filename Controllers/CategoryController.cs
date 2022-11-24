using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalePortal.Data;
using SalePortal.Entities;

namespace SalePortal.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryHttpClient _category;

        public CategoryController(ICategoryHttpClient category)
        {

            _category = category;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var categories = await _category.GetCategoriesAsync();
            return View(categories);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null )
            {
                return NotFound();
            }
            var categoryEntity = await _category.GetCategoryByIdAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            return View(categoryEntity);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CategoryEntity categoryEntity)
        {
            bool IsPostSecseeded = false;
            if (ModelState.IsValid)
            {
                IsPostSecseeded = await _category.PostCategoryAsync(categoryEntity);
                if (IsPostSecseeded == true)
                {
                    return RedirectToAction(nameof(Index));
                }        
            }
            return View(categoryEntity);
        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEntity = await _category.GetCategoryByIdAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }
            return View(categoryEntity);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryEntity categoryEntity)
        {
            if (id != categoryEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsPullSucceeded = false;
                try
                {
                    IsPullSucceeded = await _category.PutCategoryAsync(id,categoryEntity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                if (IsPullSucceeded == false) { return View("Error"); };
                return RedirectToAction(nameof(Index));
            }
            return View(categoryEntity);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var categoryEntity = await _category.GetCategoryByIdAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            return View(categoryEntity);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if ( await _category.GetCategoriesAsync() == null)
            {
                return Problem("Entity set 'SalePortalDbConnection.Categories'  is null.");
            }
            bool IsDeleted = false;
            var categoryEntity = await _category.GetCategoryByIdAsync(id);
            if (categoryEntity != null)
            {
                IsDeleted = await _category.DeleteCategoryAsync(id);
            }
            if(IsDeleted == false) { return View("Error"); };
            return RedirectToAction(nameof(Index));
        }

    }
}
