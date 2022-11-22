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
using SalePortal.Entities;
using SalePortal.Models;


namespace SalePortal.wwwroot
{
    public class CommodityController : Controller
    {
        private readonly SalePortalDbConnection _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        private readonly ILibrary _library;
        private readonly ICommodityHttpClient _commodityHttpClient;
        public CommodityController(SalePortalDbConnection context, IWebHostEnvironment environment, IMapper mapper, ILibrary library, ICommodityHttpClient commodityHttpClient)
        {
            _commodityHttpClient= commodityHttpClient;
            _mapper = mapper;
            _context = context;
            _environment = environment;
            _library = library;
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
            var OwnerId = _library.GetUserId(User.Claims.ToList());
            commodityModel.OwnerId = OwnerId;
            DateTime dateTime = DateTime.Now;
            commodityModel.PublicationDate = dateTime;
            commodityModel.Image = " ";
            var IsSucceeded = await _commodityHttpClient.PostCommoditiesAsync(commodityModel, OwnerId);
            if (IsSucceeded == false)
            {
                return View("Error");
            }

            if (ImageFile != null)
            {
                var ImageExtention = Path.GetExtension(ImageFile.FileName);

                if (ImageExtention == ".png")
                {

                    var commodities = await _commodityHttpClient.GetCommoditiesAsync();
                    var commodityModelSaved = _context.commodities.SingleOrDefault(x => x.PublicationDate == commodityModel.PublicationDate && x.OwnerId == commodityModel.OwnerId);

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
            if (id == null )
            {
                return NotFound();
            }

            var commodityModel = await _commodityHttpClient.GetCommodityByIdAsync(id);
            int userId = 0;
            if (!User.IsInRole("Admin"))
            {
                userId =_library.GetUserId(User.Claims.ToList());
            }
            if (userId != commodityModel.OwnerId && !User.IsInRole("Admin"))
            {
                return View("Error");
            }
            if (commodityModel == null)
            {
                return NotFound();
            }

            if (commodityModel != null)
            {
                if (commodityModel.Image !=" ")
                {
                    System.IO.File.Delete(commodityModel.Image);
                }
                var IsRemovalSucceeded = await _commodityHttpClient.DeleteCommodityAsync(commodityModel.Id);
                if(IsRemovalSucceeded == false) { return NotFound(); }
            }

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("AdminPage", "Identity", new { aria = "" });
            }
            return RedirectToAction("UserPage", "Identity", new { aria = "" });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            int userId = _library.GetUserId(User.Claims.ToList());
            if (id == null || _context.commodities == null)
            {
                return NotFound();
            }

            var commodityModel = await _context.commodities.FindAsync(id);
            if (commodityModel == null)
            {
                return NotFound();
            }
            
            if (userId != commodityModel.OwnerId && !User.IsInRole("Admin"))
            {
                return BadRequest();
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
                    if (entity.Image != " ")
                    {
                        System.IO.File.Delete(entity.Image);
                    }  
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
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("AdminPage", "Identity", new { aria = "" });
            }
            return RedirectToAction("UserPage", "Identity", new { aria = "" });
           
        }
    }
}
