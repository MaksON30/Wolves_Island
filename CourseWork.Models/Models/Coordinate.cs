namespace CourseWork.Models.Models
{
    public class Coordinate
    {
        public int I { get; set; }
        public int J { get; set; }

        public Coordinate()
        {

        }

        public Coordinate(int i, int j)
        {
            I = i;
            J = j;
        }

        public void SetValue(int i, int j)
        {
            I = i;
            J = j;
        }
    }
}
