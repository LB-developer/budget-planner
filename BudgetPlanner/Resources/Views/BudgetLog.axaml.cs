using System;
using System.Linq;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using BudgetPlanner.Models;
using BudgetPlanner.Services;


namespace BudgetPlanner.Resources.Views
{
  public partial class BudgetLog : UserControl
  {   
      string overallTransaction = "";
      private int GridRows = 0; 
      public BudgetLog()
      {
        InitializeComponent();
        UpdateBudgetLog();
      }
      private void ClickButton(object sender, RoutedEventArgs e)
      {
        
      }
      private void OpenPopups(object sender, RoutedEventArgs e)
          {
              
              if (sender == IncomeButton){overallTransaction = "Income";}
              else{overallTransaction = "Expense";}
              var popups = new PopupWindow(); 
              // Subscribe to the event           
              popups.ResponseReceived += OnPopupsResponseReceived; 
              popups.Show();
          }

      private void OnPopupsResponseReceived(object? sender, string response)
      {
            Debug.WriteLine($"Response received: {response}");

            var parts = response.Split(",");

            Debug.WriteLine($"Number of parts: {parts.Length}");
            // Turn response to a list and insert overallTransaction (income or expense) to position 0
            List<string> partsList = parts.ToList();
            partsList.Insert(0, overallTransaction);
            
            Debug.WriteLine($"Number of partsList: {partsList.Count}");

            if (partsList.Count == 5)
            {

                var transaction = new Transaction
                {
                    Type = partsList[0],
                    Frequency = partsList[1],
                    Name = partsList[2],
                    Value = decimal.Parse(partsList[3]),
                    Date = DateTime.Parse(partsList[4])
                };
                
                TransactionService.Instance.AddTransaction(transaction);
                UpdateBudgetLog();
                
            }
            else
            {
                // Handle the case where the popup was closed without a response
                Debug.WriteLine("Popup closed without response");
            }
      }

      private void UpdateBudgetLog()
      {
        BudgetLogGrid.Children.Clear();
        BudgetLogGrid.RowDefinitions.Clear();

        // Add the header row
        BudgetLogGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        AddHeader("Type", 0);
        AddHeader("Frequency", 1);
        AddHeader("Name", 2);
        AddHeader("Value", 3);
        AddHeader("Date", 4);

        // Add each transaction as a new row
        foreach (var transaction in TransactionService.Instance.Transactions)
        {
            BudgetLogGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var typeTextBlock = CreateTextBlock(transaction.Type, "budget-log-item");
            var frequencyTextBlock = CreateTextBlock(transaction.Frequency, "budget-log-item");
            var nameTextBlock = CreateTextBlock(transaction.Name, "budget-log-item");
            var valueTextBlock = CreateTextBlock(transaction.Value.ToString(), "budget-log-item");
            var dateTextBlock = CreateTextBlock(transaction.Date.ToString("yyyy-MM-dd"), "budget-log-item");

            if (transaction.Type == "Income")
            {
                AddClass(typeTextBlock, "income");
                AddClass(frequencyTextBlock, "income");
                AddClass(nameTextBlock, "income");
                AddClass(valueTextBlock, "income");
                AddClass(dateTextBlock, "income");
            }
            else if (transaction.Type == "Expense")
            {
                AddClass(typeTextBlock, "expense");
                AddClass(frequencyTextBlock, "expense");
                AddClass(nameTextBlock, "expense");
                AddClass(valueTextBlock, "expense");
                AddClass(dateTextBlock, "expense");
            }

            AddToGrid(typeTextBlock, GridRows, 0);
            AddToGrid(frequencyTextBlock, GridRows, 1);
            AddToGrid(nameTextBlock, GridRows, 2);
            AddToGrid(valueTextBlock, GridRows, 3);
            AddToGrid(dateTextBlock, GridRows, 4);

            GridRows++;
        }
      }

    private void AddHeader(string text, int column)
    {
        var header = new TextBlock { Text = text };
        header.Classes.Add("budget-log-header");
        BudgetLogGrid.Children.Add(header);
        Grid.SetRow(header, 0);
        Grid.SetColumn(header, column);
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
        BudgetLogGrid.Children.Add(textBlock);
        Grid.SetRow(textBlock, row);
        Grid.SetColumn(textBlock, column);
    }


  }
}