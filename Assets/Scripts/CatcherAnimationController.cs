using System.Collections;
using UnityEngine;

public class CatcherAnimationController : MonoBehaviour, ICatchAnimation
{
    [SerializeField] private Animator animator;
    [SerializeField] private float leanDampTime = 0.1f;
    [Tooltip("Time it takes to transition from the stun animation back to idle.")]
    public float stunToIdleDuration = 1f;
    [HideInInspector] public bool isStun;
    internal ICatchInput catchInput;
    private bool isHuman;
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
        isHuman = catchInput is PlayerCatchInput;
        if (isHuman)
        {
            SoundManager.Instance.Play(SFXType.Win);
        }
        animator.SetTrigger("Win");
    }

    public void PlayFail()
    {
        isHuman = catchInput is PlayerCatchInput;
        if (isHuman)
        {
            SoundManager.Instance.Play(SFXType.Fail);
        }
        animator.SetTrigger("Fail");
    }

    public void StunEffect()
    {
        StartCoroutine(PlayStun());
    }
    public IEnumerator PlayStun()
    {
        isStun = true;
        animator.SetTrigger("Stun");
        yield return new WaitForSeconds(stunToIdleDuration);
        PlayIdle();
        catchInput.SetLane(CatchLane.Middle);
        yield return null;
        isStun = false;
    }
    public void PlayIdle()
    {
        animator.SetTrigger("Idle");
    }
    public bool IsStun() => isStun;

    public void SetInput(ICatchInput catchInput) => this.catchInput = catchInput;
}
