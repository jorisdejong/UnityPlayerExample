using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{
    public ParamRange cameraHeight;
    public ParamRange cameraRotation;
    public ParamRange noiseHeight;
    public ParamColor lightColor;

    public GameObject prefab;
    
    List<GameObject> children;
    float height = 5.0f;
   
    void Start()
    {
        children = new List<GameObject>();
        for ( int y = -20; y < 20; y++ )
            for ( int x = -20; x < 20; x+=2 )
            {
                GameObject child = Instantiate<GameObject>( prefab );
                int offset = 0;
                if ( y % 2 == 0 )
                    offset = 1;
                child.transform.position = new Vector3( x + 0.5f + offset, 0.0f, y + 0.5f  );
                children.Add( child );
            }
    }

    // Update is called once per frame
    void Update()
    {
        foreach ( var child in children )
        {
            float noise = Mathf.PerlinNoise( children.IndexOf( child ) * 3.141592654f, Time.time );
            child.transform.localScale = new Vector3( 1.0f, noise * height + 1.0f, 1.0f );
        }
    }

    public void SetHeight( float newHeight )
    {
        height = newHeight;
    }
}
