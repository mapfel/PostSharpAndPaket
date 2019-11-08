using PostSharp.Patterns.Contracts;
using System;
using Xunit;

namespace PostSharpAndPaket
{
    public class ClassUsingPostSharp
    {
        [Range(1, 7)]
        public int Counter { get; set; }
    }

    public class ClassUsingPostSharpTest
    {
        [Fact]
        public void Verify_That_The_RangeAttribute_Is_Included()
        {
            var sut = new ClassUsingPostSharp();

            var exception1 = Record.Exception(() => sut.Counter = 0);
            Assert.NotNull(exception1);
            Assert.IsType<ArgumentOutOfRangeException>(exception1);
            Assert.StartsWith($"The property 'Counter' must be between 1 and 7.", exception1.Message);

            var exception2 = Record.Exception(() => sut.Counter = 8);
            Assert.NotNull(exception2);
            Assert.IsType<ArgumentOutOfRangeException>(exception2);
            Assert.StartsWith($"The property 'Counter' must be between 1 and 7.", exception2.Message);
        }
    }
}
