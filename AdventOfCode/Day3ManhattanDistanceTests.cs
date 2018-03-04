using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AdventOfCode
{
    public class Day3ManhattanDistanceTests
    {
        [Fact]
        public void Distance_to_289326_should_be_0()
        {
            // assemble
            const int INPUT = 289326;
            const int EXPECTED = 419;

            var builder = new ManhattanDistanceBuilder();

            // apply
            var distance = builder.Distance(INPUT);

            // assert
            Assert.Equal(EXPECTED, distance);
        }

        [Fact]
        public void Distance_to_1_should_be_0()
        {
            // assemble
            const int INPUT = 1;
            const int EXPECTED = 0;

            var builder = new ManhattanDistanceBuilder();

            // apply
            var distance = builder.Distance(INPUT);

            // assert
            Assert.Equal(EXPECTED, distance);
        }

        [Fact]
        public void Distance_to_12_should_be_3()
        {
            // assemble
            const int INPUT = 12;
            const int EXPECTED = 3;

            var builder = new ManhattanDistanceBuilder();

            // apply
            var distance = builder.Distance(INPUT);

            // assert
            Assert.Equal(EXPECTED, distance);
        }

        [Fact]
        public void Distance_to_23_should_be_2()
        {
            // assemble
            const int INPUT = 23;
            const int EXPECTED = 2;

            var builder = new ManhattanDistanceBuilder();

            // apply
            var distance = builder.Distance(INPUT);

            // assert
            Assert.Equal(EXPECTED, distance);
        }

        [Fact]
        public void Distance_to_25_should_be_4()
        {
            // assemble
            const int INPUT = 25;
            const int EXPECTED = 4;

            var builder = new ManhattanDistanceBuilder();

            // apply
            var distance = builder.Distance(INPUT);

            // assert
            Assert.Equal(EXPECTED, distance);
        }

        [Fact]
        public void Distance_to_49_should_be_6()
        {
            // assemble
            const int INPUT = 49;
            const int EXPECTED = 6;

            var builder = new ManhattanDistanceBuilder();

            // apply
            var distance = builder.Distance(INPUT);

            // assert
            Assert.Equal(EXPECTED, distance);
        }

        [Fact]
        public void Distance_to_1024_should_be_31()
        {
            // assemble
            const int INPUT = 1024;
            const int EXPECTED = 31;

            var builder = new ManhattanDistanceBuilder();

            // apply
            var distance = builder.Distance(INPUT);

            // assert
            Assert.Equal(EXPECTED, distance);
        }

        [Fact]
        public void Array_size_for_23_should_be_5()
        {
            // assemble
            const int INPUT = 23;
            const int EXPECTED = 5;

            var builder = new ManhattanDistanceBuilder();

            // apply
            var arraySize = builder.GetSpiralDepth(INPUT);

            // assert
            Assert.Equal(EXPECTED, arraySize);
        }

        [Fact]
        public void Array_size_for_25_should_be_5()
        {
            // assemble
            const int INPUT = 25;
            const int EXPECTED = 5;

            var builder = new ManhattanDistanceBuilder();

            // apply
            var arraySize = builder.GetSpiralDepth(INPUT);

            // assert
            Assert.Equal(EXPECTED, arraySize);
        }


        [Fact]
        public void Array_size_for_7_should_be_3()
        {
            // assemble
            const int INPUT = 7;
            const int EXPECTED = 3;

            var builder = new ManhattanDistanceBuilder();

            // apply
            var arraySize = builder.GetSpiralDepth(INPUT);

            // assert
            Assert.Equal(EXPECTED, arraySize);
        }

        [Fact]
        public void Array_size_for_12_should_be_5()
        {
            // assemble
            const int INPUT = 12;
            const int EXPECTED = 5;

            var builder = new ManhattanDistanceBuilder();

            // apply
            var arraySize = builder.GetSpiralDepth(INPUT);

            // assert
            Assert.Equal(EXPECTED, arraySize);
        }

    }

    public class ManhattanDistanceBuilder
    {
        public int Distance(int input)
        {
            if (input == 1)
            {
                return 0;
            }

            // calculates the number of spiral rings
            var arraySize = GetSpiralDepth(input);

            int remainingSize = arraySize - 1;
            int currentValue = (int)Math.Pow(arraySize, 2);

            // each loop is a leg from the outside
            // so our value has to fall on the outside
            // rings making this extremely efficient
            // worst case is 4 loops
            for (var timesLeft = 3; timesLeft >= 0; timesLeft--)
            {
                // we are in the furthest corner
                if (currentValue == input)
                {
                    // this will always be the remaining size
                    return remainingSize;
                }
                // we are on the same row or column
                else if (currentValue - remainingSize <= input)
                {
                    // This will always be remaining size difference to midpoint of this leg
                    // and the current value is always a corner.
                    var distanceToMidpoint = remainingSize / 2;
                    var midpointDistanceToCenter = remainingSize - distanceToMidpoint;
                    var midpointValue = currentValue - distanceToMidpoint;
                    var absDiffToMidpointValue = Math.Abs(midpointValue - input);
                    var steps = midpointDistanceToCenter + absDiffToMidpointValue;
                    return steps;
                }

                currentValue -= remainingSize;
            }

            return 0;
            // this was initial implementation -- same effect but could do the entire
            // spiral
            /*
            int timesLeft = 3;
            while (remainingSize > 0)
            {
                // we are in the furthest corner
                if (currentValue == input)
                {
                    // this will always be the remaining size
                    return remainingSize;
                }
                // we are on the same row or column
                else if (currentValue - remainingSize <= input)
                {
                    // this will always be remaining size to midpoint of this leg

                }
                currentValue -= remainingSize;
                timesLeft--;

                if (timesLeft == 0)
                {
                    timesLeft = 2;
                    remainingSize--;
                }
            }

            return remainingSize;
            */
        }

        public int GetSpiralDepth(int input)
        {
            for (var i = 1; ; i += 2)
            {
                if (((int)Math.Pow(i, 2)) >= input)
                {
                    return i;
                }
            }
        }
    }
}
