using System;
using System.Collections.Generic;
using System.Linq;
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

        public Dog dog { get; set; }
        public List<Dog> dogs { get; set; }

        public DogOwner()
        {

        }
        
        public DogOwner(int dogownerid, string name, string address, string postalCode, string city, string phone, string email)
        {
            DogOwnerId= dogownerid;
            Name = name;
            Address = address;
            PostalCode = postalCode;
            City = city;
            Phone = phone;
            Email = email;
        }
    }
}
