using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void Start()
    {
        var RendererMaterials = GetComponent<Renderer>();
        Material[] materials = RendererMaterials.materials;

        foreach(Material mat in materials)
        {
           // RendererMaterials.material = defaultMaterial;
        }
    }

    public void ColorChange()
    {
        //for(var i = 0; i < )
    }
}
