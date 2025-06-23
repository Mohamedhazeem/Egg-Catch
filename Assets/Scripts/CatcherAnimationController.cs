using UnityEngine;

public class CatcherAnimationController : MonoBehaviour, ICatchAnimation
{
    [SerializeField] private Animator animator;
    [SerializeField] private float leanDampTime = 0.1f;
    private float GetLeanValue(CatchLane lane)
    {
        return lane == CatchLane.Left ? -1f :
               lane == CatchLane.Right ? 1f : 0f;
    }
    public void SetLean(CatchLane lane)
    {
        animator.SetFloat("LeanDirection", GetLeanValue(lane), leanDampTime, Time.deltaTime);
    }

    public void PlayWin()
    {
        animator.SetTrigger("Win");
    }

    public void PlayFail()
    {
        animator.SetTrigger("Fail");
    }
}
