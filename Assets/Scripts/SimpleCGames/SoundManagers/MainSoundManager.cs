using SimpleCGames.GDebug;
using UnityEngine;

namespace SimpleCGames.Main.Audio
{
    public class MainSoundManager : MonoBehaviour
    {
        [Header("Audio Sources on scene")]
        [SerializeField] private AudioSource m_MainMusicSource;
        [SerializeField] private AudioSource m_AdditionalMusicSource;

        [SerializeField] private AudioSource m_SoundEffectSource;

        [SerializeField] private APlayableSound m_MusicPlayable;

        bool isInited;

        public void Init()
        {
            if (m_MainMusicSource == null)
            {
                isInited = false;
                GLogger.Instance.LogError($"Sound Manager not inited: m_MainMusicSource is null!");
                return;
            }
            if (m_AdditionalMusicSource == null)
            {
                isInited = false;
                GLogger.Instance.LogError($"Sound Manager not inited: m_AdditionalMusicSource is null!");
                return;
            }
            if (m_SoundEffectSource == null)
            {
                isInited = false;
                GLogger.Instance.LogError($"Sound Manager not inited: m_SoundEffectSource is null!");
                return;
            }
            isInited = true;
        }

        public void PlaySoundEffect(AudioClip audioClip, float volumePercentage)
        {
            StopSoundEffect();
            m_SoundEffectSource.clip = audioClip;
            m_SoundEffectSource.volume = volumePercentage;
            m_SoundEffectSource.Play();
        }

        public void StopSoundEffect()
        {
            m_SoundEffectSource.Stop();
        }

        public void PlayMainMusic(AudioClip audioClip, float volumePercentage, bool isRepeatable)
        {
            StopMainMusic();
            m_MainMusicSource.clip = audioClip;
            m_MainMusicSource.volume = volumePercentage;
            m_MainMusicSource.loop = isRepeatable;
            m_MainMusicSource.Play();
        }

        float _lastMainMusicVolume;
        public void PauseMainMusic()
        {
            m_MainMusicSource.Pause();
            _lastMainMusicVolume = m_MainMusicSource.volume;
            m_MainMusicSource.volume = 0;

            AudioListener.pause = true;
        }

        public void StopMainMusic()
        {
            m_MainMusicSource.Stop();
        }

        public void PlayAdditionalMusic(AudioClip audioClip, float volumePercentage, bool isRepeatable)
        {
            StopAdditionalMusic();
            m_AdditionalMusicSource.clip = audioClip;
            m_AdditionalMusicSource.volume = volumePercentage;
            m_AdditionalMusicSource.loop = isRepeatable;
            m_AdditionalMusicSource.Play();
        }

        float _lastAdditionalMusicVolume;
        public void PauseAdditionalMusic()
        {
            m_AdditionalMusicSource.Pause();
            _lastAdditionalMusicVolume = m_AdditionalMusicSource.volume;
            m_AdditionalMusicSource.volume = 0;
        }

        public void StopAdditionalMusic()
        {
            m_AdditionalMusicSource.Stop();
        }

        public void UnPauseAdditionalMusic()
        {
            m_AdditionalMusicSource.Play();
            m_AdditionalMusicSource.volume = _lastAdditionalMusicVolume;
        }

        public void UnPauseMainMusic()
        {
            m_MainMusicSource.Play();
            m_MainMusicSource.volume = _lastMainMusicVolume;
            AudioListener.pause = false;
        }
    }
}