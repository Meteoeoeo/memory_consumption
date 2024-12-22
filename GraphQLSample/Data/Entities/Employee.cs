namespace GraphQLSample.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public DateTime HireDate { get; set; }
    }

}
