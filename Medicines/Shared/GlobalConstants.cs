using Medicines.Data.Models;

namespace Medicines.Shared
{
    public static class GlobalConstants
    {
        public const int PharmacyNameMin = 2;
        public const int PharmacyNameMax = 50;
        public const int PharmacyPhoneNumber = 14;
        public const string PharmacyPhoneNumberRegEx = @"\(\d{3}\) (\d{3}-\d{4})";

        public const int MedicineNameMin = 3;
        public const int MedicineNameMax = 150;
        public const int MedicineProducerMin = 3;
        public const int MedicineProducerMax = 100;
        public const double MedicinePriceMin = 0.01;
        public const double MedicinePriceMax = 1000.0;


        public const int PatientFullNameMin = 5;
        public const int PatientFullNameMax = 100;


        public const int genreMin = 0;
        public const int genreMax = 1;

        public const int AgeGroupMin = 0;
        public const int AgeGroupMax = 2;
        public const int categoryMin = 0;
        public const int categoryMax = 4;

        public const string DtFormat = "yyyy-MM-dd";

        public const string PharmacyXmlRootName= "Pharmacies";




    }
}
