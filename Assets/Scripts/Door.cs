using UnityEngine;
using System.Collections;

public class Door : SelectableObject
{
    public bool requireKey = false;
    private int d = 0;

    public override void Action()
    {
        myRenderer.material.DisableKeyword("_EMISSION");
        myRenderer.material.SetColor("_EmissionColor", Color.white);
        PlayerController.controller.Deselect();
        isAvailable = false;
        StartCoroutine(Open());
    }

    IEnumerator Open()
    {
        while (d < 20)
        {
            transform.Translate(transform.forward * 0.1f);
            d++;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(this);
    }

}
