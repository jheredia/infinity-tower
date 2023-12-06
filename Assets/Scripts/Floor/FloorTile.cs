using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void Show()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = true;
        }
    }

    public void Hide()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }
}
