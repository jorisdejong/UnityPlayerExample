
using System.Collections.Generic;

[System.Serializable]
public class ParamBool : Param
{
    [System.Serializable]
    public class ParamBoolEvent : UnityEngine.Events.UnityEvent<bool> { }
    [System.Xml.Serialization.XmlIgnore]
    public ParamBoolEvent valueChanged;
    protected ParamBool()
    {
        Init();

        if ( valueChanged == null )
            valueChanged = new ParamBoolEvent();

        values.Add( 0.0f );
    }

    public void SetBool( bool newValue )
    {
        SetValues( GetValues( newValue ) );
    }

    public bool GetBool()
    {
        return GetBool( values[ 0 ] );
    }

    static public bool GetBool( float value )
    {
        return value > 0.5f;
    }

    static public List<float> GetValues( bool newValue )
    {
        List<float> values = new List<float>();
        values.Add( newValue ? 1.0f : 0.0f );
        return values;
    }

    protected override void InvokeValueChanged()
    {
        if ( valueChanged != null )
            valueChanged.Invoke( GetBool() );
    }
}
