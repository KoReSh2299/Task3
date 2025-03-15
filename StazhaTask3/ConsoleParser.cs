using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTask3
{
    public static class ConsoleParser
    {
        public static Dice[] Parse(string[] args)
        {
            if (args.Length < 3)
                throw new Exception("Count of dices must be more than 2.");

            var dices = new Dice[args.Length];
            for(int i = 0; i < dices.Length; i++)
            {
                try {
                    dices[i] = new Dice(args[i].Split(',').Select(x => int.Parse(x)).ToArray()); 
                }
                catch (FormatException)
                {
                    throw new Exception("The numbers on faces must be integers.");
                }
                
            }

            if (!dices.All(s => s.CountFaces == dices[0].CountFaces))
                throw new Exception("The number of faces on the dices must be the same.");

            return dices;
        }
    }
}
