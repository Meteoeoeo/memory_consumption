using GraphQLSample.Data.Entities;

namespace GraphQLSample.Data
{
    public class EmployeeSeeder
    {
        public void Seed(EmployeeContext context)
        {
            // if (!context.Employees.Any())
            {
                var random = new Random();
                var departments = new[] { "IT", "HR", "Finance", "Marketing", "Sales" };

                var employees = Enumerable.Range(1, 100_000).Select(i => new Employee
                {
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}",
                    Department = departments[random.Next(departments.Length)],
                    IsActive = random.Next(0, 2) == 1,
                    HireDate = DateTime.Now.AddDays(-random.Next(0, 3650))
                }).ToList();

                var batchSize = 1_000;
                for (int i = 0; i < employees.Count; i += batchSize)
                {
                    var batch = employees.Skip(i).Take(batchSize).ToList();
                    context.Employees.AddRange(batch);
                    context.SaveChanges();
                    context.ChangeTracker.Clear();
                }
            }
        }
    }
}
