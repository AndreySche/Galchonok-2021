﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    public class ProgressBarView : MonoBehaviour
    {
        [SerializeField] private Transform _area;
        [SerializeField] private GameObject _border;

        private List<GameObject> _list;
        private GameObject _prewSelectSquare;

        public void Init(TypeOneSettings settings)
        {
            _area.Destroy();
            _list = new List<GameObject>();
            GameObject square = new GameObject();

            for (int i = 0; i < settings.Questions; i++)
            {
                _list.Add( _area.Attach($"square{i}", square).SetNewColor(rgb.LightGreen) );
            }
            Destroy(square);
        }

        public void SetBorder(int index)
        {
            _prewSelectSquare?.transform.Destroy();
            Destroy(_prewSelectSquare?.GetComponent<LayoutElement>());
            _prewSelectSquare = _list[index];
            _prewSelectSquare.transform.Attach( "border", _border);
            var layout = _prewSelectSquare.GetOrAddComponent<LayoutElement>();
            layout.minHeight = 90;
            layout.minWidth = 15;
        }

        public void SetColor(bool correct)
        {
            _prewSelectSquare.SetNewColor( correct ? rgb.Green : rgb.Red );
        }
    }
}