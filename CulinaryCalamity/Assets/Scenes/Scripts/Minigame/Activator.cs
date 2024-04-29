using UnityEngine;

/// <summary>
/// Represents the activator object that changes color temporarily upon interaction with a note.
/// </summary>
public class Activator : MonoBehaviour
{
    private Color _originalColor;
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Initializes the sprite renderer and stores the original color.
    /// </summary>
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    /// <summary>
    /// Changes the color of the activator sprite temporarily with a specified delay.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="delay"></param>
    public void ChangeColorWithDelay(Color color, float delay)
    {
        _spriteRenderer.color = color;
        Invoke("RevertColor", delay);
    }

    /// <summary>
    /// Reverts the activator sprite color to its original color.
    /// </summary>
    private void RevertColor()
    {
        _spriteRenderer.color = _originalColor;
    }
}
