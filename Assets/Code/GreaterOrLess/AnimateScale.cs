using UnityEngine;

public class AnimateScale : MonoBehaviour
{
    private float progress;
    
    void Update()
    {
        progress += Time.deltaTime;
        var startScale = Vector3.one;
        var endScale = startScale * 1.3f;
        transform.localScale = Vector3.Lerp(startScale, endScale, progress);
    }

    public void ResetState()
    {
        transform.localScale = Vector3.one;
        progress = 0f;
    }
}