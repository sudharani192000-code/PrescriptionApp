using System;
using System.ComponentModel.DataAnnotations;

namespace PrescriptionApp.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        [Required(ErrorMessage = "Medication Name is required")]
        public string MedicationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fill Status is required")]
        public string FillStatus { get; set; } = "New";

        [Required(ErrorMessage = "Cost is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost must be positive")]
        public double Cost { get; set; }

        [Required(ErrorMessage = "Request Time is required")]
        public DateTime RequestTime { get; set; }

        // simple slug appended to URL for informational purposes
        public string Slug => $"{PrescriptionId}-{(MedicationName ?? string.Empty).ToLower().Replace(' ', '-')}";
    }
}
