
namespace ProgrammingExercise_Shapes
{
    public class Rectangle : Shape
    {
        // y
        // |
        // ------------------ 
        // |                |
        // ------------------ x->
        // (x, y)
        //
        //ShapeAnchorCoordinates are in the bottom left corner found in (x, y)
        public float WidthX;    // -------
        public float HeightY;   // |

        public Rectangle(int id, Vector2 rectangleBottomLeftCorner, float widthX, float heightY) : base(id, rectangleBottomLeftCorner) =>
            (WidthX, HeightY) = (widthX, heightY);
    }
}
