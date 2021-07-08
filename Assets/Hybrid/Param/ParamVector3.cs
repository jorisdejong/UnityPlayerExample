
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParamVector3 : Param
{
    [System.Serializable]
    public class ParamVector3Event : UnityEngine.Events.UnityEvent<Vector3> { }
    [System.Xml.Serialization.XmlIgnore]
    public ParamVector3Event valueChanged;
    private ParamVector3()
    {
        Init();

        for ( int i = 0; i < 3; i++ )
            values.Add( 0.0f );
    }

    public void SetVector3( Vector3 newVector3 )
    {
        SetValues( GetValues( newVector3 ) );
    }

    public Vector3 GetVector3()
    {
        return GetVector3( values );
    }

    public static Vector3 GetVector3( List<float> values )
    {
        return new Vector3( values[ 0 ], values[ 1 ], values[ 2 ] );
    }

    public static List<float> GetValues( Vector3 newVector3 )
    {
        List<float> values = new List<float>();
        values.Add( newVector3.x );
        values.Add( newVector3.y );
        values.Add( newVector3.z );
        return values;
    }

    protected override void InvokeValueChanged()
    {
        if ( valueChanged != null )
            valueChanged.Invoke( GetVector3() );
    }
}
