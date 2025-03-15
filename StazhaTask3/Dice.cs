using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTask3
{
    public class Dice(int[] numbers)
    {
        private int[] _numbers = numbers;
        public int CountFaces { get => _numbers.Length; }

        public int GetNumber(int faceNumber)
        {
            if (faceNumber < 0 || faceNumber >= CountFaces)
                throw new ArgumentOutOfRangeException($"The face number must be in the range of [0..{CountFaces - 1}]");

            return _numbers[faceNumber];
        }
    }
}
