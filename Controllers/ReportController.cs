using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClimateSmartAgriculture.Data;
using ClimateSmartAgricultureSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Linq;

namespace ClimateSmartAgricultureSystem.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Report
        public async Task<IActionResult> Index()
        {
            var reports = await _context.Reports.Include(r => r.User).ToListAsync();
            return View(reports);
        }

        // GET: Report/Create
        public IActionResult Create()
        {
            ViewData["Users"] = new SelectList(_context.Users, "UserId", "Name");
            return View();
        }

        // POST: Report/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,ReportType,ReportData,GeneratedOn")] Report report)
        {
            report.GeneratedOn = DateTime.Now;

            try
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error creating report: {ex.Message}");
            }

            ViewData["Users"] = new SelectList(_context.Users, "UserId", "Name", report.UserId);
            return View(report);
        }

        // GET: Report/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReportId == id);

            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Report/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReportId == id);

            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
