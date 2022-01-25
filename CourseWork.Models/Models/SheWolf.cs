using CourseWork.Models.Abstractions;

namespace CourseWork.Models.Models
{
    public class SheWolf : WolfBase, ISheWolf
    {
        public override string ToString()
        {
            return "she-wolf";
        }
    }

}
