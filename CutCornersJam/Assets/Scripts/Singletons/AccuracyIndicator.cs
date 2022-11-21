using TMPro;
using UnityEngine;

public class AccuracyIndicator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI label;
    [SerializeField]
    private float lifetime = 0.5f;
    public void Construct(float accuracy, Color color, Vector2 position)
    {
        label.text = Mathf.RoundToInt(accuracy * 100) + "%";
        label.color = color;
        transform.position = position;
        Destroy(gameObject, lifetime);
    }
}
