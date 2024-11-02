using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundProcessLibrary
{
    public interface IPerformanceProcessor
    {
        double GetPerformance(Tuple<DateTime, double>[] dataset, DateTime fromDate, DateTime toDate);
    }
    public class PerformanceProcessor : IPerformanceProcessor
    {
        public double GetPerformance(Tuple<DateTime, double>[] dataset, DateTime fromDate, DateTime toDate)
        {
            if (dataset == null || dataset.Length < 2) return 0; // Check if dataset isn't null or contain less than 2 elements

            var filteredData = dataset
                .Where(d => d.Item1 >= fromDate && d.Item1 <= toDate) // Keep only if date is between fromDate and toDate
                .OrderBy(d => d.Item1) // Order by croissant date
                .ToArray();

            if (filteredData.Length < 2) return 0; // Check if there is minimum 2 values

            var firstValue = filteredData.First().Item2;
            var lastValue = filteredData.Last().Item2;

            if (firstValue == 0) return 0; // Can't divide by 0

            return (lastValue / firstValue) - 1;
        }
    }
}