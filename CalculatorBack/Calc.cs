using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;


namespace CalculatorBack;


public class Calc : INotifyPropertyChanged // ICalc, 
{
    private String _display = "0";
    private String _firstOperand = "0";
    private String? _operator;
    private bool isFloat = false;
    private bool isError = false;
    private readonly Dictionary<string, Func<double, double, double>> _binaryOperationMap = new();
    private readonly Dictionary<string, Func<double, double>> _unaryOperationMap = new();
    public event PropertyChangedEventHandler? PropertyChanged;

    public ICommand InputDigitCommand { get; private set; }
    public ICommand InputUnaryOperatorCommand { get; private set; }
    public ICommand InputBinaryOperatorCommand { get; private set; }
    public ICommand ClearCommand { get; private set; }
    public ICommand CalculateCommand { get; private set; }

    public Calc()
    {
        //InputCommand = new CalculatorCommand(Input);
        
        //LoadFromJournalCommand = new CalculatorCommand(x => Display = Convert.ToDouble(x));
        //MemoryCommand = new CalculatorCommand(MemoryCalc);
        //DeleteButtonsCommand = new CalculatorCommand(DeleteCalc);

        //RemoveMemoryItemCommand = new GenericCommand<CollectionItemWrapper>(RemoveMemoryItem);
        //AddToMemoryItemCommand = new GenericCommand<CollectionItemWrapper>(AddToMemoryItem);
        //SubtractFromMemoryItemCommand = new GenericCommand<CollectionItemWrapper>(SubtractFromMemoryItem);
        //CurrentCollection = new ObservableCollection<CollectionItemWrapper>();
        _binaryOperationMap.Add("+", (x, y) => { return x + y; });
        _binaryOperationMap.Add("-", (x, y) => { return x - y; });
        _binaryOperationMap.Add("x", (x, y) => { return x * y; });
        _binaryOperationMap.Add("/", (x, y) => { return x / y; });

        _binaryOperationMap.Add("%", (x, y) => { return -x; });
        _binaryOperationMap.Add("1/x", (x, y) => { return 1 / x; });
        _binaryOperationMap.Add("x^2", (x, y) => { return x * x; });
        _binaryOperationMap.Add("√x", (x, y) => { return Math.Sqrt(x); });
        _binaryOperationMap.Add("+/-", (x, y) => { return -x; });

        _unaryOperationMap.Add("%", (x) => { return -x; });
        _unaryOperationMap.Add("1/x", (x) => { return 1/x; });
        _unaryOperationMap.Add("x^2", (x) => { return x*x; });
        _unaryOperationMap.Add("√x", Math.Sqrt );
        _unaryOperationMap.Add("+/-", (x) => { return -x; });

        InputDigitCommand = new CalculatorCommand(Input);
        InputUnaryOperatorCommand = new CalculatorCommand(InputUnaryOperator);
        InputBinaryOperatorCommand = new CalculatorCommand(InputBinaryOperator);
        ClearCommand = new CalculatorCommand(InputClear);
        CalculateCommand = new CalculatorCommand(Calculate);

    }
    public void Input(String inputString)
    {
        if (Display == "0")
        {
            if (inputString == ",")
                Display = Display + inputString;
            else if (inputString != "0")
                Display = inputString;
        }
        else if (inputString != "," || !isFloat)
                Display += inputString;
        if (inputString == ",") isFloat = true;
        OnPropertyChanged(nameof(Display));

    }

    public void InputUnaryOperator(String inputString)
    {
        if (Display == "0" && inputString == "+/-") return;
        Display = _unaryOperationMap[inputString](Double.Parse(Display)).ToString();
        Normalize();
        OnPropertyChanged(nameof(Display));
    }

    public void InputBinaryOperator(String inputString)
    {
        _operator = inputString;
        _firstOperand = Display;
        Display = "0";
        OnPropertyChanged(nameof(Display));
    }

    public void Calculate(String s)
    {
        if (_operator == null) return;
        Display = _binaryOperationMap[_operator]( Double.Parse(_firstOperand), Double.Parse(Display)).ToString();
        Normalize();
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
                _firstOperand = "0";
                break;
            case "⌫":
                if (Math.Abs(Double.Parse(Display)).ToString().Length > 1)
                    Display = Display.Substring(0, Display.Length - 1);
                else
                    Display = "0";
                    break;
            default:
                break;
        }
        Normalize();
        OnPropertyChanged(nameof(Display));
    }

    public String Display
    {
        get => _display;
        private set
        {
            _display = value;
            OnPropertyChanged(nameof(Display));
        }
    }
    private void Normalize()
    {
        isFloat = Display.Contains(",");
    }
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}

public class CalculatorCommand: ICommand
{
    private Action<string> _action;

    public CalculatorCommand(Action<string> action)
    {
        _action = action;
    }

    public void Execute(object? parameter)
    {
        _action(parameter.ToString());
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }
}
//public string ToggleButtonText => _showMemory ? "Память" : "Журнал";
//public ICommand RemoveMemoryItemCommand { get; private set; }
//public ICommand DeleteButtonsCommand { get; private set; }

//public ICommand AddToMemoryItemCommand { get; private set; }
//public ICommand SubtractFromMemoryItemCommand { get; private set; }
//public ObservableCollection<CollectionItemWrapper> CurrentCollection { get; private set; }

//public bool IsMemoryEmpty
//{
//    get => _memory.Count == 0;
//}
//public double Display
//{
//    get => display;
//    private set
//    {
//        display = value;
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Display)));
//    }
//}


//private bool _showMemory = false;
//private double? _firstOparand = null;
//private double? _secondOparand = null;

//private readonly Dictionary<string, Func<double, double, double>> _operationMap = new();
//private Func<double, double, double> _operation;
//private double display;

//public event PropertyChangedEventHandler? PropertyChanged;
//public bool ShowMemoryButtons => _showMemory;

//public ICommand InputCommand { get; private set; }
//public ICommand MemoryCommand { get; private set; }

//public ICommand LoadFromJournalCommand { get; private set; }
//private ObservableCollection<MemoryItem> _memory = new ObservableCollection<MemoryItem>();

//public ObservableCollection<MemoryItem> MemoryCollection => new ObservableCollection<MemoryItem>(_memory);
//public ObservableCollection<double> Journal { get; } = new();


//public string CollectionTitle => _showMemory ? "Память" : "Журнал";
//private bool _needToClean = false;
//private void DeleteCalc(string inputString)
//{
//    if (inputString == "C")
//    {
//        _firstOparand = null;
//        _secondOparand = null;
//        _operation = null;
//        Display = 0;
//    }
//    else if (inputString == "CE")
//    {
//        Display = 0;
//    }
//    else
//    {

//    }
//}
//private void Input(string inputString)
//{
//    var isOperation = _operationMap.ContainsKey(inputString);
//    if (isOperation)
//    {
//        if (_operation != null && _firstOparand.HasValue)
//        {
//            Calculate();
//        }
//        _operation = _operationMap[inputString];
//        _secondOparand = null;
//        return;
//    }

//    if (inputString == "=" && _operation != null)
//    {
//        Calculate();
//        return;
//    }

//    if (_operation != null && _firstOparand == null && _secondOparand.HasValue)
//    {
//        Reset();
//    }

//    if (_operation != null && _firstOparand == null)
//    {
//        _firstOparand = Display;
//        Display = 0;
//    }
//    if (_needToClean)
//    {
//        _needToClean = false;
//        Display = 0;
//    }

//    if (Display == 0 && _operation == null)
//    {
//        if (inputString != "=")
//            Display = Convert.ToDouble(inputString);
//    }
//    else
//    {
//        if (inputString != "=")
//            Display = Display * 10 + Convert.ToDouble(inputString);
//    }

//}
//private void UpdateCurrentCollection()
//{
//    CurrentCollection.Clear();

//    if (_showMemory)
//    {
//        foreach (var memoryItem in _memory)
//        {
//            CurrentCollection.Add(new CollectionItemWrapper(memoryItem));
//        }
//    }
//    else
//    {
//        foreach (var journalValue in Journal)
//        {
//            CurrentCollection.Add(new CollectionItemWrapper(journalValue));
//        }

//    }

//    OnPropertyChanged(nameof(CurrentCollection));
//}

//private void MemoryCalc(string inputString)
//{
//    switch (inputString)
//    {
//        case "MS":
//            var newItem = new MemoryItem(Display);
//            _memory.Insert(0, newItem);
//            _firstOparand = null;
//            _operation = null;
//            _secondOparand = null;
//            _needToClean = true;
//            if (_showMemory)
//                CurrentCollection.Insert(0, new CollectionItemWrapper(newItem));
//            break;
//        case "MC":
//            _memory.Clear();
//            if (_showMemory) UpdateCurrentCollection();
//            break;
//        case "M+":
//            _memory[0].Value += Convert.ToDouble(Display);
//            if (_showMemory) UpdateCurrentCollection();
//            break;
//        case "M-":
//            _memory[0].Value -= Convert.ToDouble(Display);
//            if (_showMemory) UpdateCurrentCollection();
//            break;
//        case "MR" when _memory.Count > 0:
//            Display = _memory[0].Value;
//            break;
//        case "TOGGLE_MEMORY":
//            _showMemory = !_showMemory;
//            UpdateCurrentCollection();
//            break;
//    }
//    UpdateMemoryProperties();
//}
//private void OnPropertyChanged(string propertyName)
//{
//    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//}
//private void Calculate()
//{
//    _firstOparand ??= Display;
//    _secondOparand ??= Display;
//    try
//    {
//        Display = _operation(_firstOparand.Value, _secondOparand.Value);

//        if (double.IsNaN(Display))
//        {
//            Display = 0;
//            throw new InvalidOperationException("Результат операции не определён");
//        }

//        if (double.IsInfinity(Display))
//        {
//            Display = 0;
//            throw new InvalidOperationException("Результат операции не определён");
//        }
//    }
//    catch (Exception e)
//    {
//        Display = 0;
//        return;
//    }
//    _firstOparand = null;

//    Journal.Insert(0, Display);
//    UpdateCurrentCollection();
//    UpdateMemoryProperties();
//}

//private void Reset()
//{
//    Display = 0;
//    _firstOparand = null;
//    _secondOparand = null;
//    _operation = null;
//}



//private void RemoveMemoryItem(CollectionItemWrapper wrapper)
//{
//    if (wrapper?.MemoryItem != null)
//    {
//        _memory.Remove(wrapper.MemoryItem);
//        CurrentCollection.Remove(wrapper);
//        UpdateMemoryProperties();
//    }
//}

//private void AddToMemoryItem(CollectionItemWrapper wrapper)
//{
//    if (wrapper?.MemoryItem != null)
//    {
//        wrapper.MemoryItem.Value += Display;
//        UpdateMemoryProperties();
//    }
//}

//private void SubtractFromMemoryItem(CollectionItemWrapper wrapper)
//{
//    if (wrapper?.MemoryItem != null)
//    {
//        wrapper.MemoryItem.Value -= Display;
//        UpdateMemoryProperties();
//    }
//}
//private void UpdateMemoryProperties()
//{
//    OnPropertyChanged(nameof(IsMemoryEmpty));
//    OnPropertyChanged(nameof(ShowMemoryButtons));
//    OnPropertyChanged(nameof(ToggleButtonText));
//    OnPropertyChanged(nameof(CollectionTitle));
//}


//}
//public class MemoryItem : INotifyPropertyChanged
//{
//    private double _value;

//    public double Value
//    {
//        get => _value;
//        set
//        {
//            _value = value;
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
//        }
//    }

//    public event PropertyChangedEventHandler? PropertyChanged;

//    public MemoryItem(double value)
//    {
//        Value = value;
//    }

//}

//public class CollectionItemWrapper : INotifyPropertyChanged
//{
//    private readonly MemoryItem _memoryItem;
//    private readonly double _journalValue;
//    private readonly bool _isMemoryItem;

//    public object Item => _isMemoryItem ? (object)_memoryItem : _journalValue;
//    public MemoryItem MemoryItem => _isMemoryItem ? _memoryItem : null;

//    public string DisplayValue
//    {
//        get
//        {
//            if (_isMemoryItem)
//                return _memoryItem.Value.ToString();
//            else
//                return _journalValue.ToString();
//        }
//    }

//    public event PropertyChangedEventHandler PropertyChanged;

//    public CollectionItemWrapper(MemoryItem memoryItem)
//    {
//        _memoryItem = memoryItem;
//        _isMemoryItem = true;

//        if (memoryItem != null)
//            memoryItem.PropertyChanged += OnMemoryItemPropertyChanged;
//    }

//    public CollectionItemWrapper(double journalValue)
//    {
//        _journalValue = journalValue;
//        _isMemoryItem = false;
//    }

//    private void OnMemoryItemPropertyChanged(object sender, PropertyChangedEventArgs e)
//    {
//        if (e.PropertyName == nameof(MemoryItem.Value))
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayValue)));
//        }
//    }
//}

//public class GenericCommand<T> : ICommand
//{
//    private readonly Action<T> _execute;
//    private readonly Func<T, bool> _canExecute;

//    public GenericCommand(Action<T> execute, Func<T, bool> canExecute = null)
//    {
//        _execute = execute;
//        _canExecute = canExecute;
//    }

//    public event EventHandler CanExecuteChanged;

//    public bool CanExecute(object parameter)
//    {
//        return parameter is T && (_canExecute == null || _canExecute((T)parameter));
//    }

//    public void Execute(object parameter)
//    {
//        if (parameter is T typedParameter)
//        {
//            _execute(typedParameter);
//        }
//    }

//    public void RaiseCanExecuteChanged()
//    {
//        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
//    }
//}

//public class CalculatorCommand : ICommand
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

//public interface ICalc
//{
//    double Display { get; }
//    ICommand InputCommand { get; }
//    bool IsMemoryEmpty { get; }
//    ICommand LoadFromJournalCommand { get; }
//    ObservableCollection<double> Journal { get; }
//    ObservableCollection<CollectionItemWrapper> CurrentCollection { get; }
//    string CollectionTitle { get; }
//    ICommand MemoryCommand { get; }
//    ICommand DeleteButtonsCommand { get; }

//}