using UnityEngine;
using DG.Tweening;

namespace Galchonok
{
    [CreateAssetMenu(fileName = "Bounce Button Settings",
        menuName = "Data/Bounce Button Settings")]
    public sealed class BounceButtonSettings : ScriptableObject
    {
        public Ease EaseDown = Ease.OutBounce;
        public Ease EaseUp = Ease.OutQuint;
        public float Duration = 0.5f;

        [Range(0.0f, 20.0f)]
        public float Strength = 10.0f;
    }
}