using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInit : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private List<Sprite> _images;

    public void Init(int number)
    {
        _icon.sprite = _images[number];
    }
}
