using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using BudgetPlanner.Models;

namespace BudgetPlanner.Services
{
    public class TransactionService
    {
        private static TransactionService? _instance;
        public static TransactionService Instance => _instance ??= new TransactionService();

        public List<Transaction> Transactions { get; private set; } = new List<Transaction>();

        private readonly string filePath = "transactions.json";

        private TransactionService()
        {
            LoadTransactions();
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            SaveTransactions();
        }

        public void RemoveTransaction(Transaction transaction)
        {
            Transactions.Remove(transaction);
            SaveTransactions();
        }
        public void RemoveLastTransaction()
        {
            if (Transactions.Count > 0)
            {
                Transactions.RemoveAt(Transactions.Count - 1);
                SaveTransactions();
            }
            
        }

        public void LoadTransactions() // Changed from private to public
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                Transactions = JsonConvert.DeserializeObject<List<Transaction>>(json) ?? new List<Transaction>();
            }
        }

        private void SaveTransactions()
        {
            var json = JsonConvert.SerializeObject(Transactions, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
