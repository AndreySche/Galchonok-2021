using UnityEngine;

namespace Galchonok
{
    public class Expample : MonoBehaviour
    {
        [SerializeField] private GameObject _square, _border;
        [SerializeField] private Transform _area;

        private void Awake()
        {
            ProgressBar progressBar = new ProgressBar(20, _area, _square, _border);
            progressBar.SetBorder(0);
            progressBar.SetColor(0, false);
        }
    }
}
