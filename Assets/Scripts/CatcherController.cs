using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CatcherController : MonoBehaviour, ICatch
{
    [Header("Catcher Settings")]
    [SerializeField] private Transform catchPoint; // Reference to CatchPoint near stomach in Pokenman

    private CatchLane currentLane = CatchLane.Middle;
    private ICatchInput input; ICatchAnimation catchAnimation; IScoreUI scoreUI;
    private Animator animator;
    public PlayerId playerId;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        input = GetComponent<ICatchInput>();
        catchAnimation = GetComponent<ICatchAnimation>();
        scoreUI = transform.parent.GetComponentInChildren<IScoreUI>();
    }
    void Start()
    {
        ScoreManager.Instance.RegisterPlayer(playerId, scoreUI.UpdateScoreText);
    }
    private void Update()
    {
        if (input == null) return;

        CatchLane newLane = input.GetLaneInput();
        catchAnimation.SetLean(newLane);
        if (newLane != currentLane && newLane != default)
        {
            MoveToLane(newLane);
        }
    }

    public void MoveToLane(CatchLane lane)
    {
        currentLane = lane;
        print($"lane in catch controller {lane}");
    }

    public CatchLane GetCurrentLane() => currentLane;
    public Transform CatchPoint() => catchPoint;

    public PlayerId GetPlayerId() => playerId;
}
