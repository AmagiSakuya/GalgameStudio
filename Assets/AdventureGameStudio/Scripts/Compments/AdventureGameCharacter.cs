using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    public class AdventureGameCharacter : AdventureGameActor
    {
        [HideInInspector] public AdventureGameFgImageDefine m_fgimage;

        protected override string GetShaderName()
        {
            return "Hidden/ADVGame/CharacterImage";
        }

        #region TimeLine播放处理
        public override void OnBehaviorStart(AdventureGameActorPlayableBehavior behavior)
        {
            if (behavior.fgImageDefine == null)
            {
                base.OnBehaviorStart(behavior);
                return;
            }
            m_fgimage = behavior.fgImageDefine;

            target.GetComponent<RectTransform>().sizeDelta = m_fgimage.boundingBoxSize;
            SetTexture("_BodyTex", GetBody(behavior.bodyName));
            SetTexture("_FaceTex", GetFace(behavior.bodyName, behavior.faceName));
            SetTexture("_AppendTex1", GetAppendByBodyName(behavior.append1));
            
            //要修改
            for (int i = 0; i < m_fgimage.appendFace.Length; i++)
            {
                if (m_fgimage.appendFace[i].show)
                    SetTexture("_AppendTex" + (i + 1), m_fgimage.appendFace[i]);
            }

            //target.material.SetTexture("_BodyTex", body.image.texture);
            //target.material.SetTextureOffset("_BodyTex",)

            //// 设置image
            //if (behavior.image)
            //{
            //    target.sprite = behavior.image;
            //}

            SetRule(behavior.ruleImage);
        }

        void SetTexture(string propName, AdventureGameFgImage imageSettings)
        {
            if (imageSettings.image == null) return;
            target.material.SetTexture(propName, imageSettings.image.texture);
            target.material.SetTextureScale(propName, new Vector2(imageSettings.image.texture.width / m_fgimage.boundingBoxSize.x, imageSettings.image.texture.height / m_fgimage.boundingBoxSize.y));
            target.material.SetTextureOffset(propName, new Vector2(imageSettings.offset.x / m_fgimage.boundingBoxSize.x, 1 - ((imageSettings.offset.y + imageSettings.image.texture.height) / m_fgimage.boundingBoxSize.y)));
        }
        #endregion


        #region 获取Define设置

        AdventureGameFgImage GetBody(string bodyName)
        {
            for (int i = 0; i < m_fgimage.bodyGroup.Length; i++)
            {
                var bodyGroup = m_fgimage.bodyGroup[i];
                for (int j = 0; j < bodyGroup.bodys.Length; j++)
                {
                    if (bodyGroup.bodys[j].imageName == bodyName)
                    {
                        return bodyGroup.bodys[j];
                    }
                }
            }
            return new AdventureGameFgImage();
        }

        AdventureGameFgImage GetFace(string bodyName, string faceName)
        {
            for (int i = 0; i < m_fgimage.faceGroup.Length; i++)
            {
                var faceGroup = m_fgimage.faceGroup[i];
                if (GetBodyGroupByBodyName(bodyName).bodyGroupName == faceGroup.relativeBodyGroup)
                {
                    for (int j = 0; j < faceGroup.faces.Length; j++)
                    {
                        if (faceName == faceGroup.faces[j].imageName)
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
            for (int i = 0; i < m_fgimage.bodyGroup.Length; i++)
            {
                var bodyGroup = m_fgimage.bodyGroup[i];
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

        AdventureGameFgImage GetAppendByBodyName(string append)
        {
            for (int i = 0; i < m_fgimage.appendFace.Length; i++)
            {
                if (m_fgimage.appendFace[i].imageName == append)
                {
                    return m_fgimage.appendFace[i];
                }
            }
            return new AdventureGameFgImage();
        }
        #endregion

    }
}