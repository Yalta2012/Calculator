using CalculatorBack;
namespace CalculatorTest
{
    [TestClass]
    public sealed class Test1
    {


        [TestMethod]
        public void Дисплей_0_При_старте()

        {
            var calc = new Calc();
            Assert.AreEqual("0", calc.Display);
        }
        [TestMethod]
        public void Дисплей_Ввод_одной_цифры()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("1");
            Assert.AreEqual("1", calc.Display);
        }


        [TestMethod]
        public void Дисплей_Ввод_двух_цифр()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");

            Assert.AreEqual("12", calc.Display);
        }


        [TestMethod]
        public void Дисплей_ВводНуля()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("0");

            Assert.AreEqual("0", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ВводДвухНулей()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("0");

            Assert.AreEqual("0", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ВводЗапятойИНуля()
        {
            var calc = new Calc();
            calc.InputCommand.Execute(",");
            calc.InputCommand.Execute("0");

            Assert.AreEqual("0,0", calc.Display);
        }

        [TestMethod]
        public void Дисплей_Ввод_запятой()
        {
            var calc = new Calc();
            calc.InputCommand.Execute(",");

            Assert.AreEqual("0,", calc.Display);
        }

        [TestMethod]
        public void Дисплей_Ввод_двух_запятых_подряд()
        {
            var calc = new Calc();
            calc.InputCommand.Execute(",");
            calc.InputCommand.Execute(",");

            Assert.AreEqual("0,", calc.Display);
        }

        [TestMethod]
        public void Дисплей_Ввод_числа_и_запятой()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute(",");

            Assert.AreEqual("1,", calc.Display);
        }

        [TestMethod]
        public void Дисплей_Ввод_числа_запятой_и_числа()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute(",");
            calc.InputCommand.Execute("2");

            Assert.AreEqual("1,2", calc.Display);
        }

        [TestMethod]
        public void Дисплей_Ввод_числа_запятой_числа_и_запятой()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute(",");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute(",");

            Assert.AreEqual("1,2", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ВводОтрицаня()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("+/-");

            Assert.AreEqual("0", calc.Display);
        }


        [TestMethod]
        public void Дисплей_ВводОтрицательногоНуля()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("+/-");

            Assert.AreEqual("0", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ВводОтрицательногоНуляСЗапятой()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute(",");
            calc.InputCommand.Execute("+/-");

            Assert.AreEqual("-0,", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ВводНуляСЗапятойИДвойгогоОтрицания()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute(",");
            calc.InputCommand.Execute("+/-");
            calc.InputCommand.Execute("+/-");

            Assert.AreEqual("0,", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ввод_числа_и_оператора()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");

            Assert.AreEqual("12", calc.Display);
        }


        [TestMethod]
        public void Дисплей_ввод_числа_оператора_и_числа()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("4");

            Assert.AreEqual("34", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ВычислениеВыражения()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("4");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("46", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ОтображаетРезультатСуммы_ПриОтсутсвииПервогоОперанда()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("12", calc.Display);
        }



        [TestMethod]
        public void Дисплей_ПовторноеВычисление()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("4");
            calc.InputCommand.Execute("=");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("80", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ТройноеВычисление()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("4");
            calc.InputCommand.Execute("=");
            calc.InputCommand.Execute("=");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("114", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ВычислениеИНовыйОператор()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("4");
            calc.InputCommand.Execute("-");

            Assert.AreEqual("46", calc.Display);
        }


        [TestMethod]
        public void Дисплей_ВычислениеИДваНовыхОператора()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("4");
            calc.InputCommand.Execute("-");
            calc.InputCommand.Execute("-");

            Assert.AreEqual("46", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ВычислениеИВводНовогоОперанда()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("4");
            calc.InputCommand.Execute("=");
            calc.InputCommand.Execute("5");
            calc.InputCommand.Execute("6");

            Assert.AreEqual("56", calc.Display);
        }

        [TestMethod]
        public void Дислплей_СложениеТрехЧиселЧерезРавно()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("4");
            calc.InputCommand.Execute("=");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("5");
            calc.InputCommand.Execute("6");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("102", calc.Display);
        }

        [TestMethod]
        public void Display_12Add34Eq56Sub78Eq_Neg22()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("4");
            calc.InputCommand.Execute("=");

            calc.InputCommand.Execute("5");
            calc.InputCommand.Execute("6");
            calc.InputCommand.Execute("-");
            calc.InputCommand.Execute("7");
            calc.InputCommand.Execute("8");
            calc.InputCommand.Execute("=");


            Assert.AreEqual("-22", calc.Display);
        }

        [TestMethod]
        public void Display_12Add34Add56_56()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("4");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("5");
            calc.InputCommand.Execute("6");

            Assert.AreEqual("56", calc.Display);
        }

        [TestMethod]
        public void Display_12Add34Add56Eq_102()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("4");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("5");
            calc.InputCommand.Execute("6");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("102", calc.Display);
        }

        [TestMethod]
        public void Display_2AddEq_4()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("4", calc.Display);
        }

        [TestMethod]
        public void Display_2Div0Eq_Error()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("/");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("Error", calc.Display);
        }

        [TestMethod]
        public void Display_2EqEq()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("=");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("2", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ДелениеНаНольУнарное()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1/x");
            Assert.AreEqual("Error", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ВозведениеВКвадрат()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute(",");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("x^2");

            Assert.AreEqual("1,44", calc.Display);
        }
        [TestMethod]
        public void Дисплей_ВозведениеВКвадратИСложение()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("x^2");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("5");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("149", calc.Display);
        }
        [TestMethod]
        public void Дисплей_ВозведениеСложениеИвозведениевКвадрат()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("5");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("x^2");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("149", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ПроцентБезВвода()
        {
            var calc = new Calc();

            calc.InputCommand.Execute("%");

            Assert.AreEqual("0", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ПроцентСВводомОдногоЧисла()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");

            calc.InputCommand.Execute("%");

            Assert.AreEqual("0", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ПроцентСВводомДвухЧисел()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("=");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("0");

            calc.InputCommand.Execute("%");

            Assert.AreEqual("66", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ПроцентСоСложением()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("%");

            Assert.AreEqual("66", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ПроцентСоСложениемИравно()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("%");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("286", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ПроцентСоСложениемИДвойнымравно()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("+");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("%");
            calc.InputCommand.Execute("=");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("352", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ПроцентСУмножением()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("*");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("%");

            Assert.AreEqual("0,3", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ПроцентСУмножениемИравно()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("*");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("%");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("66", calc.Display);
        }

        [TestMethod]
        public void Дисплей_ПроцентСУмножениемИДвойнымравно()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("*");
            calc.InputCommand.Execute("3");
            calc.InputCommand.Execute("0");
            calc.InputCommand.Execute("%");
            calc.InputCommand.Execute("=");
            calc.InputCommand.Execute("=");

            Assert.AreEqual("19,8", calc.Display);
        }
    }
}

