using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;

namespace AdventureGameEditor
{
    public class ADV_DramaCompsition : VisualElement
    {
        string m_characterName;
        string m_content;
        public string characterName { get { return m_characterName; } set { m_characterName = value; m_charaterNameField.SetValueWithoutNotify(value); } }
        public string content { get { return m_content; } set { m_content = value; m_contentField.SetValueWithoutNotify(value); } }

        TextField m_charaterNameField;
        TextField m_contentField;

        public new class UxmlFactory : UxmlFactory<ADV_DramaCompsition, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription m_CharacterName =
                new UxmlStringAttributeDescription { name = "CharacterName", defaultValue = "" };
            UxmlStringAttributeDescription m_Content =
                new UxmlStringAttributeDescription { name = "Content", defaultValue = "" };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ADV_DramaCompsition ate = ve as ADV_DramaCompsition;
                ate.Clear();
                ate.AddTemplet();

                ate.characterName = m_CharacterName.GetValueFromBag(bag, cc);
                ate.content = m_Content.GetValueFromBag(bag, cc);
            }
        }


        public ADV_DramaCompsition()
        {
            AddTemplet();
        }

        void AddTemplet()
        {
            VisualTreeAsset template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/AdventureGameStudio/Editor/ADV_DramaCompsitionUXML.uxml");
            template.CloneTree(this);
            m_charaterNameField = this.Q<TextField>("CharacterName");
            m_contentField = this.Q<TextField>("Content");
        }
    }
}

