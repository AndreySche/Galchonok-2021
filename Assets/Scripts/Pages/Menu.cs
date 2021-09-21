using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pages
{
    //[RequireComponent(typeof(Button))]
    class Menu : Pages
    {
        [SerializeField] List<GameObject> _buttonGame = null;
        [SerializeField] private GameObject _buttonMenu;
        [SerializeField] private Transform _areaButtonDown;
        private PageSwitch _pageSwitch;
        private List<Page> _pages = new List<Page>(){Page.Logo, Page.Warning, Page.Settings};

        public void Init(PageSwitch pageSwitch, Beethoven beethoven)
        {
            _pageSwitch = pageSwitch;
            //_beethoven = beethoven;
            _buttonGame[0].GetOrAddComponent<Button>().onClick.AddListener(() => Dispose(Page.GameA));
            _buttonGame[1].GetOrAddComponent<Button>().onClick.AddListener(() => Dispose(Page.GameB));
            _buttonGame[2].GetOrAddComponent<Button>().interactable = false;
            InitButtonMenu();
        }

        private void Dispose(Page pageEnum)
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
            _pageSwitch.LoadPage(pageEnum);
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