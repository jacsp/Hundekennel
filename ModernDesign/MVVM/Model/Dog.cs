using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesign.MVVM.Model
{
    public class Dog
    {
        public string PedigreeNumber { get; set; }//
        public string Name { get; set; }//
        public DateTime DOB { get; set; }//
        public string? DadPedigreeNumber { get; set; }//
        public string? MomPedigreeNumber { get; set; }//
        public string Gender { get; set; }//
        public bool IsDead { get; set; }
        // Expanded dog attributes
        public string? ChipNumber { get; set; }//
        public string? DKKTitles { get; set; }//
        public string? Titles { get; set; }//
        public bool BreedingStatus { get; set; }
        public bool? MentalDescription { get; set; }
        public byte[]? Picture { get; set; }
        public string? HD { get; set; }//
        public string? AD { get; set; }//
        public string? HZ { get; set; }//
        public string? SP { get; set; }//
        public string? Color { get; set; }//
        public bool BreedingApproval { get; set; }

        public Dog? Father { get; set; }
        public Dog? Mother { get; set; }
        public List<Dog>? Parents { get; set; }

        public int? OwnerId { get; set; }
        public DogOwner? Owner { get; set; }





        public Dog() { }

        public Dog(string pedigreeNumber, string name, DateTime dOB, string dadPedigreeNumber, 
            string momPedigreeNumber, string gender, bool isDead, string chipNumber, 
            string dKKTitles, string titles, bool breedingStatus, bool mentalDescription, 
            byte[] picture, string hD, string aD, string hZ, string sP, string color, bool breedingApproval)
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
            HD = hD;
            AD = aD;
            HZ = hZ;
            SP = sP;
            Color = color;
            BreedingApproval = breedingApproval;
            OwnerId = 0;
            Owner = null;

        }
    }
}
