using PostSharp.Patterns.Contracts;

namespace PostSharpAndPaket
{
    public class ClassUsingPostSharp
    {
        [Range(1, 7)]
        public int Counter { get; set; }
    }
}
