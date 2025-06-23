using System;

public interface IScore
{
    void RegisterPlayer(PlayerId playerId, Action<string> onScoreChange);
    void AddScore(PlayerId playerId, int delta);
    void SubtractScore(PlayerId id, int value);
    PlayerScore GetScore(PlayerId playerId);
}
public interface IScoreUI : IUIElement
{
    void UpdateScoreText(string value);
}