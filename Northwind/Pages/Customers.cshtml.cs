
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace Northwind.Pages;

public class CustomersModel : PageModel
{
    public List<Customer> Customers { get; set; } = new();

    public void OnGet()
    {
        using var connection = new SqliteConnection(
            "Data Source=../northwind.db;Mode=ReadOnly");

        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText = """
            SELECT CustomerID, CompanyName, ContactName, Country
            FROM Customers
            ORDER BY CompanyName;
            """;

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Customers.Add(new Customer
            {
                CustomerID = reader["CustomerID"]?.ToString() ?? "",
                CompanyName = reader["CompanyName"]?.ToString() ?? "",
                ContactName = reader["ContactName"]?.ToString() ?? "",
                Country = reader["Country"]?.ToString() ?? ""
            });
        }
    }
}

public class Customer
{
    public string CustomerID { get; set; } = "";
    public string CompanyName { get; set; } = "";
    public string ContactName { get; set; } = "";
    public string Country { get; set; } = "";
}
