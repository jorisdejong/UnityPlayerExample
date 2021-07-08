using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParamEvent : ParamBool
{
    private ParamEvent()
    {
        Init();
        if ( valueChanged == null )
            valueChanged = new ParamBoolEvent();

        values.Add( 0.0f );
    }
}
