using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectangles_Exercise
{
    public class RectangleModel
    {
        public string Name { get; set; }

        public int RecWidth { get; set; }

        public int RecHeight { get; set; }

        public Point Point { get; set; }

        public ConsoleColor Color { get; set; }
    }

    public class Validation
    {
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
