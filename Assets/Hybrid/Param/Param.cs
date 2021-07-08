using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Param
{
    [ReadOnly]
    public string guidString;
    public string displayName;
    public string category = "No Category";
    public List<float> values;

    public Param()
    {
        CreateGuid();
    }

    protected void Init()
    {
        values = new List<float>(); 
    }

    //each param will need to implement to get dynamic valueChanged calls
    //with the approriate value type as argument
    protected virtual void InvokeValueChanged()
    {
        throw new NotImplementedException();
    }
    
    //shared setvalue function
    //each param implementation will eventually call this function after doing type specific casting
    //or the parammanager will call it directly
    public void SetValues( List<float> newValues )
    {
        if ( values.Count == newValues.Count )
            values = new List<float>( newValues );
        else if ( this is ParamRange && newValues.Count == 1 ) //special exception for receiving a single value for a paramrange
            ( (ParamRange) this ).SetValue( newValues[ 0 ] );
        else
        {
            Debug.LogError( displayName + " is trying to set values the param doesn't have" );
            return;
        }

        //back to each implementation to handle the callback
        InvokeValueChanged();
    }

    public virtual void SetValuesNormalized( List<float> newValues )
    {
        SetValues( newValues );
    }

    public List<float> GetValues()
    {
        return values;
    }

    public void CreateGuid()
    {
        //if we don't have a guid for this param yet
        while ( string.IsNullOrEmpty( guidString ) )
        {
            //create a new one
            Guid guid = Guid.NewGuid();
            guidString = guid.ToString();
        }
    }
}
