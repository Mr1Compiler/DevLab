using NUnit.Framework;
using NUnit.Framework.Legacy;
using Sparky;

namespace SparkyNUnitTest;

[TestFixture]
public class CalculatorNUnitTests
{
    [Test]
    public void AddNumbers_InputTwoInt_GetCorrectAddition()
    {
        // Arrange 
        Calculator calc = new();

        // Act
        int result = calc.AddNumbers(10, 20);
        
        // Assert
        ClassicAssert.AreEqual(30, result);
    }

    [Test]
    public void IsOddChecker_InputEvenNumber_ReturnFalse()
    {
        // Arrange
        Calculator calc = new();

        // Act
        bool isOdd = calc.IsOddNumber(2);
       
        // Assert
        ClassicAssert.IsFalse(isOdd);
    }
    
    [Test]
    public void IsOddChecker_InputOffNumber_ReturnTrue()
    {
        // Arrange
        Calculator calc = new();

        // Act
        bool isOdd = calc.IsOddNumber(11);
       
        // Assert
        ClassicAssert.IsTrue(isOdd);
    }
    
    [Test]
    [TestCase(11)]
    [TestCase(13)]
    public void IsOddChecker_InputOddNumber_ReturnTrue(int a)
    {
        // Arrange
        Calculator calc = new();
        
        // Act
        bool isOdd = calc.IsOddNumber(a);
        
        // Assert
        ClassicAssert.IsTrue(isOdd);
    }
    
    [Test]
    [TestCase(10, ExpectedResult = false)]
    [TestCase(13, ExpectedResult = true)]
    public bool IsOddChecker_InputOddNumber_ReturnTrueIfOdd(int a)
    {
        // Arrange
        Calculator calc = new();
        return calc.IsOddNumber(a); 
    }
    
    [Test]
    [TestCase(5.4, 10.5)]
    [TestCase(5.34, 10.34)]
    [TestCase(5.23, 34.45)]
    public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double a, double b)
    {
        // Arrange
        Calculator calc = new();
        
        // Act 
        double result = calc.AddTwoDouble(a, b);
        
        ClassicAssert.AreEqual(15.9, result, 1);
    }
}