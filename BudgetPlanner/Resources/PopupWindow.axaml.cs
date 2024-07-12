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
        bool TransactionFrequencySelected = false;
        string TransactionFrequency = "";
        string TransactionValue = "";
      // Define an event to signal when a response is ready
        public event EventHandler<string>? ResponseReceived;
        public PopupWindow()
        {
            InitializeComponent();
            this.Focus();
        }
        
        private void OnResponseInformation(object sender, RoutedEventArgs e)
        {
            
            TransactionValue = InputValue.Text ?? string.Empty;
            if (!TransactionFrequencySelected)
            {
              
              return;
            }
            
            if (TransactionValue == string.Empty)
            {
              InputValue.Watermark = "Value must be entered";
              return;
            }
            // type of income/expense
            var type = TransactionFrequency; 
            // input value of income/expense
            var value = TransactionValue; 
            var response = $"{type},{value}";
            // Raise the event with the user's response
            ResponseReceived?.Invoke(this, response);

            // Close the popup window
            this.Close();
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
          if (e.Key == Key.Enter && TransactionFrequencySelected)
          {
            OnResponseInformation(sender, e);
          }
          if (e.Key == Key.Escape)
          {
            ExitPressed(sender, e);
          }
        }
        private void TransactionPressed(object sender, RoutedEventArgs e)
        {
          
          if (sender is Button button)
          {
          TransactionFrequencySelected = true;
          TransactionFrequency = button.Content?.ToString() ?? string.Empty;
          }

        }

        private void ExitPressed(object sender, RoutedEventArgs e)
        {
          this.Close();
        }


  }
}
