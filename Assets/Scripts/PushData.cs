using UnityEngine;

namespace Earthquake
{
    public class PushData
    {
        public float PushStrange { get; private set; }
        public Vector3 PushVector { get; private set; }
        public Vector3 TerraindPositionDelta { get; private set; }
        public float SlowdownFactor { get; private set; }

        public PushData(float pushStrange, Vector3 pushVector, Vector3 terraindPositionDelta, float slowdownFactor)
        {
            this.PushStrange = pushStrange;
            this.PushVector = pushVector;
            this.TerraindPositionDelta = terraindPositionDelta;
            this.SlowdownFactor = slowdownFactor;
        }
    }
}