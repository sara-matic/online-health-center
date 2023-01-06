using EmployeeInformation.Entities;
using MongoDB.Driver;

namespace EmployeeInformation.Data
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
                    Id = "602d2149e773f2a3990b47f5",
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
                    Id = "602d2149e773f2a3990b47f6",
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
                    Id = "602d2149e773f2a3990b47f7",
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
                    Id = "602d2149e773f2a3990b47f8",
                    FirstName = "Edward",
                    LastName = "Fry",
                    MedicalSpecialty = "Cardiology",
                    Title = "Specialist",
                    Biography = "Edward Fry, MD, FACC, attended medical school at Washington University School of Medicine in St. Louis and completed his residency in internal medicine at Barnes-Jewish Hospital. He completed a two-year cardiovascular research fellowship focused on pharmacokinetics/pharmacodynamics of native and genetically modified plasminogen activators. He also completed a general cardiology fellowship at Washington University, where he then served as assistant professor and medical director of the cardiac transplant program before completing an interventional cardiology fellowship at Ascension St. Vincent Hospital – Indianapolis.",
                    ImageFile = "edward-fry.png",
                    Mark = 0
                },

                 new Doctor
                {
                    Id = "602d2149e773f2a3990b47f9",
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
                    Id = "602d2149e773f2a3990b47fa",
                    FirstName = "Anna",
                    LastName = "Green",
                    ImageFile = "anna-green.png"
                },

                new Nurse
                {
                    Id = "602d2149e773f2a3990b47f1",
                    FirstName = "Rachel",
                    LastName = "Gray",
                    ImageFile = "rachel-gray.png"
                },

                new Nurse
                {
                    Id = "602d2149e773f2a3990b47f2",
                    FirstName = "Page",
                    LastName = "Jones",
                    ImageFile = "page-jones.png"
                }
            };
        }
    }
}
