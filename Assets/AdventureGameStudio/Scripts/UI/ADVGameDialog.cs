using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    public class ADVGameDialog : MonoBehaviour
    {

        public Image dialogBackgroundImage;
        public TMP_Text characterName;
        public TMP_Text cotent;

        public void SetDefine(GameDialogStyleDefine value, TMP_FontAsset font = null)
        {
            //设置背景图片
            dialogBackgroundImage.sprite = value.backgroundImage;
            //设置文本颜色
            SetADVTextSettings(characterName, value.charaNameTextSettings, font);
            SetADVTextSettings(cotent, value.contentTextSettings, font);
        }

        public void SetADVTextSettings(TMP_Text m_text, ADVDramaTextSettings settings, TMP_FontAsset font = null)
        {
            m_text.color = settings.color;
            m_text.fontSize = settings.fontSize;    
            if (font != null)
            {
                m_text.font = font;
            }
            m_text.fontSharedMaterial.SetColor("_OutlineColor", settings.outLineColor);
            m_text.fontSharedMaterial.SetFloat("_OutlineWidth", settings.outLineWidth);
            m_text.fontSharedMaterial.SetFloat("_FaceDilate", settings.outLineWidth);
        }
    }
}