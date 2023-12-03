using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Medicines.Data.Models.Enums;
using static Medicines.Shared.GlobalConstants;
namespace Medicines.Data.Models;

public class Medicine
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(MedicineNameMax, MinimumLength = MedicineNameMin)]
    public string Name { get; set; } = null!;
    [Required]
    [Range(MedicinePriceMin,MedicinePriceMax)]
    public decimal Price { get; set; }
    [Required]
    public Category Category { get; set; }
    [Required]
    public DateTime ProductionDate { get; set; }
    [Required]
    public DateTime ExpiryDate { get; set; }
    [Required]
    [StringLength(MedicineProducerMax,MinimumLength =MedicineProducerMin)]
    public string Producer {  get; set; }=null!;
    [Required]
    public int PharmacyId {  get; set; }
    [Required]
    [ForeignKey(nameof(PharmacyId))]
    public virtual Pharmacy Pharmacy { get; set; } = null!;

    public virtual ICollection<PatientMedicine> PatientsMedicines { get; set; } = new List<PatientMedicine>();  
}
