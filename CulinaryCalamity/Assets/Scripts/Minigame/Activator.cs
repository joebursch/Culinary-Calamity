using UnityEngine;

public class Activator : MonoBehaviour
{
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void ChangeColorWithDelay(Color color, float delay)
    {
        spriteRenderer.color = color;
        Invoke("RevertColor", delay);
    }

    private void RevertColor()
    {
        spriteRenderer.color = originalColor;
    }
}
