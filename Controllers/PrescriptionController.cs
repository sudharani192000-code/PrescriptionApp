using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrescriptionApp.Data;
using PrescriptionApp.Models;

namespace PrescriptionApp.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Dropdown helper
        private void PopulateFillStatusDropdown(string? selected = null)
        {
            var statuses = new List<string> { "New", "Filled", "Pending" };
            ViewBag.FillStatus = new SelectList(statuses, selected ?? "New");
        }

        // ✅ Create (GET)
        [HttpGet]
        public IActionResult Create()
        {
            PopulateFillStatusDropdown();
            return View("AddEdit", new Prescription
            {
                RequestTime = DateTime.Now
            });
        }

        // ✅ Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                prescription.RequestTime = DateTime.Now;
                _context.Add(prescription);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            PopulateFillStatusDropdown(prescription.FillStatus);
            return View("AddEdit", prescription);
        }

        // ✅ Edit (GET)
        [HttpGet("prescription/edit/{id:int}.{slug?}")]
        public async Task<IActionResult> Edit(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
                return RedirectToAction("Index", "Home");

            PopulateFillStatusDropdown(prescription.FillStatus);
            return View("AddEdit", prescription);
        }

        // ✅ Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _context.Update(prescription);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            PopulateFillStatusDropdown(prescription.FillStatus);
            return View("AddEdit", prescription);
        }

        // ✅ Delete (GET)
        [HttpGet("prescription/delete/{id:int}.{slug?}")]
        public async Task<IActionResult> Delete(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
                return RedirectToAction("Index", "Home");

            return View(prescription);
        }

        // ✅ Delete (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int prescriptionId)
        {
            var prescription = await _context.Prescriptions.FindAsync(prescriptionId);
            if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
