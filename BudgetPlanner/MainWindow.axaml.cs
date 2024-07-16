using Avalonia.Controls;
using Avalonia.Interactivity;
using BudgetPlanner.Resources.Views;


namespace BudgetPlanner;

public partial class MainWindow : Window
{   
    public MainWindow()
    {
        InitializeComponent();
        MainContent.Content = new DashboardView();
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
    // 
    // private void GetWindowHeight()
    // {
    //     var windowHeight = this.Bounds.Height;
    //     var windowWidth = this.Bounds.Width;
    //     Console.WriteLine($"Window Height: {windowHeight}");
    //     Console.WriteLine($"Window Width: {windowWidth}");
    // }

    // private void ClickAButton(object sender, RoutedEventArgs e)
    // {
    //     GetWindowHeight();
    // }

}
