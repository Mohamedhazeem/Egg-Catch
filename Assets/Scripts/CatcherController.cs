using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CatcherController : MonoBehaviour
{
    [Header("Catcher Settings")]
    [SerializeField] private Transform catchPoint; // Reference to CatchPoint near stomach in Pokenman

    private CatchLane currentLane = CatchLane.Middle;
    private ICatchInput input;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        input = GetComponent<ICatchInput>();
    }

    private void Update()
    {
        if (input == null) return;

        CatchLane newLane = input.GetLaneInput();
        animator.SetFloat("LeanDirection", GetLeanValue(newLane), 0.1f, Time.deltaTime);
        if (newLane != currentLane && newLane != default)
        {
            MoveToLane(newLane);
        }
    }

    private void MoveToLane(CatchLane lane)
    {
        currentLane = lane;
        print($"lane in catch controller {lane}");
    }
    private float GetLeanValue(CatchLane lane)
    {
        return lane == CatchLane.Left ? -1f :
               lane == CatchLane.Right ? 1f : 0f;
    }

    public CatchLane GetCurrentLane() => currentLane;
    public Transform CatchPoint => catchPoint;
}
