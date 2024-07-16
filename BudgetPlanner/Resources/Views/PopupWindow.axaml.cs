using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia.VisualTree;

namespace BudgetPlanner.Resources.Views
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
            var date = InputDate.SelectedDate?.ToString("yyyy-MM-dd") ?? DateTime.Now.ToString("yyyy-MM-dd"); // Get the selected date or default to today

            var response = $"{frequency},{name},{value},{date}";

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

        // require that specific entries be input
        private bool ConditionsMet()
        {
            TransactionName = InputName.Text ?? "";
            TransactionValue = InputValue.Text ?? "";
            // Confirm that entries have been added
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
                                  
            // remove highlight when clicked
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