using AdventureGame;
using UnityEditor;
using UnityEngine;

namespace AdventureGameEditor
{
    public class AdventureGameDramaEditWindow : EditorWindow
    {
        public AdventureGameDrama m_drama;

        static SerializedObject serializedObject;
        [MenuItem("ADV Studio/�籾�༭��")]
        public static void ShowWindow()
        {
            EditorWindow window = EditorWindow.GetWindow(typeof(AdventureGameDramaEditWindow), false, "�籾�༭��", true);
            window.minSize = new Vector2(1280, 720);
            window.Show();
            serializedObject = new SerializedObject(window);
        }

        void OnGUI()
        {
            serializedObject.Update();
            //GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_drama"), new GUIContent("�籾"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}