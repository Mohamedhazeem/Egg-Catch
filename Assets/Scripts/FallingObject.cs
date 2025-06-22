using UnityEngine;
using Lean.Pool;
using DG.Tweening;

public class FallingObject : MonoBehaviour
{
    public CatchPosition fallLane;
    public float fallSpeed = 3f;
    public float despawnY = -6f;
    public bool isAnimate = false;

    private Tween rotationTween;
    void OnEnable()
    {
        if (isAnimate)
        {
            // Rotate continuously on a random axis
            Vector3 randomAxis = Random.onUnitSphere;
            rotationTween = transform.DORotate(randomAxis * 360f, 1f, RotateMode.FastBeyond360)
                                    .SetEase(Ease.Linear)
                                    .SetLoops(-1, LoopType.Incremental);
        }
    }

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < despawnY)
        {
            DespawnSelf();
        }
    }

    public void DespawnSelf()
    {
        LeanPool.Despawn(gameObject); // Clean and pooled
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Catcher"))
        {
            DespawnSelf();
        }
    }
    void OnDisable()
    {
        rotationTween?.Kill();
    }
}
