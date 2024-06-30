using ClimateSmartAgriculture.Data;
using ClimateSmartAgriculture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClimateSmartAgriculture.Controllers
{
    public class CropController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CropController> _logger;

        public CropController(ApplicationDbContext context, ILogger<CropController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Crop
        public async Task<IActionResult> Index()
        {
            var crops = _context.Crops.Include(c => c.Farm);
            return View(await crops.ToListAsync());
        }

        // GET: Crop/Create
        public IActionResult Create()
        {
            if (!_context.Farms.Any())
            {
                ViewBag.Message = "No farms available. Please create a farm first.";
            }
            ViewBag.Farms = new SelectList(_context.Farms, "FarmId", "Location");
            return View();
        }

        // POST: Crop/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CropId,FarmId,CropType,PlantingDate,HarvestDate")] Crop crop)
        {
            _logger.LogInformation("Received Crop model: {@Crop}", crop);

            try
            {
                _context.Add(crop);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Crop successfully created.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating crop.");
            }

            ViewBag.Farms = new SelectList(_context.Farms, "FarmId", "Location", crop.FarmId);
            return View(crop);
        }

        // GET: Crop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crop = await _context.Crops.FindAsync(id);
            if (crop == null)
            {
                return NotFound();
            }

            ViewBag.Farms = new SelectList(_context.Farms, "FarmId", "Location", crop.FarmId);
            return View(crop);
        }

        // POST: Crop/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CropId,FarmId,CropType,PlantingDate,HarvestDate")] Crop crop)
        {
            if (id != crop.CropId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(crop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CropExists(crop.CropId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            ViewBag.Farms = new SelectList(_context.Farms, "FarmId", "Location", crop.FarmId);
            return View(crop);
        }

        // GET: Crop/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crop = await _context.Crops
                .Include(c => c.Farm)
                .FirstOrDefaultAsync(m => m.CropId == id);
            if (crop == null)
            {
                return NotFound();
            }

            return View(crop);
        }

        // POST: Crop/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crop = await _context.Crops.FindAsync(id);
            if (crop == null)
            {
                return NotFound();
            }

            _context.Crops.Remove(crop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CropExists(int id)
        {
            return _context.Crops.Any(e => e.CropId == id);
        }
    }
}
