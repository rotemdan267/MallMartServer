using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MallMartDB.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int StreetNo { get; set; }
        public char? Entrance { get; set; }
        public int? ApartmentNo { get; set; }
        public int? Postal { get; set; }

        public bool SetCity(string city)
        {
            Regex regex = new Regex(@"^[A-Z][a-zA-Z ]*[a-zA-Z]$");
            if (regex.IsMatch(city))
            {
                City = city;
                return true;
            }
            return false;
        }
        public bool SetStreet(string street)
        {
            Regex regex = new Regex(@"^[A-Z][a-zA-Z ]*[a-zA-Z]$");
            if (regex.IsMatch(street))
            {
                Street = street;
                return true;
            }
            return false;
        }
        public bool SetStreetNo(int streetNo)
        {
            if (streetNo > 0 && streetNo < 1000)
            {
                StreetNo = streetNo;
                return true;
            }
            return false;
        }
        public bool SetEntrance(char entrance)
        {
            if (entrance >= 'A' && entrance <= 'Z')
            {
                Entrance = entrance;
                return true;
            }
            return false;
        }
        public bool SetApartmentNo(int apartmentNo)
        {
            if (apartmentNo > 0 && apartmentNo < 1000)
            {
                ApartmentNo = apartmentNo;
                return true;
            }
            return false;
        }
        public bool SetPostal(int postal)
        {
            if ((postal >= 10000 && postal <= 99999) || (postal >= 1000000 && postal <= 9999999))
            {
                Postal = postal;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            string str = $"{Street} {StreetNo}, {City}";
            if (Entrance != null)
            {
                str += $", entrance: {Entrance}";
            }
            if (ApartmentNo != null)
            {
                str += $", apartment: {ApartmentNo}";
            }
            return str;
        }
    }
}
