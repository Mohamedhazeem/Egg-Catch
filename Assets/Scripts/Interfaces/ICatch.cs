using UnityEngine;

public interface ICatch
{
    void MoveToLane(CatchLane lane);
    CatchLane GetCurrentLane();
    Transform CatchPoint();
    PlayerId GetPlayerId();
}