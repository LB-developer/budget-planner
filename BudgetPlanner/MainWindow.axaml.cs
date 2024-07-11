using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using BudgetPlanner.Resources;
using System.Threading.Tasks;

namespace BudgetPlanner;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // AddExpenseButton.Click += ClickButton;
    }
    private void ClickButton(object sender, RoutedEventArgs e)
    {
      
    }
     private void OpenPopups(object sender, RoutedEventArgs e)
        {
            var popups = new PopupWindow();            
            popups.ResponseReceived += OnPopupsResponseReceived; // Subscribe to the event
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
                string type = parts[0];
                decimal value = decimal.Parse(parts[1]);
                // Do something with the response
                Debug.WriteLine($"Response from popup: type:{type} and value:{value}");
            }
            else
            {
                // Handle the case where the popup was closed without a response
                Debug.WriteLine("Popup closed without response");
            }
        }






}