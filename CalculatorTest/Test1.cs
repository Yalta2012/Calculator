using CalculatorBack;
namespace CalculatorTest
{
    [TestClass]
    public sealed class Test1
    {


        [TestMethod]
        public void Display_ShouldBeZero_WhenNewCalc()
        {
            var calc = new Calc();
            Assert.AreEqual("0", calc.Display);
        }

        [TestMethod]
        public void Display_3_3()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("1");
            Assert.AreEqual("1", calc.Display);
        }


        [TestMethod]
        public void Display_32_32()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");

            Assert.AreEqual("12", calc.Display);
        }

        [TestMethod]
        public void Display_12add_12()
        {
            var calc = new Calc();
            calc.InputCommand.Execute("1");
            calc.InputCommand.Execute("2");
            calc.InputCommand.Execute("+");

            Assert.AreEqual("12", calc.Display);
        }


        [TestMethod]
        public void Display_12add34_34()
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
        public void Display_12add34eq_46()
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
        public void Display_12add34eqeq_46()
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
        public void Display_12add34eqeqeq_46()
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
        public void Display_12add34sub_46()
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
        public void Display_12add34subsub_46()
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
        public void Display_12Add34Eq56_56()
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
        public void Display_12Add34EqAdd56Eq_102()
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
    }
}

