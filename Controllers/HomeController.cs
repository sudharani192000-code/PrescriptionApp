using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrescriptionApp.Data;

namespace PrescriptionApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var prescriptions = await _db.Prescriptions
                                        .OrderByDescending(p => p.RequestTime)
                                        .ToListAsync();
            return View(prescriptions);
        }
    }
}
