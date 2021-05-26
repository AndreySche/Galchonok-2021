using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    class Menu : MonoBehaviour
    {
        [SerializeField] List<GameObject> _buttonGame = null;
        [SerializeField] List<GameObject> _buttonMenu = null;

        public void Init(Controller controller)
        {
            _buttonGame[0].GetOrAddComponent<Button>().onClick.AddListener(() => controller.OpenPage(Pages.GameA));
            _buttonGame[1].GetOrAddComponent<Button>().onClick.AddListener(() => controller.OpenPage(Pages.GameB));

            _buttonMenu[0].GetOrAddComponent<Button>().onClick.AddListener(() => controller.OpenPage(Pages.Logo));
            _buttonMenu[1].GetOrAddComponent<Button>().interactable = false;
            _buttonMenu[2].GetOrAddComponent<Button>().interactable = false;
            _buttonMenu[3].GetOrAddComponent<Button>().interactable = false;
        }
    }
}