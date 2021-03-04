using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Galchonok
{
    class Example : MonoBehaviour
    {
        [SerializeField] List<GameObject> _buttons = null;

        void Start()
        {
            //buttonExit.AddComponent<Button>().onClick.AddListener(() => OpenPage.Open("Page Menu"));
            _buttons[0].SetName("New Button Name 0").AddClick("Page Menu");
            _buttons[1].GetOrAddComponent<Button>().onClick.AddListener(() => OpenPage("Page Settings"));
        }

        void OpenPage(string name)
        {
            Debug.Log($"OpenPage => {name}");
        }
    }
}
