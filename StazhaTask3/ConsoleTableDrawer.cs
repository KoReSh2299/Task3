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
        public static void DrawProbabilitiesTable(double[,] probabilities, Dice[] dices)
        {
            var strDices = dices.Select(x => x.ToString()).ToArray();
            var table = new ConsoleTable(["User dice v", .. strDices]);
            for(int i = 0; i < strDices.Length; i++)
            {
                var row = Enumerable.Range(0, probabilities.GetLength(1)).Select(x => Math.Round(probabilities[i, x], 4).ToString()).ToArray();
                table.AddRow([strDices[i], .. row]);
            }
            table.Write( Format.Alternative);
        }
    }
}
