using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Pro.Models
{
     public  class Course
    {
       public string Name { get; set; }

        public int CreditHours { get; set; }

        public Course(string name, int creditHours)
        {          
            Name = name;
            CreditHours = creditHours;
        }
    }

}
