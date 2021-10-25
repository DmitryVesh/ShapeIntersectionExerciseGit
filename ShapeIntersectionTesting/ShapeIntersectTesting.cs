using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingExercise_Shapes;

using System.Text.Json;
using System.Collections.Generic;

namespace ShapeIntersectionTesting
{
    [TestClass]
    public class ShapeIntersectTesting
    {
        [TestMethod]
        public void Test_CircleCompletelyInsideAnotherCircle()
        {
            //Circle2 inside Circle1, & Circle9 inside Circle3
            //So only intersections between Circle1 and Circle3

            List<Shape> shapes = new List<Shape>() { Board.circle1, Board.circle2, Board.circle3, Board.circle9 };

            Dictionary<int, List<int>> correctResult = new Dictionary<int, List<int>>();
            correctResult.Add(1, new List<int>() { 3 });
            correctResult.Add(2, new List<int>() {  });
            correctResult.Add(3, new List<int>() { 1 });
            correctResult.Add(9, new List<int>() { });

            Dictionary<int, List<int>> calculated = Board.FindIntersections(shapes);


            Assert.AreEqual(correctResult.Count, calculated.Count);
            Assert.AreEqual(correctResult[1].Count, calculated[1].Count);
            Assert.AreEqual(correctResult[1][0], calculated[1][0]);
            Assert.AreEqual(correctResult[2].Count, calculated[2].Count);
            Assert.AreEqual(correctResult[3].Count, calculated[3].Count);
            Assert.AreEqual(correctResult[3][0], calculated[3][0]);
            Assert.AreEqual(correctResult[9].Count, calculated[9].Count);
        }

        [TestMethod]
        public void Test_CircleCompletelyInsideRectangle()
        {
            //Circle9 inside Rectangle8, so 0 intersections

            List<Shape> shapes = new List<Shape>() { Board.rectangle8, Board.circle9 };

            Dictionary<int, List<int>> correctResult = new Dictionary<int, List<int>>();
            correctResult.Add(8, new List<int>() { });
            correctResult.Add(9, new List<int>() { });

            Dictionary<int, List<int>> calculated = Board.FindIntersections(shapes);


            Assert.AreEqual(correctResult.Count, calculated.Count);
            Assert.AreEqual(correctResult[8].Count, calculated[8].Count);
            Assert.AreEqual(correctResult[9].Count, calculated[9].Count);
        }

        [TestMethod]
        public void Test_RectangleCompletelyInsideCircle()
        {
            //Rectangle8 inside Circle3, so 0 intersections
            
            List<Shape> shapes = new List<Shape>() { Board.rectangle8, Board.circle3 };

            Dictionary<int, List<int>> correctResult = new Dictionary<int, List<int>>();
            correctResult.Add(8, new List<int>() { });
            correctResult.Add(3, new List<int>() { });

            Dictionary<int, List<int>> calculated = Board.FindIntersections(shapes);


            Assert.AreEqual(correctResult.Count, calculated.Count);
            Assert.AreEqual(correctResult[8].Count, calculated[8].Count);
            Assert.AreEqual(correctResult[3].Count, calculated[3].Count);
        }

        [TestMethod]
        public void Test_RectangleCompletelyInsideRectangle()
        {
            //Rectangle7 inside Rectangle5, so 0 intersections

            List<Shape> shapes = new List<Shape>() { Board.rectangle5, Board.rectangle7 };

            Dictionary<int, List<int>> correctResult = new Dictionary<int, List<int>>();
            correctResult.Add(5, new List<int>() { });
            correctResult.Add(7, new List<int>() { });

            Dictionary<int, List<int>> calculated = Board.FindIntersections(shapes);


            Assert.AreEqual(correctResult.Count, calculated.Count);
            Assert.AreEqual(correctResult[5].Count, calculated[5].Count);
            Assert.AreEqual(correctResult[7].Count, calculated[7].Count);
        }

        [TestMethod]
        public void Test_CircleIntersectCircle()
        {
            //Circle1 intersecting Circle3
            //Circle3 intersecting Circle1

            List<Shape> shapes = new List<Shape>() { Board.circle1, Board.circle3 };

            Dictionary<int, List<int>> correctResult = new Dictionary<int, List<int>>();
            correctResult.Add(1, new List<int>() { 3 });
            correctResult.Add(3, new List<int>() { 1 });

            Dictionary<int, List<int>> calculated = Board.FindIntersections(shapes);


            Assert.AreEqual(correctResult.Count, calculated.Count);
            Assert.AreEqual(correctResult[1].Count, calculated[1].Count);
            Assert.AreEqual(correctResult[1][0], calculated[1][0]);
            Assert.AreEqual(correctResult[3].Count, calculated[3].Count);
            Assert.AreEqual(correctResult[3][0], calculated[3][0]);
        }

        [TestMethod]
        public void Test_RectangleIntersectRectangle()
        {
            //Rectangle10 intersecting Rectangle4 & 8
            //Rectangle4 intersecting Rectangle10
            //Rectangle8 intersecting Rectangle10

            List<Shape> shapes = new List<Shape>() { Board.rectangle4, Board.rectangle8, Board.rectangle10 };

            Dictionary<int, List<int>> correctResult = new Dictionary<int, List<int>>();
            correctResult.Add(4, new List<int>() { 10 });
            correctResult.Add(8, new List<int>() { 10 });
            correctResult.Add(10, new List<int>() { 4, 8 });

            Dictionary<int, List<int>> calculated = Board.FindIntersections(shapes);


            Assert.AreEqual(correctResult.Count, calculated.Count);
            Assert.AreEqual(correctResult[4].Count, calculated[4].Count);
            Assert.AreEqual(correctResult[4][0], calculated[4][0]);
            Assert.AreEqual(correctResult[8].Count, calculated[8].Count);
            Assert.AreEqual(correctResult[8][0], calculated[8][0]);
            Assert.AreEqual(correctResult[10].Count, calculated[10].Count);
            Assert.AreEqual(correctResult[10][0], calculated[10][0]);
            Assert.AreEqual(correctResult[10][1], calculated[10][1]);
        }

        [TestMethod]
        public void Test_RectangleIntersectCircle()
        {
            //Circle1 intersecting Rectangle4
            //Rectangle4 intersecting Circle1

            List<Shape> shapes = new List<Shape>() { Board.circle1, Board.rectangle4 };

            Dictionary<int, List<int>> correctResult = new Dictionary<int, List<int>>();
            correctResult.Add(1, new List<int>() { 4 });
            correctResult.Add(4, new List<int>() { 1 });

            Dictionary<int, List<int>> calculated = Board.FindIntersections(shapes);


            Assert.AreEqual(correctResult.Count, calculated.Count);
            Assert.AreEqual(correctResult[1].Count, calculated[1].Count);
            Assert.AreEqual(correctResult[1][0], calculated[1][0]);
            Assert.AreEqual(correctResult[4].Count, calculated[4].Count);
            Assert.AreEqual(correctResult[4][0], calculated[4][0]);
        }

        [TestMethod]
        public void Test_FullTest()
        {
            //Circle1 intersecting Rectangle4
            //Rectangle4 intersecting Circle1

            List<Shape> shapes = new List<Shape>() 
            { 
                Board.circle1, Board.circle2, Board.circle3,
                Board.rectangle4, Board.rectangle5,Board.rectangle6, Board.rectangle7,
                Board.rectangle8, Board.circle9, Board.rectangle10
            };

            Dictionary<int, List<int>> correctResult = new Dictionary<int, List<int>>();
            correctResult.Add(1, new List<int>() { 3, 4, 5, 10 });
            correctResult.Add(2, new List<int>() { 4 });
            correctResult.Add(3, new List<int>() { 1, 4, 10 });
            correctResult.Add(4, new List<int>() { 1, 2, 3, 10 });
            correctResult.Add(5, new List<int>() { 1 });
            correctResult.Add(6, new List<int>() {  });
            correctResult.Add(7, new List<int>() {  });
            correctResult.Add(8, new List<int>() { 10 });
            correctResult.Add(9, new List<int>() {  });
            correctResult.Add(10, new List<int>() { 1, 3, 4, 8 });

            Dictionary<int, List<int>> calculated = Board.FindIntersections(shapes);


            Assert.AreEqual(correctResult.Count, calculated.Count);
            Assert.AreEqual(correctResult[1].Count, calculated[1].Count);
            Assert.AreEqual(correctResult[1][0], calculated[1][0]);
            Assert.AreEqual(correctResult[1][1], calculated[1][1]);
            Assert.AreEqual(correctResult[1][2], calculated[1][2]);
            Assert.AreEqual(correctResult[1][3], calculated[1][3]);

            Assert.AreEqual(correctResult[2].Count, calculated[2].Count);
            Assert.AreEqual(correctResult[2][0], calculated[2][0]);

            Assert.AreEqual(correctResult[3].Count, calculated[3].Count);
            Assert.AreEqual(correctResult[3][0], calculated[3][0]);
            Assert.AreEqual(correctResult[3][1], calculated[3][1]);
            Assert.AreEqual(correctResult[3][2], calculated[3][2]);

            Assert.AreEqual(correctResult[4].Count, calculated[4].Count);
            Assert.AreEqual(correctResult[4][0], calculated[4][0]);
            Assert.AreEqual(correctResult[4][1], calculated[4][1]);
            Assert.AreEqual(correctResult[4][2], calculated[4][2]);
            Assert.AreEqual(correctResult[4][3], calculated[4][3]);

            Assert.AreEqual(correctResult[5].Count, calculated[5].Count);
            Assert.AreEqual(correctResult[5][0], calculated[5][0]);

            Assert.AreEqual(correctResult[6].Count, calculated[6].Count);
            Assert.AreEqual(correctResult[7].Count, calculated[7].Count);

            Assert.AreEqual(correctResult[8].Count, calculated[8].Count);
            Assert.AreEqual(correctResult[8][0], calculated[8][0]);

            Assert.AreEqual(correctResult[9].Count, calculated[9].Count);

            Assert.AreEqual(correctResult[10].Count, calculated[10].Count);
            Assert.AreEqual(correctResult[10][0], calculated[10][0]);
            Assert.AreEqual(correctResult[10][1], calculated[10][1]);
            Assert.AreEqual(correctResult[10][2], calculated[10][2]);
            Assert.AreEqual(correctResult[10][3], calculated[10][3]);
        }


    }
}
