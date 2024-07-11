using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BudgetPlanner.Resources
{
    public partial class PopupWindow : Window
    {   
        string transaction = "";
      // Define an event to signal when a response is ready
        public event EventHandler<string>? ResponseReceived;
        public PopupWindow()
        {
            InitializeComponent();
        }
        
        private void OnResponseInformation(object sender, RoutedEventArgs e)
        {
            var response = "User's response"; // Replace with actual logic to get the response

            // Raise the event with the user's response
            ResponseReceived?.Invoke(this, response);

            // Close the popup
            this.Close();
        }
    
        private void TransactionPressed(object sender, RoutedEventArgs e)
        {
          Debug.WriteLine("TransactionPressed");

        }

        private void ExitPressed(object sender, RoutedEventArgs e)
        {
          this.Close();
        }


  }
}
