using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using BudgetPlanner.Resources;
using System.Threading.Tasks;

namespace BudgetPlanner;

public partial class MainWindow : Window
{   
    string overallTransaction = "";
    int GridRows = 1; 
    public MainWindow()
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
            // Handle the response from the popup
            var parts = response.Split(",");
            foreach (var part in parts)
            {
              Console.WriteLine(part);
            };
            if (parts.Length == 2)
            {
                string frequency = parts[0];
                decimal value = decimal.Parse(parts[1]);
                // Create row then increment row counter for next transaction
                BudgetLog.RowDefinitions.Add(new RowDefinition());
                // create textblocks of info
                TextBlock transactionType = new TextBlock();
                transactionType.Text = overallTransaction;
                
                TextBlock transactionFrequency = new TextBlock();
                transactionFrequency.Text = frequency;
                
                TextBlock transactionValue = new TextBlock();
                transactionValue.Text = "$"+value.ToString("F2");

                // set info to grid
                Grid.SetRow(transactionType, GridRows);
                Grid.SetColumn(transactionType, 0);

                Grid.SetRow(transactionFrequency, GridRows);
                Grid.SetColumn(transactionFrequency, 1);
                
                Grid.SetRow(transactionValue, GridRows);
                Grid.SetColumn(transactionValue, 2);
                
                
                GridRows++;
                BudgetLog.Children.Add(transactionType);
                BudgetLog.Children.Add(transactionFrequency);
                BudgetLog.Children.Add(transactionValue);
            }
            else
            {
                // Handle the case where the popup was closed without a response
                Debug.WriteLine("Popup closed without response");
            }
        }






}