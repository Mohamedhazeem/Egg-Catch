using UnityEngine;

public interface ICatch : IComponent
{
    void MoveToLane(CatchLane lane);
    CatchLane GetCurrentLane();
    Transform CatchPoint();
    PlayerId GetPlayerId();
    void SetPlayerId(PlayerId id);
}
