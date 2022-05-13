using System;
using System.Collections.Generic;

#nullable disable

namespace ArmyTech.Models
{
    public partial class City
    {
        public City()
        {
            Branches = new HashSet<Branch>();
        }

        public int Id { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
    }
}
