using UnityEngine;

public class Activator : MonoBehaviour
{
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Initializes the sprite renderer and stores the original color.
    /// </summary>
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    /// <summary>
    /// Changes the color of the activator sprite temporarily with a specified delay.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="delay"></param>
    public void ChangeColorWithDelay(Color color, float delay)
    {
        spriteRenderer.color = color;
        Invoke("RevertColor", delay);
    }

    /// <summary>
    /// Reverts the activator sprite color to its original color.
    /// </summary>
    private void RevertColor()
    {
        spriteRenderer.color = originalColor;
    }
}
