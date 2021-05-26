using UnityEngine;
using UnityEngine.UI;

namespace Galchonok.Bar
{
    class Example : MonoBehaviour
    {
        readonly float _drawStep = 0.1f;
        [SerializeField] Text _field = null; // в будущем будет линия
        ProgressBar _progressBar = null;

        private void Start()
        {
            _progressBar = new ProgressBar(1, _drawStep, _field);
            _progressBar.AddScore(5);

            InvokeRepeating("UpdateSlow", 0.0f, _drawStep);
        }

        void UpdateSlow()
        {
            _progressBar.Update();
        }
    }
}