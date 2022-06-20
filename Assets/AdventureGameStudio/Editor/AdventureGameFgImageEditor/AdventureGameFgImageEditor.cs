using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using AdventureGame;
using System.Linq;
using System.Collections.Generic;

namespace AdventureGameEditor
{
    public class AdventureGameFgImageEditor : EditorWindow
    {
        string m_basePath = "Assets/AdventureGameStudio/Editor/AdventureGameFgImageEditor/";
        AdventureGameFgImageDefine m_advFgImageDefine;

        DropdownField bodyDropDown;
        DropdownField faceDropDown;
        VisualElement fgImageContainer;
        VisualElement bodyImage;
        VisualElement faceImage;

        [MenuItem("ADV Studio/立绘编辑器")]
        public static void ShowWindow()
        {
            AdventureGameFgImageEditor wnd = GetWindow<AdventureGameFgImageEditor>();
            wnd.titleContent = new GUIContent("AdventureGameFgImageEditor");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(m_basePath + "/AdventureGameFgImageEditor.uxml");
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(m_basePath + "/AdventureGameFgImageEditor.uss");


            root.styleSheets.Add(styleSheet);
            visualTree.CloneTree(root);

            bodyDropDown = rootVisualElement.Q<DropdownField>("BodyDropDown");
            faceDropDown = rootVisualElement.Q<DropdownField>("FaceDropDown");
            fgImageContainer = rootVisualElement.Q<VisualElement>("FgImageContainer");
            bodyImage = rootVisualElement.Q<VisualElement>("BodyImage");
            faceImage = rootVisualElement.Q<VisualElement>("FaceImage");

            InitEvent();


        }


        private void Update()
        {
            RefreshDropDown();
            CheckValue(bodyDropDown);
            CheckValue(faceDropDown);
            DrawFgImage();
        }

        void InitEvent()
        {
            //ADVDramaSelector
            ObjectField selector = rootVisualElement.Q<ObjectField>("ADVFgImageSelector");
            selector.objectType = typeof(AdventureGameFgImageDefine);
            selector.RegisterValueChangedCallback((ChangeEvent<Object> evt) =>
            {
                AdventureGameFgImageDefine m_newValue = (AdventureGameFgImageDefine)evt.newValue;
                m_advFgImageDefine = m_newValue;
                Refrash();
            });

            bodyDropDown.RegisterValueChangedCallback((ChangeEvent<string> evt) =>
            {
                List<string> faceList = new List<string>();
                if (m_advFgImageDefine == null)
                {
                    faceDropDown.choices = faceList;
                    return;
                }


            });

            //Refrash按钮
            Button m_refrash = rootVisualElement.Q<Button>("Refrash");
            m_refrash.clicked += () =>
            {
                Refrash();
            };

        }

        void Refrash()
        {
            if (m_advFgImageDefine == null)
            {
                rootVisualElement.Unbind();
                DrawImage(new AdventureGameFgImage(), bodyImage);
                DrawImage(new AdventureGameFgImage(), faceImage);
                rootVisualElement.Q<Vector2Field>("CanvasSize").value = Vector2.zero;
                fgImageContainer.style.width = 0;
                fgImageContainer.style.height = 0;
            }
            else
            {
                rootVisualElement.Bind(new SerializedObject(m_advFgImageDefine));
            };
        }

        #region Update
        /// <summary>
        /// 刷新选项
        /// </summary>
        List<string> bodyList;
        List<string> faceList;
        void RefreshDropDown()
        {
            if (m_advFgImageDefine == null)
            {
                bodyDropDown.choices = new List<string>();
                faceDropDown.choices = new List<string>();
                return;
            }

            bodyList = new List<string>();
            for (int i = 0; i < m_advFgImageDefine.bodyGroup.Length; i++)
            {
                var bodyGroup = m_advFgImageDefine.bodyGroup[i];
                for (int j = 0; j < bodyGroup.bodys.Length; j++)
                {
                    bodyList.Add(bodyGroup.bodys[j].imageName);
                }
            }

            bodyDropDown.choices = bodyList;

            faceList = new List<string>();

            for (int i = 0; i < m_advFgImageDefine.faceGroup.Length; i++)
            {
                var faceGroup = m_advFgImageDefine.faceGroup[i];
                if (GetBodyGroupByBodyName(bodyDropDown.value).bodyGroupName == faceGroup.relativeBodyGroup)
                {
                    for (int j = 0; j < faceGroup.faces.Length; j++)
                    {
                        faceList.Add(faceGroup.faces[j].imageName);
                    }
                    faceDropDown.choices = faceList;
                    return;
                }
            }
            faceDropDown.choices = faceList;
        }

        /// <summary>
        /// 检查当前选项
        /// </summary>
        bool flag;
        void CheckValue(DropdownField dropDown)
        {
            flag = false;
            for (int i = 0; i < dropDown.choices.Count; i++)
            {
                if (dropDown.choices[i] == dropDown.value)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                if (dropDown.choices.Count == 0)
                {
                    dropDown.value = "";
                }
                else
                {
                    dropDown.value = dropDown.choices[0];
                }
            }
        }

        /// <summary>
        /// 绘制立绘预览
        /// </summary>
        List<VisualElement> appendFace = new List<VisualElement>();
        void DrawFgImage()
        {
            if (m_advFgImageDefine == null)
            {
                return;
            }
            //绘制画布
            fgImageContainer.style.width = m_advFgImageDefine.boundingBoxSize.x;
            fgImageContainer.style.height = m_advFgImageDefine.boundingBoxSize.y;
            //绘制身体
            DrawImage(GetBody(), bodyImage);
            //绘制face
            DrawImage(GetFace(), faceImage);
            //绘制AppendFace
            DrawAppendFaces();
        }

        void DrawAppendFaces()
        {
            if (appendFace.Count != m_advFgImageDefine.appendFace.Length)
            {
                //刷新face数量
                appendFace = new List<VisualElement>();
                var m_container = rootVisualElement.Q<VisualElement>("FaceAppend");
                m_container.Clear();
                for (int i = 0; i < m_advFgImageDefine.appendFace.Length; i++)
                {
                    var element = new VisualElement();
                    element.style.position = Position.Absolute;
                    m_container.Add(element);
                    appendFace.Add(element);
                }
            }
            //刷新AppendFace图片和位置
            for (int i = 0; i < m_advFgImageDefine.appendFace.Length; i++)
            {
                if (m_advFgImageDefine.appendFace[i].show)
                {
                    DrawImage(m_advFgImageDefine.appendFace[i], appendFace[i]);
                }
                else
                {
                    appendFace[i].style.width = 0;
                    appendFace[i].style.height = 0;
                }
            }
        }

        void DrawImage(AdventureGameFgImage settings, VisualElement image)
        {
            image.style.top = settings.offset.y;
            image.style.left = settings.offset.x;
            if (settings.image)
            {
                image.style.width = settings.image.rect.width;
                image.style.height = settings.image.rect.height;
                image.style.backgroundImage = new StyleBackground(settings.image);
            }
            else
            {
                image.style.width = 0;
                image.style.height = 0;
            }
        }
        AdventureGameFgImage GetBody()
        {
            if (m_advFgImageDefine == null)
            {
                return new AdventureGameFgImage();
            }
            for (int i = 0; i < m_advFgImageDefine.bodyGroup.Length; i++)
            {
                var bodyGroup = m_advFgImageDefine.bodyGroup[i];
                for (int j = 0; j < bodyGroup.bodys.Length; j++)
                {
                    if (bodyGroup.bodys[j].imageName == bodyDropDown.value)
                    {
                        return bodyGroup.bodys[j];
                    }
                }
            }
            return new AdventureGameFgImage();
        }

        AdventureGameFgImage GetFace()
        {
            if (m_advFgImageDefine == null)
            {
                return new AdventureGameFgImage();
            }
            for (int i = 0; i < m_advFgImageDefine.faceGroup.Length; i++)
            {
                var faceGroup = m_advFgImageDefine.faceGroup[i];
                if (GetBodyGroupByBodyName(bodyDropDown.value).bodyGroupName == faceGroup.relativeBodyGroup)
                {
                    for (int j = 0; j < faceGroup.faces.Length; j++)
                    {
                        if (faceDropDown.value == faceGroup.faces[j].imageName)
                        {
                            return faceGroup.faces[j];
                        }
                    }
                }
            }
            return new AdventureGameFgImage();
        }

        AdventureGameFgImageBodyGroup GetBodyGroupByBodyName(string bodyName)
        {
            if (m_advFgImageDefine == null)
            {
                return new AdventureGameFgImageBodyGroup();
            }
            for (int i = 0; i < m_advFgImageDefine.bodyGroup.Length; i++)
            {
                var bodyGroup = m_advFgImageDefine.bodyGroup[i];
                for (int j = 0; j < bodyGroup.bodys.Length; j++)
                {
                    if (bodyGroup.bodys[j].imageName == bodyName)
                    {
                        return bodyGroup;
                    }
                }
            }
            return new AdventureGameFgImageBodyGroup();
        }
        #endregion

    }
}
