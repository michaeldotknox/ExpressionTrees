using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionTrees.Search;

namespace ExpressionTrees
{
    public class Program
    {
        static void Main(string[] args)
        {
            var testData = DataFactory.Create().ToList();
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

        public static void RunTest(IEnumerable<Model> data, SearchCriteria criteria )
        {
            Console.WriteLine("");
            foreach (var field in criteria.Fields)
            {
                Console.WriteLine($"Search Criteria: Field = {field.FieldName}, Operator = {field.Operator}, Value = {field.Value}");
            }
            Console.Read();
            var results = Search.Search.SearchAsync(data, criteria);
            foreach (var result in results.Result)
            {
                Console.WriteLine($"Found results: {result.EmployeeId} - {result.FirstName} {result.LastName}: {result.Title}", result.EmployeeId, result.FirstName, result.LastName, result.Title);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Read();
        } 
    }
}

