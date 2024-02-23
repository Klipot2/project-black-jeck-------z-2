namespace Casino.CardGames.Poker.Combinations
{
    public class ValueData
    {
        private const int COMBINATION_AMOUNT = 10;
        private const int BIGGEST_ACCEPTED_VALUE = 14;
        private const int SMALLEST_ACCEPTED_VALUE = 0;

        public byte[] ValueArray { get {return _valueArray;} }
        public int Size { get {return _valueDataSize;} }

        private readonly int _valueDataSize;
        private readonly byte[] _valueArray; 

        private int _filledValues;

        public ValueData(int size, bool usedForCombination = false)
        {
            _valueDataSize = usedForCombination ? size * COMBINATION_AMOUNT : size;
            _valueArray = new byte[_valueDataSize];
            ResetData();
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
                    _filledValues++;
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
            int shiftSize = valueData.Size;
            ShiftData(shiftSize);
            AddValueData(valueData);
        }

        public void ResetData()
        {
            for (int i = 0; i < _valueDataSize; i++)
            {
                _valueArray[i] = 0;
            }
            _filledValues = 0;
        }

        private void ShiftData(int shiftSize)
        {
            if (shiftSize + _filledValues > Size) // Check if possible
                throw new OverflowException(
                string.Format("Already filled {0} out of {1} spaces and trying to do shift of size {2}.",
                _filledValues, Size, shiftSize));

            for (int i = Size - 1; i >= shiftSize; i--) // Shift data
            {
                _valueArray[i] = _valueArray[i - shiftSize];
            }

            for (int i = 0; i < shiftSize; i++) // Clear start
            {
                _valueArray[i] = 0;
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
                return ToString().Equals(otherValueData.ToString());
            return false;
        }
        
        public override int GetHashCode() => ToString().GetHashCode();

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

        public override string ToString()
        {
            string valueStr = "";
            foreach (var number in ValueArray)
            {
                valueStr += ValueToString(number);
            }
            return valueStr;
        }

        public string DebugString(int chunkSize = 5)
        {
            string valueStr = "";
            int counter = 0;
            foreach (var number in ValueArray)
            {
                counter++;
                valueStr += ValueToString(number) + " ";
                if (counter >= chunkSize)
                {
                    counter = 0;
                    valueStr += "| ";
                }
            }
            return valueStr;
        }

        public int GetValueBracket(int chunkSize = 5)
        {
            if (Size % chunkSize != 0)
                throw new ArgumentException(
                string.Format("ValueData size ({0}) should be divisible by chunk size ({1}).",
                Size, chunkSize));

            if (Size == chunkSize) return -1;

            int valueBracket = Size / chunkSize - 1;
            int counter = 0;
            foreach (var number in ValueArray)
            {
                if (number > 0) break;

                counter++;
                if (counter >= chunkSize)
                {
                    counter = 0;
                    valueBracket -= 1;
                }
            }
            return valueBracket;
        }
    }
}