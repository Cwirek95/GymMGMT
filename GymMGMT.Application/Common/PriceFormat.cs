using System.Text.RegularExpressions;

namespace GymMGMT.Application.Common
{
    public static class PriceFormat
    {
        public static bool CheckPriceFormat(double price)
        {
            Regex expression = new Regex(@"^\d{0,10}(\.\d{1,2})?$");
            if (expression.IsMatch(price.ToString()))
                return true;

            return false;
        }
    }
}