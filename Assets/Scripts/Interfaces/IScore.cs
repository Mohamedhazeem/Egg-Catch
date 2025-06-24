using System;

public interface IScore
{
    void RegisterPlayer(PlayerId playerId, Action<string> onScoreChange);
    void AddScore(PlayerId playerId, int delta);
    void SubtractScore(PlayerId id, int value);
    PlayerScore GetScore(PlayerId playerId);
}
public interface IPlayerScoreUI : IUIElement
{
    void UpdateScoreText(string value);
}
public interface IRemainingFallingObjectCounterUI : IUIElement
{
    void SetRemainingFallingObjectCounterText(string value);
    void UpdateRemainingFallingObjectCounterText(string value);

}