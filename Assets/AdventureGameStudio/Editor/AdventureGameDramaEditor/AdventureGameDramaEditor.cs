using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using AdventureGame;
using System.Collections.Generic;
using System.Linq;

public class AdventureGameDramaEditor : EditorWindow
{
    AdventureGameDrama m_advDrama;

    ListView compistionList;
    VisualTreeAsset compistionItemPrefab;

    string m_basePath = "Assets/AdventureGameStudio/Editor/AdventureGameDramaEditor";

    [MenuItem("ADV Studio/剧本编辑器")]
    public static void ShowWindow()
    {
        AdventureGameDramaEditor wnd = GetWindow<AdventureGameDramaEditor>();
        wnd.titleContent = new GUIContent("AdventureGameDramaEditor");
    }

    public void CreateGUI()
    {
        LoadHTML();
        InitEvent();
        Refrash();
    }

    private void Update()
    {
        compistionList.RefreshItems();
    }

    void LoadHTML()
    {
        VisualElement root = rootVisualElement;
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(m_basePath + "/AdventureGameDramaEditor.uxml");
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(m_basePath + "/AdventureGameDramaEditor.uss");

        compistionItemPrefab = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(m_basePath + "/CustomElement/CompistionItem.uxml");
        root.styleSheets.Add(styleSheet);
        visualTree.CloneTree(root);
        compistionList = rootVisualElement.Q<ListView>("CompsitionList");

    }

    void InitEvent()
    {
        //ADVDramaSelector
        ObjectField selector = rootVisualElement.Q<ObjectField>("ADVDramaSelector");
        selector.objectType = typeof(AdventureGameDrama);
        selector.RegisterValueChangedCallback((ChangeEvent<Object> evt) =>
        {
            AdventureGameDrama m_newValue = (AdventureGameDrama)evt.newValue;
            m_advDrama = m_newValue;
            Refrash();
        });

        compistionList.makeItem += () => compistionItemPrefab.Instantiate();

        compistionList.bindItem += (VisualElement ve, int index) =>
        {
            ve.Q<Label>("Index").text = (index + 1).ToString();
            ve.Q<Label>("CharacterName").text = m_advDrama.compositions[index].characterName;
            ve.Q<Label>("Cotent").text = m_advDrama.compositions[index].content;
        };

        compistionList.onSelectionChange += (objects) =>
        {
            var item = objects.FirstOrDefault();
            m_advDrama.m_selectedCompistion = (ADV_Drama_Composition)item;
        };

        //Add按钮
        Button m_addBtn = rootVisualElement.Q<Button>("addBtn");
        m_addBtn.clicked += () =>
        {
            if (m_advDrama == null) return;
            var compistion = new ADV_Drama_Composition();
            compistion.content = "New Compistion";
            m_advDrama.compositions.Add(compistion);
            Refrash();
        };

        //Delete按钮
        Button m_deleteBtn = rootVisualElement.Q<Button>("deleteBtn");
        m_deleteBtn.clicked += () =>
        {
            if (m_advDrama == null || !EditorUtility.DisplayDialog("提示", "是否确定删除？", "确定", "取消")) return;
            List<ADV_Drama_Composition> res = new List<ADV_Drama_Composition>();
            foreach (int number in compistionList.selectedIndices)
            {
                res.Add(m_advDrama.compositions[number]);
            }
            for (int i = 0; i < res.Count; i++)
            {
                m_advDrama.compositions.Remove(res[i]);
            }
            Refrash();
        };

        //Refrash按钮
        Button m_refrash = rootVisualElement.Q<Button>("Refrash");
        m_refrash.clicked += () =>
        {
            Refrash();
        };
    }

    void Refrash()
    {
        if (m_advDrama == null)
        {
            compistionList.itemsSource = new List<ADV_Drama_Composition>();
            rootVisualElement.Unbind();
        }
        else
        {
            compistionList.itemsSource = m_advDrama.compositions;
            rootVisualElement.Bind(new SerializedObject(m_advDrama));
        };
        compistionList.RefreshItems();
    }
}