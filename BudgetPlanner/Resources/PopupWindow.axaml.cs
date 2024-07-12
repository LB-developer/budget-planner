using System;
using System.Linq;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia.VisualTree;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BudgetPlanner.Resources
{
    public partial class PopupWindow : Window
    {   
        string TransactionFrequency = "";
        string TransactionValue = "";
        string TransactionName = "";
      // Define an event to signal when a response is ready
        public event EventHandler<string>? ResponseReceived;
        public PopupWindow()
        {
            InitializeComponent();
            this.Focus();
        }
        
        private void OnResponseInformation(object sender, RoutedEventArgs e)
        {

            if (ConditionsMet())
            {
            var frequency = TransactionFrequency; 
            var name = TransactionName;
            var value = TransactionValue; 
            var response = $"{frequency},{name},{value}";

            // Raise the event with the user's response
            ResponseReceived?.Invoke(this, response);

            // Reset Input values & Close the popup window
            TransactionFrequency = "";
            TransactionValue = "";
            TransactionName = "";
            this.Close();
            }
            else{
              return;
            }


        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
          if (e.Key == Key.Escape)
          {
            ExitPressed(sender, e);
          }
          else if (e.Key == Key.Enter)
          {
            OnResponseInformation(sender, e);
          }

        }
        private void TransactionPressed(object sender, RoutedEventArgs e)
        {
          if (sender is Button button)
          {
          ChangeButtonClasses("highlighted", "default");
          TransactionFrequency = button.Content?.ToString() ?? string.Empty;
          button.Classes.Add("selected");
          }

        }

        private void ExitPressed(object sender, RoutedEventArgs e)
        {
          this.Close();
        }

        private bool ConditionsMet()
        {
            TransactionName = InputName.Text ?? "";
            TransactionValue = InputValue.Text ?? string.Empty;
            
            if (TransactionFrequency == string.Empty)
            {
              ChangeButtonClasses("default", "highlighted");
              return false;
            }
            else if (TransactionValue == string.Empty)
            {
              InputValue.Watermark = "Value must be entered";
              return false;
            }
            else if (TransactionName == string.Empty)
            {
              InputName.Watermark = "Name must be entered";
              return false;
            }

            return true;
        }
        private void ChangeButtonClasses(string oldClass, string newClass)
        {

            // Get all buttons in the PopupGrid grid
            var buttons = PopupGrid.GetVisualDescendants()
                                  .OfType<Button>();
                                  
            
            foreach (var button in buttons)
            {
              if (button.Classes.Contains("selected"))
              {
                button.Classes.Remove("selected");
                button.Classes.Add("default");
              }
              if (button.Classes.Contains(oldClass))
              {
                // Remove the old class and add the new class
                button.Classes.Remove(oldClass);
                button.Classes.Add(newClass);
              }   
            }
        }
    }
}
