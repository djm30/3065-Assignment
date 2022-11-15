using System;
using Microsoft.Data.SqlClient;

namespace qubgrademe_statefulsaving;

public class SqlConnectionService
{
    
    public static SqlConnection GetConnection()
    {
        SqlConnection connection = new SqlConnection(GetSqlConnectionString());
        return connection;
    }
    
    private static string GetSqlConnectionString()
    {
        // Prepare the connection string to Azure SQL Database.
        var sqlConnectionSB = new SqlConnectionStringBuilder 
        {
            // Change these values to your values.
            DataSource           = "tcp:csc3065-assignment2.database.windows.net", //["Server"]
            InitialCatalog       = "Assigment2-StatefulSaving",                                       //["Database"]
            UserID               = "assignment2",                                          // "@yourservername"  as suffix sometimes.
            Password             = Environment.GetEnvironmentVariable("SQL_PASSWORD"), //["Password"]
            // Adjust these values if you like. (ADO.NET 4.5.1 or later.)
            ConnectRetryCount    = 3,
            ConnectRetryInterval = 10, // Seconds.
            // Leave these values as they are.
            IntegratedSecurity = false,
            Encrypt            = true,
            ConnectTimeout     = 30
        };
        return sqlConnectionSB.ToString();
    }
}