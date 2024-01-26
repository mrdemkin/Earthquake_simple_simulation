using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleCGames.Additional
{
    public static class Random
    {
        /// <summary>
        /// Get randomly double value, include negative values, If crashed - return 0 (zero)
        /// </summary>
        /// <param name="min">Minimal vlaue, included</param>
        /// <param name="max">Maximum value, excluded</param>
        /// <returns></returns>
        public static double NextDouble(float min, float max)
        {
            double random_value = 0;
            try
            {
                var rnd = new System.Random();
                random_value = rnd.NextDouble() * (max - min) + min;
            }
            catch(System.Exception)
            {
                //TODO: console error
            }
            return random_value;
        }
    }
}