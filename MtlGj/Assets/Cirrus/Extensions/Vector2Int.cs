using UnityEngine;
using System.Collections;

namespace Cirrus.Extensions
{

    public static class Vector2IntExtension
    {
        public static Vector3Int ToVector3Int(this Vector2Int vec3)
        {
            return new Vector3Int(vec3.x, vec3.y, 0);
        }

    }
}
