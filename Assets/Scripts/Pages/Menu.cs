using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    //[RequireComponent(typeof(Button))]
    class Menu : MonoBehaviour
    {
        [SerializeField] List<GameObject> _buttonGame = null;
        [SerializeField] private GameObject _buttonMenu;
        [SerializeField] private Transform _areaButtonDown;
        private PageSwitch _pageSwitch;
        private List<Pages> _pages = new List<Pages>(){Pages.Logo, Pages.Settings, Pages.Warning};

        public void Init(PageSwitch pageSwitch)
        {
            _pageSwitch = pageSwitch;
            _buttonGame[0].GetOrAddComponent<Button>().onClick.AddListener(() => Dispose(Pages.GameA));
            _buttonGame[1].GetOrAddComponent<Button>().onClick.AddListener(() => Dispose(Pages.GameB));
            _buttonGame[2].GetOrAddComponent<Button>().interactable = false;
            InitButtonMenu();
        }

        private void Dispose(Pages page)
        {
            foreach (var child in _buttonGame)
            {
                child.GetOrAddComponent<Button>().onClick.RemoveAllListeners();
                //child.GetOrAddComponent<Button>().interactable = false;
            }

            foreach (Transform child in _areaButtonDown)
            {
                child.gameObject.GetOrAddComponent<Button>().onClick.RemoveAllListeners();
                //child.gameObject.GetOrAddComponent<Button>().interactable = false;
            }
            _pageSwitch.LoadPage(page);
        }

        private void InitButtonMenu()
        {
            _areaButtonDown.Destroy();
            int i = 0;
            foreach (var page in _pages)
            {
                GameObject butt = Object.Instantiate(_buttonMenu, _areaButtonDown, false);
                butt.GetOrAddComponent<Button>().onClick.AddListener(() => Dispose(page));
                butt.GetComponent<ButtonInit>().Init(i++);
            }
        }
    }
}