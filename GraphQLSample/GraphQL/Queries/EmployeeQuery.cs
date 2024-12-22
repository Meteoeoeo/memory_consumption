using GraphQLSample.Data;
using GraphQLSample.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLSample.GraphQL.Queries
{
    public class EmployeeQuery
    {
        [UseOffsetPaging(IncludeTotalCount = true, MaxPageSize = 10000, DefaultPageSize = 10000)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Employee> GetEmployees([Service] EmployeeContext context)
        {
            var query = context.Employees.AsNoTracking();

            return query;
        }
    }
}
