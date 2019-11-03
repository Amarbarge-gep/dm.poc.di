using dm.poc.service.chevron.Interface;
using System;

namespace dm.poc.service.chevron
{
    public class CategoryServiceChevron : CategoryService, IChevron
    {
        public override string get()
        {
            base.get();
            return "Calling Chevron";
        }
    }
}
