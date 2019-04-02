using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekenprogramma
{
    public class Receiver
    {
        private string action;

        public Receiver(string action)
        {
            this.action = action;
        }

        public void Actions(string action)
        {
            this.action = action;
            if (action == "create rectangle")
            {
                //MakeRectangle.Execute();
            }
            else if(action =="create elipse"){
                //MakeEllipse.Execute();
            }            
        }
    }
}
