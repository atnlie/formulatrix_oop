using System;

namespace oop_challange
{

    class Program
    {
        static void Main()
        {
            FormulatrixRepositoryManager repositoryManager = new FormulatrixRepositoryManager();
            repositoryManager.Initialize();

            // Register and retrieve items
            repositoryManager.Register("json1", "{ 'name': 'Anton', 'age': 30 }", (int)EnumFileFormat.JSON);
            repositoryManager.Register("xml1", "<person><name>Atnlie</name><age>21</age></person>", (int)EnumFileFormat.XML);
            repositoryManager.Register("json2", "{ 'name': 'Budi', 'age': 35 }", (int)EnumFileFormat.JSON);
            repositoryManager.Register("xml2", "<person><name>Budilie</name><age>41</age></person>", (int)EnumFileFormat.XML);

            Console.WriteLine("Retrieved JSON item: " + repositoryManager.Retrieve("json1"));
            Console.WriteLine("Retrieved XML item: " + repositoryManager.Retrieve("xml1"));

            // Retrieve item type
            Console.WriteLine("Type of jsonItem: " + repositoryManager.GetType("json1"));
            Console.WriteLine("Type of xmlItem: " + repositoryManager.GetType("xml1"));

            // Deregister an item
            repositoryManager.Deregister("json1");

            // Retrieve item type
            Console.WriteLine("Type of jsonItem: " + repositoryManager.GetType("json1"));
            Console.WriteLine("Type of xmlItem: " + repositoryManager.GetType("xml1"));

        }
    }
}