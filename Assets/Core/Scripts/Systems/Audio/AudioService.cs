using System.Collections.Generic;
using UnityEngine;

namespace PlanetMerge.Systems.Audio
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField, Range(0.8f, 1f)] private float _minPitch;
        [SerializeField, Range(1f, 1.1f)] private float _maxPitch;
        [SerializeField, Range(0.3f, 1f)] private float _minSoundVolume;
        [SerializeField, Range(0.5f, 1f)] private float _maxSoundVolume;
        [SerializeField, Min(1)] private int _soundSourceAmount;

        private Queue<AudioSource> _soundSources = new();

        private void Awake()
        {
            _musicSource.loop = true;

            for (int i = 0; i < _soundSourceAmount; i++)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();

                source.playOnAwake = false;
                _soundSources.Enqueue(source);
            }
        }

        public void PlaySound(AudioClip clip)
        {
            AudioSource source = _soundSources.Dequeue();

            source.pitch = GetRandomPitch();
            source.PlayOneShot(clip, GetRandomVolume());
            _soundSources.Enqueue(source);
        }

        public void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void MuteAudio()
        {
            foreach (AudioSource source in _soundSources)
                source.mute = true;

            _musicSource.mute = true;
        }

        public void UnmuteAudio()
        {
            foreach (AudioSource source in _soundSources)
                source.mute = false;

            _musicSource.mute = false;
        }

        public void PauseAudio()
        {
            AudioListener.pause = true;
            AudioListener.volume = 0f;
        }

        public void UnpauseAudio()
        {
            AudioListener.pause = false;
            AudioListener.volume = 1f;
        }

        private float GetRandomPitch()
        {
            return Random.Range(_minPitch, _maxPitch);
        }

        private float GetRandomVolume()
        {
            return Random.Range(_minSoundVolume, _maxSoundVolume);
        }
    }
}