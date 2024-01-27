using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Earthquake.Data
{
    public struct EarthquakeVariant
    {
        public EarthQuakeType type { get; private set; }
        public float slowDownFactor { get; private set; }
        public float maxPushStrange { get; private set; }
        public float maxPushAdvancedStrange { get; private set; }
        public float maxSecondsPerPush { get; private set; }
        public float maxSecondsBetweenPush { get; private set; }
        public EarthquakeVariant(EarthQuakeType type, float maxPushStrange, float maxPushAdvancedStrange, float maxSecondsPerPush, float maxSecondsBetweenPush, float slowDownFactor)
        {
            this.type = type;
            this.maxPushStrange = maxPushStrange;
            this.maxPushAdvancedStrange = maxPushAdvancedStrange;
            this.maxSecondsPerPush = maxSecondsPerPush;
            this.maxSecondsBetweenPush = maxSecondsBetweenPush;
            this.slowDownFactor = slowDownFactor;
        }
    }

    public class EarthquakeDataProvider
    {
        Dictionary<EarthQuakeType, EarthquakeVariant> quakeData;
        public EarthquakeDataProvider(string rawData) {
            quakeData = new Dictionary<EarthQuakeType, EarthquakeVariant>();
            Parse(rawData);
        }

        private void Parse(string rawData) {
            //dummy
            //TODO: parse from resources or CDN
            var _nextEarthquakeVariant = new EarthquakeVariant(EarthQuakeType.light, 100, 0.001f, 1, 1, 0.2f);
            quakeData.Add(EarthQuakeType.light, _nextEarthquakeVariant);
            _nextEarthquakeVariant = new EarthquakeVariant(EarthQuakeType.medium, 200, 0.003f, 1, 1, 0.2f);
            quakeData.Add(EarthQuakeType.medium, _nextEarthquakeVariant);
            _nextEarthquakeVariant = new EarthquakeVariant(EarthQuakeType.strong, 400, 0.006f, 0.75f, 0.75f, 0.1f);
            quakeData.Add(EarthQuakeType.strong, _nextEarthquakeVariant);
            _nextEarthquakeVariant = new EarthquakeVariant(EarthQuakeType.disaster, 600, 0.01f, 0.5f, 0.5f, 0.05f);
            quakeData.Add(EarthQuakeType.disaster, _nextEarthquakeVariant);
        }

        public EarthquakeVariant GetEartquakeVariant(EarthQuakeType type)
        {
            return quakeData[type];
        }

    }
}