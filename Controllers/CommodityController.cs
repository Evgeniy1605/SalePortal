using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalePortal.Data;
using SalePortal.DbConnection;
using SalePortal.Models;


namespace SalePortal.wwwroot
{
    public class CommodityController : Controller
    {
        private readonly SalePortalDbConnection _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public CommodityController(SalePortalDbConnection context, IWebHostEnvironment environment, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _environment = environment;
        }



        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var salePortalDbConnection = _context.commodities.Include(c => c.Owner).Include(c => c.Type);
            return View(await salePortalDbConnection.ToListAsync());
        }


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
            CommodityViewModel commodityViewModel = _mapper.Map<CommodityViewModel>(commodityModel);
            return View(commodityViewModel);
        }


        [Authorize]
        public IActionResult Create()
        {
            //ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["TypeId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,TypeId,Price")] CommodityInputModel model, IFormFile ImageFile)
        {
            model.Name = model.Name.ToLower();
            CommodityEntity commodityModel = _mapper.Map<CommodityEntity>(model);
            var OwnerId = Library.GetUserId(User.Claims.ToList());
            commodityModel.OwnerId = OwnerId;
            DateTime dateTime = DateTime.Now;
            commodityModel.PublicationDate = dateTime;
            commodityModel.Image = " ";
            _context.Add(commodityModel);
            await _context.SaveChangesAsync();

            if (ImageFile != null)
            {
                var ImageExtention = Path.GetExtension(ImageFile.FileName);

                if (ImageExtention == ".png")
                {
                    var commodityModelSaved = _context.commodities.SingleOrDefault(x => x == commodityModel);
                    var path = Path.Combine(_environment.WebRootPath, "Images", commodityModelSaved.Id.ToString() + ImageExtention);
                    using (var uploading = new FileStream(path, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(uploading);
                        commodityModelSaved.Image = path;
                        _context.commodities.Update(commodityModelSaved);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction("UserPage", "Identity", new { aria = "" });
        }

        [Authorize]
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

            int userId = Library.GetUserId(User.Claims.ToList());
            if (userId != commodityModel.OwnerId)
            {
                return View("Error");
            }
            if (commodityModel == null)
            {
                return NotFound();
            }
            commodityModel = await _context.commodities.FindAsync(id);
            
            if (commodityModel != null)
            {
                if (commodityModel.Image !=" ")
                {
                    System.IO.File.Delete(commodityModel.Image);
                }
                _context.commodities.Remove(commodityModel);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("UserPage", "Identity", new { aria = "" });
        }

        [Authorize]
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
           CommodityInputModel inputModel = _mapper.Map<CommodityInputModel>(commodityModel);
            return View(inputModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] CommodityInputModel inputModel, IFormFile ImageFile)
        {
            if (id != inputModel.Id)
            {
                return NotFound();
            }

            var entity = await _context.commodities.SingleOrDefaultAsync(x => x.Id == id);
            entity.Description = inputModel.Description;
            entity.Name = inputModel.Name.ToLower().Trim();
            entity.Price = inputModel.Price;
            if (ImageFile != null)
            {
                var ImageExtention = Path.GetExtension(ImageFile.FileName);

                if (ImageExtention == ".png")
                {
                    System.IO.File.Delete(entity.Image);

                    var path = Path.Combine(_environment.WebRootPath, "Images", entity.Id.ToString() + ImageExtention);
                    using (var uploading = new FileStream(path, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(uploading);
                        entity.Image = path;
                        
                    }
                }
            }

            _context.commodities.Update(entity);

            await _context.SaveChangesAsync();
            
            return RedirectToAction("UserPage", "Identity", new { aria = "" });
           
        }
    }
}
