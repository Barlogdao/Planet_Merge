using UnityEngine;

namespace PlanetMerge.Sevices.Audio
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundSource;

        private void Awake()
        {
            _musicSource.loop = true;
        }

        public void PlaySound(AudioClip clip)
        {
            _soundSource.PlayOneShot(clip, Random.Range(0.8f, 1f));
        }

        public void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void MuteAudio()
        {
            _soundSource.mute = true;
            _musicSource.mute = true;
        }

        public void UnmuteAudio()
        {
            _soundSource.mute = false;
            _musicSource.mute = false;
        }

        public void PauseAudio()
        {
            AudioListener.pause = true;
        }

        public void UnpauseAudio()
        {
            AudioListener.pause = false;
        }
    }
}