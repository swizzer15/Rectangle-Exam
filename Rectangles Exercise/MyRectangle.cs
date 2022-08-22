using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectangles_Exercise
{
    public class MyRectangle
    {
        private int RecWidth { get; set; }

        private int RecHeight { get; set; }

        private Point _Point { get; set; }

        private ConsoleColor Color { get; set; }


        public MyRectangle(int recWidth, int recHeight, Point point, ConsoleColor color)
        {
            RecWidth = recWidth;
            RecHeight = recHeight;
            _Point = point;
            Color = color;
        }

       public Validation Rectangle()
        {
            Validation validation = new Validation();
            validation.IsValid = true;
            try
            {
                // if the size is smaller then 1 than don't do anything
                if (RecWidth < 1 || RecHeight < 1)
                {
                    validation.IsValid = false;
                    validation.ErrorMessage = "width and height size should not be smaller to 1";
                }

                BorderTopLine();
                BorderMiddleLine();
                BorderBottomLine();
            }
            catch (Exception ex)
            {

            }

            return validation;
        }

        private void BorderTopLine()
        {

            DrawLine(_Point.X, _Point.Y, "┌");
            for (int x = _Point.X + 1; x < _Point.X + RecWidth; x++)
            {
                DrawLine(x, _Point.Y, "─");
            }
            DrawLine(_Point.X + RecWidth, _Point.Y, "┐");
        }

        private void BorderMiddleLine()
        {
            for (int x = _Point.Y + RecHeight; x > _Point.Y; x--)
            {
                DrawLine(_Point.X, x, "│");
                DrawLine(_Point.X + RecWidth, x, "│");
            }
        }

        private void BorderBottomLine()
        {
            DrawLine(_Point.X, _Point.Y + RecHeight + 1, "└");
            for (int x = _Point.X + 1; x < _Point.X + RecWidth; x++)
            {
                DrawLine(x, _Point.Y + RecHeight + 1, "─");
            }
            DrawLine(_Point.X + RecWidth, _Point.Y + RecHeight + 1, "┘");
        }

        private void DrawLine(int leftLocation, int topLocation, string ch)
        {
            ConsoleColor savedColor = Console.ForegroundColor;
            Console.ForegroundColor = Color;
            Console.CursorTop = topLocation;
            Console.CursorLeft = leftLocation;
            Console.Write(ch);
            Console.ResetColor();
        }

        public static Validation isGridValid(int gridWidth, int gridHeight)
        {
            Validation validation = new Validation();
            validation.IsValid = true;
            //A grid must have a width and height of no less than 5 and no greater than 25
            if (!((gridWidth >= 5 && gridWidth <= 25) && (gridHeight >= 5 && gridHeight <= 25)))
            {
                validation.IsValid = false;
                validation.ErrorMessage = "A grid must have a width and height of no less than 5 and no greater than 25";
            }

            return validation;
        }
        public static Validation isRectangleValid(List<RectangleModel> rectangleList, RectangleModel currentRectangle)
        {
            Validation validation = new Validation();
            validation.IsValid = true;

            // Check if Positions on the grid are non-negative integer coordinates starting at 0
            if (!(currentRectangle.Point.X >= 0 && currentRectangle.Point.Y >= 0))
            {
                validation.IsValid = false;
                validation.ErrorMessage = "Invalid positions set. Please enter non-negative integer coordinates starting at 0";

                return validation;
            }

            // Rectangles must not extend beyond the edge of the grid
            var gridRecOption = rectangleList.FirstOrDefault(x => x.Name.ToLower() == "grid");
            if (gridRecOption!= null)
            {
                if (!(currentRectangle.RecWidth + currentRectangle.Point.X <= gridRecOption.RecWidth && currentRectangle.RecHeight + currentRectangle.Point.Y <= gridRecOption.RecHeight))
                {
                    validation.IsValid = false;
                    validation.ErrorMessage = "Rectangles must not extend beyond the edge of the grid";
                    return validation;
                }
            }
            
            // Rectangles must not overlap
            foreach (var rectangle in rectangleList)
            {
                if (rectangle.Name.ToLower() != "grid")
                {
                    if (rectangle.Point.X < currentRectangle.RecWidth + currentRectangle.Point.X && rectangle.RecWidth + rectangle.Point.X > currentRectangle.Point.X &&
                        rectangle.Point.Y > currentRectangle.RecHeight - currentRectangle.Point.Y && rectangle.RecHeight - rectangle.Point.Y < currentRectangle.Point.Y)
                    {
                        validation.IsValid = false;
                        validation.ErrorMessage = $"{currentRectangle.Name} Overlap to {rectangle.Name}";
                        return validation;
                    }

                }
            }

            return validation;
        }

        public static RectangleModel FindRectangleInGrid(List<RectangleModel> rectanglesOption, Point point)
        {
            var selectedRectangle = rectanglesOption.FirstOrDefault(x => x.Point.X == point.X && x.Point.Y == point.Y);

            return selectedRectangle;
        }

        public static List<RectangleModel> RemoveSelectedRectangle(List<RectangleModel> rectanglesOption, Point point)
        {
            var selectedRectangle = rectanglesOption.FindIndex(x => x.Point.X == point.X || x.Point.Y == point.Y);

            if (selectedRectangle >= 1)
                rectanglesOption.RemoveAt(selectedRectangle);

            return rectanglesOption;
        }
    }
}
