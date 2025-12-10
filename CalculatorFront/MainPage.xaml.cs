
using CalculatorBack;
namespace CalculatorFront
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            BindingContext = new Calc();
            InitializeComponent();
        }

      
    }
}
