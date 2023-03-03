using ProsperDaily.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace ProsperDaily.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class StatisticsViewModel
    {
        public ObservableCollection<TransactionSummary> Summary { get; set; }
        public ObservableCollection<Transaction> SpendingList { get; set; }
        
        public void GetTransactionSummary()
        {
            var data = App.TransactionRepo.GetItems();

            var result = new List<TransactionSummary>();

            var groupedTransactions = data.GroupBy(t => t.OperationDate.Date);

            foreach (var group in groupedTransactions)
            {
                var transactionSummary = new TransactionSummary
                {
                    TransactionsDate = group.Key,
                    TransactionsTotal = group.Sum(t => t.IsIncome ? t.Amount : -t.Amount),
                    ShownDate = group.Key.ToString("MM/dd")
                };
                result.Add(transactionSummary);
            }

            result = result.OrderBy(x => x.TransactionsDate).ToList();

            Summary = new ObservableCollection<TransactionSummary>(result);

            var spendingList = data.Where(x => x.IsIncome == false);

            SpendingList = new ObservableCollection<Transaction>(spendingList);
        }
    }
}
