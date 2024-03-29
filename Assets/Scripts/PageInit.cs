﻿using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    public class PageInit : MyMonoBehaviour
    {
        [SerializeField] GameObject _buttonBack = null;
        [HideInInspector] public Controller _controller;

        public void Init(Controller controller)
        {
            _controller = controller;
            _buttonBack.GetOrAddComponent<Button>().onClick.AddListener(() => controller.OpenPage(Pages.Menu));
        }
    }
}