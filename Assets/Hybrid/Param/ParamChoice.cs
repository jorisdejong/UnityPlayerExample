
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ParamChoice : Param
{
    [SerializeField]
    public string[] choices;

    [System.Serializable]
    public class ParamChoiceEvent : UnityEngine.Events.UnityEvent<int> { }
    [System.Xml.Serialization.XmlIgnore]
    public ParamChoiceEvent valueChanged;
    private ParamChoice()
    {
        Init();
        if ( valueChanged == null )
            valueChanged = new ParamChoiceEvent();
        values.Add( -1.0f );
    }
    public void SetIndex( int newIndex )
    {
        values[ 0 ] = (float) newIndex;
        SetValues( values );
    }
    public int GetIndex()
    {
        return (int) values[ 0 ];
    }


    protected override void InvokeValueChanged()
    {
        if ( valueChanged != null )
            valueChanged.Invoke( GetIndex() );
    }

}
