using UnityEngine;
using UnityEngine.Events;

public class CutIndicator : MonoBehaviour
{
    public UnityEvent OnHit;
    [SerializeField]
    private float lifetime = 0f;

    public void Construct(float rotationDeg)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationDeg);
    }

    internal void Hit()
    {
        OnHit.Invoke();
        Destroy(gameObject, lifetime);
    }
}
