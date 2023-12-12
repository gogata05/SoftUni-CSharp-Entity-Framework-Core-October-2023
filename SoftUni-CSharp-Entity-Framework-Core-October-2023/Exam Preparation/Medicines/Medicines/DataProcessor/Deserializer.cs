using Newtonsoft.Json;
namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models.Enums;
    using Medicines.Data.Models;
    using Medicines.DataProcessor.ImportDtos;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml;
    using Medicines.Utilities;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";
        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            ImportPatientDto[] patientDtos = 
                JsonConvert.DeserializeObject<ImportPatientDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Patient> patients = new List<Patient>();
            foreach (ImportPatientDto patientDto in patientDtos)
            {
                if (!IsValid(patientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Patient patient = new Patient()
                {
                    //"FullName": "Ivan Petrov",
                    //"AgeGroup": "1",
                    //"Gender": "0",
                    //"Medicines": [
                    //15,
                    //23
                    FullName = patientDto.FullName,
                    AgeGroup = (AgeGroup)patientDto.AgeGroup,
                    Gender = (Gender)patientDto.Gender,

                };
                foreach (var medicineDto in patientDto.Medicines)//to allow repeated Ids from all collections
                {
                    if (patient.PatientsMedicines.Any(x => x.MedicineId == medicineDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    patient.PatientsMedicines.Add(new PatientMedicine()
                    {
                        //Patient = patient,
                        //MedicineId = medicineDto.MedicineId
                        Patient = patient,
                        MedicineId = medicineDto
                    });
                }
                patients.Add(patient);
                sb.AppendLine(String.Format(SuccessfullyImportedPatient, patient.FullName, patient.PatientsMedicines.Count));
            }
            context.Patients.AddRange(patients);
            context.SaveChanges();
            return sb.ToString().TrimEnd();

        }
        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            ImportPharmacyDto[] pharmacyDtos =
                xmlHelper.Deserialize<ImportPharmacyDto[]>
                    (xmlString, "Pharmacies");//root
            StringBuilder sb = new StringBuilder();
            List<Pharmacy> pharmacies = new List<Pharmacy>();
            foreach (ImportPharmacyDto pharmacyDto in pharmacyDtos)
            {
                //var pharmacyUniqueNameCheck = pharmacys.FirstOrDefault(x => x.PharmacyName == pharmacyDto.PharmacyName);|| pharmacyUniqueNameCheck != null
                if (!IsValid(pharmacyDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Pharmacy pharmacy = new Pharmacy()
                {
                    //<Pharmacy non-stop="true">
                    //<Name>Vitality</Name>
                    //<PhoneNumber>(123) 456-7890</PhoneNumber>
                    //<Medicines>
                    //IsNonStop = pharmacyDto.IsNonStop == "true" ? true : false,
                    IsNonStop = bool.Parse(pharmacyDto.IsNonStop),
                    Name = pharmacyDto.Name,
                    PhoneNumber = pharmacyDto.PhoneNumber,
                };
                foreach (ImportPharmacyMedicineDto medicineDto in pharmacyDto.Medicines)
                {
                    if (!IsValid(medicineDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime? startDate = null;
                    if (!String.IsNullOrWhiteSpace(medicineDto.ProductionDate))
                    {
                        DateTime startDateValue;
                        bool isProductionDateValid = DateTime.TryParseExact(medicineDto.ProductionDate, "yyyy-MM-dd",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateValue);//make sure "dd/MM/yyyy" its the correct datetime format!
                        if (!isProductionDateValid)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }
                        startDate = startDateValue;
                    }
                    DateTime? endDate = null;
                    if (!String.IsNullOrWhiteSpace(medicineDto.ExpiryDate))
                    {
                        DateTime endDateValue;
                        bool isExpiryDateValid = DateTime.TryParseExact(medicineDto.ExpiryDate, "yyyy-MM-dd",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out endDateValue);//make sure "dd/MM/yyyy" its the correct datetime format!
                        if (!isExpiryDateValid)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }
                        endDate = endDateValue;
                    }
                    if (startDate >= endDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    //ContractStartDate = startDate,//for the class
                    //ContractEndDate = endDate ?? DateTime.MinValue,//for the class

                    if (pharmacy.Medicines.Any(x => x.Name == medicineDto.Name && x.Producer == medicineDto.Producer))//if there a medicines with the same name and producer
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Medicine medicine = new Medicine()
                    {
                        //<Medicine category="1">
                        //<Name>Ibuprofen</Name>
                        //<Price>8.50</Price>
                        //<ProductionDate>2022-02-10</ProductionDate>
                        //<ExpiryDate>2025-02-10</ExpiryDate>
                        //<Producer>ReliefMed Labs</Producer>
                        Category = (Category)medicineDto.Category,
                        Name = medicineDto.Name,
                        Price = (decimal)medicineDto.Price,
                        ProductionDate = startDate ?? DateTime.MinValue,
                        ExpiryDate = endDate ?? DateTime.MinValue,
                        Producer = medicineDto.Producer,
                    };
                    pharmacy.Medicines.Add(medicine);
                }
                pharmacies.Add(pharmacy);
                sb.AppendLine(String.Format(SuccessfullyImportedPharmacy, pharmacy.Name, pharmacy.Medicines.Count));
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
