using System.ComponentModel.DataAnnotations;
using static Medicines.Shared.GlobalConstants;
namespace Medicines.Data.Models;

public class Pharmacy
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(PharmacyNameMax, MinimumLength = PharmacyNameMin)]
    public string Name { get; set; } = null!;
    [Required]
    [RegularExpression(PharmacyPhoneNumberRegEx)]
    [StringLength(PharmacyPhoneNumber)]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    public bool IsNonStop {  get; set; }
    [Required]
    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
