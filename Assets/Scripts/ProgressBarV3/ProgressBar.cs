using UnityEngine;
using System.Collections.Generic;

namespace Galchonok
{
    public class ProgressBar
    {
        private List<ProgressBarSegment> _list = new List<ProgressBarSegment>();

        public ProgressBar(int count, Transform area, GameObject square, GameObject border)
        {
            area.Destroy();
            for (int i = 0; i < count; i++)
            {
                var squa = area.Attach($"square{i}", square).SetNewColor(rgb.Yellow);
                var bord = squa.transform.Attach("border", border);
                _list.Add(new ProgressBarSegment(squa, bord, i));
            }
        }

        public void SetBorder(int number)
        {
            foreach (var child in _list) child.SetBorder(number);
        }

        public void SetColor(int number, bool correct)
        {
            foreach (var child in _list) child.SetColor(number, correct);
        }
    }
}
