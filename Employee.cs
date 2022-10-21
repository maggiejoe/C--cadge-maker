namespace CatWorx.BadgeMaker
{
    class Employee
    {
        public string FirstName;
        public string LastName;
        public int Id;
        public string PhotoUrl;
        public Employee(string firstName, string lastName, int id, string photoUrl) {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            PhotoUrl = photoUrl;
        }
        
        // Get the first and last name of the employee
        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }

        // Get the employee Id
        public int GetId()
        {
            return Id;
        }

        // Get the employee photo Url
        public string GetPhotoUrl()
        {
            return PhotoUrl;
        }
    }
}