using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionTrees.Sort;

namespace ExpressionTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            var testData = DataFactory.Create().ToList();
            Console.WriteLine("Overall data set:");
            foreach (var employee in testData)
            {
                Console.WriteLine("Found results: {0} - {1} {2}: {3}", employee.EmployeeId, employee.FirstName, employee.LastName, employee.Title);
            }

            Console.WriteLine("Query 1 - Sort all developers by Title");
            var criteria = new SortCriteria
            {
                Fields = new List<Field>
                {
                    new Field {FieldName = "Title", Direction = "asc"}
                }
            };
            RunTest(testData, criteria);

            Console.WriteLine("Query 2 - Select all developers by First Name then Last Name");
            criteria = new SortCriteria
            {
                Fields = new List<Field>
                {
                    new Field {FieldName = "FirstName", Direction = "asc"},
                    new Field {FieldName = "LastName", Direction = "desc"}
                }
            };
            RunTest(testData, criteria);

            Console.Read();
        }

        public static void RunTest(IEnumerable<Model> data, SortCriteria criteria)
        {
            Console.WriteLine("");
            foreach (var field in criteria.Fields)
            {
                Console.WriteLine($"Sort Criteria: Field = {field.FieldName}, Direction = {field.Direction}");
            }
            Console.Read();
            var results = Sort.Sort.SortAsync(data, criteria);
            foreach (var result in results.Result)
            {
                Console.WriteLine($"Found results: {result.EmployeeId} - {result.FirstName} {result.LastName}: {result.Title}");
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Read();
        }
    }
}
