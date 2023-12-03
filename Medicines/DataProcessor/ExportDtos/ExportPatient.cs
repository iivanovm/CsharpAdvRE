using Medicines.Data.Models;
using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Medicines.Shared.GlobalConstants;

namespace Medicines.DataProcessor.ExportDtos;
[XmlType("Patient")]
public class ExportPatient
{
    [Required]
    [StringLength(PatientFullNameMax, MinimumLength = PatientFullNameMin)]
    [XmlElement("Name")]
    public string Name { get; set; } = null!;
    [Required]
    [XmlElement("AgeGroup")]
    public AgeGroup AgeGroup { get; set; }
    [Required]
    [XmlAttribute("Gender")]
    public Gender Gender { get; set; }
    [XmlArray("Medicines")]
    public ExportDtoPatientMedicament[] Medicines { get; set; } = null!;

}
