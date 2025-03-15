using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTask3
{
    public static class WinningProbabilitiesCalculator
    {
        public static double[,] Calculate(Dice[] dices)
        {
            var probabilities = new double[dices.Length, dices.Length];

            for(int i = 0; i < dices.Length; i++)
            {
                for(int j = i + 1; j < dices.Length; j++)
                {
                    probabilities[i, j] = CalculateWinProbability(dices[i], dices[j]); 
                    probabilities[j, i] = CalculateWinProbability(dices[j], dices[i]); 
                }
            }

            for(int i = 0; i < dices.Length; i++)
            {
                probabilities[i, i] = 0.5;
            }

            return probabilities;
        }

        private static double CalculateWinProbability(Dice chosen, Dice enemy)
        {
            double countPotentialWins = 0;

            for(int i = 0; i < chosen.CountFaces; i++)
            {
                for(int j = 0; j < enemy.CountFaces; j++)
                {
                    if(chosen.GetNumber(i) > enemy.GetNumber(j))
                        countPotentialWins++;
                }
            }

            return countPotentialWins / (chosen.CountFaces * enemy.CountFaces);
        }
    }
}
