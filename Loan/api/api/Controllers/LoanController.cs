using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Logic;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private IInterestLogic logic;
        public LoanController() => logic = new InterestLogic();
        // GET: api/Loan
        [HttpGet(Name = "Get Intrerest Rate")]
        public double Get()
        {
            return logic.GetInterestRate();
        }

        // GET: api/Loan/5
        [HttpGet("{rate}", Name = "Set Intrerest Rate")]
        public void Set(double rate)
        {
            logic.SetInterestRate(rate);
        }

        // GET: api/Loan/10000/3
        [HttpGet("{principle}/{years}", Name = "Interest Calculator")]
        public IEnumerable<InterestData> Calculator(double principle, int years)
        {
            return logic.InterestCalculator(principle, years);
        }
    }
}
