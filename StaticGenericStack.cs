
namespace MathematicalExpressionEvaluator
{
    interface IStack<T>
    {
        void Push(T item); 
        T Pop(); 
        int Count { get; } // The Count method returns the number of items in the stack
        void Clear(); // Clear removes all items from the stack

    } //IStack

    class StaticGenericStack<T> : IStack<T>
    {
        //Private members
        private T[] array; // The ToArray method will return a copy of the private array structure.
        private int topIndex = -1; // keep track of the index of the top item in the stack.
        private const int maxSize = 100;


        public StaticGenericStack()
        { 
        array = new T[maxSize];
        }
        public int Count => topIndex + 1;

        public void Push(T item)
        {
            if (topIndex >= maxSize - 1)
            {
                throw new InvalidOperationException("Stack Overflow");
            }
            array[++topIndex] = item;

        }

        public T Pop()
        {
            if (topIndex < 0)
            {
                throw new InvalidOperationException("Stack underFlow");
            }
            return array[topIndex--];
        }

        public void Clear()
        {
           topIndex = -1;
        }



       

    }
}
