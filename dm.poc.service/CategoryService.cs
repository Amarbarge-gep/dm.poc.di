using System;
using dm.poc.iservice;

namespace dm.poc.service
{
    public class CategoryService : ICategoryService
    {
        public virtual string get()
        {
            return "Calling Default";
        }
        public virtual string getSub()
        {
            return "Calling Default Sub";
        }
    }
}
