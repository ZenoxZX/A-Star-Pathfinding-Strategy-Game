using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ZenoxZX.StrategyGame.Utils
{
    public static class UtilClass
    {
        #region World TMP

        public static TextMeshPro CreateWorldTMP(string text, 
                                                 Vector3 position,
                                                 Quaternion rotation = default,
                                                 Transform parent = null,
                                                 int width = 10, 
                                                 int height = 10, 
                                                 float fontSize = 30,
                                                 HorizontalAlignmentOptions hao = HorizontalAlignmentOptions.Center,
                                                 VerticalAlignmentOptions vao = VerticalAlignmentOptions.Middle
                                                 )
        {
            GameObject go = new("WorldTMP", typeof(TextMeshPro));
            Transform transform = go.transform;
            RectTransform rectTransform = transform as RectTransform;
            TextMeshPro tmp = go.GetComponent<TextMeshPro>();

            transform.SetPositionAndRotation(position, rotation);
            rectTransform.sizeDelta = new Vector2(width, height);
            tmp.SetText(text);
            tmp.horizontalAlignment = hao;
            tmp.verticalAlignment = vao;
            tmp.fontSize = fontSize;
            tmp.overflowMode = TextOverflowModes.Ellipsis;

            if (parent != null) transform.SetParent(parent);
            return tmp;
        }

        #endregion
    }
}
