using System;
using System.Collections.Generic;

namespace Galchonok
{
    public class PullResort
    {
        int _current, _verify, _max;
        public List<int> Numbers { get; private set; }
        private Random _rnd = new Random();

        public PullResort(int count)
        {
            _current = 0;
            _verify = -1;
            _max = count;
            Numbers = new List<int>();
            for (int i = 0; i < _max; i++) Numbers.Add(i);
            Numbers = Numbers.RandomList();
        }

        public int Next(int drop = -1)
        {
            if (_current >= _max) _current = 0;

            int prew = _verify;
            _verify = Numbers[_current++];
            if ((prew == _verify || _verify == drop) && Numbers.Count > 1) return Next(drop);

            return _verify;
        }
    }
}