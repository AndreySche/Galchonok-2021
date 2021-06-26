using UnityEngine;
using System.Collections.Generic;

namespace Galchonok
{
    public class ProgressBarA
    {
        Transform _area = null;
        int _current = 0, _all = 0;
        List<GameObject> _squares = null;

        public ProgressBarA(int count,Transform area)
        {
            _area = area;
            Init(count);
        }

        public void Init(int count)
        {
            GameObject _prefab = new GameObject("square");
            _current = 0;
            _all = count;
            _area.Destroy();
            _squares = new List<GameObject>();
            for (int i = 0; i < _all; i++)
            {
                _squares.Add(_area.Attach($"{i}", _prefab).SetNewColor("yellow"));
            }
            Object.Destroy(_prefab);
        }

        public void Set(bool correct)
        {
            //if (_current >= _all) Init(_all);
            if (_current >= _all) return;

            //if (!correct) _controller.beethoven.Wrong();
            _squares[_current].SetNewColor(correct ? "green" : "red");
            _current++;
        }

        public void SetResult(List<bool> list)
        {
            foreach (var child in list)
            {
                Set(child);
            }
        }
    }
}
