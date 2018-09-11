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
        EditorGUI.PropertyField(position, property.FindPropertyRelative("Character"));
        SerializedProperty m_Chara = property.FindPropertyRelative("Character");
        if (m_Chara.objectReferenceValue != null)
        {
            GalgameCharacterDefine m_obj = (GalgameCharacterDefine)m_Chara.objectReferenceValue;
            position.y += 20;
            List<string> showTxt = new List<string>();
            for(int i = 0;i< m_obj.Emojis.Count; i++)
            {
                showTxt.Add(m_obj.Emojis[i].EmojiName);
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
                    GalgameCharacterDefine charaDefine = (GalgameCharacterDefine)property.FindPropertyRelative("Character").objectReferenceValue;
                    if (charaDefine == null) return;
                    GalgameCharacterEmoji emoji = charaDefine.Emojis[index];
                    m_baseImg = GalgameUtil.Instance.CreateGalgameEditorObject("角色", emoji.BaseImg);
                    m_faceImg = GalgameUtil.Instance.CreateGalgameEditorObject("表情", emoji.FaceImg, m_baseImg.transform);
                    GalgameUtil.Instance.SetLocalTransform(m_faceImg, emoji.Position, emoji.Rotation, emoji.Scale);
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
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Position"));
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Rotation"));
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Scale"));
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("TimeAxis"));
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
