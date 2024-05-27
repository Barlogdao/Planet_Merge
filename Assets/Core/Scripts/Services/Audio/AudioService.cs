using UnityEngine;

namespace PlanetMerge.Sevices.Audio
{
    public class AudioService : MonoBehaviour
    {
        public const string MuteAdio = nameof(MuteAdio);

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundSource;

        private void Awake()
        {
            _musicSource.loop = true;
        }

        public void PlaySound(AudioClip clip)
        {
            _soundSource.clip = clip;

            _soundSource.pitch = Random.Range(0.9f, 1.1f);
            _soundSource.Play();
        }

        public void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
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