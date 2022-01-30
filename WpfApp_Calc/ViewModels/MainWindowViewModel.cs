using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp_Calc.Model;

namespace WpfApp_Calc.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        #region [Объявление свойств]
        // Свойство, хранящее состояние поля ввода
        private string field;
        public string Field
        {
            get => field;
            set
            {
                /*
                if (value == "" || value == null)
                {
                    field = "0";
                }*/
                field = value;
                OnPropertyChanged();

            }
        }

        // Свойство, хранящее состояние строки с операцией
        private string operationField;
        public string OperationField
        {
            get => operationField;
            set
            {
                operationField = value;
                OnPropertyChanged();
            }
        }

        // Свойство, хранящее первый операнд
        private double operand1;
        public double Operand1
        {
            get => operand1;
            set
            {
                operand1 = value;
                OnPropertyChanged();
            }
        }

        // Свойство, хранящее второй операнд
        private double operand2;
        public double Operand2
        {
            get => operand2;
            set
            {
                operand2 = value;
                OnPropertyChanged();
            }
        }

        // Перечисление для типов операций
        public enum Operations
        {
            Empty, Plus, Minus, Divide, Multiply
        }

        // Свойство, хранящее текущую операцию
        private Operations operation;
        public Operations Operation
        {
            get => operation;
            set
            {
                operation = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region [Объявление команд для ввода значений в поле]

        public ICommand OneButton { get; }
        private void OnOneButtonExecute(object p)
        {
            InputSymbol("1");
        }

        public ICommand TwoButton { get; }
        private void OnTwoButtonExecute(object p)
        {
            InputSymbol("2");
        }

        public ICommand ThreeButton { get; }
        private void OnThreeButtonExecute(object p)
        {
            InputSymbol("3");
        }

        public ICommand FourButton { get; }
        private void OnFourButtonExecute(object p)
        {
            InputSymbol("4");
        }

        public ICommand FiveButton { get; }
        private void OnFiveButtonExecute(object p)
        {
            InputSymbol("5");
        }

        public ICommand SixButton { get; }
        private void OnSixButtonExecute(object p)
        {
            InputSymbol("6");
        }

        public ICommand SevenButton { get; }
        private void OnSevenButtonExecute(object p)
        {
            InputSymbol("7");
        }

        public ICommand EightButton { get; }
        private void OnEightButtonExecute(object p)
        {
            InputSymbol("8");
        }

        public ICommand NineButton { get; }
        private void OnNineButtonExecute(object p)
        {
            InputSymbol("9");
        }

        public ICommand ZeroButton { get; }
        private void OnZeroButtonExecute(object p)
        {
            InputSymbol("0");
        }

        public ICommand CommaButton { get; }
        private void OnCommaButtonExecute(object p)
        {
            InputSymbol(",");
        }

        public ICommand BackspaceButton { get; }
        private void OnBackspaceButtonExecute(object p)
        {
            Backspace();
        }

        public ICommand CleanEntryButton { get; }
        private void OnCleanEntryButtonExecute(object p)
        {
            CleanEntry();
        }

        public ICommand ChangeSignButton { get; }
        private void OnChangeSignButtonExecute(object p)
        {
            ChangeSign();
        }

        #endregion

        #region [Объявление команд для расчёта]

        public ICommand ClearButton { get; }
        private void OnClearButtonExecute(object p)
        {
            Clear();
        }

        public ICommand PlusButton { get; }
        private void OnPlusButtonExecute(object p)
        {
            Input(Operations.Plus);
        }

        public ICommand MinusButton { get; }
        private void OnMinusButtonExecute(object p)
        {
            Input(Operations.Minus);
        }

        public ICommand DivideButton { get; }
        private void OnDivideButtonExecute(object p)
        {
            Input(Operations.Divide);
        }

        public ICommand MultiplyButton { get; }
        private void OnMultiplyButtonExecute(object p)
        {
            Input(Operations.Multiply);
        }

        public ICommand EqualButton { get; }
        private void OnEqualButtonExecute(object p)
        {
            Equal();
        }

        #endregion

        #region [Методы калькулятора]

        // Метод установки символа в строку ввода
        void InputSymbol(string symbol)
        {
            if (Field.Replace(" ", "").Replace(",", "").Replace("-", "").Length >= 15)
            {
                return;
            }

            if (OperationField.Contains("="))
            {
                Clear();
                if (symbol != ",")
                {
                    Field = symbol;
                }
                else
                {
                    Field += symbol;
                }

            }
            else if (Field == "0" && symbol != ",")
            {
                Field = symbol;
            }
            else if (Field.Contains(",") && symbol == ",")
            {
                return;
            }
            else if (symbol == ",")
            {
                Field += symbol;
            }
            else
            {
                Field += symbol;
                Field = Calculator.Format(Field, false);
            }
        }

        // Метод удаления последнего символа в строке ввода
        void Backspace()
        {
            if (Field == "Нельзя")
            {
                Clear();
                return;
            }
            if (Field.Length == 1 ||
               (Field.Length == 2 && Field.Substring(0, 1) == "-"))
            {
                Field = "0";
                return;
            }

            else
            {
                Field = Calculator.Format(Field.Remove(Field.Length - 1), false);
            }

            if (OperationField.Contains("="))
            {
                string temp = Field;
                Clear();
                Field = temp;
            }
        }

        // Метод очистки поля ввода

        void CleanEntry()
        {
            if (OperationField.Contains("="))
            {
                Clear();
            }

            Field = "0";
        }

        // Метод смены знака введенного значения

        void ChangeSign()
        {
            Field = Calculator.Format(Field, true);
        }

        // Метод очистки всех введённых данных калькулятора

        void Clear()
        {
            Operand1 = 0;
            Operand2 = 0;
            Field = "0";
            Operation = Operations.Empty;
            OperationField = "";
        }

        // Метод ввода операции и первого операнда

        void Input(Operations operation)
        {
            if (Field == "Нельзя")
            {
                Clear();
                return;
            }

            string result;
            string opSign = "";
            switch (operation)
            {
                case Operations.Empty:
                    return;
                case Operations.Plus:
                    opSign = "+";
                    break;
                case Operations.Minus:
                    opSign = "-";
                    break;
                case Operations.Divide:
                    opSign = "/";
                    break;
                case Operations.Multiply:
                    opSign = "x";
                    break;
            }

            if (Operation != Operations.Empty)
            {
                result = Calculator.Calculate(Operand1, double.Parse(Field), Operation);
                Operand1 = result != "Нельзя" ? double.Parse(result) : 0;
            }
            else
            {
                Operand1 = double.Parse(Field);
            }

            OperationField = Calculator.Format($"{Operand1}", false) + $" {opSign}";
            Operation = operation;
            CleanEntry();
        }

        // Метод подсчёта конечного результата

        void Equal()
        {
            if (Field == "Нельзя")
            {
                Clear();
                return;
            }
            if (Operation == Operations.Empty)
            {
                return;
            }

            Operand2 = double.Parse(Field);
            OperationField += " " + Calculator.Format($"{Operand2}", false) + " =";
            Field = Calculator.Calculate(Operand1, Operand2, Operation);
            Operation = Operations.Empty;
        }

        #endregion

        // Конструктор окна
        public MainWindowViewModel()
        {
            field = "0";
            operationField = "";

            OneButton = new RelyCommand(OnOneButtonExecute);
            TwoButton = new RelyCommand(OnTwoButtonExecute);
            ThreeButton = new RelyCommand(OnThreeButtonExecute);
            FourButton = new RelyCommand(OnFourButtonExecute);
            FiveButton = new RelyCommand(OnFiveButtonExecute);
            SixButton = new RelyCommand(OnSixButtonExecute);
            SevenButton = new RelyCommand(OnSevenButtonExecute);
            EightButton = new RelyCommand(OnEightButtonExecute);
            NineButton = new RelyCommand(OnNineButtonExecute);
            ZeroButton = new RelyCommand(OnZeroButtonExecute);

            BackspaceButton = new RelyCommand(OnBackspaceButtonExecute);
            CleanEntryButton = new RelyCommand(OnCleanEntryButtonExecute);
            ChangeSignButton = new RelyCommand(OnChangeSignButtonExecute);
            CommaButton = new RelyCommand(OnCommaButtonExecute);

            PlusButton = new RelyCommand(OnPlusButtonExecute);
            MinusButton = new RelyCommand(OnMinusButtonExecute);
            DivideButton = new RelyCommand(OnDivideButtonExecute);
            MultiplyButton = new RelyCommand(OnMultiplyButtonExecute);
            ClearButton = new RelyCommand(OnClearButtonExecute);
            EqualButton = new RelyCommand(OnEqualButtonExecute);
        }
    }
}
