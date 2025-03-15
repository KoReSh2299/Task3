using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace InternshipTask3
{
    public static class ConsoleTableDrawer
    {
        public static void DrawProbabilitiesTable(double[,] probabilities, string[] dices)
        {
            var table = new ConsoleTable(["User dice v", .. dices]);
            for(int i = 0; i < dices.Length; i++)
            {
                var row = Enumerable.Range(0, probabilities.GetLength(1)).Select(x => Math.Round(probabilities[i, x], 4).ToString()).ToArray();
                table.AddRow([dices[i], .. row]);
            }
            table.Write();
        }
    }
}
