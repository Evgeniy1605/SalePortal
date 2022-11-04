using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalePortal.DbConnection;
using SalePortal.Models;

namespace SalePortal.wwwroot
{
    public class CommodityController : Controller
    {
        private readonly SalePortalDbConnection _context;
        private readonly IWebHostEnvironment _environment;

        public CommodityController(SalePortalDbConnection context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: CommodityModels

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var salePortalDbConnection = _context.commodities.Include(c => c.Owner).Include(c => c.Type);
            return View(await salePortalDbConnection.ToListAsync());
        }

        // GET: CommodityModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.commodities == null)
            {
                return NotFound();
            }

            var commodityModel = await _context.commodities
                .Include(c => c.Owner)
                .Include(c => c.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commodityModel == null)
            {
                return NotFound();
            }

            return View(commodityModel);
        }

        // GET: CommodityModels/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["TypeId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: CommodityModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,OwnerId,PublicationDate,Description,TypeId,Image")] CommodityEntity commodityModel, IFormFile ImageFile)
        {
            // 
            var OwnerId = int.Parse(User.Claims.ToList()[0].ToString().Split(':')[2].Trim());
            commodityModel.OwnerId = OwnerId;
            DateTime dateTime = DateTime.Now;
            commodityModel.PublicationDate = dateTime;
            commodityModel.Image = " ";
            _context.Add(commodityModel);
            await _context.SaveChangesAsync();

            var commodityModelSaved = _context.commodities.SingleOrDefault(x => x == commodityModel);
            var ImageExtention = Path.GetExtension(ImageFile.FileName);


            if (ImageExtention == ".png" )
            {
                var path = Path.Combine(_environment.WebRootPath, "Images", commodityModelSaved.Id.ToString() + ImageExtention);
                using (var uploading = new FileStream(path, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(uploading);
                    commodityModelSaved.Image = path;
                }
            }
            else
            {
                commodityModelSaved.Image = " ";
            }
            _context.commodities.Update(commodityModelSaved);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            /*ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", commodityModel.OwnerId);
            ViewData["TypeId"] = new SelectList(_context.Categories, "Id", "Id", commodityModel.TypeId);
            return View(commodityModel);*/
        }

        // GET: CommodityModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.commodities == null)
            {
                return NotFound();
            }

            var commodityModel = await _context.commodities.FindAsync(id);
            if (commodityModel == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", commodityModel.OwnerId);
            ViewData["TypeId"] = new SelectList(_context.Categories, "Id", "Id", commodityModel.TypeId);
            return View(commodityModel);
        }

        // POST: CommodityModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,OwnerId,PublicationDate,Description,TypeId,Image")] CommodityEntity commodityModel)
        {
            if (id != commodityModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commodityModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommodityModelExists(commodityModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", commodityModel.OwnerId);
            ViewData["TypeId"] = new SelectList(_context.Categories, "Id", "Id", commodityModel.TypeId);
            return View(commodityModel);
        }

        // GET: CommodityModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.commodities == null)
            {
                return NotFound();
            }

            var commodityModel = await _context.commodities
                .Include(c => c.Owner)
                .Include(c => c.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commodityModel == null)
            {
                return NotFound();
            }

            return View(commodityModel);
        }

        // POST: CommodityModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.commodities == null)
            {
                return Problem("Entity set 'SalePortalDbConnection.commodities'  is null.");
            }
            var commodityModel = await _context.commodities.FindAsync(id);
            if (commodityModel != null)
            {
                _context.commodities.Remove(commodityModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommodityModelExists(int id)
        {
          return _context.commodities.Any(e => e.Id == id);
        }
    }
}
