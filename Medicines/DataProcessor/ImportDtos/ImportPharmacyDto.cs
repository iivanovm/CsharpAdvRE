
using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Medicines.Shared.GlobalConstants;
namespace Medicines.DataProcessor.ImportDtos;

[XmlType("Pharmacy")]
public class ImportPharmacyDto
{
    [Required]
    [StringLength(PharmacyNameMax, MinimumLength = PharmacyNameMin)]
    [XmlElement("Name")]
    public string Name { get; set; } = null!;
    [Required]
    [RegularExpression(PharmacyPhoneNumberRegEx)]
    [StringLength(PharmacyPhoneNumber)]
    [XmlElement("PhoneNumber")]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    [XmlAttribute("non-stop")]

    public string IsNonStop { get; set; } = null!;
    
    public ImportPharmacyMedicineDto[] Medicines {  get; set; }=null!;
}
