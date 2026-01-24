namespace OOP_Pro.Models
{
     public  class Course
    {
       public string Name { get; set; }

        public int CreditHours { get; set; }
        public int NumberOfLeactures { get; set; } 

        public Course(string name, int creditHours,int numberOfLectures)
        {          
            Name = name;
            CreditHours = creditHours;
            NumberOfLeactures = numberOfLectures;
        }
    }

}
