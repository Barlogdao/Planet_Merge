using Lean.Localization;
using UnityEngine;

namespace PlanetMerge.Systems.Localization
{
    public class LocalizationSystem : MonoBehaviour
    {
        private const string EnglishCode = "English";
        private const string RussianCode = "Russian";
        private const string TurkishCode = "Turkish";
        private const string Turkish = "tr";
        private const string Russian = "ru";
        private const string English = "en";

        [SerializeField] private LeanLocalization _leanLanguage;

        private void Awake()
        {
            ChangeLanguage();
        }

        private void ChangeLanguage()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            string languageCode = YandexGamesSdk.Environment.i18n.lang;
#else
            string languageCode = Russian;
#endif
            switch (languageCode)
            {
                case English:
                    _leanLanguage.SetCurrentLanguage(EnglishCode);
                    break;

                case Russian:
                    _leanLanguage.SetCurrentLanguage(RussianCode);
                    break;

                case Turkish:
                    _leanLanguage.SetCurrentLanguage(TurkishCode);
                    break;

                default:
                    _leanLanguage.SetCurrentLanguage(EnglishCode);
                    break;
            }
        }
    }
}