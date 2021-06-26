using System.Collections.Generic;
using System.Linq;

namespace Galchonok
{
    public class RandomRange
    {
        private List<int> _numbers;
        private int _current, _max;

        public RandomRange(int count)
        {
            _max = count;
            _current = 0;
            //_numbers = AddDigitalToList(_max).RandomList();
            _numbers = Enumerable.Range(0, count).ToList().RandomList();
        }

        public int Next(int drop = -1)
        {
            if (_current >= _max) _current = 0;
            if (_numbers[_current] == drop) _current++;
            if (_current >= _max) _current = 0;

            return _numbers[_current++];
        }

        /*private List<int> AddDigitalToList(int count)
        {  
            List<int> numbers = new List<int>();
            for (int i = 0; i < count; i++) numbers.Add(i);
            return numbers;
        }*/
    }
}