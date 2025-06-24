using System.Collections;
using UnityEngine;

public class CatcherAnimationController : MonoBehaviour, ICatchAnimation
{
    [SerializeField] private Animator animator;
    [SerializeField] private float leanDampTime = 0.1f;
    [Tooltip("Time it takes to transition from the stun animation back to idle.")]
    public float stunToIdleDuration = 1f;
    [HideInInspector] public bool isStun;
    private ICatchInput catchInput;
    void Start()
    {
        catchInput = GetComponent<ICatchInput>();
    }
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
}
