using System.Collections.Generic;

namespace ExpressionTrees
{
    public static class DataFactory
    {
        public static IEnumerable<Model> Create()
        {
            return new List<Model>
            {
                new Model {EmployeeId = 1, FirstName = "Michael", LastName = "Knox", Title = "Developer"},
                new Model {EmployeeId = 2, FirstName = "Michael", LastName = "Coker", Title = "Developer"},
                new Model {EmployeeId = 3, FirstName = "Jeff", LastName = "Johnson", Title = "Developer"},
                new Model {EmployeeId = 4, FirstName = "Daniel", LastName = "Phelps", Title = "Developer"},
                new Model
                {
                    EmployeeId = 5,
                    FirstName = "Ponchai",
                    LastName = "Reainthong",
                    Title = "Developer"
                },
                new Model {EmployeeId = 6, FirstName = "Ryan", LastName = "Church", Title = "Manager"},
                new Model {EmployeeId = 7, FirstName = "Deryl", LastName = "Byrket", Title = "Manager"}
            };
        }
    }
}
