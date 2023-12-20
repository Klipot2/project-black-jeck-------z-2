namespace Casino.CardGames.Poker.Combinations
{
    public class ValueData
    {
        private const int BIGGEST_ACCEPTED_VALUE = 14;
        private const int SMALLEST_ACCEPTED_VALUE = 2;

        public string ValueString 
        { 
            get 
            {
                string valueStr = "";
                foreach (var number in _valueArray)
                {
                    valueStr += ValueToString(number);
                }
                return valueStr;
            } 
        }
        public byte[] ValueArray { get {return _valueArray;} }
        public int Size { get {return _valueDataSize;} }

        private readonly int _valueDataSize;
        private readonly byte[] _valueArray; 

        private int _lastFilledIndex;

        public ValueData(int size = 5)
        {
            _valueDataSize = size;
            _valueArray = new byte[_valueDataSize];
            for (int i = 0; i < _valueDataSize; i++)
            {
                _valueArray[i] = 0;
            }
            _lastFilledIndex = 0;
        }

        public void AddValue(int value)
        {
            if (value < SMALLEST_ACCEPTED_VALUE || value > BIGGEST_ACCEPTED_VALUE)
                throw new ArgumentOutOfRangeException(
                string.Format("ValueData.AddValue() only accepts values between {0} and {1}, but recieved {2}.",
                SMALLEST_ACCEPTED_VALUE, BIGGEST_ACCEPTED_VALUE, value));

            for (int i = 0; i < _valueDataSize; i++)
            {
                if(_valueArray[i] == 0)
                {
                    _valueArray[i] = Convert.ToByte(value);
                    _lastFilledIndex++;
                    return;
                }
            }

            throw new OverflowException("Value array is full, so ValueData.AddValue() failed.");
        }

        public void AddValueData(ValueData valueData)
        {
            foreach (var value in valueData.ValueArray)
            {
                AddValue(value);
            }
        }

        public void AddValueDataAtFront(ValueData valueData)
        {
            // 02020202030000000000
            // 00000000000202020203
            // 02020202030202020203
            int shiftSize = valueData.Size;
            if (shiftSize + _lastFilledIndex >= Size)
                throw new OverflowException("No space to insert ValueData during AddValueDataAtFront().");

            for (int i = shiftSize - 1; i >= shiftSize; i--)
            {
                _valueArray[i] = _valueArray[i - shiftSize];
            }
            _lastFilledIndex += shiftSize;

            foreach (var value in valueData.ValueArray)
            {
                AddValue(value);
            }
        }

        private static string ValueToString(byte value)
        {
            string convertedValue = "";
            if (value < 10) convertedValue += "0";
            convertedValue += value.ToString();
            return convertedValue;
        }

        public override bool Equals(object? other)
        { 
            if (other == null) return false;
            if (other is ValueData otherValueData)
                return ValueString.Equals(otherValueData.ValueString);
            return false;
        }
        
        public override int GetHashCode() => ValueString.GetHashCode();

        public static bool operator <(ValueData a, ValueData b)
        {
            if (a.Size != b.Size) return a.Size < b.Size;

            for (int i = 0; i < a.Size; i++)
            {
                if (a.ValueArray[i] != b.ValueArray[i])
                    return a.ValueArray[i] < b.ValueArray[i];
            }

            return false;
        }

        public static bool operator >(ValueData a, ValueData b)
        {
            if (a.Size != b.Size) return a.Size > b.Size;

            for (int i = 0; i < a.Size; i++)
            {
                if (a.ValueArray[i] != b.ValueArray[i])
                    return a.ValueArray[i] > b.ValueArray[i];
            }

            return false;
        }

        public static bool operator <=(ValueData a, ValueData b) => !(a > b);

        public static bool operator >=(ValueData a, ValueData b) => !(a < b);
    }
}