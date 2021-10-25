using System;

namespace ProgrammingExercise_Shapes
{
    public abstract class Shape
    {
        public int ID;
        public Vector2 ShapeAnchorCoordinates;

        public Shape(int id, Vector2 anchorCoordinate) =>
            (ID, ShapeAnchorCoordinates) = (id, anchorCoordinate);

        public static bool AreShapesIntersecting(Shape shape1, Shape shape2)
        {
            if (shape1 is null || shape2 is null)
                throw new ArgumentNullException();


            if (shape1 is Circle && shape2 is Circle)
                return Shape.CircleToCircleIntersection((Circle)shape1, (Circle)shape2);

            else if (shape1 is Rectangle && shape2 is Rectangle)
                return Shape.RectangleToRectangleIntersection((Rectangle)shape1, (Rectangle)shape2);

            else if (shape1 is Rectangle && shape2 is Circle)
                return Shape.RectangleToCircleIntersection((Rectangle)shape1, (Circle)shape2);

            else if (shape1 is Circle && shape2 is Rectangle)
                return Shape.RectangleToCircleIntersection((Rectangle)shape2, (Circle)shape1);


            else
                throw new NotImplementedException("" +
                    "New type or types inheriting from class Shape have been added to." +
                    "\nAdd the interesection detection for the new type or types");
        }
        
        
        private static bool CircleToCircleIntersection(Circle circle1, Circle circle2)
        {
            //(x + a)^2 + (y + b)^2 = r^2
            //Find distance between centres
            float x1 = circle1.ShapeAnchorCoordinates.x;
            float y1 = circle1.ShapeAnchorCoordinates.y;

            float x2 = circle2.ShapeAnchorCoordinates.x;
            float y2 = circle2.ShapeAnchorCoordinates.y;

            float deltaX = x1 - x2;
            float deltaY = y1 - y2;

            //Pythagorous, to find distance from the centres of circles
            float C1C2 = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);

            //Adding Radii to see the total length if 2 circles are combined
            float R1R2 = circle1.Radius + circle2.Radius;

            //Midpoints of Cirlce1 and Circle2 are equal to the total length of Radii, so
            //Cirlce1 and Circle2 are just touching, which is an intersection at 1 point
            if (C1C2 == R1R2)
                return true;

            //Midpoints of Cirlce1 and Circle2 are greather than total length, so
            //Circle1 and Circle2 are not touching at all, which is 0 intersection points
            else if (C1C2 > R1R2)
                return false;

            //Midpoints of Cirlce1 and Circle2 are lesser than total length, so
            //Circle1 and Circle2 are intersecting well, at 2 intersection points
            //else if (C1C2 < R1R2)
            else
            {
                //Deal with a circle being completely inside the circle, so 0 intersections
                float minRadius = MathF.Min(circle1.Radius, circle2.Radius);
                float maxRadius = MathF.Max(circle1.Radius, circle2.Radius);
                if (C1C2 + minRadius < maxRadius) //The smaller circle is inside the bigger circle, and 0 intersection points
                    return false;

                return true;
            }
        }


        private static bool RectangleToCircleIntersection(Rectangle rectangle, Circle circle)
        {
            //Centre of circle
            float cirX = circle.ShapeAnchorCoordinates.x;
            float cirY = circle.ShapeAnchorCoordinates.y;

            //Find rectangles nearest X & Y point to The circle
            float recX = rectangle.ShapeAnchorCoordinates.x;
            float recY = rectangle.ShapeAnchorCoordinates.y;

            float recNearestX = MathF.Max(recX, MathF.Min(cirX, recX + rectangle.WidthX));
            float recNearestY = MathF.Max(recY, MathF.Min(cirY, recY + rectangle.HeightY));

            //Finding difference between the circle centre & rectangle's nearest point
            float deltaX = cirX - recNearestX;
            float deltaY = cirY - recNearestY;

            //Pythagorous to find the length between the centre & nearest point
            float CircleToNearestRectanglePointLength = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);

            //Midpoint of Cirlce and Rectangle nearest point are equal to the circle radius, so 
            //Circle and Rectangle are just touching, 1 point of intersection
            if (CircleToNearestRectanglePointLength == circle.Radius)
                return true;

            //Midpoint of Cirlce and Rectangle nearest point are greater than circle radius, so 
            //Circle and Rectangle are not touching, or interescting, 0 points of intersection
            else if (CircleToNearestRectanglePointLength > circle.Radius)
                return false;

            //Midpoint of Cirlce and Rectangle nearest point are lesser than circle radius, so 
            //Circle and Rectangle are interscting well, 2 points of intersection
            else
            {
                //Check if rectangle is completely inside the circle, so 0 intersection, by
                // checking if the farthest corner of the rectangle is within the circle radius
                if (RectangleInsideCircleCompletely(cirX, cirY, recX, recY, rectangle.WidthX, rectangle.HeightY, circle.Radius))
                    return false;


                //Check if the a circle is completely inside a rectangle, so 0 intersection, by
                // if circle completely inside the rectangle, the nearest point of rectangle is greater than radius, but also the nearest point of circle to rectangle calculated before is 0
                if (CircleInsideRectangleCompletely(cirX, cirY, recX, recY, rectangle.WidthX, rectangle.HeightY, circle.Radius, CircleToNearestRectanglePointLength))
                    return false;

                return true;
            }
        }
        private static bool CircleInsideRectangleCompletely(float cirX, float cirY, float recX, float recY, float rectangleWidth, float rectangleHeight, float circleRadius, float CircleToNearestRectanglePointLength)
        {
            float absDeltaX1 = MathF.Abs(cirX - recX);
            float absDeltaX2 = MathF.Abs((recX + rectangleWidth) - cirX);
            float deltaX = MathF.Min(absDeltaX1, absDeltaX2);

            float absDeltaY1 = MathF.Abs(cirY - (recY + rectangleHeight));
            float absDeltaY2 = MathF.Abs(recY - cirY);
            float deltaY = MathF.Min(absDeltaY1, absDeltaY2);

            float closestCornerRectangleToCircleCentre = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY); 
            if (circleRadius < closestCornerRectangleToCircleCentre && CircleToNearestRectanglePointLength == 0) //Must be completely inside the circle, therefore 0 intersection
                return true;

            return false;
        }
        private static bool RectangleInsideCircleCompletely(float cirX, float cirY,  float recX, float recY, float rectangleWidth, float rectangleHeight, float circleRadius)
        {
            float absDeltaX1 = MathF.Abs(cirX - recX);
            float absDeltaX2 = MathF.Abs((recX + rectangleWidth) - cirX);
            float deltaX = MathF.Max(absDeltaX1, absDeltaX2);

            float absDeltaY1 = MathF.Abs(cirY - (recY + rectangleHeight));
            float absDeltaY2 = MathF.Abs(recY - cirY);
            float deltaY = MathF.Max(absDeltaY1, absDeltaY2);

            float farthestCornerRectangleToCircleCentre = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
            if (circleRadius > farthestCornerRectangleToCircleCentre) //Must be completely inside the circle, therefore 0 intersection
                return true;

            return false;
        }


        private static bool RectangleToRectangleIntersection(Rectangle rectangle1, Rectangle rectangle2)
        {
            //                              (x3, y4)
            //                              ------------------
            //                              |   Rec2         |
            //                              |                |
            //                              ------------------
            //                                                (x4, y3)                                          
            // (x1, y2)
            // ------------------
            // |    Rec1        |
            // |                |
            // ------------------
            //                  (x2, y1)

            //top-left most point of Rec1
            float Rec1x1 = rectangle1.ShapeAnchorCoordinates.x;
            float Rec1y2 = rectangle1.ShapeAnchorCoordinates.y + rectangle1.HeightY;
            
            //bottom-right most point of Rec1
            float Rec1x2 = rectangle1.ShapeAnchorCoordinates.x + rectangle1.WidthX;
            float Rec1y1 = rectangle1.ShapeAnchorCoordinates.y;


            //top-left most point of Rec2
            float Rec2x3 = rectangle2.ShapeAnchorCoordinates.x;
            float Rec2y4 = rectangle2.ShapeAnchorCoordinates.y + rectangle2.HeightY;

            //bottom-right most point of Rec2
            float Rec2x4 = rectangle2.ShapeAnchorCoordinates.x + rectangle2.WidthX;
            float Rec2y3 = rectangle2.ShapeAnchorCoordinates.y;

            //If Rec1x1 is more than Rec2x4, then means the rectangle Rec1 is more right than Rec2, 0 intersections
            //If Rec2x3 is more than Rec1x2, then means the rectangle Rec2 is more right than Rec1, 0 intersections
            if (Rec1x1 > Rec2x4 || Rec2x3 > Rec1x2)
                return false;
            //If Rec1y1 is more than Rec2y4, then means the rectangle Rec1 is above Rec2, 0 intersections
            //If Rec2y3 is more than Rec1y2, then means the rectangle Rec2 is above Rec1, 0 intersections
            else if (Rec1y1 > Rec2y4 || Rec2y3 > Rec1y2)
                return false;
            //Leaves only either if the rectangles just touching: 1 intersection
            //      Or That they are intersecting: 2 intersections
            //      Or the 1 rectangle is completely inside the other
            else
            {
                //Need to deal with a rectangle being completely inside the other, so 0 intersections
                //                                                                                          
                // (x1, y2)                                       (x3, y4)
                // -------------------------------                -------------------------------
                // |    Rec1                     |                |    Rec2                     |
                // |                             |                |                             |
                // |                             |                |                             |
                // |  (x3, y4)                   |                |  (x1, y2)                   |
                // |   -----------               |       or       |   -----------               |
                // |   | Rec2    |               |                |   | Rec1    |               |
                // |   |         |               |                |   |         |               |
                // |   |         |               |                |   |         |               |
                // |   -----------               |                |   -----------               |
                // |              (x4, y3)       |                |              (x2, y1)       |
                // -------------------------------                -------------------------------
                //                               (x2, y1)                                       (x4, y3) 
                if  ((Rec1x1 < Rec2x3 && Rec1x2 > Rec2x4 && Rec1y2 > Rec2y4 && Rec1y1 < Rec2y3) //If Rec2 is completely inside Rec1
                    || 
                    (Rec2x3 < Rec1x1 && Rec2x4 > Rec1x2 && Rec2y4 > Rec1y2 && Rec2y3 < Rec1y1)) //If Rec1 is completely inside Rec2
                    return false;

                return true;
            }
        }
    }
}
