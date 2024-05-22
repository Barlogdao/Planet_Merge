
using UnityEngine;
using Lean.Localization;
using Agava.YandexGames;
using System;

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

        [SerializeField] private Lang _lang;

        private void Awake()
        {
            ChangeLanguage();
        }

        private void ChangeLanguage()
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang;
            //string languageCode = _lang switch
            //{
            //    Lang.English => English,
            //    Lang.Russian => Russian,
            //    Lang.Turkish => Turkish,
            //};

            switch (languageCode)
            {
                case English:
                    _leanLanguage.SetCurrentLanguage( EnglishCode);
                    break;

                case Russian:
                    _leanLanguage.SetCurrentLanguage(RussianCode);
                    break;

                case Turkish:
                    _leanLanguage.SetCurrentLanguage(TurkishCode);
                    break;
            }
        }

        public enum Lang
        {
            English,
            Russian,
            Turkish
        }
    }
}