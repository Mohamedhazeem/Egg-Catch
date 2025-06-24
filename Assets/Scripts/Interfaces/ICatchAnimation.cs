using System.Collections;

public interface ICatchAnimation
{
    void SetLean(CatchLane lane);
    void PlayWin();
    void PlayFail();
    void StunEffect();
    IEnumerator PlayStun();
    void PlayIdle();
    bool IsStun();
}