using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Pages
{
    public class Pages : MonoBehaviour
    {
        [SerializeField] private GameObject _backButton;
        private Button _back;

        public void Init(UnityAction callBack, Beethoven beethoven, int gameId = 0)
        {
            _back = _backButton.IphoneMove(GetComponentInParent<Main>()._safeArea.hole).GetOrAddComponent<Button>();
            _back.onClick.AddListener(() =>
            {
                Dispose();
                callBack();
            });
        }

        private void Dispose()
        {
            _back.onClick.RemoveAllListeners();
            //_back.interactable = false;
        }
    }

    public enum Page
    {
        Logo,
        GameA,
        GameB,
        Menu,
        Warning,
        Settings
    }
}