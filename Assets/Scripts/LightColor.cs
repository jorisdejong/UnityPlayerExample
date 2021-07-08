using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColor : MonoBehaviour
{
    Light l;

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Light>();
    }

  
    public void SetColor( Color color )
    {
        l.color = color;
    }
}
