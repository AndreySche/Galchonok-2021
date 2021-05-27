using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    [RequireComponent(typeof(Button))]
    class Menu : MonoBehaviour
    {
        [SerializeField] List<Button> _buttonGame = null;
        [SerializeField] List<Button> _buttonMenu = null;
        private PageSwitch _pageSwitch;

        public void Init(PageSwitch pageSwitch)
        {
            _pageSwitch = pageSwitch;
            _buttonGame[0].onClick.AddListener(() => Dispose(Pages.GameA));
            _buttonGame[1].onClick.AddListener(() => Dispose(Pages.GameB));
            _buttonGame[2].interactable = false;

            _buttonMenu[0].onClick.AddListener(() => Dispose(Pages.Logo));
            _buttonMenu[1].interactable = false;
            _buttonMenu[2].interactable = false;
            _buttonMenu[3].onClick.AddListener(() => Dispose(Pages.Warning));
        }

        private void Dispose(Pages page)
        {
            foreach (var child in _buttonGame) child.onClick.RemoveAllListeners();
            foreach (var child in _buttonMenu) child.onClick.RemoveAllListeners();
            _pageSwitch.LoadPage(page);
        }
    }
}