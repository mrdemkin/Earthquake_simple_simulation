using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Earthquake.Building
{
    public class HorizontalPanel : BuildingComponent
    {
        bool isApplyedForce;
        public override void ApplyPushForce(float value, Vector3 vector)
        {
            isApplyedForce = true;
            base.GetRigidbody().AddForce(vector, ForceMode.Impulse);
        }

        private void FixedUpdate()
        {
            if (!isApplyedForce)
            {
                return;
            }
        }
    }
}