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

        public int GetNumber(int faceIndex)
        {
            if (faceIndex < 0 || faceIndex >= CountFaces)
                throw new Exception($"The face index must be in the range of [0..{CountFaces - 1}]");

            return _numbers[faceIndex];
        }

        public override string ToString()
        {
            return $"[{String.Join(",", _numbers)}]";
        }
    }
}
