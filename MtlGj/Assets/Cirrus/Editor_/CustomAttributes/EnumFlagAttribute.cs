using UnityEngine;
using System;

namespace Cirrus.Editor
{

    /// <summary>
    /// Display multi-select popup for Flags enum correctly.
    /// </summary>
	[AttributeUsage(AttributeTargets.Field)]
    public class EnumFlagAttribute : PropertyAttribute
    {
    }

}