using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColor : MonoBehaviour
{
    Material m;

    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<MeshRenderer>().sharedMaterial;
    }

    public void SetColor( Color color )
    {
        m.SetColor( "_EmissiveColor", color );
    }
}
