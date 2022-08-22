
using Rectangles_Exercise;
using System.Drawing;

List<RectangleModel> rectanglesOption = new List<RectangleModel>();
RectangleModel recOption = new RectangleModel();
Point newGridPoint = new Point();

Console.WriteLine("Create Grid:");
Console.Write("Enter Width: ");
int gridWidth = int.Parse(Console.ReadLine());
Console.Write("Enter Heigth: ");
int gridHeight = int.Parse(Console.ReadLine());


if (MyRectangle.isGridValid(gridWidth, gridHeight).IsValid == false)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("A grid must have a width and height of no less than 5 and no greater than 25");
    Console.ResetColor();
    Environment.Exit(0);
}


recOption = new RectangleModel()
{
    Name = "grid",
    RecWidth = gridWidth,
    RecHeight = gridHeight,
    Point = new Point(0, 0),
    Color = ConsoleColor.White
};
rectanglesOption.Add(recOption);

string input;
int indx = 0;
do
{
    indx = indx + 1;
    Console.WriteLine();
    Console.WriteLine("Create Rectangle inside the grid:");
    Console.Write("Enter Width: ");
    int recWidth = int.Parse(Console.ReadLine());
    Console.Write("Enter Heigth: ");
    int recHeight = int.Parse(Console.ReadLine());
    Console.Write("Enter Point X: ");
    int recPointX = int.Parse(Console.ReadLine());
    Console.Write("Enter Point Y: ");
    int recPointY = int.Parse(Console.ReadLine());
    Console.Write("Enter Color: ");
    string colorName = Console.ReadLine();

    recOption = new RectangleModel()
    {
        Name = $"rectangle{indx}",
        RecWidth = recWidth,
        RecHeight = recHeight,
        Point = new Point(recPointX, recPointY),
        Color = string.IsNullOrEmpty(colorName) ? ConsoleColor.Gray : (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorName, true)
    };

    // check if rectangle is valid
    var validation = MyRectangle.isRectangleValid(rectanglesOption, recOption);
    if (validation.IsValid == false)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(validation.ErrorMessage);
        Console.ResetColor();
    }
    else
        rectanglesOption.Add(recOption);

    Console.WriteLine();
    Console.Write("Do you want to create Reactangle again? (yes/no): ");
    input = Console.ReadLine();
}
while (input.ToLower().Equals("yes", StringComparison.OrdinalIgnoreCase));

if (input.ToLower() != "yes")
{
    Console.WriteLine();
    Console.WriteLine("Result: ");
    newGridPoint.X = Console.GetCursorPosition().Left;
    newGridPoint.Y = Console.GetCursorPosition().Top;

    foreach (var item in rectanglesOption)
    {
        item.Point = new Point(item.Point.X + newGridPoint.X, item.Point.Y + newGridPoint.Y);
        MyRectangle myRectangle = new MyRectangle(item.RecWidth, item.RecHeight, item.Point, item.Color);
        myRectangle.Rectangle();
    }
}

Console.SetCursorPosition(0, Console.WindowTop + Console.WindowHeight - 1);
while (true)
{
    Console.WriteLine("Select an option:");
    Console.WriteLine("[1] Find a rectangle based on a given position.");
    Console.WriteLine("[2] Remove a rectangle from the grid by specifying any point within the rectangle");
    Console.WriteLine("[3] Exit");

    Console.Write("Enter : ");
    string inputOption = Console.ReadLine();
    if (inputOption == "1")
    {
        Console.Write("Enter position X: ");
        int positionX = int.Parse(Console.ReadLine());
        Console.Write("Enter position Y: ");
        int positionY = int.Parse(Console.ReadLine());

        var selectedRectangle = MyRectangle.FindRectangleInGrid(rectanglesOption, new Point(positionX + newGridPoint.X, positionY + newGridPoint.Y));
        if (selectedRectangle != null)
        {
            Console.WriteLine("Result: ");
            MyRectangle myRectangle = new MyRectangle(selectedRectangle.RecWidth, selectedRectangle.RecHeight, new Point(0, Console.GetCursorPosition().Top), selectedRectangle.Color);
            myRectangle.Rectangle();
            Console.SetCursorPosition(0, Console.WindowTop + Console.WindowHeight + 1);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Result: Unable to find rectangle based on a given position.");
            Console.ResetColor();
        }
    }
    else if (inputOption == "2")
    {
        Console.Write("Enter any point within the rectangle: ");
        int point = int.Parse(Console.ReadLine());
        Console.WriteLine("Result: ");

        //var getRemainingRectangle = rectanglesOption.FindIndex(x => x.Point.X == point + newGridPoint.X || x.Point.Y == point + newGridPoint.Y);
        var getRemainingRectangle = MyRectangle.RemoveSelectedRectangle(rectanglesOption, new Point(point + newGridPoint.X, point + newGridPoint.Y));
        if (getRemainingRectangle != null)
        {
            newGridPoint.X = Console.GetCursorPosition().Left;
            newGridPoint.Y = Console.GetCursorPosition().Top;
            foreach (var item in getRemainingRectangle)
            {
                item.Point = newGridPoint;
                MyRectangle myRectangle = new MyRectangle(item.RecWidth, item.RecHeight, item.Point, item.Color);
                myRectangle.Rectangle();
            }
            Console.SetCursorPosition(0, Console.WindowTop + Console.WindowHeight + 1);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Result: Unable to find rectangle based on the provided point.");
            Console.ResetColor();
        }
    }
    else if (inputOption == "3")
    {
        Console.WriteLine("End");
        Environment.Exit(0);
    }
}


Console.ReadKey();