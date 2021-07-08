using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer( typeof( ParamColor ) )]
public class ParamColorDrawer : PropertyDrawer
{
    bool foldout;
    public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
    {
        EditorGUI.BeginProperty( position, label, property );

        //get the param
        var param = fieldInfo.GetValue( property.serializedObject.targetObject ) as ParamColor;

        //set the displayname
        var displayNameProperty = property.FindPropertyRelative( "displayName" );
        if ( string.IsNullOrEmpty( displayNameProperty.stringValue ) )
            displayNameProperty.stringValue = property.displayName;

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float nextLine = lineHeight + EditorGUIUtility.standardVerticalSpacing;
        float width = EditorGUIUtility.fieldWidth;
        int paramIndent = 30;

        var rect = new Rect( position.x, position.y, position.width, lineHeight );
        EditorGUI.LabelField( rect, property.displayName );
        //set the param
        rect.x += EditorGUIUtility.labelWidth + 2;
        rect.width -= EditorGUIUtility.labelWidth + width;
      
        param.SetColor( EditorGUI.ColorField( rect, param.GetColor() ) );

        rect.x = position.x + paramIndent;
        rect.y += nextLine;
        foldout = EditorGUI.Foldout( rect, foldout, "Advanced" );

        if ( foldout )
        {
            EditorGUIUtility.labelWidth -= paramIndent;
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
            height += 2 * ( EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing );
            var valueChanged = property.FindPropertyRelative( "valueChanged" );
            height += EditorGUI.GetPropertyHeight( valueChanged );
        }
        return height;
    }
}
