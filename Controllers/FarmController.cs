using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClimateSmartAgriculture.Data;
using ClimateSmartAgriculture.Models;
using Microsoft.Extensions.Logging;

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
            _logger.LogInformation("Accessed Farm Index.");
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
        public async Task<IActionResult> Create([Bind("FarmId,UserId,Location,Size,ClimateZone")] Farm farm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("FarmId,UserId,Location,Size,ClimateZone")] Farm farm)
        {
            if (id != farm.FarmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farm);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(farm);
        }

        // GET: Farm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Attempting to delete farm with ID {id}.");
            //var farm = await _context.Farms.FirstOrDefaultAsync(m => m.FarmId == id);
            var farm = await _context.Farms.FindAsync(id);
            if (farm == null)
            {
                _logger.LogWarning($"Farm with ID {id} not found.");
                return NotFound();
            }
            //_context.Farms.Remove(farm);
            //await _context.SaveChangesAsync();
            //_logger.LogInformation($"Deleted farm with ID {id}.");
            //return RedirectToAction(nameof(Index));
            return View(farm);
        }

        private bool FarmExists(int id)
        {
            return _context.Farms.Any(e => e.FarmId == id);
        }

        // POST: Farm/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var farm = await _context.Farms.FindAsync(id);
        //    _context.Farms.Remove(farm);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
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
        //private bool FarmExists(int id)
        //{
        //    return _context.Farms.Any(e => e.FarmId == id);
        //}
    }
}
