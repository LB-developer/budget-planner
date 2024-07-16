using System.Linq;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;

namespace BudgetPlanner.Resources.Views
{
  public partial class BudgetLog : UserControl
  {   
      string overallTransaction = "";
      private int GridRows = 0; 
      public BudgetLog()
      {
        InitializeComponent();
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
          var parts = response.Split(",");
          // Turn response to a list and insert overallTransaction (income or expense) to position 0
          List<string> partsList = parts.ToList();
          partsList.Insert(0, overallTransaction);
          

          if (partsList.Count == 5)
          {
              UpdateBudgetLog(partsList);
          }
          else
          {
              // Handle the case where the popup was closed without a response
              Debug.WriteLine("Popup closed without response");
          }
      }

      private void UpdateBudgetLog(List<string> responseParts)
      {
        // add a row to the grid to hold the new transaction
        BudgetLogGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

        // Create an array to hold the TextBlock elements
        TextBlock[] budgetLogColumn = new TextBlock[responseParts.Count];
        for (var i = 0; i < responseParts.Count; i++)
        {

            // Create textblocks for each piece of info
            budgetLogColumn[i] = new TextBlock()
            {
                Text = responseParts[i],
            };

            // Add class to each item
            budgetLogColumn[i].Classes.Add("budget-log-item");

            // Apply class for income or expense
            if (responseParts[0] == "Income")
            {
                budgetLogColumn[i].Classes.Add("income");
            }
            else if (responseParts[0] == "Expense")
            {
                budgetLogColumn[i].Classes.Add("expense");
            }
            // Set the grid row and column
            Grid.SetRow(budgetLogColumn[i], GridRows);
            Grid.SetColumn(budgetLogColumn[i], i);

            // Add TextBlock to the grid
            BudgetLogGrid.Children.Add(budgetLogColumn[i]);
        }
        GridRows++;
      }
  }
}