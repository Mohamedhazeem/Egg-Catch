using System;
using System.Collections.Generic;

public interface IScore
{
    void RegisterPlayer(PlayerId playerId, Action<string> onScoreChange, ICatchAnimation catchAnimation);
    void AddScore(PlayerId playerId, int delta);
    void SubtractScore(PlayerId id, int value);
    PlayerScore GetScore(PlayerId playerId);

    KeyValuePair<PlayerId, PlayerScore> GetWinner();
    void ShowWinnerAndLosers();
}
public interface IPlayerScoreUI : IUIElement
{
    void SetPlayerName(string name);
    void UpdateScoreText(string value);
}
public interface IRemainingFallingObjectCounterUI : IUIElement
{
    void SetRemainingFallingObjectCounterText(string value);
    void UpdateRemainingFallingObjectCounterText(string value);
}
