using Earthquake.Building;
using Earthquake.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Earthquake.Grounded
{
    public class BuildingAssembly : MonoBehaviour, IGroundStackable
    {
        [SerializeField] private GameObject m_BuildingContainer;
        [Header("Соблюдать порядок! 0 - низ")]
        [SerializeField] private BuildingComponent[] m_BuildingComponents;

        //Needed in the Euler forward
        private Vector3[] posNew;
        private Vector3[] posOld;
        private Vector3[] velArray;

        private float m = 5000;
        private float k;
        private float c;

        void OnEnable()
        {
            //TODO: complete new version first
            //InitNewVer();
        }

        public void ApplyRegularPushForce(float pushValue, Vector3 vector, Vector3 groudMoveDelta, float slowdownFactor)
        {
            // transform.position += groudMoveDelta;
            vector = vector * pushValue;
            for (int i = 0; i < m_BuildingComponents.Length; i++)
            {
                m_BuildingComponents[i].ApplyPushForce(pushValue, vector);
                pushValue -= slowdownFactor;
            }
        }

        public void ApplyRegularPushAdvancedForce(float pushValue, Vector3 vector, Vector3 groudMoveDelta, float slowdownFactor)
        {
            // transform.position += groudMoveDelta;
            vector = vector * pushValue * 0.05f;
            for (int i = 0; i < m_BuildingComponents.Length; i++)
            {
                m_BuildingComponents[i].ApplyPushForce(pushValue, vector);
                pushValue -= slowdownFactor;
            }
        }

        public void ApplyPushForce(bool useAdvanced, PushData pushData)
        {
            if (!useAdvanced)
            {
                ApplyRegularPushForce(pushData.PushStrange, pushData.PushVector, pushData.TerraindPositionDelta, pushData.SlowdownFactor);
            }
            else
            {
                ApplyRegularPushAdvancedForce(pushData.PushStrange, pushData.PushVector, pushData.TerraindPositionDelta, pushData.SlowdownFactor);
                //TODO: require warkaround
                //ApplyPushForceNewVer(pushData.PushStrange, pushData.PushVector, pushData.TerraindPositionDelta, pushData.SlowdownFactor);
            }
        }

        private void InitNewVer()
        {
            posNew = new Vector3[m_BuildingComponents.Length];
            posOld = new Vector3[m_BuildingComponents.Length];
            velArray = new Vector3[m_BuildingComponents.Length];

            for (int i = 0; i < posNew.Length; i++)
            {
                posNew[i] = Vector3.zero;
                posOld[i] = m_BuildingComponents[i].GetObject().transform.position;
                velArray[i] = Vector3.zero;
            }

            k = 2f * m;
            c = k / 10f;
        }

        private void ApplyPushForceNewVer(float pushValue, Vector3 vector, Vector3 groudMoveDelta, float slowdownFactor)
        {
            m_BuildingContainer.transform.position = groudMoveDelta;
            float h = 0.02f;

            for (int i = 0; i < m_BuildingComponents.Length; i++)
            {
                Vector3 oldPosVec = posOld[i];

                Vector3 accVec = Vector3.zero;

                if (i == 0)
                {
                    accVec = (-k * (oldPosVec - m_BuildingContainer.transform.position) + k * (posOld[i + 1] - oldPosVec)) / m;
                }
                else if (i == m_BuildingComponents.Length - 1)
                {
                    accVec = (-k * (oldPosVec - posOld[i - 1])) / m;
                }
                else
                {
                    accVec = (-k * (oldPosVec - posOld[i - 1]) + k * (posOld[i + 1] - oldPosVec)) / m;
                }

                accVec -= (c * velArray[i]) / m;


                posNew[i] = oldPosVec + h * velArray[i];
                velArray[i] = velArray[i] + h * accVec;
            }


            for (int i = 0; i < m_BuildingComponents.Length; i++)
            {
                Vector3 newPos = new Vector3(
                    posNew[i].x,
                    m_BuildingComponents[i].GetObject().transform.position.y,
                    posNew[i].z);

                m_BuildingComponents[i].GetObject().transform.position = newPos;

                posOld[i] = posNew[i];
            }
        }

        public void StopCurrentForce()
        {

        }
    }
}