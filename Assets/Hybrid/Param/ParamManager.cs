
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ParamManager : ScriptableObject
{
    static ParamManager instance;

    //non serialized because we build this list every time we enter and exit playmode
    Dictionary<string, Param> parameters;
    public static ParamManager GetInstance()
    {
        if ( instance == null )
        {
            instance = CreateInstance<ParamManager>();
            instance.parameters = new Dictionary<string, Param>();

            //Debug.Log( "New ParamManager created" );
        }
        return instance;
    }
    //gets called by anyone tracking this param
    public void SetParam( string uid, List<float> values )
    {
        if ( !parameters.ContainsKey( uid ) )
            return;
        parameters[ uid ].SetValues( values );
    }

    public void AddParam( Param newParam )
    {
        if ( parameters == null )
        {
            Debug.Log( "no parameters dict" );
            return;
        }

        if ( parameters.ContainsKey( newParam.guidString ) )
        {
            if ( parameters[ newParam.guidString ] == newParam )
            {
                //param already in list, just continue
                //Debug.Log( "Param already in list" );
            }
            else
            {
                //uh oh, duplicate paramguid detected
                //so let's create a new guid
                //if we're in edit mode, great, this will resolve the problem permanently
                //if not, strange, this should have been resolved by DuplicateParamResolver before we got here
                //so in that case, we still create a new temporary guid for this param
                //this will break functionality for any external apps tracking this param
                //but at least we won't have two params responding to the same control
                if ( !Application.isEditor )
                    Debug.LogError( "Duplicate guid detected!" );
                while ( parameters.Keys.Contains( newParam.guidString ) )
                {
                    System.Guid guid = System.Guid.NewGuid();
                    newParam.guidString = guid.ToString();
                }
                parameters.Add( newParam.guidString, newParam );
            }
        }
        else if ( parameters.ContainsValue( newParam ) )
        {
            //param is already somewhere in the list with another guid
            //so remove it there and then add the new one
            Debug.LogError( "We should never see this. Something is going wrong in ParamManager" );
            parameters.Remove( parameters.FirstOrDefault( x => x.Value == newParam ).Key );
            parameters.Add( newParam.guidString, newParam );
        }
        else
        {
            //not found, just add
            parameters.Add( newParam.guidString, newParam );
        }
        //Debug.Log( "There are " + parameters.Count + " parameters" );
    }
}
