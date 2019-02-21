using System;

namespace Lib
{
    public class ThisLib
    {
        public void ThisWillCrash()
        {
            throw new ApplicationException("Whooooups");
        }
    }
}
