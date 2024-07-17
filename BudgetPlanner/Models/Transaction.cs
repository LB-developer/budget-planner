using System;
namespace BudgetPlanner.Models
{
    public class Transaction
    {
        public string? Type { get; set; }
        public string? Frequency { get; set; }
        public string? Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}