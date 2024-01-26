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
            InitNewVer();
        }

        public void ApplyPushForce(EarthQuakeType quakeType)
        {
            StopCurrentForce();
        }

        public void ApplyPushForce(float pushValue, Vector3 vector, Vector3 groudMoveDelta, float slowdownFactor)
        {
            //Move assembly with terrain
            //TODO: move via position?
            transform.position += groudMoveDelta;
            vector = vector * pushValue;
            for (int i = 0; i < m_BuildingComponents.Length; i++)
            {
                m_BuildingComponents[i].ApplyPushForce(pushValue, vector);
                pushValue -= slowdownFactor;
            }
            //ApplyPushForceNewVer();
        }

        private void InitNewVer()
        {
            //Initialize the arrays
            posNew = new Vector3[m_BuildingComponents.Length];
            posOld = new Vector3[m_BuildingComponents.Length];
            velArray = new Vector3[m_BuildingComponents.Length];

            //Add init values to the arrays
            for (int i = 0; i < posNew.Length; i++)
            {
                posNew[i] = Vector3.zero;
                posOld[i] = m_BuildingComponents[i].GetObject().transform.position;
                velArray[i] = Vector3.zero;
            }

            k = 2f * m;
            c = k / 10f;
        }

        private void ApplyPushForceNewVer()
        {
            //Time.deltatime might give an unstable result because we are using Euler forward
            float h = 0.02f;

            //Iterate through the floors to calculate the new position and velocity
            for (int i = 0; i < m_BuildingComponents.Length; i++)
            {
                Vector3 oldPosVec = posOld[i];

                //
                //Calculate the floor's acceleration
                //
                Vector3 accVec = Vector3.zero;

                //First floor
                if (i == 0)
                {
                    accVec = (-k * (oldPosVec - transform.position) + k * (posOld[i + 1] - oldPosVec)) / m;
                }
                //Last floor
                else if (i == m_BuildingComponents.Length - 1)
                {
                    //m = 500f; //If the last floor is smaller
                    accVec = (-k * (oldPosVec - posOld[i - 1])) / m;
                }
                //Middle floors
                else
                {
                    accVec = (-k * (oldPosVec - posOld[i - 1]) + k * (posOld[i + 1] - oldPosVec)) / m;
                }

                //Add damping to the final acceleration
                accVec -= (c * velArray[i]) / m;


                //
                //Euler forward
                //
                //Add the new position
                posNew[i] = oldPosVec + h * velArray[i];
                //Add the new velocity
                velArray[i] = velArray[i] + h * accVec;
            }


            //Add the new coordinates to the floors
            for (int i = 0; i < m_BuildingComponents.Length; i++)
            {
                //Assume no spring-like behavior in y direction
                Vector3 newPos = new Vector3(
                    posNew[i].x,
                    m_BuildingComponents[i].GetObject().transform.position.y,
                    posNew[i].z);

                m_BuildingComponents[i].GetObject().transform.position = newPos;

                //Transfer the values from this update to the next
                posOld[i] = posNew[i];
            }
        }

        public void StopCurrentForce()
        {

        }
    }
}