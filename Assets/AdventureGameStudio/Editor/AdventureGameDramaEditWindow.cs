using AdventureGame;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AdventureGameEditor
{
    public class AdventureGameDramaEditWindow : EditorWindow
    {
        [SerializeField] private AdventureGameDrama m_drama;

        static SerializedObject serializedObject;
        static VisualElement m_visualElement;
        public AdventureGameDrama drama
        {
            get { return m_drama; }
            set
            {
                Debug.Log(value);
                LoadDrama(value);
            }
        }

        List<ADV_Drama_Composition> m_compsition = new List<ADV_Drama_Composition>();

        private void OnEnable()
        {
            string path = "Assets/AdventureGameStudio/Editor/AdventureGameDramaEditWindowUXML.uxml";
            var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            asset.CloneTree(this.rootVisualElement);
            m_visualElement = this.rootVisualElement;
            //rootVisualElement.Query<IMGUIContainer>("DramaLoaderArea").First().onGUIHandler = IMGUIExecute;
            rootVisualElement.Query<Button>("AddBtn").First().clicked += () =>
            {
                AddCompsition();
            };
        }

        [MenuItem("ADV Studio/¾ç±¾±à¼­Æ÷")]
        public static void ShowWindow()
        {
            EditorWindow window = EditorWindow.GetWindow(typeof(AdventureGameDramaEditWindow), false, "¾ç±¾±à¼­Æ÷", true);
            window.minSize = new Vector2(1280, 720);
            window.Show();
            serializedObject = new SerializedObject(window);
            m_visualElement.Bind(serializedObject);
        }

        // is called by Unity when ever a value in the inspector is changed
        void OnValidate()
        {
            drama = m_drama;
        }

        void LoadDrama(AdventureGameDrama advDrama)
        {
            if (advDrama == null)
            {
                m_compsition.Clear();
            }
            else
            {
                m_compsition = advDrama.drama;
            }
        }

        void AddCompsition()
        {
            // rootVisualElement.Query<Button>("AddBtn").
            ADV_DramaCompsition compsition = new ADV_DramaCompsition();
            compsition.characterName = "";
            compsition.content = "";
            rootVisualElement.Q<ScrollView>("TextScrollArea").Add(compsition);
        }
        //void OnGUI()
        //{
        //    serializedObject.Update();
        //    //GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        //    EditorGUILayout.PropertyField(serializedObject.FindProperty("m_drama"), new GUIContent("¾ç±¾"));
        //    serializedObject.ApplyModifiedProperties();
        //}

        //void IMGUIExecute()
        //{
        //    serializedObject.Update();
        //    //EditorGUILayout.PropertyField(serializedObject.FindProperty("m_drama"), new GUIContent("¼ÓÔØ¾ç±¾"));
        //    serializedObject.ApplyModifiedProperties();
        //}
    }
}