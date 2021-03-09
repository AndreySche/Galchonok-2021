using System.Linq;
using System.Collections.Generic;

class PullResort
{
    int _current, _verify, _max;
    public List<int> Numbers { get; private set; }
    private System.Random _rnd = new System.Random();

    public PullResort(int count)
    {
        _current = 0;
        _verify = -1;
        _max = count;
        Numbers = Enumerable.Range(0, _max).OrderBy(i => _rnd.Next()).ToList();
    }

    public int Next(int drop = -1)
    {
        if (_current >= _max) _current = 0;

        int prew = _verify;
        _verify = Numbers[_current++];
        if (prew == _verify || _verify == drop) return Next(drop);

        return _verify;
    }
}