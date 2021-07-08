
using System.Collections.Generic;

[System.Serializable]
public class ParamFloat : Param
{
    [System.Serializable]
    public class ParamFloatEvent : UnityEngine.Events.UnityEvent<float> { }
    [System.Xml.Serialization.XmlIgnore]
    public ParamFloatEvent valueChanged;
    protected ParamFloat()
    {
        Init();

        if ( valueChanged == null )
            valueChanged = new ParamFloatEvent();

        values.Add( 0.0f );
    }

    public virtual void SetValue( float newValue )
    {
        values[ 0 ] = newValue;
        SetValues ( GetValues() );
    }

    public float GetValue()
    {
        return values[0];
    }
    static public List<float> GetValues( float singleValue )
    {
        List<float> values = new List<float>();
        values.Add( singleValue );
        return values;
    }

    protected override void InvokeValueChanged()
    {
        if ( valueChanged != null )
            valueChanged.Invoke( GetValue() );
    }


}
