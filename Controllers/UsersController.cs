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
    public class UsersController : Controller
    {

        private readonly IUserHttpClient _userHttp;

        public UsersController( IUserHttpClient userHttp)
        {

            _userHttp = userHttp;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View( _userHttp.GetUsers());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _userHttp.GetUsers() == null)
            {
                return NotFound();
            }

            var userEntity = await _userHttp.GetUserByIdAsync(id);
            if (userEntity == null)
            {
                return NotFound();
            }

            return View(userEntity);
        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _userHttp.GetUsers() == null)
            {
                return NotFound();
            }

            var userEntity = await _userHttp.GetUserByIdAsync(id);
            if (userEntity == null)
            {
                return NotFound();
            }
            return View(userEntity);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SurName,EmailAddress,PhoneNumber,Password")] UserEntity userEntity)
        {
            if (id != userEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsPullSucceeded = false;
                try
                {
                    IsPullSucceeded = await _userHttp.PutUserAsync(id, userEntity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                if (IsPullSucceeded == false)
                {
                    return View("Error");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userEntity);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _userHttp.GetUsers() == null)
            {
                return NotFound();
            }

            var userEntity = await _userHttp.GetUserByIdAsync(id);
            if (userEntity == null)
            {
                return NotFound();
            }

            return View(userEntity);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_userHttp.GetUsers() == null)
            {
                return Problem("Entity set 'SalePortalDbConnection.Users'  is null.");
            }
            var userEntity = await _userHttp.GetUserByIdAsync(id);
            bool IsDeleted = false;
            if (userEntity != null)
            {
                IsDeleted = await _userHttp.DeleteUserAsync(id);
            }
            if (IsDeleted == false)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
