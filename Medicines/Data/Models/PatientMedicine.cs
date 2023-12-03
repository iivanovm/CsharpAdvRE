using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicines.Data.Models
{
    public class PatientMedicine
    {
        [Required]
        public int PatientId {  get; set; }
        [Required]
        [ForeignKey(nameof(PatientId))]
        public virtual Patient Patient { get; set; } = null!;
        [Required]
        public int MedicineId {  get; set; }
        [Required]
        [ForeignKey(nameof(MedicineId))]
        public virtual Medicine Medicine { get; set; } = null!;
    }
}