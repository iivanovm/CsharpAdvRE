namespace Medicines.DataProcessor
{
    using static Medicines.Shared.GlobalConstants;
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ImportDtos;
    using Medicines.Utils;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            List<ImportPatientDto> patientDtos = jsonString.DeserializeFromJson<List<ImportPatientDto>>();  
            List<Patient> patients = new List<Patient>();

            StringBuilder sb = new StringBuilder(); 

            foreach(ImportPatientDto patientDto in patientDtos)
            {
                if (!IsValid(patientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Patient patient = new Patient()
                {
                    FullName = patientDto.FullName,
                    AgeGroup = (AgeGroup)patientDto.AgeGroup,
                    Gender = (Gender)patientDto.Gender

                };

                foreach ( int medIds in patientDto.Medicines)
                {
                    bool isExist = patient.PatientsMedicines.Any(p=>p.MedicineId==medIds);
                    if( isExist)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    patient.PatientsMedicines.Add(new PatientMedicine()
                    {
                        Patient = patient,
                        MedicineId = medIds
                    });
                }

                patients.Add(patient);
                sb.AppendLine(string.Format(SuccessfullyImportedPatient,patient.FullName,patient.PatientsMedicines.Count()));
            }
            context.Patients.AddRange(patients);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            List<ImportPharmacyDto> pharmacyDtos = xmlString.DeserializeFromXml<List<ImportPharmacyDto>>(PharmacyXmlRootName);

            List<Pharmacy> pharmacies = new List<Pharmacy>();

            StringBuilder sb = new StringBuilder();

            foreach(ImportPharmacyDto pharmacyDto in pharmacyDtos)
            {
                if(!IsValid(pharmacyDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                if (pharmacyDto.IsNonStop.ToLower()!="true"
                    && pharmacyDto.IsNonStop.ToLower()!="false")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Pharmacy pharmacy = new Pharmacy()
                {
                    IsNonStop = bool.Parse(pharmacyDto.IsNonStop),
                    Name = pharmacyDto.Name,
                    PhoneNumber = pharmacyDto.PhoneNumber,

                };

                foreach(ImportPharmacyMedicineDto medicineDto in pharmacyDto.Medicines)
                {
                    if(!IsValid(medicineDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var producer = medicineDto.Producer;
                    if (producer == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime prodDate;
                    DateTime expDate;
                    bool validPdate=DateTime.TryParseExact(medicineDto.ProductionDate, DtFormat,CultureInfo.InvariantCulture,DateTimeStyles.None, out prodDate);
                    bool validEdate=DateTime.TryParseExact(medicineDto.ExpiryDate, DtFormat,CultureInfo.InvariantCulture, DateTimeStyles.None, out expDate);    


                    if(prodDate>=expDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var isExistMedicine = pharmacy.Medicines.Any(m => m.Name == medicineDto.Name && m.Producer == medicineDto.Producer);
                    if (isExistMedicine)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Medicine medicine = new Medicine()
                    {
                        Name = medicineDto.Name,
                        Price = medicineDto.Price,
                        ProductionDate = prodDate,
                        ExpiryDate = expDate,
                        Producer = medicineDto.Producer,
                        Category=(Category)medicineDto.Category,
                        
                    };

                    pharmacy.Medicines.Add(medicine);
                }

                pharmacies.Add(pharmacy);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy,pharmacy.Name,pharmacy.Medicines.Count));
            }
            context.Pharmacies.AddRange(pharmacies);    
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
