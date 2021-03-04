using System.Linq;
using System.Collections.Generic;
using UnityEngine;

class PullResort
{
    int _current;
    int _max;
    public List<int> Numbers { get; private set; }
    System.Random rnd = new System.Random();

    public PullResort(int count)
    {
        _max = count;
        ReSort();
    }

    void ReSort()
    {
        _current = 0;
        Numbers = Enumerable.Range(0, _max).OrderBy(i => rnd.Next()).ToList();
        //for (int i = 0; i < numbers.Count; i++) Debug.Log($"{i}: {numbers[i]}");
    }

    public int Next()
    {
        if (_current >= _max) ReSort();
        return Numbers[_current++];
    }
}