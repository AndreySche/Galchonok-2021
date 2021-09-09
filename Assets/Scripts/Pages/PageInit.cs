using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Galchonok
{
    //[RequireComponent(typeof(Button))]
    public class PageInit : MonoBehaviour
    {
        [SerializeField] private GameObject _backButton;
        private Button _back;
        [HideInInspector] public int _gameId;

        public void Init(UnityAction callBack, int gameId = 0)
        {
            _gameId = gameId;
            _back = _backButton.GetOrAddComponent<Button>();
            _back.onClick.AddListener(() =>
            {
                Dispose();
                callBack();
            });
        }

        private void Dispose()
        {
            _back.onClick.RemoveAllListeners();
            _back.interactable = false;
        }
    }
}