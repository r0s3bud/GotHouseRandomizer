using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesHouseRandomizer.Models
{
    class RandomizerResult
    {
        public int PlayerID { get; set; }

        public int[] Scores { get; set; } = new int[6];
    }
}
