using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pages
{
    class Settings : Pages
    {
        [SerializeField] private Transform _area;
        [SerializeField] private GameObject _togglePrefab;
        [SerializeField] private GameObject _sliderPrefab;
        [SerializeField] private TypeOne.Settings _settings;

        private void Start()
        {
            List<ToggleList> toggleList = new List<ToggleList>
            {
                new ToggleList(Cookie.Hint, "Подсказки", _settings.Hint),
                new ToggleList(Cookie.Mp3, "Звуки и вибрация", _settings.Mp3)
            };

            _area.Destroy();
            foreach (var child in toggleList)
            {
                var target = _area.Attach("toggle", _togglePrefab);
                target.GetComponentInChildren<Text>().text = child.Title;
                target.GetComponent<ToggleSet>().Init(child.Cookie, child.Var);
            }

            _area.Attach("answers", _sliderPrefab).GetComponent<SliderSet>()
                .Init(Cookie.Answers, _settings.Answers, 2, 5, true);

            _area.Attach("questions", _sliderPrefab).GetComponent<SliderSet>()
                .Init(Cookie.Questions, _settings.Questions, 5, 20, true);
        }
    }

    public class ToggleList
    {
        public Cookie Cookie;
        public string Title;
        public int Var;

        public ToggleList(Cookie cookie, string title, int var)
        {
            Cookie = cookie;
            Title = title;
            Var = var;
        }
    }
}