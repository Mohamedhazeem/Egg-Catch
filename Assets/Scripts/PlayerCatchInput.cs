using UnityEngine;

public class PlayerCatchInput : MonoBehaviour, ICatchInput
{
    private CatchLane currentLane = CatchLane.Middle;

    public CatchLane GetLaneInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            currentLane = CatchLane.Left;
        else if (Input.GetKeyDown(KeyCode.S))
            currentLane = CatchLane.Middle;
        else if (Input.GetKeyDown(KeyCode.D))
            currentLane = CatchLane.Right;
        return currentLane;
    }

    public void SetLane(CatchLane catchLane)
    {
        currentLane = catchLane;
    }
}
