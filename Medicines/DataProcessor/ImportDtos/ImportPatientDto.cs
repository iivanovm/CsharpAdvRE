
using Medicines.Data.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using static Medicines.Shared.GlobalConstants;
namespace Medicines.DataProcessor.ImportDtos;

public class ImportPatientDto
{
    [Required]
    [StringLength(PatientFullNameMax, MinimumLength = PatientFullNameMin)]
    [JsonProperty("FullName")]
    public string FullName { get; set; } = null!;
    [Required]
    [JsonProperty("AgeGroup")]
    [Range(AgeGroupMin,AgeGroupMax)]
    public int AgeGroup { get; set; }
    [Required]
    [JsonProperty("Gender")]
    [Range(genreMin,genreMax)]
    public int Gender { get; set; }


    public int[] Medicines { get; set; }=null!;

}
