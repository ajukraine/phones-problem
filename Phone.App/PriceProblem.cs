using System.Collections.Generic;
using System.Linq;

namespace Phone.App
{
    public class PriceProblem
    {
        public static PriceProblemSolution Solve(IEnumerable<APhone> phones)
        {
            return new PriceProblemSolution
            {
                PhonesSortedByPrice = phones.OrderBy(p => p.Price).ToArray(),
                TotalPrice = phones.Sum(p => p.Price)
            };
        }
    }

    public class PriceProblemSolution
    {
        public APhone[] PhonesSortedByPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}