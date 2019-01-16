using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Logic
{
    public interface IInterestLogic
    {
        double GetInterestRate();
        void SetInterestRate(double rate);
        IEnumerable<InterestData> InterestCalculator(double principle, int years);
    }
}
