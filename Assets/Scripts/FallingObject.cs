using UnityEngine;
using Lean.Pool;
using DG.Tweening;
using System.Collections;

public class FallingObject : MonoBehaviour
{
    [Header("Fall Settings")]
    public CatchLane fallLane;
    public float fallSpeed = 3f;
    public float despawnY = -6f;
    [Header("Bomb")]
    public bool isBomb;
    [Header("Catch Settings")]
    [SerializeField, Tooltip("Time it takes for the object to move to the catcher after being caught")]
    private float catchMoveDuration = 0.3f;
    public Ease catchEasing = Ease.InOutSine;
    private bool isCaught = false, isStun;
    private Transform targetCatchPoint;

    [Header("Animation")]
    public bool isAnimate = false;
    public float maximumRotationAngle = 360f, duration = 1f;
    public Ease animationEasing = Ease.Linear;
    public LoopType loopType = LoopType.Incremental;

    private Tween rotationTween;
    void OnEnable()
    {
        if (isAnimate)
        {
            Vector3 randomAxis = Random.onUnitSphere;
            rotationTween = transform.DORotate(randomAxis * maximumRotationAngle, duration, RotateMode.FastBeyond360)
                                    .SetEase(animationEasing)
                                    .SetLoops(-1, loopType);
        }
    }
    private void Update()
    {
        if (!isCaught)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;

            if (transform.position.y < despawnY)
            {
                DespawnSelf();
            }
        }
    }

    private void OnCaught(ICatch catchController, ICatchAnimation catcherAnimation)
    {
        if (isBomb)
        {
            ScoreManager.Instance.SubtractScore(catchController.GetPlayerId(), 5);
            catcherAnimation.StunEffect();
        }
        else
            ScoreManager.Instance.AddScore(catchController.GetPlayerId(), 1);

        LeanPool.Despawn(gameObject);
    }

    public void DespawnSelf()
    {
        LeanPool.Despawn(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCaught) return;
        if (other.CompareTag("CatchZone"))
        {
            var catcher = other.GetComponentInParent<ICatch>();
            var catcherAnimation = other.GetComponentInParent<ICatchAnimation>();

            if (catcher != null && catcher.GetCurrentLane() == fallLane && !catcherAnimation.IsStun())
            {
                isCaught = true;
                targetCatchPoint = catcher.CatchPoint();
                transform
                    .DOMove(targetCatchPoint.position, catchMoveDuration)
                    .SetEase(catchEasing)
                    .OnComplete(() => OnCaught(catcher, catcherAnimation));
            }
        }
    }


    void OnDisable()
    {
        rotationTween?.Kill();
        isCaught = false;
        targetCatchPoint = null;
    }
}
