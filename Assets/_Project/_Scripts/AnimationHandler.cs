using UnityEngine;
using DG.Tweening;

namespace VoidPixel
{
    public class AnimationHandler : MonoBehaviour
    {

        public void TweenShakeScale(GameObject tileIn, float duration, float strength)
        {
            var tileTransform = tileIn.GetComponent<RectTransform>();
            if (tileTransform != null) { tileTransform.DOShakeScale(duration, strength, 10, 90, false); }
            else { return; }
            
        }
    }
}

