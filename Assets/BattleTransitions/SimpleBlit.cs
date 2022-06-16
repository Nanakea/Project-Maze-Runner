using UnityEngine;
using System.Collections;

public class SimpleBlit : MonoBehaviour
{
    public Material TransitionMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Debug.Log("OnRenderImage1");
        if (TransitionMaterial != null)
        {
            Debug.Log("OnRenderImage2");
            Graphics.Blit(src, dst, TransitionMaterial);
        }
    }
}
