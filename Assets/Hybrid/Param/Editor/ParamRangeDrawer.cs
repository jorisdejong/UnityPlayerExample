using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer( typeof( ParamRange ) )]
public class ParamRangeDrawer : PropertyDrawer
{
    bool foldout;
    public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
    {
        EditorGUI.BeginProperty( position, label, property );

        //get the param
        var param = fieldInfo.GetValue( property.serializedObject.targetObject ) as ParamRange;

        //set the displayname
        var displayNameProperty = property.FindPropertyRelative( "displayName" );
        if ( string.IsNullOrEmpty( displayNameProperty.stringValue ) )
            displayNameProperty.stringValue = property.displayName;

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float nextLine = lineHeight + EditorGUIUtility.standardVerticalSpacing;
        float width = EditorGUIUtility.fieldWidth;
        float valueStart = EditorGUIUtility.labelWidth + 2;
        int paramIndent = 30;

        //set the slider
        var rect = new Rect( position.x, position.y, position.width, lineHeight );
        param.SetValue( EditorGUI.Slider( rect, property.displayName, param.values[ 0 ], param.values[ 1 ], param.values[ 2 ] ) );

        rect.x += paramIndent;
        rect.y += nextLine;
        foldout = EditorGUI.Foldout( rect, foldout, "Advanced" );

        if ( foldout )
        {
            EditorGUIUtility.labelWidth -= paramIndent;
            rect.y += nextLine;
            EditorGUI.LabelField( rect, "Range" );

            //custom rects for the min and max fields
            rect = new Rect( position.x + valueStart, rect.y, width, lineHeight );
            param.values[ 1 ] = EditorGUI.FloatField( rect, param.values[ 1 ] );
            var maxRect = new Rect( position.xMax - 100, rect.y, width, lineHeight );
            param.values[ 2 ] = EditorGUI.FloatField( maxRect, param.values[ 2 ] );

            //reset the rect
            rect = new Rect( position.x + paramIndent, rect.y + nextLine, position.width - width - paramIndent, lineHeight );
            
            displayNameProperty.stringValue = EditorGUI.TextField( rect, displayNameProperty.displayName, displayNameProperty.stringValue );

            rect.y += nextLine;
            var catProperty = property.FindPropertyRelative( "category" );
            catProperty.stringValue = EditorGUI.TextField( rect, catProperty.displayName, catProperty.stringValue );

            rect.y += nextLine;

            var valueChanged = property.FindPropertyRelative( "valueChanged" );
            EditorGUI.PropertyField( rect, valueChanged );
        }
        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
    {
        float height = 2 * ( EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing );
        if ( foldout )
        {
            height += 3 * ( EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing );
            var valueChanged = property.FindPropertyRelative( "valueChanged" );
            height += EditorGUI.GetPropertyHeight( valueChanged );
        }
        return height;
    }
}
