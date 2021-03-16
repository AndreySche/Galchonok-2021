using UnityEngine;

namespace Galchonok
{
    public class ProgressBarSegment
    {
        private GameObject _square, _border;
        private int _number;

        public ProgressBarSegment(GameObject square, GameObject border, int number)
        {
            _square = square;
            _border = border;
            _number = number;
        }

        public void SetBorder(int number)
        {
            _border.SetActive(_number == number);
        }

        public void SetColor(int number, bool correct)
        {
            if (_number == number) _square.SetNewColor(correct ? rgb.Green : rgb.Red);
        }
    }
}