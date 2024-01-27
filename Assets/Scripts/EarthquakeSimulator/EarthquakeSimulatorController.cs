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
        private bool isSimulating, isPausedByPlayer, isPaused, isUseAdvanced;
        private EarthquakeVariant currentEarthquakeVariant;

        private long simulationStep;

        /// <summary>
        /// OBSOLETE! bool - использовать вторую версию симуляции, float - значение толчка; Vector3 - направление толчка, Vector3 - дельта позиции террейна-базы, float - slowdown factor
        /// </summary>
        public UnityEvent<bool, PushData> ApplyPush;

        public void SetSimulationParameters(EarthquakeVariant earthquakeVariant)
        {
            //TODO: this is for model!!!
            this.currentEarthquakeVariant = earthquakeVariant;
        }

        public void StartSimulation(bool isUseAdvanced)
        {
            //TODO: use differ class for simulation type
            this.isUseAdvanced = isUseAdvanced;
            isPausedByPlayer = false;
            if (!isUseAdvanced)
            {
                StartSimpleSimulation();
            }
            else
            {
                StartAdvancedSimulation();
            }
        }

        public void StopSimulation()
        {
            isPausedByPlayer = true;
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

        private Vector3 PushTerrain()
        {
            float magnitude = 0.4f; //Not the same magnitude people talk about in an actual earthquakes
            float slowDownFactor = 0.01f;

            Vector2 randomPos = Random.insideUnitCircle * currentEarthquakeVariant.maxPushAdvancedStrange; // * magnitude instead

            float randomY = Random.Range(-1f, 1f) * 0.01f;// currentEarthquakeVariant.maxPushAdvancedStrange;

            //Will generate a more realistic earthquake - otherwise the ground will jitter and not shake
            float randomX = Mathf.Lerp(transform.position.x, randomPos.x, Time.fixedTime * currentEarthquakeVariant.slowDownFactor);
            float randomZ = Mathf.Lerp(transform.position.z, randomPos.y, Time.fixedTime * currentEarthquakeVariant.slowDownFactor);

            randomY = Mathf.Lerp(transform.position.y, randomY, Time.fixedTime * currentEarthquakeVariant.slowDownFactor * 0.1f);

            Vector3 moveVec = new Vector3(randomX, randomY, randomZ);

            m_MainTerrain.transform.position = originalPosition + moveVec;

            return originalPosition + moveVec;
        }

        private void NewPush()
        {
            GLogger.Instance.Log($"NEW PUSH!: {currentEarthquakeVariant.maxPushStrange}");
            var oldPositiom = m_MainTerrain.transform.position;
            var lastPosition = m_MainTerrain.transform.position;
            //#if DEBUG_BUILD
            //            ApplyPush?.Invoke(100000, Vector3.left, lastPosition - oldPositiom);
            //#else

            var vector = new Vector3((float)SimpleCGames.Additional.Random.NextDouble(-1, 1),0, (float)SimpleCGames.Additional.Random.NextDouble(-1, 1));
            ApplyPush?.Invoke(false, new PushData(currentEarthquakeVariant.maxPushStrange, vector, lastPosition - oldPositiom, currentEarthquakeVariant.slowDownFactor));
//#endif
            isPaused = true;
            StartCoroutine(NextPushAfter(currentEarthquakeVariant.maxSecondsBetweenPush));
        }

        private void NewPushAdvanced()
        {
            GLogger.Instance.Log($"NEW PUSH ADVANCED!: {currentEarthquakeVariant.maxPushStrange}");
            var oldPositiom = m_MainTerrain.transform.position;
            var moveTerrainVector = PushTerrain();
            var lastPosition = m_MainTerrain.transform.position;
            var vector = new Vector3((float)SimpleCGames.Additional.Random.NextDouble(-1, 1), 0, (float)SimpleCGames.Additional.Random.NextDouble(-1, 1));
            ApplyPush?.Invoke(true, new PushData(currentEarthquakeVariant.maxPushStrange, vector, moveTerrainVector, currentEarthquakeVariant.slowDownFactor));
            //isPaused = true;
            //StartCoroutine(NextPushAfter(currentEarthquakeVariant.maxSecondsBetweenPush));
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
                        if (!isUseAdvanced) { NewPush(); }
                        else { NewPushAdvanced(); }
                    }
                }
            }
        }
    }
}