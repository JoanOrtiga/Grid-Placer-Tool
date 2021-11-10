using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;


using GridPlacing;


[CustomEditor(typeof(AttachToGrid))]
public class AttachToGrid_Editor : Editor
{
    AttachToGrid attachToGridScript;

    bool placingOptions;
    bool placedEvents;
    bool collisionOptions;


    SerializedProperty placedOnGrid;
    SerializedProperty gridID;

    private void OnEnable()
    {
        placedOnGrid = serializedObject.FindProperty("isPlacedOnGrid");
        gridID = serializedObject.FindProperty("gridID");
    }

    public override void OnInspectorGUI()
    {
        attachToGridScript = (AttachToGrid)target;

        EditorGUILayout.Space();
        GridPlacingMasterEditor.TitleCreator("GridPlacer", 10, true);
        GridPlacingMasterEditor.DrawUILine(Color.white, 2, 20);

        gridID.intValue = EditorGUILayout.IntField("Grid ID", gridID.intValue);

        EditorGUILayout.Space();

        placingOptions = EditorGUILayout.Foldout(placingOptions, "Placing Options", true);

        if (placingOptions)
        {
            attachToGridScript.mousePlacement = EditorGUILayout.Toggle("Mouse Placement", attachToGridScript.mousePlacement);

            if (attachToGridScript.mousePlacement)
            {
                attachToGridScript.mousePlacingButton = (KeyCode)EditorGUILayout.EnumPopup("Mouse Placing Button", attachToGridScript.mousePlacingButton);
            }

            attachToGridScript.keyboardPlacement = EditorGUILayout.Toggle("Keyboard Placement", attachToGridScript.keyboardPlacement);

            if (attachToGridScript.keyboardPlacement)
            {
                attachToGridScript.keyboardPlacingButton = (KeyCode)EditorGUILayout.EnumPopup("Keyboard Placing Button", attachToGridScript.keyboardPlacingButton);
            }

            attachToGridScript.mouseMoving = EditorGUILayout.Toggle("Mouse Moving", attachToGridScript.mouseMoving);

            attachToGridScript.keyboardMoving = EditorGUILayout.Toggle("Keyboard Moving", attachToGridScript.keyboardMoving);

            if (attachToGridScript.keyboardMoving)
            {
                EditorGUILayout.HelpBox("Key set to none disables direction", MessageType.Info);

                attachToGridScript.moveUp = (KeyCode)EditorGUILayout.EnumPopup("Move up", attachToGridScript.moveUp);
                attachToGridScript.moveDown = (KeyCode)EditorGUILayout.EnumPopup("Move down", attachToGridScript.moveDown);
                attachToGridScript.moveRight = (KeyCode)EditorGUILayout.EnumPopup("Move right", attachToGridScript.moveRight);
                attachToGridScript.moveLeft = (KeyCode)EditorGUILayout.EnumPopup("Move left", attachToGridScript.moveLeft);
                attachToGridScript.moveUpRight = (KeyCode)EditorGUILayout.EnumPopup("Move up right", attachToGridScript.moveUpRight);
                attachToGridScript.moveUpLeft = (KeyCode)EditorGUILayout.EnumPopup("Move up left", attachToGridScript.moveUpLeft);
                attachToGridScript.moveDownRight = (KeyCode)EditorGUILayout.EnumPopup("Move down right", attachToGridScript.moveDownRight);
                attachToGridScript.moveDownLeft = (KeyCode)EditorGUILayout.EnumPopup("Move down left", attachToGridScript.moveDownLeft);
            }

            attachToGridScript.confineMouse = EditorGUILayout.Toggle("Confine Mouse", attachToGridScript.confineMouse);
        }

        GridPlacingMasterEditor.DrawUILine(Color.gray, 1.5f, 6);

        collisionOptions = EditorGUILayout.Foldout(collisionOptions, "Collision options", true);

        if (collisionOptions)
        {
            attachToGridScript.modeOfCollision = (CollisionMode)EditorGUILayout.EnumPopup(new GUIContent("Mode of collision", "None will disable collisions. Simple will check between box, circle and cylinder shapes either automatically or by your choice. Complex will instantiate the object that you are using and detect if it collides with something."), attachToGridScript.modeOfCollision);

            if (attachToGridScript.modeOfCollision == CollisionMode.Simple)
            {
                attachToGridScript.automaticDetection = EditorGUILayout.Toggle("Automatic detection", attachToGridScript.automaticDetection);

                if (attachToGridScript.automaticDetection)
                {
                    if (Selection.activeGameObject.GetComponent<Collider2D>() == null)
                    {
                        EditorGUILayout.HelpBox("Automatic detection is enabled, but gameobject doesn't have collider.", MessageType.Warning);

                        attachToGridScript.collisionType = (CollisionType)EditorGUILayout.EnumPopup(new GUIContent("Type of collision", "Select which type of collision will be used."), attachToGridScript.collisionType);
                    }

                }
                else
                {
                    attachToGridScript.collisionType = (CollisionType)EditorGUILayout.EnumPopup(new GUIContent("Type of collision", "Select which type of collision will be used."), attachToGridScript.collisionType);
                }

            }
            else if (attachToGridScript.modeOfCollision == CollisionMode.Complex)
            {

            }

            if(attachToGridScript.modeOfCollision != CollisionMode.None)
            {
                LayerMask tempMask = EditorGUILayout.MaskField(new GUIContent("Ignore Collision Layer Mask", "Unselected layers will not be checked for collision."), InternalEditorUtility.LayerMaskToConcatenatedLayersMask(attachToGridScript.ignoreLayers), InternalEditorUtility.layers);

                attachToGridScript.ignoreLayers = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
            }
        }


        GridPlacingMasterEditor.DrawUILine(Color.gray, 1.5f, 6);

        placedEvents = EditorGUILayout.Foldout(placedEvents, "Placed events", true);

        if (placedEvents)
        {
            attachToGridScript.placed = EditorGUILayout.Toggle("Is Placed On Grid", attachToGridScript.placed);

            EditorGUILayout.PropertyField(placedOnGrid, new GUIContent("Placed On Grid", "Event is called when object is placed on grid"));
        }

        GridPlacingMasterEditor.DrawUILine(Color.gray, 1.5f, 6);



        GUIStyle versionBox = new GUIStyle(EditorStyles.helpBox);
        GUIStyle a = new GUIStyle(EditorStyles.helpBox);

        a.alignment = TextAnchor.MiddleCenter;
        a.fontStyle = FontStyle.Bold;
        a.fontSize = 15;

        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(10 + 2));
        r.height = 42;
        r.y += 10 / 2;
        r.x -= 2;
        r.xMin = 0;
        r.width += 6;

        if (attachToGridScript.placed)
        {
            EditorGUI.DrawRect(r, new Color(0.2509f, 0.576f, 0.145f));
            EditorGUILayout.LabelField("Placed", a);
        }
        else
        {
            EditorGUI.DrawRect(r, new Color(0.576f, 0.145f, 0.145f));
            EditorGUILayout.LabelField("UnPlaced", a);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
