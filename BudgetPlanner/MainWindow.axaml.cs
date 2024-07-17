using Avalonia.Controls;
using Avalonia.Interactivity;
using BudgetPlanner.Resources.Views;
using BudgetPlanner.Services;


namespace BudgetPlanner;

public partial class MainWindow : Window
{   
    public MainWindow()
    {
        InitializeComponent();
        TransactionService.Instance.LoadTransactions(); // Load transactions on startup
        MainContent.Content = new DashboardView(); // Default to dashboard view
    }

    private void ShowDashboard(object sender, RoutedEventArgs e)
    {
        MainContent.Content = new DashboardView();
        TitleHeader.Text = "Dashboard";
    }

    private void ShowAddTransaction(object sender, RoutedEventArgs e)
    {
        MainContent.Content = new BudgetLog();
        TitleHeader.Text = "Transaction Log";
    }

    private void ShowReports(object sender, RoutedEventArgs e)
    {
        MainContent.Content = new ReportsView();
        TitleHeader.Text = "Reports";
    }

    private void ShowSettings(object sender, RoutedEventArgs e)
    {
        MainContent.Content = new SettingsView();
        TitleHeader.Text = "Settings";
    }


}
