namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents the data structure for storing values related to poker hand evaluation.
    /// </summary>
    public class ValueData
    {
        private const int COMBINATION_AMOUNT = 10;
        private const int BIGGEST_ACCEPTED_VALUE = 14;
        private const int SMALLEST_ACCEPTED_VALUE = 0;

        /// <summary>
        /// Gets the array of values stored in the ValueData.
        /// </summary>
        public byte[] ValueArray { get { return _valueArray; } }        
        /// <summary>
        /// Gets the size of the ValueData.
        /// </summary>
        public int Size { get { return _valueDataSize; } }

        private readonly int _valueDataSize;
        private readonly byte[] _valueArray; 
        private int _filledValues;

        /// <summary>
        /// Initializes a new instance of the ValueData class with the specified size.
        /// </summary>
        /// <param name="size">The size of the ValueData.</param>
        /// <param name="usedForCombination">Indicates whether the ValueData is used for combinations.</param>
        public ValueData(int size, bool usedForCombination = false)
        {
            _valueDataSize = usedForCombination ? size * COMBINATION_AMOUNT : size;
            _valueArray = new byte[_valueDataSize];
            ResetData();
        }

        /// <summary>
        /// Adds a value to the ValueData.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public void AddValue(int value)
        {
            if (value < SMALLEST_ACCEPTED_VALUE || value > BIGGEST_ACCEPTED_VALUE)
                throw new ArgumentOutOfRangeException(
                    string.Format("ValueData.AddValue() only accepts values between {0} and {1}, but received {2}.",
                        SMALLEST_ACCEPTED_VALUE, BIGGEST_ACCEPTED_VALUE, value));

            for (int i = 0; i < _valueDataSize; i++)
            {
                if (_valueArray[i] == 0)
                {
                    _valueArray[i] = Convert.ToByte(value);
                    _filledValues++;
                    return;
                }
            }

            throw new OverflowException("Value array is full, so ValueData.AddValue() failed.");
        }

        /// <summary>
        /// Adds values from another ValueData to this ValueData.
        /// </summary>
        /// <param name="valueData">The ValueData containing values to add.</param>
        public void AddValueData(ValueData valueData)
        {
            foreach (var value in valueData.ValueArray)
            {
                AddValue(value);
            }
        }

        /// <summary>
        /// Adds values from another ValueData to the front of this ValueData.
        /// </summary>
        /// <param name="valueData">The ValueData containing values to add.</param>
        public void AddValueDataAtFront(ValueData valueData)
        {
            int shiftSize = valueData.Size;
            ShiftData(shiftSize);
            AddValueData(valueData);
        }

        /// <summary>
        /// Resets the values stored in the ValueData.
        /// </summary>
        public void ResetData()
        {
            for (int i = 0; i < _valueDataSize; i++)
            {
                _valueArray[i] = 0;
            }
            _filledValues = 0;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? other)
        { 
            if (other == null) return false;
            if (other is ValueData otherValueData)
                return ToString().Equals(otherValueData.ToString());
            return false;
        }
        
        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => ToString().GetHashCode();

        /// <summary>
        /// Determines whether one specified <see cref="ValueData"/> object is less than another specified <see cref="ValueData"/> object.
        /// </summary>
        /// <param name="a">The first <see cref="ValueData"/> to compare.</param>
        /// <param name="b">The second <see cref="ValueData"/> to compare.</param>
        /// <returns>true if <paramref name="a"/> is less than <paramref name="b"/>; otherwise, false.</returns>
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

        /// <summary>
        /// Determines whether one specified <see cref="ValueData"/> object is greater than another specified <see cref="ValueData"/> object.
        /// </summary>
        /// <param name="a">The first <see cref="ValueData"/> to compare.</param>
        /// <param name="b">The second <see cref="ValueData"/> to compare.</param>
        /// <returns>true if <paramref name="a"/> is greater than <paramref name="b"/>; otherwise, false.</returns>
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

        /// <summary>
        /// Determines whether one specified <see cref="ValueData"/> object is less than or equal to another specified <see cref="ValueData"/> object.
        /// </summary>
        /// <param name="a">The first <see cref="ValueData"/> to compare.</param>
        /// <param name="b">The second <see cref="ValueData"/> to compare.</param>
        /// <returns>true if <paramref name="a"/> is less than or equal to <paramref name="b"/>; otherwise, false.</returns>
        public static bool operator <=(ValueData a, ValueData b) => !(a > b);

        /// <summary>
        /// Determines whether one specified <see cref="ValueData"/> object is greater than or equal to another specified <see cref="ValueData"/> object.
        /// </summary>
        /// <param name="a">The first <see cref="ValueData"/> to compare.</param>
        /// <param name="b">The second <see cref="ValueData"/> to compare.</param>
        /// <returns>true if <paramref name="a"/> is greater than or equal to <paramref name="b"/>; otherwise, false.</returns>
        public static bool operator >=(ValueData a, ValueData b) => !(a < b);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            string valueStr = "";
            foreach (var number in ValueArray)
            {
                valueStr += ValueToString(number);
            }
            return valueStr;
        }

        /// <summary>
        /// Returns a string representation of the <see cref="ValueData"/> object, suitable for debugging purposes.
        /// </summary>
        /// <param name="chunkSize">The number of elements in each chunk.</param>
        /// <returns>A string representation of the <see cref="ValueData"/> object.</returns>
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

        /// <summary>
        /// Gets the value bracket of the <see cref="ValueData"/> object based on the specified chunk size.
        /// </summary>
        /// <param name="chunkSize">The number of elements in each chunk.</param>
        /// <returns>The value bracket of the <see cref="ValueData"/> object.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="Size"/> of the <see cref="ValueData"/> object is not divisible by the chunk size.</exception>
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
    }
}