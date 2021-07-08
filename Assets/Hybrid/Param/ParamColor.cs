
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParamColor : Param
{
    [System.Serializable]
    public class ParamColorEvent : UnityEngine.Events.UnityEvent<Color> { }
    [System.Xml.Serialization.XmlIgnore]
    public ParamColorEvent valueChanged;
    private ParamColor()
    {
        Init();
        if ( valueChanged == null )
            valueChanged = new ParamColorEvent();
        for ( int i = 0; i < 3; i++ )
            values.Add( i ==  0 ? 1.0f : 0.0f );
    }

    public void SetColor( Color newColor )
    {
        SetValues( GetValues( newColor ) );
    }
    public Color GetColor()
    {
        return GetColor( values );
    }

    public static Color GetColor( List<float> values )
    {
        return new Color( values[ 0 ], values[ 1 ], values[ 2 ] );
    }

    public static List<float> GetValues( Color newColor )
    {
        List<float> values = new List<float>();
        values.Add( newColor.r );
        values.Add( newColor.g );
        values.Add( newColor.b );
        return values;
    }

    protected override void InvokeValueChanged()
    {
        if ( valueChanged != null )
            valueChanged.Invoke( GetColor() );
    }

}
