using System;
using System.Collections.Generic;
using System.Linq;

public class ScoreManager : Singleton<ScoreManager>, IScore
{
    public FallingObjectSpawner fallingObjectSpawner;
    private Dictionary<PlayerId, PlayerScore> scores = new();
    protected override void Awake()
    {
        base.Awake();
    }
    public void AddScore(PlayerId id, int value)
    {
        if (scores.TryGetValue(id, out var score))
        {
            score.AddScore(value);
        }

    }
    public void SubtractScore(PlayerId id, int value)
    {
        if (scores.TryGetValue(id, out var score))
        {
            score.SubtractScore(value);
        }
    }

    public PlayerScore GetScore(PlayerId id) => scores.TryGetValue(id, out var score) ? score : null;

    public void RegisterPlayer(PlayerId playerId, Action<string> onScoreChange, ICatchAnimation catchAnimation)
    {
        if (scores.ContainsKey(playerId)) return;

        var score = new PlayerScore(playerId, catchAnimation);
        score.OnScoreChanged += onScoreChange;
        scores.Add(playerId, score);
    }
    public KeyValuePair<PlayerId, PlayerScore> GetWinner()
    {
        return scores
            .OrderByDescending(pair => pair.Value.Score)
            .First();
    }

    public void ShowWinnerAndLosers()
    {
        var winner = GetWinner();

        foreach (var pair in scores)
        {
            if (pair.Key == winner.Key)
            {
                pair.Value.CatchAnimation.PlayWin();
            }
            else
            {
                pair.Value.CatchAnimation.PlayFail();
            }
        }
    }

}
