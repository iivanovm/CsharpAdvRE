using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Medicines.Shared.GlobalConstants;
namespace Medicines.DataProcessor.ImportDtos;

[XmlType("Medicine")]
public class ImportPharmacyMedicineDto
{
    [Required]
    [StringLength(MedicineNameMax, MinimumLength = MedicineNameMin)]
    [XmlElement("Name")]
    public string Name { get; set; } = null!;
    [Required]
    [Range(MedicinePriceMin, MedicinePriceMax)]
    [XmlElement("Price")]
    public decimal Price { get; set; }
    [Required]
    [XmlAttribute("category")]
    [Range(categoryMin,categoryMax)]
    public int Category { get; set; }
    [Required]
    [XmlElement("ProductionDate")]
    public string ProductionDate { get; set; } = null!;
    [Required]
    [XmlElement("ExpiryDate")]
    public string  ExpiryDate { get; set; }=null!;
    [Required]
    [StringLength(MedicineProducerMax, MinimumLength = MedicineProducerMin)]
    [XmlElement("Producer")]
    public string Producer { get; set; } = null!;
}