using System;

public class PlayerScore
{
    public PlayerId PlayerId { get; private set; }
    public int Score { get; private set; }
    public ICatchAnimation CatchAnimation { get; private set; }
    public event Action<string> OnScoreChanged;

    public PlayerScore(PlayerId id, ICatchAnimation catchAnimation)
    {
        PlayerId = id;
        Score = 0;
        CatchAnimation = catchAnimation;
    }

    public void AddScore(int value)
    {
        Score += value;
        OnScoreChanged?.Invoke(Score.ToString());
    }
    public void SubtractScore(int value)
    {
        Score -= value;
        if (Score <= 0) Score = 0;
        OnScoreChanged?.Invoke(Score.ToString());
    }
}
