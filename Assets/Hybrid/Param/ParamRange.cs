
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParamRange : ParamFloat
{
    private ParamRange()
    {
        Init();

        if ( valueChanged == null )
            valueChanged = new ParamFloatEvent();

        values.Add( 0.0f ); //value
        values.Add( 0.0f ); //min
        values.Add( 1.0f ); //max
    }


    /** will clamp the incoming value to the min and max of the range */
    public override void SetValue( float newValue )
    {
        if ( newValue < values[ 1 ] || newValue > values[ 2 ] )
            Debug.LogWarning( displayName + " is trying to set a value outside its min/max range..." );

        values[ 0 ] = Mathf.Clamp( newValue, values[ 1 ], values[ 2 ] );
        SetValues( GetValues() );
    }

    /// <summary>
    /// will return this params value in the 0...1 range 
    /// </summary>
    public float GetValueNormalized()
    {
        return ( values[ 0 ] - values[ 1 ] ) / ( values[ 2 ] - values[ 1 ] );
    }

    /// <summary>
    /// Expects a list of values in the 0...1 range. The function will scale it to the params existing min/max.
    /// </summary>
    public override void SetValuesNormalized( List<float> newValues )
    {
        values[ 0 ] = newValues[ 0 ] * ( values[ 2 ] - values[ 1 ] ) + values[ 1 ];
        SetValues( values );
    }
}
