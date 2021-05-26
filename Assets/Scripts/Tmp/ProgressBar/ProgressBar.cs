using UnityEngine.UI;

namespace Galchonok.Bar
{
    internal sealed class ProgressBar : IBar
    {
        Text _field = null;
        public float Score { get; set; }
        public float OldScore { get; set; }
        public float DrawStep { get; set; }

        public ProgressBar(float score, float drawStep, Text field)
        {
            Score = OldScore = score;
            DrawStep = drawStep;
            _field = field;
            Draw();
        }

        public void AddScore(float score) => Score += score;

        public void Draw() => _field.text = OldScore.ToString();

        public void Update()
        {
            if (OldScore >= Score) return;

            OldScore += DrawStep;
            Draw();
        }
    }
}