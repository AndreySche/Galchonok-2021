using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    public class Controller : MonoBehaviour
    {
        readonly float _fadeDuration = 0.2f/*, _logoDuration = 2f*/;

        [SerializeField] List<GameObject> _pagesPrefabs = null;
        [SerializeField] GameObject _curtain = null;
        [SerializeField] Transform _area = null;
        [SerializeField] GameObject _audioListener = null;
        [SerializeField] AudioClip[] _sounds = null;

        PageSwitch _pageSwitch = null;
        bool _buttonsEnabled = true;
        IEnumerator _logoCoroutine;
        [HideInInspector] public List<bool> clickResult = new List<bool>();
        public Beethoven beethoven;

        void Start()
        {
            _curtain?.SetActive(true);
            _pageSwitch = new PageSwitch(_pagesPrefabs, _area, _curtain, _fadeDuration);
            beethoven = new Beethoven(_audioListener, _sounds);
            Invoke("Init", _fadeDuration);
        }

        void Init()
        {
            OpenPage(Pages.Menu);
        }

        public void OpenPage(Pages pageName)
        {
            if (!_buttonsEnabled) return;
            _buttonsEnabled = !_buttonsEnabled;

            _pageSwitch.CurtainShow();
            StartCoroutine(NewPage(_fadeDuration, pageName));
        }

        public void OpenResult(List<bool> list)
        {
            clickResult = list;
            OpenPage(Pages.Result);
        }

        IEnumerator NewPage(float wait, Pages pageName)
        {
            yield return new WaitForSeconds(wait);

            _buttonsEnabled = true;
            GameObject page = _pageSwitch.OpenPage(pageName);
            if (pageName == Pages.Menu) page.GetComponent<Menu>().Init(this);
            if (pageName == Pages.GameA) page.GetComponent<GameA>().Init(this);
            if (pageName == Pages.GameB) page.GetComponent<GameA>().Init(this);
            if (pageName == Pages.Result) page.GetComponent<Result>().Init(this);
            if (pageName == Pages.Settings) page.GetComponent<Settings>().Init(this);
            if (pageName == Pages.Logo) page.GetComponent<Logo>().Init(this);
            //else StopCoroutine(_logoCoroutine);
        }
    }
}