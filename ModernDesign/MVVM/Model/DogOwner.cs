using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesign.MVVM.Model
{
    public class DogOwner
    {
        public int DogOwnerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Dog Dog { get; set; }
        public List<Dog> Dogs { get; set; }

        public DogOwner() 
        {
            DogOwnerId = 0;
            Name = "";
            Address = "";
            PostalCode = "";
            City = "";
            Phone = "";
            Email = "";
            Dog = null;
            Dogs = new List<Dog>();
        }
        
        public DogOwner(string name, string address, string postalCode, string city, string phone, string email)
        {
            DogOwnerId = 0;
            Name = name;
            Address = address;
            PostalCode = postalCode;
            City = city;
            Phone = phone;
            Email = email;
            Dog = null;
            Dogs = new List<Dog>();
        }
    }
}
