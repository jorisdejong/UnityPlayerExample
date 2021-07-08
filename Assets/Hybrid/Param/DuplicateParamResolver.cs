using UnityEngine;

using System;
using System.Reflection;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
// ensure class initializer is called whenever scripts recompile
[InitializeOnLoadAttribute]
public static class DuplicateParamResolver
{
    // register an event handler when the class is initialized
    static DuplicateParamResolver()
    {
        EditorApplication.playModeStateChanged += ResolveDuplicates;
    }

    private static void ResolveDuplicates( PlayModeStateChange state )
    {
        //Debug.Log( state );

        MonoBehaviour[] allMonos = MonoBehaviour.FindObjectsOfType<MonoBehaviour>();

        if ( state == PlayModeStateChange.ExitingEditMode )
        {
            List<string> guids = new List<string>();
            foreach ( MonoBehaviour mono in allMonos )
                foreach ( FieldInfo info in mono.GetType().GetFields() )
                    if ( info.FieldType.IsSubclassOf( typeof( Param ) ) )
                    {
                        Param p = info.GetValue( mono ) as Param;

                        while ( guids.Contains( p.guidString ) )
                        {
                            Guid newGuid = Guid.NewGuid();
                            p.guidString = newGuid.ToString();
                        }
                        guids.Add( p.guidString );
                    }
        }
    }
}

#endif
