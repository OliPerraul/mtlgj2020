using UnityEngine;
using System.Collections;
using System.Linq;

namespace Cirrus.Extensions
{

    public static class ICollectionExtension
    {
        public static bool IsEmpty<T>(this System.Collections.Generic.ICollection<T> collection)
        {
            return collection.Count() == 0;
        } 
    }
}
