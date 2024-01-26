using Earthquake.Data;
using SimpleCGames.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Earthquake.Main
{
    //TODO: attribute for initing
    public class EarthquakeManagerModel : IRequredInit
    {
        private EarthquakeDataProvider dataProvider;

        public void Init()
        {
            dataProvider = new EarthquakeDataProvider(string.Empty);
        }

        public EarthquakeVariant GetEarthquakeParameters(EarthQuakeType type)
        {
            return dataProvider.GetEartquakeVariant(type);
        }
    }
}