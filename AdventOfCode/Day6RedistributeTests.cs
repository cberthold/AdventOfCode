using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AdventOfCode
{
    public class Day6RedistributeTests
    {
        [Fact]
        public void t()
        {
            var bank = new MemoryBankDistribution();

            var input = new int[] { 10, 3, 15, 10, 5, 15, 5, 15, 9, 2, 5, 8, 5, 2, 3, 6 };
            const int EXPECTED = 14029;

            var result = bank.FindDuplicateDistribution(input);

            Assert.Equal(EXPECTED, result);
        }
        [Fact]
        public void Given_0270_find_duplicate_should_produce_5()
        {
            var bank = new MemoryBankDistribution();
            var input = new int[] { 0, 2, 7, 0 };
            const int EXPECTED = 5;

            var result = bank.FindDuplicateDistribution(input);

            Assert.Equal(EXPECTED, result);
        }

        [Fact]
        public void Given_0270_redistribute_should_produce_2412()
        {
            var bank = new MemoryBankDistribution();
            var result = new int[] { 0, 2, 7, 0 };
            var expected = new int[] { 2, 4, 1, 2 };

            bank.Redistribute(result);

            Assert.True(AreSame(expected, result));
        }

        [Fact]
        public void Given_2412_redistribute_should_produce_3123()
        {
            var bank = new MemoryBankDistribution();
            var result = new int[] { 2, 4, 1, 2 };
            var expected = new int[] { 3, 1, 2, 3 };

            bank.Redistribute(result);

            Assert.True(AreSame(expected, result));
        }

        [Fact]
        public void Given_3123_redistribute_should_produce_0234()
        {
            var bank = new MemoryBankDistribution();
            var result = new int[] { 3, 1, 2, 3 };
            var expected = new int[] { 0, 2, 3, 4 };

            bank.Redistribute(result);

            Assert.True(AreSame(expected, result));
        }

        [Fact]
        public void Given_0234_redistribute_should_produce_1341()
        {
            var bank = new MemoryBankDistribution();
            var result = new int[] { 0, 2, 3, 4 };
            var expected = new int[] { 1, 3, 4, 1 };

            bank.Redistribute(result);

            Assert.True(AreSame(expected, result));
        }

        [Fact]
        public void Given_1341_redistribute_should_produce_2412()
        {
            var bank = new MemoryBankDistribution();
            var result = new int[] { 1, 3, 4, 1 };
            var expected = new int[] { 2, 4, 1, 2 };

            bank.Redistribute(result);

            Assert.True(AreSame(expected, result));
        }

        private bool AreSame(int[] left, int[] right)
        {
            if (left.Count() != right.Count())
            {
                return false;
            }

            return left.SequenceEqual(right);
        }

        public class MemoryBankDistribution
        {
            public void Redistribute(int[] input)
            {
                var idx = 0;
                for (var i = 1; i < input.Length; i++)
                {
                    if (input[i] > input[idx])
                    {
                        idx = i;
                    }
                }

                var distribute = input[idx];
                input[idx] = 0;
                var remaining = distribute % input.Length;
                var distributeEven = (distribute - remaining) / input.Length;
                var nextIndex = idx + 1;
                for (var i = 0; i < input.Length; i++)
                {
                    idx++;
                    if (idx == input.Length)
                    {
                        idx = 0;
                    }
                    if (remaining > 0)
                    {
                        remaining--;
                        input[idx] += distributeEven + 1;
                    }
                    else
                    {
                        input[idx] += distributeEven;
                    }
                }

            }

            public int FindDuplicateDistribution(int[] input)
            {
                var existingBanks = new List<int[]>();
                var count = 0;
                var newInput = input.ToArray();
                existingBanks.Add(input.ToArray());
                while (true)
                {
                    count++;
                    Redistribute(newInput);
                    
                    if (existingBanks.Any(a => IsArrayEqual(a, newInput)))
                    {
                        return count;
                    }
                    existingBanks.Add(newInput.ToArray());
                }
            }

            private bool IsArrayEqual(int[] left, int[] right)
            {
                var leftLength = left.Length;
                if (leftLength != right.Length)
                {
                    return false;
                }

                for (var i = 0; i < leftLength; i++)
                {
                    if (left[i] != right[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public class HashIntegerArray : ValueObject
        {
            public int[] Input { get; }

            public HashIntegerArray(int[] input)
            {
                Input = input;
            }

            protected override IEnumerable<object> GetAtomicValues()
            {
                foreach (var integer in Input)
                {
                    yield return integer;
                }
            }

            public HashIntegerArray Clone()
            {
                return new HashIntegerArray(Input.ToArray());
            }
        }

        public abstract class ValueObject
        {
            protected static bool EqualOperator(ValueObject left, ValueObject right)
            {
                if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
                {
                    return false;
                }
                return ReferenceEquals(left, null) || left.Equals(right);
            }

            protected static bool NotEqualOperator(ValueObject left, ValueObject right)
            {
                return !(EqualOperator(left, right));
            }

            protected abstract IEnumerable<object> GetAtomicValues();

            public override bool Equals(object obj)
            {
                if (obj == null || obj.GetType() != GetType())
                {
                    return false;
                }

                ValueObject other = (ValueObject)obj;
                IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
                IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
                while (thisValues.MoveNext() && otherValues.MoveNext())
                {
                    if (ReferenceEquals(thisValues.Current, null) ^
                        ReferenceEquals(otherValues.Current, null))
                    {
                        return false;
                    }

                    if (thisValues.Current != null &&
                        !thisValues.Current.Equals(otherValues.Current))
                    {
                        return false;
                    }
                }
                return !thisValues.MoveNext() && !otherValues.MoveNext();
            }
            // Other utilility methods
        }
    }
}
