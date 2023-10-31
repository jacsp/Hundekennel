using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesign.MVVM.Model
{
    public class Dog
    {
        public string PedigreeNumber { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string DadPedigreeNumber { get; set; }
        public string MomPedigreeNumber { get; set; }
        public string Gender { get; set; }
        public bool IsDead { get; set; }
        // Expanded dog attributes
        public string ChipNumber { get; set; }
        public string DKKTitles { get; set; }
        public string Titles { get; set; }
        public bool BreedingStatus { get; set; }
        public bool MentalDescription { get; set; }
        public BinaryData Picture { get; set; }
        public Illness Health { get; set; }
        public ColorType Color { get; set; }
        public BreedingApprovalType BreedingApproval { get; set; }

        public Dog(string pedigreeNumber, string name, DateTime dOB, string dadPedigreeNumber, 
            string momPedigreeNumber, string gender, bool isDead, string chipNumber, string dKKTitles, 
            string titles, bool breedingStatus, bool mentalDescription, BinaryData picture,
            Illness health, ColorType color, BreedingApprovalType breedingApproval)
        {
            PedigreeNumber = pedigreeNumber;
            Name = name;
            DOB = dOB;
            DadPedigreeNumber = dadPedigreeNumber;
            MomPedigreeNumber = momPedigreeNumber;
            Gender = gender;
            IsDead = isDead;
            ChipNumber = chipNumber;
            DKKTitles = dKKTitles;
            Titles = titles;
            BreedingStatus = breedingStatus;
            MentalDescription = mentalDescription;
            Picture = picture;
            Health = health;
            Color = color;
            BreedingApproval = breedingApproval;
        }

        public Dog(string pedigreeNumber,
                   string name,
                   DateTime dOB,
                   string dadPedigreeNumber,
                   string momPedigreeNumber,
                   string gender,
                   bool isDead)
        {
            PedigreeNumber = pedigreeNumber;
            Name = name;
            DOB = dOB;
            DadPedigreeNumber = dadPedigreeNumber;
            MomPedigreeNumber = momPedigreeNumber;
            Gender = gender;
            IsDead = isDead;
        }
    }
}
