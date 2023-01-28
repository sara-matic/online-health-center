using EmployeeInformation.Common.Entities;
using MongoDB.Driver;

namespace EmployeeInformation.Common.Data
{
    public class EmployeeInformationContextSeed
    {
        public static void SeedDoctor(IMongoCollection<Doctor> doctorCollection)
        {
            var exist = doctorCollection.Find(p => true).Any();
            if (!exist)
            {
                doctorCollection.InsertMany(DoctorPreconfigured());
            }
        }
        public static void SeedNurse(IMongoCollection<Nurse> nurseCollection)
        {
            var exist = nurseCollection.Find(p => true).Any();
            if (!exist)
            {
                nurseCollection.InsertMany(NursePreconfigured());
            }
        }
        private static IEnumerable<Doctor> DoctorPreconfigured()
        {
            return new List<Doctor>()
            {
                new Doctor
                {
                    Id = Guid.Parse("bf248cb4-863d-4858-b551-54a1e5fba068"),
                    FirstName = "Alan",
                    LastName = "Stern",
                    MedicalSpecialty = "Cardiology",
                    Title = "Specialist",
                    Biography = "Dr. Alan Stern was born in DuBois, Pennsylvania and is a graduate of Villanova\r\nUniversity. He obtained his medical degree at Thomas Jefferson University in\r\nPhiladelphia. His residency was at Thomas Jefferson and its affiliated Wills Eye\r\nHospital, and he completed his training with fellowships at the University of\r\nConnecticut in cataract and corneal surgery.",
                    ImageFile = "alan-stern.png",
                    Mark = 0
                },
                

                new Doctor
                {
                    Id = Guid.Parse("d483510e-a3c2-4d00-8d6a-de860ab61deb"),
                    FirstName = "David",
                    LastName = "Sowa",
                    MedicalSpecialty = "Gynecology",
                    Title = "Specialist",
                    Biography = "Dr. David Sowa is an established and highly skilled physician with over 25 years of\r\nexperience in obstetrics and gynecology. He is well regarded in the central\r\nConnecticut community, earning numerous accolades for his quality and patient-\r\ncentered care. In 2014, he was named a Top Doctor among the Southington, Bristol,\r\nand Plainville areas by The Observer’s Reader’s Choice Awards. He also received\r\nCompassionate Doctor Recognition for four consecutive years and the Patient’s\r\nChoice Award for seven years.",
                    ImageFile = "david-sowa.png",
                    Mark = 0
                },
                
                new Doctor
                {
                    Id = Guid.Parse("c5f4078f-67ae-4588-a60b-63a7ad56196e"),
                    FirstName = "George",
                    LastName = "Green",
                    MedicalSpecialty = "Pulmonology",
                    Title = "Resident",
                    Biography = "George Green, MD, FCCP, is a graduate of George Washington University Medical School. He trained in internal medicine at the University of Oregon Health Sciences (UOHS) and pulmonary critical care medicine at UOHS and the University of Southern California.",
                    ImageFile = "george-green.png",
                    Mark = 0
                },
                
                 new Doctor
                {
                    Id = Guid.Parse("35fa1299-dd73-43dd-860f-1c876b283bef"),
                    FirstName = "Hadley",
                    LastName = "Wilson",
                    MedicalSpecialty = "Cardiology",
                    Title = "Medical director",
                    Biography = "Hadley Wilson, MD, FACC, is an interventional cardiologist and executive vice chair at Sanger Heart and Vascular Institute in North Carolina, where he previously served as chief of cardiology for more than 13 years. He has published more than 75 articles with interests spanning STEMI systems of care, stent technologies and devices for coronary intervention, left main stenting, chronic total occlusions, anticoagulation and antiplatelet therapies, structural and valvular heart disease, appropriate public reporting of PCI outcomes and quality improvement projects for systems of care, and clinician well-being.\r\n\r\nRead More\r\n",
                    ImageFile = "hadley-wilson.png",
                    Mark = 0
                 }
                
            };
                
        }
        private static IEnumerable<Nurse> NursePreconfigured()
        {
            return new List<Nurse>
            {
                
                new Nurse
                {
                    Id = Guid.Parse("91bdb681-3c97-490d-9333-b670838abe52"),
                    FirstName = "Anna",
                    LastName = "Green",
                    ImageFile = "anna-green.png"
                },

                new Nurse
                {
                    Id = Guid.Parse("6fa5502f-db30-491f-a87a-573113096f09"),
                    FirstName = "Rachel",
                    LastName = "Gray",
                    ImageFile = "rachel-gray.png"
                }
            };
        }
    }
}
