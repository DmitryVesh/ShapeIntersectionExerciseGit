using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammingExercise_Shapes
{
    public class Board
    {
        public static Circle circle1 = new Circle(1, new Vector2(1.21f, 2.91f), 5);
        public static Circle circle2 = new Circle(2, new Vector2(3.43f, 3.1f), 1);
        public static Circle circle3 = new Circle(3, new Vector2(7.85f, -1.81f), 4);
        public static Circle circle9 = new Circle(9, new Vector2(7.99f, -2.88f), 0.25f);

        public static Rectangle rectangle4 = new Rectangle(4, new Vector2(3.61f, 0.29f), 4, 5);
        public static Rectangle rectangle5 = new Rectangle(5, new Vector2(-4.3f, -4.68f), 3.91f, 5);
        public static Rectangle rectangle6 = new Rectangle(6, new Vector2(-12.38f, 2.87f), 3.74f, 3.74f);
        public static Rectangle rectangle7 = new Rectangle(7, new Vector2(-3.04f, -3.53f), 1.32f, 1.4f);
        public static Rectangle rectangle8 = new Rectangle(8, new Vector2(6.85f, -4.14f), 2.49f, 2.7f);
        public static Rectangle rectangle10 = new Rectangle(10, new Vector2(5.71f, -2.08f), 4.34f, 4.54f);

        public static Dictionary<int, List<int>> FindIntersections(List<Shape> shapes)
        {
            Dictionary<int, List<int>> intersectionDictionary = new Dictionary<int, List<int>>(shapes.Count);

            foreach (Shape shape1 in shapes)
            {
                intersectionDictionary.Add(shape1.ID, new List<int>());

                foreach (Shape shape2 in shapes)
                {
                    if (shape1.ID == shape2.ID) //Same shape, so ignor
                        continue;

                    if (Shape.AreShapesIntersecting(shape1, shape2))
                        intersectionDictionary[shape1.ID].Add(shape2.ID);
                }
            }

            return intersectionDictionary;
        }

        public static void PrintIntersectionDictionary(Dictionary<int, List<int>> IntersectionDictionary)
        {
            foreach (KeyValuePair<int, List<int>> pair in IntersectionDictionary)
            {
                Console.Write($"{pair.Key} -> (");
                foreach (int intersectID in pair.Value)
                {
                    Console.Write($"{intersectID} ");
                }

                Console.WriteLine(" )");
            }
        }
    }
}
