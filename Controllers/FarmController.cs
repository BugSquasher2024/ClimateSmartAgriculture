using ClimateSmartAgriculture.Data;
using ClimateSmartAgriculture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClimateSmartAgriculture.Controllers
{
    public class FarmController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FarmController> _logger;

        public FarmController(ApplicationDbContext context, ILogger<FarmController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Farm
        public async Task<IActionResult> Index()
        {
            return View(await _context.Farms.ToListAsync());
        }

        // GET: Farm/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Farm/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmId,Location,Size,ClimateZone")] Farm farm)
        {
            _logger.LogInformation("Received Farm model: {@Farm}", farm);

            try
            {
                _context.Add(farm);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Farm successfully created.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating farm.");
            }

            return View(farm);
        }

        // GET: Farm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farm = await _context.Farms.FindAsync(id);
            if (farm == null)
            {
                return NotFound();
            }

            return View(farm);
        }

        // POST: Farm/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FarmId,Location,Size,ClimateZone")] Farm farm)
        {
            if (id != farm.FarmId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(farm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FarmExists(farm.FarmId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return View(farm);
        }

        // GET: Farm/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farm = await _context.Farms
                .FirstOrDefaultAsync(m => m.FarmId == id);
            if (farm == null)
            {
                return NotFound();
            }

            return View(farm);
        }

        // POST: Farm/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farm = await _context.Farms.FindAsync(id);
            if (farm == null)
            {
                return NotFound();
            }

            _context.Farms.Remove(farm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmExists(int id)
        {
            return _context.Farms.Any(e => e.FarmId == id);
        }
    }
}
