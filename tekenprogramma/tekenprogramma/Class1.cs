using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekenprogramma
{
    public class Shape {
        public void Create()
        {


        }
    }

    class CreateRectangle
    {
        private Shape rectangle;

        public CreateRectangle(Shape rectangle)
        {
            this.rectangle = rectangle;
        }

        public void execute()
        {
            rectangle.Create();
        }
    }

    class CreateElipse
    {
        private Shape elipse;

        public CreateElipse(Shape elipse)
        {
            this.elipse = elipse;
        }

        public void execute()
        {
            elipse.Create();
        }
    }

    class CreateShape
    {
    }

}
