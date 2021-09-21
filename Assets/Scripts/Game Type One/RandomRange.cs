using System.Collections.Generic;
using System.Linq;

namespace TypeOne
{
    public class RandomRange
    {
        private List<int> _numbers;
        private int _current, _max;

        public RandomRange(int count)
        {
            _max = count;
            _current = 0;
            _numbers = Enumerable.Range(0, count).ToList().RandomList();
        }

        public int Next(int drop = -1)
        {
            if (_current >= _max) _current = 0;
            if (_numbers[_current] == drop) _current++;
            if (_current >= _max) _current = 0;

            return _numbers[_current++];
        }
    }
}