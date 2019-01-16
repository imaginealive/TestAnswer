using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Logic
{
    public class InterestLogic : IInterestLogic
    {
        private double InterestRate;
        public InterestLogic() => InterestRate = 0.12;

        public double GetInterestRate()
        {
            return InterestRate;
        }

        public IEnumerable<InterestData> InterestCalculator(double principle, int years)
        {
            var data = new List<InterestData>();
            for (int i = 0; i < years; i++)
            {
                var item = new InterestData
                {
                    YearCount = i + 1,
                    Principle = principle,
                    Interest = Math.Round(principle * InterestRate, 2),
                    Total = Math.Round(principle + principle * InterestRate, 2) 
                };
                data.Add(item);
                principle = item.Total;
            }
            return data;
        }

        public void SetInterestRate(double rate)
        {
            if (rate >= 0)
                InterestRate = rate;
        }
    }
}
