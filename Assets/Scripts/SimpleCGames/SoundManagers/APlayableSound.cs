using UnityEngine;

namespace SimpleCGames.Main.Audio
{
    public abstract class APlayableSound : MonoBehaviour
    {
        public abstract void SetVolume(float volume);
        public abstract float GetVolume();

        public abstract void Play(bool playRepeatly);
        public abstract void Stop();
    }
}