using UnityEngine;
using System.Collections;
using Cirrus.Extensions;

// Allow text management via json

namespace Cirrus.Resources
{
    public class Description
    {
        public static string FormatName(string name)
        {
            string n = name;
            if (n == string.Empty || n == "[?]")
            {
                n = n.RemoveBefore(".");
                n = n.Replace("Large", " ");
                n = n.Replace("Small", " ");
                n = n.Replace("Medium", " ");
                n = n.Replace(".", " ");
            }

            return n;
        }
    }
}
