namespace Galchonok.Bar
{
    public interface IBar
    {
        float Score { get; set; }
        float OldScore { get; set; }
        float DrawStep { get; set; }

        void Draw();
        void AddScore(float score);
    }
}
