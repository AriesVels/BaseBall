using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generic.Rendering
{
    public static class ColorUtil
    {
        public static Color EmissionColor(Color _color,float _intensity)
        {
            float factor = Mathf.Pow(2, _intensity);
            return new Color(_color.r * factor, _color.g * factor, _color.b * factor);
        }
    }
}
