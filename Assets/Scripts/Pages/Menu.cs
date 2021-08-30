using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    //[RequireComponent(typeof(Button))]
    class Menu : MonoBehaviour
    {
        [SerializeField] List<GameObject> _buttonGame = null;
        [SerializeField] List<GameObject> _buttonMenu = null;
        private PageSwitch _pageSwitch;

        public void Init(PageSwitch pageSwitch)
        {
            _pageSwitch = pageSwitch;
            _buttonGame[0].GetOrAddComponent<Button>().onClick.AddListener(() => Dispose(Pages.GameA));
            _buttonGame[1].GetOrAddComponent<Button>().onClick.AddListener(() => Dispose(Pages.GameB));
            _buttonGame[2].GetOrAddComponent<Button>().interactable = false;

            _buttonMenu[0].GetOrAddComponent<Button>().onClick.AddListener(() => Dispose(Pages.Logo));
            _buttonMenu[1].GetOrAddComponent<Button>().interactable = false;
            _buttonMenu[2].GetOrAddComponent<Button>().onClick.AddListener(() => Dispose(Pages.Settings));
            _buttonMenu[3].GetOrAddComponent<Button>().onClick.AddListener(() => Dispose(Pages.Warning));
        }

        private void Dispose(Pages page)
        {
            foreach (var child in _buttonGame)
            {
                child.GetOrAddComponent<Button>().onClick.RemoveAllListeners();
                child.GetOrAddComponent<Button>().interactable = false;
            }

            foreach (var child in _buttonMenu)
            {
                child.GetOrAddComponent<Button>().onClick.RemoveAllListeners();
                child.GetOrAddComponent<Button>().interactable = false;
            }
            _pageSwitch.LoadPage(page);
        }
    }
}