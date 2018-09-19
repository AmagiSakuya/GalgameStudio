using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GalgameKeyframe))]
public class GalgameKeyframeDrawer : PropertyDrawer
{
    public static GameObject m_baseImg;
    static GameObject m_faceImg;
    static Vector3 _localPostion;
    static Vector3 _localRotation;
    static Vector3 _localScale;
    public static bool isEditMode = false;
    public static string path;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position.height = 16f;
        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);
        if (property.isExpanded)
        {
            DrawProperty(position, property);
        }
        EditorGUI.EndProperty();
    }

    private static void DrawProperty(Rect position, SerializedProperty property)
    {
        SerializedProperty state = property;
        position.y += 20;
        SerializedProperty m_Chara = property.FindPropertyRelative("Character");
        EditorGUI.PropertyField(position, m_Chara);
        if (m_Chara.objectReferenceValue != null)
        {
            GalgameCharacterDefine charaDefine = (GalgameCharacterDefine)m_Chara.objectReferenceValue;
            position.y += 20;
            List<string> showTxt = new List<string>();
            for(int i = 0;i< charaDefine.Emojis.Count; i++)
            {
                showTxt.Add(charaDefine.Emojis[i].EmojiName);
            }
            int index = property.FindPropertyRelative("EmojiSelect").intValue;
            index = EditorGUI.Popup(new Rect(position.x, position.y, 200, 16), index, showTxt.ToArray());
            property.FindPropertyRelative("EmojiSelect").intValue = index;
            Color oldGUIColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (!isEditMode )
            {
                if (GUI.Button(new Rect(position.x + 210, position.y, position.xMax - 250, 16), "直观调整"))
                {
                    isEditMode = true;
                    path = property.propertyPath;
                    GalgameCharacterEmoji emoji = charaDefine.Emojis[index];
                    m_baseImg = GalgameUtil.Instance.CreateGalgameEditorObject("角色", emoji.BaseImg);
                    m_faceImg = GalgameUtil.Instance.CreateGalgameEditorObject("表情", emoji.FaceImg, m_baseImg.transform);
                    GalgameUtil.Instance.SetLocalTransform(m_faceImg, emoji.Position, emoji.Rotation, emoji.Scale);
                    GalgameUtil.Instance.SetLocalTransform(m_baseImg, property);
                }
                GUI.backgroundColor = oldGUIColor;
            }
            else
            {
                GUI.backgroundColor = oldGUIColor;
                if (GUI.Button(new Rect(position.x + 210, position.y, position.xMax - 250, 16), "Exit"))
                {
                    GameObject.DestroyImmediate(m_baseImg);
                    isEditMode = false;
                    state = null;
                }
                GalgameUtil.Instance.SetLocalTransform(m_baseImg, state);
            }
            position.y += 20;
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.xMax - 100, 16), property.FindPropertyRelative("Position"), new GUIContent("位置"));
            if(property.FindPropertyRelative("Kpos").boolValue) GUI.backgroundColor = Color.blue;
            if (GUI.Button(new Rect(position.xMax - 60, position.y, 50, 16), "动至")) property.FindPropertyRelative("Kpos").boolValue = !property.FindPropertyRelative("Kpos").boolValue;
            GUI.backgroundColor = oldGUIColor;
            position.y += 20;
            EditorGUI.PropertyField(new Rect(position.x , position.y, position.xMax - 100, 16), property.FindPropertyRelative("Rotation"), new GUIContent("旋转"));
            if (property.FindPropertyRelative("Kro").boolValue) GUI.backgroundColor = Color.blue;
            if (GUI.Button(new Rect(position.xMax - 60, position.y, 50, 16), "动至")) property.FindPropertyRelative("Kro").boolValue = !property.FindPropertyRelative("Kro").boolValue;
            GUI.backgroundColor = oldGUIColor;
            position.y += 20;
            EditorGUI.PropertyField(new Rect(position.x , position.y, position.xMax - 100, 16), property.FindPropertyRelative("Scale"), new GUIContent("缩放"));
            if (property.FindPropertyRelative("Kscale").boolValue) GUI.backgroundColor = Color.blue;
            if (GUI.Button(new Rect(position.xMax - 60, position.y, 50, 16), "动至")) property.FindPropertyRelative("Kscale").boolValue = !property.FindPropertyRelative("Kscale").boolValue;
            GUI.backgroundColor = oldGUIColor;
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Delay"),new GUIContent("延时"));
            position.y += 20;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return GetPropertyHeight(property);
    }

    public static float GetPropertyHeight(SerializedProperty property)
    {
        var h = 20;
        if (property.isExpanded)
        {
            h += 120;
        }
        return h;
    }
}
