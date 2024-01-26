using Earthquake.Data;
using SimpleCGames.GDebug;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Earthquake.Main
{
    public class EarthquakeSimulatorController : MonoBehaviour
    {
        //TODO: use unity event to soft connections
        [SerializeField] private GameObject m_MainTerrain;

        private Vector3 originalPosition;
        private bool isSimulating, isPausedByPlayer, isPaused;
        private EarthquakeVariant currentEarthquakeVariant;

        private long simulationStep;

        /// <summary>
        /// flaot - значение толчка; Vector3 - направление толчка, Vector3 - дельта позиции террейна-базы, float - slowdown factor
        /// </summary>
        public UnityEvent<float, Vector3, Vector3, float> ApplyPush; 

        public void SetSimulationParameters(EarthquakeVariant earthquakeVariant)
        {
            //TODO: this is for model!!!
            this.currentEarthquakeVariant = earthquakeVariant;
        }

        public void StartSimulation(bool isUseAdvanced)
        {
            if (!isUseAdvanced)
            {
                StartSimpleSimulation();
            }
            else
            {
                StartAdvancedSimulation();
            }
        }

        private void StartAdvancedSimulation()
        {
            GLogger.Instance.Log($"StartAdvancedSimulation");
            originalPosition = m_MainTerrain.transform.position;
            simulationStep = 0;
            isSimulating = true;
        }

        private void StartSimpleSimulation()
        {
            GLogger.Instance.Log($"StartSimpleSimulation");
            originalPosition = m_MainTerrain.transform.position;
            simulationStep = 0;
            isSimulating = true;
        }

        private void PushTerrain()
        {
            Vector2 randomPos = Random.insideUnitCircle * currentEarthquakeVariant.maxPushStrange;

            float randomY = Random.Range(-1f, 1f) * currentEarthquakeVariant.maxPushStrange;

            //Will generate a more realistic earthquake - otherwise the ground will jitter and not shake
            float randomX = Mathf.Lerp(transform.position.x, randomPos.x, Time.fixedTime * currentEarthquakeVariant.slowDownFactor);
            float randomZ = Mathf.Lerp(transform.position.z, randomPos.y, Time.fixedTime * currentEarthquakeVariant.slowDownFactor);

            randomY = Mathf.Lerp(transform.position.y, randomY, Time.fixedTime * currentEarthquakeVariant.slowDownFactor * 0.1f);

            Vector3 moveVec = new Vector3(randomX, randomY, randomZ);

            m_MainTerrain.transform.position = originalPosition + moveVec;
        }

        private void NewPush()
        {
            GLogger.Instance.Log($"NEW PUSH!: {currentEarthquakeVariant.maxPushStrange}");
            var oldPositiom = m_MainTerrain.transform.position;
            //PushTerrain();
            var lastPosition = m_MainTerrain.transform.position;
            //#if DEBUG_BUILD
            //            ApplyPush?.Invoke(100000, Vector3.left, lastPosition - oldPositiom);
            //#else

            var vector = new Vector3((float)SimpleCGames.Additional.Random.NextDouble(-1, 1),0, (float)SimpleCGames.Additional.Random.NextDouble(-1, 1));
            ApplyPush?.Invoke(currentEarthquakeVariant.maxPushStrange, vector, lastPosition - oldPositiom, currentEarthquakeVariant.slowDownFactor);
//#endif
            isPaused = true;
            StartCoroutine(NextPushAfter(currentEarthquakeVariant.maxSecondsBetweenPush));
        }

        private IEnumerator NextPushAfter(float seconds)
        {
            GLogger.Instance.Log($"NextPushAfter: {seconds}");
            yield return new WaitForSeconds(seconds);
            isPaused = false;
        }

        private void FixedUpdate()
        {
            if (isSimulating)
            {
                if (!isPausedByPlayer)
                {
                    if (!isPaused)
                    {
                        NewPush();
                    }
                }
            }
        }
    }
}