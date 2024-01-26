using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Earthquake.Data
{
    public class EarthQuakeType
    {
        protected EarthQuakeType () { }

        public static EarthQuakeType light { get; } = new EarthQuakeType();
        public static EarthQuakeType medium { get; } = new EarthQuakeType();
        public static EarthQuakeType strong { get; } = new EarthQuakeType();
        public static EarthQuakeType disaster { get; } = new EarthQuakeType();
    }
}