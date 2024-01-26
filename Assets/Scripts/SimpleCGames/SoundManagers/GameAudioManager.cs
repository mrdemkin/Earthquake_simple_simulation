using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleCGames.Main.Audio
{
    public class GameAudioManager : MonoBehaviour
    {
        [Header("Set volume to zero on any app lost focus event")]
        [SerializeField] private bool disableSoundOnLostFocus;

        [Header("Sound Manager")]
        [SerializeField] private MainSoundManager m_SoundManager;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip[] m_MainMusics;
        [SerializeField] private AudioClip[] m_OpenGiftBox;
        [SerializeField] private AudioClip m_WindowAppear;
        [SerializeField] private AudioClip m_WindowClose;
        [SerializeField] private AudioClip m_AddCoins;
        [SerializeField] private AudioClip m_AddEnergy;
        [SerializeField] private AudioClip m_NotEnoughEnergy;
        [SerializeField] private AudioClip m_MainMenuButtonClick;
        [SerializeField] private AudioClip m_ButtonClick;

        private static GameAudioManager _instance;

        public static GameAudioManager Instance
        {
            get
            { return _instance; }
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            _instance = this;
        }

        public void PlayMainMusic()
        {
            var rnd = new System.Random();
            var index = rnd.Next(0, this.m_MainMusics.Length);
            m_SoundManager.PlayMainMusic(this.m_MainMusics[index], 0.5f, true);
        }

        public void StopMainMusic()
        {
            m_SoundManager.StopMainMusic();
        }

        public void PlayWindowAppear()
        {
            m_SoundManager.PlaySoundEffect(this.m_WindowAppear, 1f);
        }

        public void PlayWindowClose()
        {
            m_SoundManager.PlaySoundEffect(this.m_WindowClose, 1f);
        }

        public void PlayOpenGiftBox()
        {
            var rnd = new System.Random();
            var index = rnd.Next(0, this.m_MainMusics.Length);
            m_SoundManager.PlaySoundEffect(this.m_OpenGiftBox[index], 1f);
        }

        public void PlayAddCoins()
        {
            m_SoundManager.PlaySoundEffect(this.m_AddCoins, 1f);
        }

        public void PlayAddEnergy()
        {
            m_SoundManager.PlaySoundEffect(this.m_AddEnergy, 1f);
        }

        public void PlayNotEnoughEnergy()
        {
            m_SoundManager.PlaySoundEffect(this.m_NotEnoughEnergy, 1f);
        }

        public void PlayMainMenuButtonClick()
        {
            m_SoundManager.PlaySoundEffect(this.m_MainMenuButtonClick, 1f);
        }

        public void PlayButtonClick()
        {
            m_SoundManager.PlaySoundEffect(this.m_ButtonClick, 1f);
        }

        public void PauseMainMusic()
        {
            m_SoundManager.PauseMainMusic();
        }

        public void PauseAdditionalMusic()
        {
            m_SoundManager.PauseAdditionalMusic();
        }

        public void UnpauseMainMusic()
        {
            m_SoundManager.UnPauseMainMusic();
        }

        public void UnpauseAdditionalMusic()
        {
            m_SoundManager.UnPauseAdditionalMusic();
        }

        private void OnApplicationFocus(bool focus)
        {
            Silence(!focus);
        }

        private void OnApplicationPause(bool pause)
        {
            Silence(!pause);
        }

        private void Silence(bool setSilence)
        {
            if (setSilence && disableSoundOnLostFocus)
            {
                PauseMainMusic();
                PauseAdditionalMusic();
            }
            else
            {
                UnpauseMainMusic();
                // UnpauseAdditionalMusic();
            }
        }
    }
}