using System;
using Xunit;
using api.Logic;
using System.Linq;
using System.Collections.Generic;
using api.Models;
using FluentAssertions;

namespace api.test
{
    public class InterestLogicTest
    {
        private IInterestLogic logic;
        public InterestLogicTest() => logic = new InterestLogic();

        [Theory]
        [InlineData(0.12)]
        [InlineData(0.01)]
        [InlineData(0.08)]
        public void GetAndSetInterestRateShouldWork(double rate)
        {
            logic.SetInterestRate(rate);
            var result = logic.GetInterestRate();
            result.Should().Be(rate);
        }

        [Theory]
        [MemberData(nameof(InterestCalculatorCase))]
        public void InterestCalculatorShouldWork(double rate, double principle, int years, IList<InterestData> expected)
        {
            logic.SetInterestRate(rate);
            var result = logic.InterestCalculator(principle, years);
            result.Should().BeEquivalentTo(expected);
        }


        public static IEnumerable<object[]> InterestCalculatorCase => new List<object[]>
        {
            new object[]{
                0.12,
                10000,
                3,
                new List<InterestData>{
                    new InterestData{ YearCount = 1, Principle = 10000, Interest = 1200, Total = 11200},
                    new InterestData{ YearCount = 2, Principle = 11200, Interest = 1344, Total = 12544},
                    new InterestData{ YearCount = 3, Principle = 12544, Interest = 1505.28, Total = 14049.28}
                }
            },
            new object[]{
                0.12,
                15000,
                1,
                new List<InterestData>{
                    new InterestData{ YearCount = 1, Principle = 15000, Interest = 1800, Total = 16800},
                }
            },
            new object[]{
                0.12,
                20000,
                3,
                new List<InterestData>{
                    new InterestData{ YearCount = 1, Principle = 20000, Interest = 2400, Total = 22400},
                    new InterestData{ YearCount = 2, Principle = 22400, Interest = 2688, Total = 25088},
                    new InterestData{ YearCount = 3, Principle = 25088, Interest = 3010.56, Total = 28098.56},
                }
            },
            new object[]{
                0,
                20000,
                2,
                new List<InterestData>{
                    new InterestData{ YearCount = 1, Principle = 20000, Interest = 0, Total = 20000},
                    new InterestData{ YearCount = 2, Principle = 20000, Interest = 0, Total = 20000},
                }
            },
            new object[]{
                0.07,
                10000,
                1,
                new List<InterestData>{
                    new InterestData{ YearCount = 1, Principle = 10000, Interest = 700, Total = 10700},
                }
            }
        };
    }
}
