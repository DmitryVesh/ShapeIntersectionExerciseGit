
namespace ProgrammingExercise_Shapes
{
    public class Circle : Shape
    {
        //ShapeAnchorCoordinates is in the centre
        public float Radius;

        public Circle(int id, Vector2 centre, float radius) : base(id, centre) =>
            Radius = radius;
    }
}
