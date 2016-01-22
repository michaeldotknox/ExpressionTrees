using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionTrees
{
    public class Program
    {
        static void Main(string[] args)
        {
            var testData = CreateTestData().ToList();
            Console.WriteLine("Overall data set:");
            foreach (var employee in testData)
            {
                Console.WriteLine("Found results: {0} - {1} {2}: {3}", employee.EmployeeId, employee.FirstName, employee.LastName, employee.Title);
            }

            Console.WriteLine("Query 1 - Select all developers (one field)");
            var searchCriteria = new SearchCriteria
            {
                Fields = new List<Field>
                {
                    new Field {FieldName = "Title", Operator = "equal", Value = "Developer"}
                }
            };
            RunTest(testData, searchCriteria);

            Console.WriteLine("Query 2 - Select all developers named Michael (two fields)");
            searchCriteria = new SearchCriteria
            {
                Fields = new List<Field>
                {
                    new Field {FieldName = "Title", Operator = "equal", Value = "Developer"},
                    new Field {FieldName = "FirstName", Operator = "equal", Value = "Michael"}
                }
            };
            RunTest(testData, searchCriteria);


            Console.Read();

        }

        public static IEnumerable<SearchModel> CreateTestData()
        {
            return new List<SearchModel>
            {
                new SearchModel {EmployeeId = 1, FirstName = "Michael", LastName = "Knox", Title = "Developer"},
                new SearchModel {EmployeeId = 2, FirstName = "Michael", LastName = "Coker", Title = "Developer"},
                new SearchModel {EmployeeId = 3, FirstName = "Jeff", LastName = "Johnson", Title = "Developer"},
                new SearchModel {EmployeeId = 4, FirstName = "Daniel", LastName = "Phelps", Title = "Developer"},
                new SearchModel
                {
                    EmployeeId = 5,
                    FirstName = "Ponchai",
                    LastName = "Reainthong",
                    Title = "Developer"
                },
                new SearchModel {EmployeeId = 6, FirstName = "Ryan", LastName = "Church", Title = "Manager"},
                new SearchModel {EmployeeId = 7, FirstName = "Deryl", LastName = "Byrket", Title = "Manager"}
            };
        }

        public static void RunTest(IEnumerable<SearchModel> data, SearchCriteria criteria )
        {
            Console.WriteLine("");
            foreach (var field in criteria.Fields)
            {
                Console.WriteLine("Search Criteria: Field = {0}, Operator = {1}, Value = {2}", field.FieldName, field.Operator, field.Value );
            }
            Console.Read();
            var results = Search.SearchAsync(data, criteria);
            foreach (var result in results.Result)
            {
                Console.WriteLine("Found results: {0} - {1} {2}: {3}", result.EmployeeId, result.FirstName, result.LastName, result.Title);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Read();
        } 
    }
}

