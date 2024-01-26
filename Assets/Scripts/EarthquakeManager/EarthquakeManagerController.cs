using Earthquake.Data;
using SimpleCGames.FSM;
using SimpleCGames.GDebug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Earthquake.Main
{
    public class EarthquakeManagerController : MonoBehaviour, IRequredInit
    {
        [SerializeField] private EarthquakeManagerView m_View;
        [SerializeField] private EarthquakeSimulatorController m_Controller;
        private EarthquakeManagerModel m_Model;
        bool useAdvancedSimulationType;
#if DEBUG_BUILD
        void Start()
        {
            Init();
        }
#endif

        public void Init()
        {
            useAdvancedSimulationType = false;

            m_Model = new EarthquakeManagerModel();
            m_Model.Init();
            m_View.Init();

            InitViewEvents();
        }

        private void InitViewEvents()
        {
            m_View.StartSimulateRequest.RemoveAllListeners();
            m_View.StartSimulateRequest.AddListener(StartSimulate);

            m_View.StopSimulateRequest.RemoveAllListeners();
            m_View.StopSimulateRequest.AddListener(StoptSimulate);

            m_View.SelectQuakeType.RemoveAllListeners();
            m_View.SelectQuakeType.AddListener(OnSelectQuakeType);

            m_View.ChangeSimulationType.RemoveAllListeners();
            m_View.ChangeSimulationType.AddListener(ChangeSimulationType);
        }

        private void StartSimulate()
        {
            //TODO: check it's quake type is set first!
            //now only one quake mode may started
            m_Controller.StartSimulation(useAdvancedSimulationType);
            m_View.BlockUi();
        }

        private void StoptSimulate()
        {
            m_View.UnblockUi();
        }

        private void ChangeSimulationType()
        {
            useAdvancedSimulationType = !useAdvancedSimulationType;
        }

        private void OnSelectQuakeType(short type)
        {
            GLogger.Instance.Log($"OnSelectQuakeType: {type}");
            //TODO: switch-case? Really?
            switch (type)
            {
                case 0:
                    m_Controller.SetSimulationParameters(m_Model.GetEarthquakeParameters(EarthQuakeType.light));
                    break;

                case 1:
                default:
                    m_Controller.SetSimulationParameters(m_Model.GetEarthquakeParameters(EarthQuakeType.medium));
                    break;

                case 2:
                    m_Controller.SetSimulationParameters(m_Model.GetEarthquakeParameters(EarthQuakeType.strong));
                    break;

                case 3:
                    m_Controller.SetSimulationParameters(m_Model.GetEarthquakeParameters(EarthQuakeType.disaster));
                    break;
            }
            m_View.EnableStartButtons();
        }

        void OnDestroy()
        {
            m_View.StartSimulateRequest.RemoveAllListeners();
            m_View.StopSimulateRequest.RemoveAllListeners();
            m_View.SelectQuakeType.RemoveAllListeners();
        }
    }
}