using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Medicines.Shared.GlobalConstants;
namespace Medicines.DataProcessor.ExportDtos;

[XmlType("Medicine")]
public class ExportDtoPatientMedicament
{
    [Required]
    [StringLength(MedicineProducerMax, MinimumLength = MedicineNameMin)]
    [XmlElement("Name")]
    public string Name { get; set; } = null!;
    [Required]
    [Range(MedicinePriceMin, MedicinePriceMax)]
    [XmlElement("Price")]
    public string Price { get; set; }=null!;
    [Required]
    [XmlAttribute("Category")]
    public Category Category { get; set; }
    [Required]
    [StringLength(MedicineProducerMax, MinimumLength = MedicineProducerMax)]
    [XmlElement("Producer")]
    public string Producer { get; set; } = null!;
    [Required]
    [XmlElement("BestBefore")]
    public string BestBefore { get; set; }=null !;
   
}
