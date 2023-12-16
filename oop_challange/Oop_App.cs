//using System;
//using System.Collections.Generic;


//namespace Oop_Challange
//{
//    class Oop_App
//    {
//        static void Main()
//        {
//            FormulatrixRepositoryManager repositoryManager = new FormulatrixRepositoryManager();
//            repositoryManager.Initialize();

//            // Register items
//            repositoryManager.Register("json1", "{ 'name': 'Anton', 'age': 30 }", 1);
//            repositoryManager.Register("xml1", "<person><name>Atnlie</name><age>21</age></person>", 2);
//            repositoryManager.Register("json2", "{ 'name': 'Budi', 'age': 35 }", 1);
//            repositoryManager.Register("xml2", "<person><name>Budilie</name><age>41</age></person>", 2);

//            // Retrieve item
//            Console.WriteLine("Retrieved JSON item: " + repositoryManager.Retrieve("json1"));
//            Console.WriteLine("Retrieved XML item: " + repositoryManager.Retrieve("xml1"));

//            Console.WriteLine("Retrieved JSON item: " + repositoryManager.Retrieve("json2"));
//            Console.WriteLine("Retrieved XML item: " + repositoryManager.Retrieve("xmlI2"));

//            // Retrieve item type
//            Console.WriteLine("Type of jsonItem: " + repositoryManager.GetType("json1"));
//            Console.WriteLine("Type of xmlItem: " + repositoryManager.GetType("xml1"));

//            // Deregister an item
//            repositoryManager.Deregister("json1");
//            repositoryManager.Deregister("json1");

//            Console.WriteLine("Retrieved JSON json1: " + repositoryManager.Retrieve("json1"));
//            Console.WriteLine("Retrieved Json json2: " + repositoryManager.Retrieve("json2"));
//        }
//    }
//}

using System;
using System.Data;
using System.Data.SQLite;
using oop_challange;

public class FormulatrixRepositoryManager
{
    private SQLiteConnection dbConnection;
    private const string ConnectionString = "Data Source=FormulatrixRepository.db;Version=3;";

    // Initialize the repository for use (can be called only once)
    public void Initialize()
    {
        if (dbConnection == null)
        {
            dbConnection = new SQLiteConnection(ConnectionString);
            dbConnection.Open();

            // Create a table if it does not exist
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS RepositoryItems (
                    ItemName TEXT PRIMARY KEY,
                    ItemContent TEXT,
                    ItemType INTEGER
                );";
            ExecuteNonQuery(createTableQuery);
        }
        else
        {
            Console.WriteLine("Repository has already been initialized.");
        }
    }

    // Store an item to the repository
    public void Register(string itemName, string itemContent, int itemType)
    {
        if (dbConnection == null)
        {
            Console.WriteLine("Repository has not been initialized. Call Initialize method first.");
            return;
        }

        if (IsItemExists(itemName))
        {
            Console.WriteLine($"Item with name '{itemName}' already exists. Use a different name.");
            return;
        }

        // Validate itemContent based on the given itemType
        // (Validation logic is not implemented here, but you should include it in your actual implementation)

        // Store the item in the database
        string insertQuery = "INSERT INTO RepositoryItems (ItemName, ItemContent, ItemType) VALUES (@ItemName, @ItemContent, @ItemType)";
        using (var command = new SQLiteCommand(insertQuery, dbConnection))
        {
            command.Parameters.AddWithValue("@ItemName", itemName);
            command.Parameters.AddWithValue("@ItemContent", itemContent);
            command.Parameters.AddWithValue("@ItemType", itemType);
            command.ExecuteNonQuery();
        }

        Console.WriteLine($"Item '{itemName}' registered successfully.");
    }

    // Retrieve an item from the repository
    public string Retrieve(string itemName)
    {
        if (dbConnection == null)
        {
            Console.WriteLine("Repository has not been initialized. Call Initialize method first.");
            return null;
        }

        if (IsItemExists(itemName))
        {
            string selectQuery = "SELECT ItemContent FROM RepositoryItems WHERE ItemName = @ItemName";
            using (var command = new SQLiteCommand(selectQuery, dbConnection))
            {
                command.Parameters.AddWithValue("@ItemName", itemName);
                return command.ExecuteScalar() as string;
            }
        }
        else
        {
            Console.WriteLine($"Item with name '{itemName}' not found.");
            return null;
        }
    }

    // Retrieve the type of the item (JSON or XML)
    public int GetType(string itemName)
    {
        if (dbConnection == null)
        {
            Console.WriteLine("Repository has not been initialized. Call Initialize method first.");
            return -1;
        }

        if (IsItemExists(itemName))
        {
            string selectQuery = "SELECT ItemType FROM RepositoryItems WHERE ItemName = @ItemName";
            using (var command = new SQLiteCommand(selectQuery, dbConnection))
            {
                command.Parameters.AddWithValue("@ItemName", itemName);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        else
        {
            Console.WriteLine($"Item with name '{itemName}' not found.");
            return -1;
        }
    }

    // Remove an item from the repository
    public void Deregister(string itemName)
    {
        if (dbConnection == null)
        {
            Console.WriteLine("Repository has not been initialized. Call Initialize method first.");
            return;
        }

        if (IsItemExists(itemName))
        {
            string deleteQuery = "DELETE FROM RepositoryItems WHERE ItemName = @ItemName";
            using (var command = new SQLiteCommand(deleteQuery, dbConnection))
            {
                command.Parameters.AddWithValue("@ItemName", itemName);
                command.ExecuteNonQuery();
            }

            Console.WriteLine($"Item '{itemName}' deregistered successfully.");
        }
        else
        {
            Console.WriteLine($"Item with name '{itemName}' not found.");
        }
    }

    // Check if an item exists in the repository
    private bool IsItemExists(string itemName)
    {
        string selectQuery = "SELECT COUNT(*) FROM RepositoryItems WHERE ItemName = @ItemName";
        using (var command = new SQLiteCommand(selectQuery, dbConnection))
        {
            command.Parameters.AddWithValue("@ItemName", itemName);
            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }
    }

    // Execute a non-query SQL command
    private void ExecuteNonQuery(string query)
    {
        using (var command = new SQLiteCommand(query, dbConnection))
        {
            command.ExecuteNonQuery();
        }
    }

    // Close the database connection
    public void Dispose()
    {
        dbConnection?.Close();
        dbConnection?.Dispose();
    }
}

class Program
{
    static void Main()
    {
        using (FormulatrixRepositoryManager repositoryManager = new FormulatrixRepositoryManager())
        {
            repositoryManager.Initialize();

            // Register and retrieve items
            repositoryManager.Register("jsonItem", "{ \"name\": \"John\" }", 1);
            repositoryManager.Register("xmlItem", "<person><name>Emily</name></person>", 2);

            Console.WriteLine("Retrieved JSON item: " + repositoryManager.Retrieve("jsonItem"));
            Console.WriteLine("Retrieved XML item: " + repositoryManager.Retrieve("xmlItem"));

            // Retrieve item type
            Console.WriteLine("Type of jsonItem: " + repositoryManager.GetType("jsonItem"));
            Console.WriteLine("Type of xmlItem: " + repositoryManager.GetType("xmlItem"));

            // Deregister an item
            repositoryManager.Deregister("jsonItem");
            repositoryManager.Deregister("nonexistentItem");
        }
    }
}
