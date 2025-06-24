using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CatcherController : MonoBehaviour, ICatch
{
    [Header("Catcher Settings")]
    [SerializeField] private Transform catchPoint; // Reference to CatchPoint near stomach in Pokenman

    private CatchLane currentLane = CatchLane.Middle;
    private ICatchInput input; ICatchAnimation catchAnimation; IPlayerScoreUI scoreUI;
    public PlayerId playerId;

    private void Awake()
    {
        input = GetComponent<ICatchInput>();
        catchAnimation = GetComponent<ICatchAnimation>();
        scoreUI = transform.parent.GetComponentInChildren<IPlayerScoreUI>();
    }
    void Start()
    {
        scoreUI.SetPlayerName(playerId.ToString());
        ScoreManager.Instance.RegisterPlayer(playerId, scoreUI.UpdateScoreText);
    }
    private void Update()
    {
        if (input == null || catchAnimation.IsStun()) return;

        CatchLane newLane = input.GetLaneInput();
        catchAnimation.SetLean(newLane);

        if (newLane != currentLane)
        {
            MoveToLane(newLane);
        }
    }

    public void MoveToLane(CatchLane lane)
    {
        currentLane = lane;
    }

    public CatchLane GetCurrentLane() => currentLane;
    public Transform CatchPoint() => catchPoint;

    public PlayerId GetPlayerId() => playerId;

    public void SetPlayerId(PlayerId id)
    {
        playerId = id;
    }

    public void AddComponent<T>() where T : Component
    {
        gameObject.AddComponent<T>();
    }
}
