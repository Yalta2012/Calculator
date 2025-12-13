using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace CalculatorBack;

public enum CalculatorCondition
{
    Input,
    Operator,
    Equal,
    Error
}

public class JournalItem : INotifyPropertyChanged
{
    public String? FirstOperator { get; set; }
    public String? SecondOperator { get; set; }
    public String? Operator { get; set; }
    private string? _display;
    public event PropertyChangedEventHandler? PropertyChanged;

    public string? Display
    {
        get => _display;
        set
        {
            if (_display != value)
            {
                _display = value;
                OnPropertyChanged(nameof(Display));
            }
        }
    }
    public CalculatorCondition Condition { get; set; }
    public JournalItem(String fo, String so, String op, String dsp, CalculatorCondition cnd) {
        FirstOperator = fo;
        SecondOperator = so;
        Operator = op;
        Display = dsp;
        Condition = cnd;
    }

    override public String ToString()
    {
        return (FirstOperator ?? "") + " " + (Operator ?? "") + " " + (SecondOperator == null ? "" : FirstOperator == null ? Display : SecondOperator) + " = " + (Display ?? "");
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}

public class Calc : INotifyPropertyChanged // ICalc, 
{
    private String _display = "0";
    private String? _firstOperand = null;
    private String? _secondOperand = null;
    private String? _operator = null;
    private bool _isFloat = false;
    private bool _showMemoryMemoryButtons = false;
    private bool _showCalculatorMemoryButtons = false;
    private CalculatorCondition _condition = CalculatorCondition.Input;
    private readonly Dictionary<string, Func<double, double, double>> _binaryOperationMap = new();
    private readonly Dictionary<string, Func<double, double>> _unaryOperationMap = new();
   
    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<JournalItem> Journal { get; private set; } = new ObservableCollection<JournalItem>();
    public ObservableCollection<JournalItem> Memory { get; private set; } = new ObservableCollection<JournalItem>();

    public ObservableCollection<JournalItem> CurrentCollection { get; private set; } = new ObservableCollection<JournalItem>();
    public ICommand InputCommand { get; private set; }
    public ICommand LoadFromJournalCommand { get; private set; }
    public ICommand ClearCurrentCollectionCommand { get; private set; }

    public ICommand ShowJournalCommand { get; private set; }
    public ICommand ShowMemoryCommand { get; private set; }

    public String Display
    {
        get => _display;
        private set
        {
            _display = value;
            OnPropertyChanged(nameof(Display));
        }
    }

    public bool ShowMemoryMemoryButtons
    {
        get => _showMemoryMemoryButtons;
        private set
        {
            _showMemoryMemoryButtons = value;
            OnPropertyChanged(nameof(ShowMemoryMemoryButtons));
        }
    }
    public Calc()
    {

        _binaryOperationMap.Add("+", (x, y) => { return x + y; });
        _binaryOperationMap.Add("-", (x, y) => { return x - y; });
        _binaryOperationMap.Add("*", (x, y) => { return x * y; });
        _binaryOperationMap.Add("/", (x, y) => { return x / y; });

        _binaryOperationMap.Add("%", (x, y) => { return x * y / 100; });
        _binaryOperationMap.Add("1/x", (x, y) => { return 1 / x; });
        _binaryOperationMap.Add("x^2", (x, y) => { return x * x; });
        _binaryOperationMap.Add("√x", (x, y) => { return Math.Sqrt(x); });
        _binaryOperationMap.Add("+/-", (x, y) => { return -x; });

        //_unaryOperationMap.Add("%", (x) => { return -x; });
        _unaryOperationMap.Add("1/x", (x) => { return 1/x; });
        _unaryOperationMap.Add("x^2", (x) => { return x*x; });
        _unaryOperationMap.Add("√x", Math.Sqrt );
        _unaryOperationMap.Add("+/-", (x) => { return -x; });

        InputCommand = new Command<String>(Input);
        LoadFromJournalCommand = new Command<JournalItem>(LoadFromJournal);
        ClearCurrentCollectionCommand = new Command(ClearCurrentCollection);

        ShowJournalCommand = new Command(ShowJournal);
        ShowMemoryCommand = new Command(ShowMemory);


        //MemoryCommand = new Command<String>(MemoryInput);
        CurrentCollection = Journal;

    }  

    private void MemoryInput(String inputString)
    {
        switch (inputString)
        {
            case "MC":
                Memory.Clear();
                break;
            case "MR":
                if (Memory.Count() > 0)
                    Display = Memory.Last().Display;
                break;
            case "M+":
                if (Memory.Count() > 0)
                {
                    Memory.Last().Display = Calculate(Memory.Last().Display, "+", Display);
                    CurrentCollection = Memory;
                    OnPropertyChanged(nameof(CurrentCollection));
                }

                break;
            case "M-":
                if (Memory.Count() > 0)
                {
                    Memory.Last().Display = Calculate(Memory.Last().Display, "-", Display);
                    CurrentCollection = Memory;
                    OnPropertyChanged(nameof(CurrentCollection));
                }

                break;
            case "MS":
                Memory.Add(new JournalItem(null, null, null, Display, CalculatorCondition.Input));
                _condition = CalculatorCondition.Equal;
                break;
            default:
                break;
        }
    }

    private void ShowJournal()
    {
        _showMemoryMemoryButtons = false;
        CurrentCollection = Journal;
        OnPropertyChanged(nameof(CurrentCollection));
    }
    private void ShowMemory()
    {
        _showMemoryMemoryButtons = true;
        CurrentCollection = Memory;
        OnPropertyChanged(nameof(CurrentCollection));

    }
    private String Calculate(String first, String op)
    {
        var result =  _unaryOperationMap[op](Double.Parse(first));
        if (!Double.IsFinite(result))
        {
            _condition = CalculatorCondition.Error;
            _firstOperand = null;
            _secondOperand = null;
            _operator = null;
            return "Error";
        }
        //SaveToJournal();
        return result.ToString();
    }

    private String Calculate(String first, String op, String second)
    {
        var result = _binaryOperationMap[op](Double.Parse(first), Double.Parse(second));
        if (!Double.IsFinite(result))
        {
            _condition = CalculatorCondition.Error;
            return "Error";
        }
        //aveToJournal();
        return result.ToString();
    }
    public void Input(String inputString)
    {
        if (",0123456789".Contains(inputString))
        {
            InputDigit(inputString);
            return;
        }
        if(inputString == "=")
        {
            Equal();
            return;
        }
        if ("+-/*".Contains(inputString))
        {
            InputBinaryOperator(inputString);
            return;
        }
        if ("CE⌫".Contains(inputString))
        {
            InputClear(inputString);
            return;
        }
        if(inputString == "%")
        {
            CalculatePercent();
            return;
        }
        if("MCMRM+M-MS".Contains(inputString))
        {
            MemoryInput(inputString);
            return;
        }
        InputUnaryOperator(inputString);
    }
    private void AddSymbol(String symbol)
    {
        if (_isFloat && symbol == ",") return;
        if (symbol == ",")
            _isFloat = true;
        else if (Display == "0")
            Display = "";
        Display += symbol;
    }
    public void InputDigit(String inputString)
    {
        if (_condition == CalculatorCondition.Error || _condition == CalculatorCondition.Equal)
        {
            Display = inputString;
            _firstOperand = null;
            _secondOperand = null;
            _operator = null;
        }
        else if (_firstOperand == null)
        {
            AddSymbol(inputString);
        }
        else if (_secondOperand == null)
        {
            if (_condition == CalculatorCondition.Input)
            {
                AddSymbol(inputString);
                //_firstOperand = null;
                //_secondOperand = null;
                //_operator = null;
            }
            else
                Display = inputString;
        }
        else
        {
            Display = inputString;
            _firstOperand = null;
            _secondOperand = null;
            _operator = null;
        }
        Normalize();
        OnPropertyChanged(nameof(Display));
        _condition = CalculatorCondition.Input;

    }

    private void CalculatePercent()
    {
        if (_condition == CalculatorCondition.Error) return;
        _condition = CalculatorCondition.Operator;
        Display = Calculate(_firstOperand ?? "0", "%", Display);
        _firstOperand = null;
        _secondOperand = null;
        _operator = null;
        OnPropertyChanged(nameof(Display));

    }
    private void InputUnaryOperator(String inputString)
    {
        if (Display == "0" && inputString == "+/-" || _condition == CalculatorCondition.Error) return;
        _condition = CalculatorCondition.Operator;
        Display = Calculate(Display, inputString);
        _firstOperand = null;
        _secondOperand = null;
        _operator = null;
        //if(_condition != CalculatorCondition.Error) SaveToJournal();
        OnPropertyChanged(nameof(Display));

    }

    private void InputBinaryOperator(String inputString)
    {
        if (_condition == CalculatorCondition.Error) return;
        if (_condition == CalculatorCondition.Operator)
        {
            _operator = inputString;
            return;
        }
        if (_firstOperand == null)
        {
            _firstOperand = Display;
        }
        else
        {
            if(_condition != CalculatorCondition.Equal) {
                Display = Calculate(_firstOperand, _operator, Display);
                if (_condition != CalculatorCondition.Error) SaveToJournal();

            }
            _secondOperand = null;
            _firstOperand = Display;
        }
        _operator = inputString;

        if(_condition != CalculatorCondition.Equal)
            _condition = CalculatorCondition.Operator;
        Normalize();
        OnPropertyChanged(nameof(Display));

    }

    public void Equal()
    {
        if (_condition == CalculatorCondition.Error) return;

        if(_firstOperand == null /*|| _operator == null*/)
        {
           //_firstOperand = Display;
            SaveToJournal();

        }
       
        else if (_secondOperand == null && _operator != null)
        {

            _secondOperand = Display;
            Display = Calculate(_firstOperand, _operator, Display);
            Normalize();
            SaveToJournal();

            _firstOperand = Display;


        }
        else if (_operator != null)
        {
            Display = Calculate(_firstOperand, _operator, _secondOperand);
            Normalize();
            SaveToJournal();

            _firstOperand = Display;

        }

        if (_condition != CalculatorCondition.Error)
        {
            _condition = CalculatorCondition.Equal;
        }
        OnPropertyChanged(nameof(Display));
    }

    public void InputClear(String inputString)
    {
        switch (inputString)
        {
            case "CE":
                Display = "0";
                break;
            case "C":
                Display = "0";
                _firstOperand = null;
                _secondOperand = null;
                _operator = null;
                break;
            case "⌫":
                if (Display.StartsWith("-") && Display.Length == 2 || Display.Length == 1)
                    Display = "0";
                else
                    Display = Display.Substring(0, Display.Length - 1);
                    break;
            default:
                break;
        }
        //Normalize();
        OnPropertyChanged(nameof(Display));
        _condition = CalculatorCondition.Input;
        _isFloat = false;

    }


    private void Normalize()
    {
        _isFloat = Display.Contains(",");
        if (Display == ",") Display = "0,";
    }
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void LoadFromJournal(JournalItem journalItem)
    {
        _firstOperand = journalItem.FirstOperator;
        _secondOperand = journalItem.SecondOperator;
        _operator = journalItem.Operator;
        _condition = journalItem.Condition;
        Display = journalItem.Display;
        OnPropertyChanged(nameof(Display));
    }

    private void SaveToJournal()
    {
        Journal.Add( new JournalItem(_firstOperand, _secondOperand, _operator, _display, _condition));
        OnPropertyChanged(nameof(Journal));
    }

    public void ClearCurrentCollection()
    {
        CurrentCollection.Clear();
    }

}

//public class CalculatorCommand: ICommand
//{
//    private Action<string> _action;

//    public CalculatorCommand(Action<string> action)
//    {
//        _action = action;
//    }

//    public void Execute(object? parameter)
//    {
//        _action(parameter.ToString());
//    }

//    public event EventHandler? CanExecuteChanged;

//    public bool CanExecute(object? parameter)
//    {
//        return true;
//    }
//}
public class CalculatorCommand : ICommand
{
    private readonly Action<object> _execute;

    // Конструктор для методов с параметром
    public CalculatorCommand(Action<object> execute)
    {
        _execute = execute;
    }

    // Конструктор для методов без параметра (лямбда)
    public CalculatorCommand(Action execute)
    {
        _execute = _ => execute();
    }

    public void Execute(object parameter) => _execute(parameter);

    public bool CanExecute(object parameter) => true;

    public event EventHandler CanExecuteChanged;
}
