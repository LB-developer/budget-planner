using Avalonia.Controls;
using BudgetPlanner.Services;
using BudgetPlanner.Models;
using BudgetPlanner;

namespace BudgetPlanner.Resources.Views
{

  
  public partial class DashboardView : UserControl
  {
    decimal totalCurrentBalanceValue = 0;
    public DashboardView()
    {
      InitializeComponent();
      UpdateRecentTransactions();
      IncomeExpensePieChart.Content = new IncomeExpensePieChart();
    }


    // make this a general function called by both transaction logs
    // make headers static.
    private void UpdateRecentTransactions()
      {
        RecentTransactions.Children.Clear();
        RecentTransactions.RowDefinitions.Clear();
        AddRecentTransactionHeaders();
        var GridRows = 0;
    
        // Add each transaction as a new row
        foreach (var transaction in TransactionService.Instance.Transactions)
        {
            RecentTransactions.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            if (transaction.Type != null && transaction.Frequency != null && transaction.Name != null)
            {
              
              var typeTextBlock = CreateTextBlock(transaction.Type, "budget-log-item");
              var frequencyTextBlock = CreateTextBlock(transaction.Frequency, "budget-log-item");
              var nameTextBlock = CreateTextBlock(transaction.Name, "budget-log-item");
              var valueTextBlock = CreateTextBlock("$"+transaction.Value.ToString("#,##0"), "budget-log-item");
              var dateTextBlock = CreateTextBlock(transaction.Date.ToString("yyyy-MM-dd"), "budget-log-item");
              TextBlock[] transactionDataBlocks = [typeTextBlock, frequencyTextBlock, nameTextBlock, valueTextBlock, dateTextBlock];
              
              if (transaction.Type == "Income")
              {foreach(var block in transactionDataBlocks){AddClass(block, "income");}
                  totalCurrentBalanceValue +=  transaction.Value;}
              else if (transaction.Type == "Expense")
              {foreach(var block in transactionDataBlocks){AddClass(block, "expense");}
                  totalCurrentBalanceValue -=  transaction.Value;}

              AddToGrid(nameTextBlock, GridRows, 0);
              AddToGrid(valueTextBlock, GridRows, 1);
              AddToGrid(dateTextBlock, GridRows, 2);

              GridRows++;
            }
            CurrentBalanceValue.Text = "$"+totalCurrentBalanceValue.ToString("#,##0");
        }
      }

    private TextBlock CreateTextBlock(string text, string className)
    {
        var textBlock = new TextBlock { Text = text };
        textBlock.Classes.Add(className);
        return textBlock;
    }

    private void AddClass(TextBlock textBlock, string className)
    {
        textBlock.Classes.Add(className);
    }

    private void AddToGrid(TextBlock textBlock, int row, int column)
    {
        RecentTransactions.Children.Add(textBlock);
        Grid.SetRow(textBlock, row);
        Grid.SetColumn(textBlock, column);
    }
    private void AddRecentTransactionHeaders()
    {
        var nameHeader = CreateTextBlock("Name", "budget-log-header");
        var valueHeader = CreateTextBlock("Value", "budget-log-header");
        var dateHeader = CreateTextBlock("Date", "budget-log-header");
        
        AddToGrid(nameHeader, 0, 0);
        AddToGrid(valueHeader, 0, 1);
        AddToGrid(dateHeader, 0, 2);
    }
    private void UpdateCurrentBalanceValue()
    {
        
    }
  }
}