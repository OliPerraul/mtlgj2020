using UnityEngine.UI;

namespace MTLGJ
{
    //TODO remove this nonsense
    public static class Utils
    {
        public static bool InMenu = false;
    }

    public static class ColorExtension2
    {
        public static UnityEngine.Color Copy(UnityEngine.Color c)
        {
            return new UnityEngine.Color(c.r, c.g, c.b, c.a);
        }
        public static UnityEngine.Color SetR(this UnityEngine.Color im, float g)
        {
            //var c = im;
            im.r = g;
            //im = c;
            return im;
        }

        public static UnityEngine.Color SetG(this UnityEngine.Color im, float g)
        {
            //var c = im;
            im.g = g;
            //im = c;
            return im;
        }

        public static UnityEngine.Color SetB(this UnityEngine.Color im, float b)
        {
            //var c = im;
            im.b = b;
            //im = c;
            return im;
        }

        public static UnityEngine.Color SetA(this UnityEngine.Color im, float a)
        {
            im.a = a;
            return im;
        }
    }

}