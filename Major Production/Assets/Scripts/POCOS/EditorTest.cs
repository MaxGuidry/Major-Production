using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomPropertyDrawer(typeof(SectionHeaderAttribute))]
public class EditorTest : DecoratorDrawer {

    private GUIStyle headerStyle;

    public GUIStyle HeaderStyle
    {
        get
        {
            if (headerStyle == null)
            {
               
                headerStyle = new GUIStyle("TE NodeBox");
                headerStyle.fontSize = EditorStyles.label.fontSize;
                headerStyle.fontStyle = FontStyle.Bold;
                headerStyle.normal.textColor = EditorStyles.largeLabel.normal.textColor;
                headerStyle.alignment = TextAnchor.MiddleCenter;
                headerStyle.padding = new RectOffset(2, 2, 0, 2);
                headerStyle.contentOffset = Vector2.zero;
            }
            return headerStyle;
        }
    }

    /// <inheritdoc />
    public override float GetHeight()
    {
        float result;

        result = 2.5f * EditorGUIUtility.singleLineHeight;

        return result;
    }

    /// <inheritdoc />
    public override void OnGUI(Rect position)
    {
        var attribute = (SectionHeaderAttribute)base.attribute;

        position.yMin +=  EditorGUIUtility.singleLineHeight;
        position.yMax -= EditorGUIUtility.standardVerticalSpacing;

        position.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.LabelField(position, attribute.text, HeaderStyle);
    }
}

