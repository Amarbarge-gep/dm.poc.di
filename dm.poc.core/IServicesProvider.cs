using System;
using System.Collections.Generic;
using System.Text;

namespace dm.poc.core
{
    public interface IServicesProvider<TInterface>
    {
        TInterface GetInstance();
        TInterface GetInstance(string key);
    }
}
