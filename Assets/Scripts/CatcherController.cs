using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CatcherController : MonoBehaviour, ICatch, IPlayerSetup
{
    [Header("Catcher Settings")]
    [SerializeField] private Transform catchPoint; // Reference to CatchPoint near stomach in Pokenman

    private CatchLane currentLane = CatchLane.Middle;
    private ICatchInput input; ICatchAnimation catchAnimation; IPlayerScoreUI scoreUI;
    public PlayerId playerId;
    private bool isHuman;
    private void Awake()
    {
        // input = GetComponent<ICatchInput>();
        catchAnimation = GetComponent<ICatchAnimation>();
        scoreUI = transform.parent.GetComponentInChildren<IPlayerScoreUI>();
    }
    void Start()
    {
        scoreUI.SetPlayerName(playerId.ToString());
        ScoreManager.Instance.RegisterPlayer(playerId, scoreUI.UpdateScoreText, catchAnimation);
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

    public T AddComponent<T>() where T : Component
    {

        var component = gameObject.AddComponent<T>();
        if (component is ICatchInput inputComponent)
        {
            input = inputComponent;
            catchAnimation.SetInput(input);
        }
        return component;
    }

    public void SetAsHuman(bool isHuman) => this.isHuman = isHuman;

    public bool IsHuman() => isHuman;
}
