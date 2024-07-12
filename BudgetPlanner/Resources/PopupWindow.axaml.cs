using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BudgetPlanner.Resources
{
    public partial class PopupWindow : Window
    {   
        string TransactionType = "";
        string TransactionValue = "";
      // Define an event to signal when a response is ready
        public event EventHandler<string>? ResponseReceived;
        public PopupWindow()
        {
            InitializeComponent();
        }
        
        private void OnResponseInformation(object sender, RoutedEventArgs e)
        {
            
            TransactionValue = InputValue.Text ?? string.Empty;
            if (TransactionValue == string.Empty)
            {
              InputValue.Watermark = "Value must be entered";
              return;
            }
            
            var type = TransactionType; // type of income/expense
            var value = TransactionValue; // input value of income/expense
            var response = $"{type},{value}";
            // Raise the event with the user's response
            ResponseReceived?.Invoke(this, response);

            // Close the popup window
            this.Close();
        }
    
        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
          if (e.Key == Key.Enter)
          {
            OnResponseInformation(sender, e);
          }
          if (e.Key == Key.Back)
          {
            ExitPressed(sender, e);
          }
        }
        private void TransactionPressed(object sender, RoutedEventArgs e)
        {
          
          if (sender is Button button)
          {
          TransactionType = button.Content?.ToString() ?? string.Empty;
          }

        }

        private void ExitPressed(object sender, RoutedEventArgs e)
        {
          this.Close();
        }


  }
}
