using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GridPlacing
{
    public class GridPlacingMasterEditor
    {
        public static void TitleCreator(string title = "", int fontSize = 0, bool bold = false, TextAnchor textAlignment = TextAnchor.MiddleLeft)
        {
            GUIStyle titleStyle;

            if (bold)
                titleStyle = new GUIStyle(EditorStyles.boldLabel);
            else
                titleStyle = new GUIStyle(EditorStyles.label);

            titleStyle.fontSize += fontSize;
            titleStyle.alignment = textAlignment;

            EditorGUILayout.LabelField(title, titleStyle);
        }

        public static void DrawUILine(Color color, float thickness = 2, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.xMin = 0;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }
    }
}

