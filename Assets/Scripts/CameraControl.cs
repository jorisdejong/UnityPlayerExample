using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float rotate;
    float height = 0.2f;

    void SetCameraPosition()
    {
        float y = Mathf.Sin( Mathf.Deg2Rad * height );

        float x = Mathf.Lerp( Mathf.Cos( Mathf.Deg2Rad * rotate ), 0.0f, y );

        float z = Mathf.Lerp( Mathf.Sin( Mathf.Deg2Rad * rotate ), 0.0f, y );

        y *= 20.0f;
        x *= 20.0f;
        z *= 20.0f;

        transform.position = new Vector3( x, y, z );
        transform.LookAt( new Vector3( 0, 0, 0 ) );
    }

    public void SetHeight( float newHeight )
    {
        height = newHeight;
        SetCameraPosition();
    }

    public void SetRotation( float newRotation )
    {
        rotate = newRotation;
        SetCameraPosition();
    }
}
