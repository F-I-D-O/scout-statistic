using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKS
{
    public class Info
    {
        public double StartAge {get;  private set; }

        public double QuitAge { get; private set; }

        public Info(double startAge, double quitAge)
        {
            this.StartAge = startAge;
            this.QuitAge = quitAge;
        }


    }
}
