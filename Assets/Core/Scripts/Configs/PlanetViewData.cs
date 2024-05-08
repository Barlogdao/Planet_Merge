using UnityEngine;

namespace PlanetMerge.Configs
{
    public struct PlanetViewData
        {
            public Sprite Sprite;
            public Color LabelColor;
            public string RankText;

            public PlanetViewData(Sprite sprite, Color labelColor, string rankText)
            {
                Sprite = sprite;
                LabelColor = labelColor;
                RankText = rankText;
            }

            public PlanetViewData(PlanetViewConfig config, string rankText)
            {
                Sprite = config.Sprite;
                LabelColor = config.LabelColor;
                RankText = rankText;
            }
        }
    
}