using WpfApp_Calc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_Calc.Model
{
    public static class Calculator
    {
        // Метод для расчётов
        internal static string Calculate(double operand1, double operand2, MainWindowViewModel.Operations operation)
        {
            switch (operation)
            {
                case MainWindowViewModel.Operations.Empty:
                    return "";
                case MainWindowViewModel.Operations.Plus:
                    return Format($"{operand1 + operand2}", false);
                case MainWindowViewModel.Operations.Minus:
                    return Format($"{operand1 - operand2}", false);
                case MainWindowViewModel.Operations.Divide:
                    if (operand2 == 0)
                    {
                        return "Нельзя";
                    }
                    return Format($"{operand1 / operand2}", false);
                case MainWindowViewModel.Operations.Multiply:
                    return Format($"{operand1 * operand2}", false);
                default:
                    break;
            }
            return "";
        }

        // Метод для форматирования
        internal static string Format(string num, bool changeSign)
        {
            double result = (changeSign ? -1 : 1) * double.Parse(num);
            if (num.Replace(" ", "").Replace(",", "").Replace("-", "").Length > 15)
            {
                return $"{result:0.###############E+0}";
            }
            return $"{result:#,###,###,###,###,##0.################}";
        }
    }
}
