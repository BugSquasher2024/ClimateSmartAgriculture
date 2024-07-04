using Microsoft.AspNetCore.Mvc;
using ClimateSmartAgricultureSystem.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClimateSmartAgriculture.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace ClimateSmartAgricultureSystem.Controllers
{
    public class IrrigationScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IrrigationScheduleController> _logger;

        public IrrigationScheduleController(ApplicationDbContext context, ILogger<IrrigationScheduleController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: IrrigationSchedule
        public async Task<IActionResult> Index()
        {
            var schedules = await _context.IrrigationSchedules.Include(s => s.Farm).ToListAsync();
            return View(schedules);
        }

        // GET: IrrigationSchedule/Create
        public IActionResult Create()
        {
            ViewData["Farms"] = new SelectList(_context.Farms, "FarmId", "Location");
            return View();
        }

        // POST: IrrigationSchedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmId,StartTime,EndTime,WaterAmount")] IrrigationSchedule schedule)
        {
            _logger.LogInformation("Received IrrigationSchedule model: {@IrrigationSchedule}", schedule);

            try
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                _logger.LogInformation("IrrigationSchedule data successfully created.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating irrigation schedule data.");
            }

            ViewData["Farms"] = new SelectList(_context.Farms, "FarmId", "Location", schedule.FarmId);
            return View(schedule);
        }

        // GET: IrrigationSchedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.IrrigationSchedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            ViewData["Farms"] = new SelectList(_context.Farms, "FarmId", "Location", schedule.FarmId);
            return View(schedule);
        }

        // POST: IrrigationSchedule/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,FarmId,StartTime,EndTime,WaterAmount")] IrrigationSchedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IrrigationScheduleExists(schedule.ScheduleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            ViewData["Farms"] = new SelectList(_context.Farms, "FarmId", "Location", schedule.FarmId);
            return View(schedule);
        }

        // GET: IrrigationSchedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.IrrigationSchedules
                .Include(s => s.Farm)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: IrrigationSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.IrrigationSchedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.IrrigationSchedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IrrigationScheduleExists(int id)
        {
            return _context.IrrigationSchedules.Any(e => e.ScheduleId == id);
        }
    }
}
