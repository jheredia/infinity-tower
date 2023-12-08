using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshRenderer rangeMeshRenderer;

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

    public void ShowRange()
    {
        if (rangeMeshRenderer != null)
        {
            rangeMeshRenderer.enabled = true;
        }
    }
}
