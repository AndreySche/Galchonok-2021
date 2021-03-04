using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    public class Curtain
    {
        Image _curtain;
        float _duration;

        public Curtain(GameObject curtain, float duration)
        {
            _duration = duration;
            _curtain = curtain.GetComponent<Image>();
        }

        public void Show(bool stat)
        {
            float progress = stat ? 1f : 0f;
            _curtain.CrossFadeAlpha(progress, _duration, false);
        }
    }
}
