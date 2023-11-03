using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesign.MVVM.Model
{
    public class DogOwner
    {
        private string Name;
        private string Address;
        private string ZipCode;
        private string City;
        private string Phone;
        private string Email;


        public DogOwner(string name, string address, string zipCode, string city, string phone, string email)
        {
            Name = name;
            Address = address;
            ZipCode = zipCode;
            City = city;
            Phone = phone;
            Email = email;
        }
    }
}
