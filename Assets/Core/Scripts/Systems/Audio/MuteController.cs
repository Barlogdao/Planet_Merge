using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.Systems.Audio
{
    public class MuteController : MonoBehaviour
    {
        private const string MuteAdioKey = nameof(MuteAdioKey);

        [SerializeField] private Sprite _muteSprite;
        [SerializeField] private Sprite _unmuteSprite;

        [SerializeField] private Image _muteImage;
        [SerializeField] private Button _muteButton;

        private AudioService _audioService;
        private bool _isMuted;

        public void Initialize(AudioService audioService)
        {
            _audioService = audioService;

            LoadMuteValue();
            UpdateMuteStatus();

            _muteButton.onClick.AddListener(ToggleMute);
        }

        private void OnDestroy()
        {
            _muteButton.onClick.RemoveListener(ToggleMute);
        }

        private void ToggleMute()
        {
            _isMuted = !_isMuted;
            UpdateMuteStatus();
            SaveMuteValue();
        }

        private void UpdateMuteStatus()
        {
            if (_isMuted)
            {
                _audioService.MuteAudio();
                _muteImage.sprite = _muteSprite;

            }
            else
            {
                _audioService.UnmuteAudio();
                _muteImage.sprite = _unmuteSprite;
            }
        }

        private void LoadMuteValue()
        {
            int value = PlayerPrefs.GetInt(MuteAdioKey, 0);
            _isMuted = value != 0;
        }

        private void SaveMuteValue()
        {
            PlayerPrefs.SetInt(MuteAdioKey, _isMuted ? 1 : 0);
        }
    }
}