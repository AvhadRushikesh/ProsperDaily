using ProsperDaily.MVVM.Models;
using ProsperDaily.MVVM.Views;
using ProsperDaily.Repositories;

namespace ProsperDaily;

public partial class App : Application
{
    public static BaseRepository<Transaction> TransactionRepo { get; private set; }

    public App(BaseRepository<Transaction> _transactionRepo)
    {
        InitializeComponent();

        TransactionRepo = _transactionRepo;

        MainPage = new NavigationPage(new DashboardPage());
    }
}