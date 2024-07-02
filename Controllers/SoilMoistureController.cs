using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClimateSmartAgriculture.Data;
using ClimateSmartAgriculture.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace ClimateSmartAgriculture.Controllers
{
    public class SoilMoistureController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SoilMoistureController> _logger;

        public SoilMoistureController(ApplicationDbContext context, ILogger<SoilMoistureController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: SoilMoisture
        public async Task<IActionResult> Index()
        {
            var soilMoistureData = _context.SoilMoisture.Include(s => s.Farm);
            return View(await soilMoistureData.ToListAsync());
        }

        // GET: SoilMoisture/Create
        public IActionResult Create()
        {
            ViewData["FarmId"] = new SelectList(_context.Farms, "FarmId", "Location");
            return View();
        }

        // POST: SoilMoisture/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoistureId,FarmId,Date,Level")] SoilMoisture soilMoisture)
        {
            _logger.LogInformation("Received SoilMoisture model: {@SoilMoisture}", soilMoisture);

            try
            {
                _context.Add(soilMoisture);
                await _context.SaveChangesAsync();
                _logger.LogInformation("SoilMoisture data successfully created.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating soil moisture data.");
            }

            ViewData["FarmId"] = new SelectList(_context.Farms, "FarmId", "Location", soilMoisture.FarmId);
            return View(soilMoisture);
        }

        // GET: SoilMoisture/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soilMoisture = await _context.SoilMoisture.FindAsync(id);
            if (soilMoisture == null)
            {
                return NotFound();
            }

            ViewData["FarmId"] = new SelectList(_context.Farms, "FarmId", "Location", soilMoisture.FarmId);
            return View(soilMoisture);
        }

        // POST: SoilMoisture/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MoistureId,FarmId,Date,Level")] SoilMoisture soilMoisture)
        {
            if (id != soilMoisture.MoistureId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(soilMoisture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoilMoistureExists(soilMoisture.MoistureId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            ViewData["FarmId"] = new SelectList(_context.Farms, "FarmId", "Location", soilMoisture.FarmId);
            return View(soilMoisture);
        }

        // GET: SoilMoisture/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soilMoisture = await _context.SoilMoisture
                .Include(s => s.Farm)
                .FirstOrDefaultAsync(m => m.MoistureId == id);
            if (soilMoisture == null)
            {
                return NotFound();
            }

            return View(soilMoisture);
        }

        // POST: SoilMoisture/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soilMoisture = await _context.SoilMoisture.FindAsync(id);
            if (soilMoisture == null)
            {
                return NotFound();
            }

            _context.SoilMoisture.Remove(soilMoisture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoilMoistureExists(int id)
        {
            return _context.SoilMoisture.Any(e => e.MoistureId == id);
        }
    }
}
