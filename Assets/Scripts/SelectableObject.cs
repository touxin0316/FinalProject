using UnityEngine;
using System.Collections;

public abstract class SelectableObject : MonoBehaviour
{
    private float v;
    private float delta;

    public bool isAvailable = true;
    private bool isMouseOver;
    public Renderer myRenderer;

    private void OnMouseDown()
    {
        if (isMouseOver)
            Action();
    }

    public void MouseEnter()
    {
        if (isMouseOver)
            return;

        isMouseOver = true;
        myRenderer.material.EnableKeyword("_EMISSION");
        myRenderer.material.SetColor("_EmissionColor", Color.white);

        v = 1.0f;
        delta = -0.05f;

        StartCoroutine(Flash());
    }

    public void MouseExit()
    {
        isMouseOver = false;
        myRenderer.material.DisableKeyword("_EMISSION");
        myRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    public abstract void Action();

    private IEnumerator Flash()
    {
        while (isMouseOver)
        {
            myRenderer.material.SetColor("_EmissionColor", new Color(1, v, v));

            v += delta;
            if (v < 0.6f || v > 1.0f)
                delta *= -1;

            yield return new WaitForSeconds(0.05f);
        }
    }

}
