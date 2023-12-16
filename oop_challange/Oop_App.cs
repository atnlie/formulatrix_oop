using System;
using System.Collections.Generic;

namespace oop_challange
{
    class Oop_App
    {
        static void Main()
        {
            FormulatrixRepositoryManager repositoryManager = new FormulatrixRepositoryManager();
            repositoryManager.Initialize();

            // Register items
            repositoryManager.Register("json1", "{ 'name': 'Anton', 'age': 30 }", 1);
            repositoryManager.Register("xml1", "<person><name>Atnlie</name><age>21</age></person>", 2);
            repositoryManager.Register("json2", "{ 'name': 'Budi', 'age': 35 }", 1);
            repositoryManager.Register("xml2", "<person><name>Budilie</name><age>41</age></person>", 2);

            // Retrieve item
            Console.WriteLine("Retrieved JSON item: " + repositoryManager.Retrieve("json1"));
            Console.WriteLine("Retrieved XML item: " + repositoryManager.Retrieve("xml1"));

            Console.WriteLine("Retrieved JSON item: " + repositoryManager.Retrieve("json2"));
            Console.WriteLine("Retrieved XML item: " + repositoryManager.Retrieve("xmlI2"));

            // Retrieve item type
            Console.WriteLine("Type of jsonItem: " + repositoryManager.GetType("json1"));
            Console.WriteLine("Type of xmlItem: " + repositoryManager.GetType("xml1"));

            // Deregister an item
            repositoryManager.Deregister("json1");
            repositoryManager.Deregister("json1");

            Console.WriteLine("Retrieved JSON json1: " + repositoryManager.Retrieve("json1"));
            Console.WriteLine("Retrieved Json json2: " + repositoryManager.Retrieve("json2"));
        }
    }
}