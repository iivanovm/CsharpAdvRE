using System.ComponentModel.DataAnnotations;
using Medicines.Data.Models.Enums;
using static Medicines.Shared.GlobalConstants;
namespace Medicines.Data.Models;

public class Patient
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(PatientFullNameMax,MinimumLength =PatientFullNameMin)]
    public string FullName { get; set; }=null!;
    [Required]
    public AgeGroup AgeGroup { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public virtual ICollection<PatientMedicine> PatientsMedicines { get; set; }= new List<PatientMedicine>();
}