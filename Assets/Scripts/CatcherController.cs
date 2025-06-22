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
        if (newLane != currentLane && newLane != default)
        {
            MoveToLane(newLane);
        }
    }

    private void MoveToLane(CatchLane lane)
    {
        currentLane = lane;
        print($"lane in catch controller {lane}");
        // animator.SetTrigger(lane.ToString());
    }

    public CatchLane GetCurrentLane() => currentLane;
    public Transform CatchPoint => catchPoint;
}
