using DG.Tweening;
using TMPro;
using UnityEngine;

public static class TMPExtensions {
    public static string WithTMPColor(this string text, Color color) {
        return $"<color={ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>";
    }

    public static Tweener DOCharColor(this TMP_Text text, int charIndex, Color from, Color to, float duration) {
        return DOVirtual.Color(from, to, duration, (Color curr) => {
            var textInfo = text.textInfo;
            if (charIndex < 0 || charIndex >= textInfo.characterCount) return;
            var charInfo = textInfo.characterInfo[charIndex];
            if (!charInfo.isVisible) return;
            var vertIndex = charInfo.vertexIndex;
            var meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];
            meshInfo.colors32[vertIndex] = curr;
            meshInfo.colors32[vertIndex + 1] = curr;
            meshInfo.colors32[vertIndex + 2] = curr;
            meshInfo.colors32[vertIndex + 3] = curr;
            text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        });
    }
}
