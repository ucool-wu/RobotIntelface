using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanucRobot
{
    public class PR
    {
        public float[] pc = new float[6];
        public float[] pj = new float[6];
    }
    enum RegCls
    {
        Rint,
        Rfloat,
        Rpos,
        Rpr,
        Rstring,
    }
}
