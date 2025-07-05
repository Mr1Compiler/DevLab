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
}