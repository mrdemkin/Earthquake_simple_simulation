using SimpleCGames.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Earthquake.Main
{
    public class EarthquakeManagerView : MonoBehaviour, IRequredInit
    {
        [SerializeField] private Button m_SimulationTypeSwitch;
        [SerializeField] private Text m_SimulationTypeSwitchText;
        [SerializeField] private Button m_StartSimulate;
        [SerializeField] private Button m_StopSimulate;
        [SerializeField] private Button m_ResetSimulate;
        [SerializeField] private Button m_CameraTravelButton;
        [Header("Тип землетрясения. 0 - самый слабый")]
        [SerializeField] private Button[] m_QuakeTypeSelectButtons;

        public UnityEvent StartSimulateRequest;
        public UnityEvent StopSimulateRequest;
        public UnityEvent ResetSimulateRequest;
        public UnityEvent ChangeSimulationType;
        public UnityEvent ChangeCameraType;
        public UnityEvent<short> SelectQuakeType;

        public void Init()
        {
            InitButtons();
        }

        private const string SIMPLE_SIMULATION_TYPE = "Simple simulation";
        private const string ADVANCED_SIMULATION_TYPE = "Advanced simulation";
        private void InitButtons()
        {
            m_SimulationTypeSwitchText.text = SIMPLE_SIMULATION_TYPE;
            m_SimulationTypeSwitch.onClick.RemoveAllListeners();
            m_SimulationTypeSwitch.onClick.AddListener(() => { SwitchSimulationType(m_SimulationTypeSwitchText.text); });

            m_StartSimulate.onClick.RemoveAllListeners();
            m_StartSimulate.onClick.AddListener(() => { StartSimulateRequest?.Invoke(); });

            m_StartSimulate.interactable = false;

            m_StopSimulate.onClick.RemoveAllListeners();
            m_StopSimulate.onClick.AddListener(() => { StopSimulateRequest?.Invoke(); });

            m_ResetSimulate.onClick.RemoveAllListeners();
            m_ResetSimulate.onClick.AddListener(() => { ResetSimulateRequest?.Invoke(); });

            m_CameraTravelButton.onClick.RemoveAllListeners();
            m_CameraTravelButton.onClick.AddListener(() => { ChangeCameraType?.Invoke(); });

            m_StopSimulate.interactable = false;

            for(short i = 0; i < m_QuakeTypeSelectButtons.Length; i++)
            {
                var val = i;
                m_QuakeTypeSelectButtons[i].onClick.RemoveAllListeners();
                m_QuakeTypeSelectButtons[i].onClick.AddListener(() => { SelectQuakeType?.Invoke(val); });
            }
        }

        public void EnableStartButtons()
        {
            m_StopSimulate.interactable = false;
            m_ResetSimulate.interactable = false;
            m_StartSimulate.interactable = true;
        }

        public void BlockUi()
        {
            m_SimulationTypeSwitch.interactable = false;
            m_StartSimulate.interactable = false;
            m_StopSimulate.interactable = true;
            m_ResetSimulate.interactable = true;

            foreach (var btn in m_QuakeTypeSelectButtons)
            {
                btn.interactable = false;
            }
        }

        public void UnblockUi()
        {
            m_SimulationTypeSwitch.interactable = true;
            m_StartSimulate.interactable = true;
            m_StopSimulate.interactable = false;
            m_ResetSimulate.interactable = true;

            foreach (var btn in m_QuakeTypeSelectButtons)
            {
                btn.interactable = true;
            }
        }

        private void SwitchSimulationType(string currentText)
        {
            if (currentText.Equals(SIMPLE_SIMULATION_TYPE))
            {
                m_SimulationTypeSwitchText.text = ADVANCED_SIMULATION_TYPE;
            }
            else
            {
                m_SimulationTypeSwitchText.text = SIMPLE_SIMULATION_TYPE;
            }
            ChangeSimulationType?.Invoke();
        }

        private void OnDestroy()
        {
            m_SimulationTypeSwitch.onClick.RemoveAllListeners();
            m_StartSimulate.onClick.RemoveAllListeners();
            m_StopSimulate.onClick.RemoveAllListeners();
            foreach (var btn in m_QuakeTypeSelectButtons)
            {
                btn.onClick.RemoveAllListeners();
            }
        }
    }
}