namespace Medicines.DataProcessor
{
    using static Medicines.Shared.GlobalConstants;
    using Medicines.Data;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ExportDtos;
    using Medicines.Utils;
    using System.Globalization;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            DateTime pDte;

            var isvalidD = DateTime.TryParseExact(date, DtFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out pDte);

            var patients = context.Patients.
                Where(p => p.PatientsMedicines.Any(pt => pt.Medicine.ProductionDate > pDte))
                .ToArray()
                     .Select(t => new ExportPatient() { 
                    Name = t.FullName,
                    AgeGroup = t.AgeGroup,
                    Gender = t.Gender,
                    Medicines = t.PatientsMedicines
                                 .Where(t=>t.Medicine.ProductionDate>pDte)
                                 .OrderByDescending(t=>t.Medicine.ExpiryDate)
                                 .ThenBy(t=>t.Medicine.Price)
                                 .ToArray()
                                 .Select(
                                     m => new ExportDtoPatientMedicament()
                                     {
                                         Name = m.Medicine.Name,
                                         Price = m.Medicine.Price.ToString("F2"),
                                         BestBefore = m.Medicine.ExpiryDate.ToString(DtFormat,CultureInfo.InvariantCulture),
                                         Producer = m.Medicine.Producer,
                                         Category=m.Medicine.Category,

                                     })
                                    .ToArray(),
                })
                .OrderByDescending(t=>t.Medicines.Length)
                .ThenBy(t=>t.Name)
                .ToArray();


            return patients.SerializeToXml("Patients");

        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {
           var medicament=context.Medicines.
                Where(m=>m.Pharmacy.IsNonStop==true && (int)m.Category==medicineCategory)
                .OrderBy(m=>m.Price)
                .ThenBy(m=>m.Name)
                .Select(m => new
                {
                    Name=m.Name,
                    Price=m.Price.ToString("F2"),
                    Pharmacy = new
                    {
                        Name= m.Pharmacy.Name,
                        PhoneNumber=m.Pharmacy.PhoneNumber
                    }
                    
                })
                .ToArray();



            return medicament.SerializaToJSON() ;

                
        }
    }
}
