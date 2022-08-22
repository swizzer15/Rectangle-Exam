using Rectangles_Exercise;
using System.Drawing;

namespace RectangleXUnitTests
{
    public class RectangleTests
    {
        [Fact]
        public void Create_Valid_Grid()
        {
            // Arrange
            var grid = new RectangleModel()
            {
                Name = "grid",
                RecWidth = 25,
                RecHeight = 11,
                Point = new Point(0, 0),
                Color = ConsoleColor.White
            };

            // Act
            MyRectangle gridRec = new MyRectangle(grid.RecWidth, grid.RecHeight, grid.Point, grid.Color);
            var create = gridRec.Rectangle();

            // Assert
            Assert.Equal(true, create.IsValid);
        }

        [Fact]
        public void Should_Error_If_Grid_Width_Height_Is_0()
        {
            // Arrange
            var grid = new RectangleModel()
            {
                Name = "grid",
                RecWidth = 0,
                RecHeight = 0,
                Point = new Point(0, 0),
                Color = ConsoleColor.White
            };

            // Act
            MyRectangle gridRec = new MyRectangle(grid.RecWidth, grid.RecHeight, grid.Point, grid.Color);
            var create = gridRec.Rectangle();

            // Assert
            Assert.Equal(false, create.IsValid);
        }

        [Fact]
        public void Should_Error_If_Grid_Width_Is_lessThan_5()
        {
            // Arrange
            var grid = new RectangleModel()
            {
                Name = "grid",
                RecWidth = 4,
                RecHeight = 25,
                Point = new Point(0, 0),
                Color = ConsoleColor.White
            };

            // Act
            var validate = MyRectangle.isGridValid(grid.RecWidth, grid.RecHeight);

            // Assert
            Assert.Equal(false, validate.IsValid);
            Assert.Equal("A grid must have a width and height of no less than 5 and no greater than 25", validate.ErrorMessage);
        }

        public void Should_Error_If_Grid_Heigth_Is_GreaterThan_25()
        {
            // Arrange
            var grid = new RectangleModel()
            {
                Name = "grid",
                RecWidth = 5,
                RecHeight = 30,
                Point = new Point(0, 0),
                Color = ConsoleColor.White
            };

            // Act
            var validate = MyRectangle.isGridValid(grid.RecWidth, grid.RecHeight);

            // Assert
            Assert.Equal(false, validate.IsValid);
            Assert.Equal("A grid must have a width and height of no less than 5 and no greater than 25", validate.ErrorMessage);
        }

        [Fact]
        public void Create_Valid_Rectangle()
        {
            // Arrange
            var recOptions = new RectangleModel()
            {
                Name = "rectangle1",
                RecWidth = 2,
                RecHeight = 2,
                Point = new Point(2, 2),
                Color = ConsoleColor.Red
            };

            // Act
            MyRectangle gridRec = new MyRectangle(recOptions.RecWidth, recOptions.RecHeight, recOptions.Point, recOptions.Color);
            var create = gridRec.Rectangle();

            // Assert
            Assert.Equal(true, create.IsValid);
        }

        [Fact]
        public void Should_Not_Create_If_Positions_Are_Negative()
        {
            // Arrange
            var recOptions = new RectangleModel()
            {
                Name = "rectangle1",
                RecWidth = 2,
                RecHeight = 2,
                Point = new Point(-2, -1),
                Color = ConsoleColor.Red
            };

            // Act
            var create = MyRectangle.isRectangleValid(new List<RectangleModel>() { }, recOptions);

            // Assert
            Assert.Equal(false, create.IsValid);
            Assert.Equal("Invalid positions set. Please enter non-negative integer coordinates starting at 0", create.ErrorMessage);
        }

        [Fact]
        public void Should_Create_If_Positions_Are_Not_Negative()
        {
            // Arrange
            var recOptions = new RectangleModel()
            {
                Name = "rectangle1",
                RecWidth = 2,
                RecHeight = 2,
                Point = new Point(2, 1),
                Color = ConsoleColor.Red
            };

            // Act
            var create = MyRectangle.isRectangleValid(new List<RectangleModel>() { }, recOptions);

            // Assert
            Assert.Equal(true, create.IsValid);
        }

        [Fact]
        public void Should_Not_Create_If_Rectangle_Extend_Beyond_Edge()
        {
            // Arrange
            List<RectangleModel> rectangleList = new List<RectangleModel>();
            var grid = new RectangleModel()
            {
                Name = "grid",
                RecWidth = 25,
                RecHeight = 11,
                Point = new Point(0, 0),
                Color = ConsoleColor.Red
            };
            rectangleList.Add(grid);

            var rectangle1 = new RectangleModel()
            {
                Name = "rectangle1",
                RecWidth = 4,
                RecHeight = 6,
                Point = new Point(2, 8),
                Color = ConsoleColor.Red
            };
            rectangleList.Add(rectangle1);

            // Act
            var create = MyRectangle.isRectangleValid(rectangleList, rectangle1);

            // Assert
            Assert.Equal(false, create.IsValid);
            Assert.Equal("Rectangles must not extend beyond the edge of the grid", create.ErrorMessage);
        }

        [Fact]
        public void Should_Not_Create_If_Rectangle_Overlaps_Inside_Grid()
        {
            // Arrange
            List<RectangleModel> rectangleList = new List<RectangleModel>();
            var grid = new RectangleModel()
            {
                Name = "grid",
                RecWidth = 25,
                RecHeight = 11,
                Point = new Point(0, 0),
                Color = ConsoleColor.Red
            };
            rectangleList.Add(grid);

            var rectangle1 = new RectangleModel()
            {
                Name = "rectangle1",
                RecWidth = 4,
                RecHeight = 2,
                Point = new Point(2, 4),
                Color = ConsoleColor.Red
            };
            rectangleList.Add(rectangle1);

            var currentRectangle = new RectangleModel()
            {
                Name = "rectangle2",
                RecWidth = 4,
                RecHeight = 2,
                Point = new Point(3, 4),
                Color = ConsoleColor.Blue
            };

            // Act
            var create = MyRectangle.isRectangleValid(rectangleList, currentRectangle);

            // Assert
            Assert.Equal(false, create.IsValid);
            Assert.Equal($"{currentRectangle.Name} Overlap to rectangle1", create.ErrorMessage);
        }

        [Fact]
        public void Should_Create_If_Rectangle_Not_Overlaps_Inside_Grid()
        {
            // Arrange
            List<RectangleModel> rectangleList = new List<RectangleModel>();
            var grid = new RectangleModel()
            {
                Name = "grid",
                RecWidth = 25,
                RecHeight = 11,
                Point = new Point(0, 0),
                Color = ConsoleColor.Red
            };
            rectangleList.Add(grid);

            var rectangle1 = new RectangleModel()
            {
                Name = "rectangle1",
                RecWidth = 4,
                RecHeight = 2,
                Point = new Point(2, 4),
                Color = ConsoleColor.Red
            };
            rectangleList.Add(rectangle1);

            var currentRectangle = new RectangleModel()
            {
                Name = "rectangle2",
                RecWidth = 4,
                RecHeight = 2,
                Point = new Point(6, 4),
                Color = ConsoleColor.Blue
            };

            // Act
            var create = MyRectangle.isRectangleValid(rectangleList, currentRectangle);

            // Assert
            Assert.Equal(true, create.IsValid);
        }

        [Fact]
        public void Is_Rectangle_Exist_In_Grid_By_Position()
        {
            // Arrange
            List<RectangleModel> rectangleList = new List<RectangleModel>();
            var grid = new RectangleModel()
            {
                Name = "grid",
                RecWidth = 25,
                RecHeight = 11,
                Point = new Point(0, 0),
                Color = ConsoleColor.Red
            };
            rectangleList.Add(grid);

            var rectangle1 = new RectangleModel()
            {
                Name = "rectangle1",
                RecWidth = 2,
                RecHeight = 2,
                Point = new Point(2, 2),
                Color = ConsoleColor.Red
            };
            rectangleList.Add(rectangle1);

            // Act
            var findRectangle = MyRectangle.FindRectangleInGrid(rectangleList, new Point(2, 2));

            // Assert
            Assert.Equal("rectangle1", findRectangle.Name);
        }

        [Fact]
        public void Is_Not_Rectangle_Exist_In_Grid_By_Position()
        {
            // Arrange
            List<RectangleModel> rectangleList = new List<RectangleModel>();
            var grid = new RectangleModel()
            {
                Name = "grid",
                RecWidth = 25,
                RecHeight = 11,
                Point = new Point(0, 0),
                Color = ConsoleColor.Red
            };
            rectangleList.Add(grid);

            var rectangle1 = new RectangleModel()
            {
                Name = "rectangle1",
                RecWidth = 2,
                RecHeight = 2,
                Point = new Point(2, 2),
                Color = ConsoleColor.Red
            };
            rectangleList.Add(rectangle1);



            // Act
            var findRectangle = MyRectangle.FindRectangleInGrid(rectangleList, new Point(4, 4));

            // Assert
            Assert.Equal(null, findRectangle);
        }

        [Fact]
        public void Remove_Rectangle_If_Any_Point_Found_In_Rectangle()
        {
            // Arrange
            List<RectangleModel> rectangleList = new List<RectangleModel>();
            var grid = new RectangleModel()
            {
                Name = "grid",
                RecWidth = 25,
                RecHeight = 11,
                Point = new Point(0, 0),
                Color = ConsoleColor.Red
            };
            rectangleList.Add(grid);

            var rectangle1 = new RectangleModel()
            {
                Name = "rectangle1",
                RecWidth = 2,
                RecHeight = 2,
                Point = new Point(2, 2),
                Color = ConsoleColor.Red
            };
            rectangleList.Add(rectangle1);



            // Act
            var getRemainingRectangles = MyRectangle.RemoveSelectedRectangle(rectangleList, new Point(2, 2));

            // Assert
            Assert.Equal(1, getRemainingRectangles.Count);
            Assert.Equal("grid", getRemainingRectangles[0].Name);
            Assert.NotEqual("rectangle1", getRemainingRectangles[0].Name);
        }
    }
}