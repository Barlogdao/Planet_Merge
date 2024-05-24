using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.Systems.Tutorial
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private Pointer _pointer;
        [SerializeField] private TMP_Text _firstTip;
        [SerializeField] private TMP_Text _secondTip;
        [SerializeField] Image _background;

        [SerializeField] private Canvas _tutorialCanvas;

        private PlayerInput _playerInput;
        private bool _isClicked = false;
        private Color _backgroundColor;

        private void Awake()
        {
            _firstTip.enabled = false;
            _secondTip.enabled = false;
            _backgroundColor = _background.color;

        }

        public void Initialize(PlayerInput playerInput)
        {
            _playerInput = playerInput;

            _playerInput.ClickedUp += OnClickedUp;
        }

        private void OnDestroy()
        {
            _playerInput.ClickedUp -= OnClickedUp;

        }

        private void Start()
        {
            _pointer.Disable();
            _tutorialCanvas.enabled = false;
        }



        public async UniTaskVoid ShowTitorial()
        {
            _tutorialCanvas.enabled = true;

            await RunFirstTip();

            await _background.DOFade(0f, 0.3f);
            await UniTask.WaitForSeconds(2f);
            await RunSecondTip();
            await _background.DOFade(0f, 0.3f);

            _tutorialCanvas.enabled = false;
        }


        private async UniTask RunFirstTip()
        {
            _pointer.Enable();
            _isClicked = false;
            _pointer.Move(Vector3.zero);
            _pointer.SetScaling();
            _firstTip.enabled = true;

            await UniTask.WaitUntilValueChanged(this, (controller) => controller._isClicked);
            _firstTip.enabled = false;
            _pointer.Disable();
        }


        private async UniTask RunSecondTip()
        {
            _pointer.Enable();

            _isClicked = false;
            _background.color = _backgroundColor;
            _secondTip.enabled = true;

            _pointer.Move(new Vector3(1f, -1f, 0f));
            var tween = _pointer.transform.DOMoveX(-1f, 1f).SetLoops(-1, LoopType.Yoyo);
            await UniTask.WaitUntilValueChanged(this, (controller) => controller._isClicked);
            _secondTip.enabled = false;

            tween.Kill();
            _pointer.Disable();
        }

        private void OnClickedUp()
        {
            _isClicked = true;
        }
    }
}