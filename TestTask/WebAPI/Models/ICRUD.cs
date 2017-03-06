using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    interface ICRUD<T>
    {
        T Create(T obj);
        T Retrieve(int ID);
        IEnumerable Retrieve();
        T Update(T obj);
        void Delete(T obj);
    }
}