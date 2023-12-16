using System;

namespace oop_challange
{
	public class FormulatrixRepositoryManager
	{
        private Dictionary<string, Tuple<string, int>> dataStore;
        private bool isInitialized = false;

        // Initialize the repository for use
        public void Initialize()
        {
            if (!isInitialized)
            {
                dataStore = new Dictionary<string, Tuple<string, int>>();
                isInitialized = true;
            }
            else
            {
                Console.WriteLine("Repository has already been initialized.");
            }
        }

        // Store an item to the repository
        public void Register(string itemName, string itemContent, int itemType)
        {
            if (!isInitialized)
            {
                Console.WriteLine("Repository has not been initialized. Call Initialize method first.");
                return;
            }

            if (dataStore.ContainsKey(itemName))
            {
                Console.WriteLine($"Item with name '{itemName}' already exists. Use a different name.");
                return;
            }

            // Validate itemContent based on the given itemType
            
            // Store the item in the dataStore
            dataStore[itemName] = new Tuple<string, int>(itemContent, itemType);
            Console.WriteLine($"Item '{itemName}' registered successfully.");
        }

        // Retrieve an item from the repository
        public string Retrieve(string itemName)
        {
            if (!isInitialized)
            {
                Console.WriteLine("Repository has not been initialized. Call Initialize method first.");
                return null;
            }

            if (dataStore.ContainsKey(itemName))
            {
                return dataStore[itemName].Item1;
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
            if (!isInitialized)
            {
                Console.WriteLine("Repository has not been initialized. Call Initialize method first.");
                return -1;
            }

            if (dataStore.ContainsKey(itemName))
            {
                return dataStore[itemName].Item2;
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
            if (!isInitialized)
            {
                Console.WriteLine("Repository has not been initialized. Call Initialize method first.");
                return;
            }

            if (dataStore.ContainsKey(itemName))
            {
                dataStore.Remove(itemName);
                Console.WriteLine($"Item '{itemName}' deregistered successfully.");
            }
            else
            {
                Console.WriteLine($"Item with name '{itemName}' not found.");
            }
        }
    }
}

