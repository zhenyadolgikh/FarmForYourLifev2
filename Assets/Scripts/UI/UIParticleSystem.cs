using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIParticleSystem : MaskableGraphic
{
    [SerializeField] private ParticleSystemRenderer particleSystemRenderer;
    [SerializeField] private Camera uiCamera;

    private void Update()
    {
        SetVerticesDirty();
    }
    protected override void OnPopulateMesh(Mesh mesh)
    {
        mesh.Clear(mesh);
        if (particleSystemRenderer != null && uiCamera != null)
        {
            particleSystemRenderer.BakeMesh(mesh, uiCamera);
        }
    }
}
