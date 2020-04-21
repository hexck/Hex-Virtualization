namespace Hex.VM.Tests
{
    public class Maths
    {
        public int Sum { get; set; }
        private int _x;
        private int _y;
        
        public Maths(int x, int y)
        {
            _x = x;
            _y = y;
            Sum = _x + _y;
        }

        public int Add()
        {
            return _x + _y;
        }
        
        public int Subtract()
        {
            return _x - _y;
        }
        
        public int Multiply()
        {
            return _x * _y;
        }
        
        public int Divide()
        {
            return _x / _y;
        }
    }
}