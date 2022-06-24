using AdventureGame;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AdventureGameEditor
{
    [CustomEditor(typeof(AdventureGameActorPlayableAssets))]
    public class AdventureGameActorPlayableAssetsEditor : Editor
    {
        List<string> bodyList;
        List<string> faceList;
        List<string> appendList;
        int bodySelected = 0;
        int faceSelected = 0;
        int append1Selected = 0;
        int append2Selected = 0;
        int append3Selected = 0;

        AdventureGameActorPlayableAssets t;
        AdventureGameActorTransformPresets old_transformPresets;
        private void OnEnable()
        {
            t = (AdventureGameActorPlayableAssets)target;
            RefrashBodyList();
            bodySelected = bodyList.IndexOf(t.settings.bodyName);
            bodySelected = bodySelected < 0 ? 0 : bodySelected;
            RefrashFaceList();
            faceSelected = faceList.IndexOf(t.settings.faceName);
            faceSelected = faceSelected < 0 ? 0 : faceSelected;
            RefrashAppendList();
            append1Selected = appendList.IndexOf(t.settings.append1);
            append2Selected = appendList.IndexOf(t.settings.append2);
            append3Selected = appendList.IndexOf(t.settings.append3);
            append1Selected = append1Selected < 0 ? 0 : append1Selected;
            append2Selected = append2Selected < 0 ? 0 : append2Selected;
            append3Selected = append3Selected < 0 ? 0 : append3Selected;
            old_transformPresets = t.settings.transformPresets;
        }

        public override void OnInspectorGUI()
        {
            if (t.settings.fgImageDefine)
            {
                RefrashBodyList();
                bodySelected = EditorGUILayout.Popup("Body", bodySelected, bodyList.ToArray());
                RefrashFaceList();
                faceSelected = EditorGUILayout.Popup("Face", faceSelected, faceList.ToArray());
                RefrashAppendList();
                append1Selected = EditorGUILayout.Popup("Append1", append1Selected, appendList.ToArray());
                append2Selected = EditorGUILayout.Popup("Append2", append2Selected, appendList.ToArray());
                append3Selected = EditorGUILayout.Popup("Append3", append3Selected, appendList.ToArray());

            }
            else
            {
                bodySelected = 0;
                faceSelected = 0;
                append1Selected = 0;
                append2Selected = 0;
                append3Selected = 0;
            }

            if (t.settings.fgImageDefine)
            {
                t.settings.bodyName = bodySelected < bodyList.Count ? bodyList[bodySelected] : "None";
                t.settings.faceName = faceSelected < faceList.Count ? faceList[faceSelected] : "None";
                t.settings.append1 = append1Selected < appendList.Count ? appendList[append1Selected] : "None";
                t.settings.append2 = append2Selected < appendList.Count ? appendList[append2Selected] : "None";
                t.settings.append3 = append3Selected < appendList.Count ? appendList[append3Selected] : "None";
            }

            base.OnInspectorGUI();

            if(old_transformPresets != t.settings.transformPresets)
            {
                t.settings.scaleAnime.from = t.settings.transformPresets != null ? Vector2.zero : Vector2.one;
                old_transformPresets = t.settings.transformPresets;
            }
        }

        void RefrashBodyList()
        {
            bodyList = new List<string>();
            if (t.settings.fgImageDefine == null) return;
            bodyList.Add("None");
            for (int i = 0; i < t.settings.fgImageDefine.bodyGroup.Length; i++)
            {
                var bodyGroup = t.settings.fgImageDefine.bodyGroup[i];
                for (int j = 0; j < bodyGroup.bodys.Length; j++)
                {
                    bodyList.Add(bodyGroup.bodys[j].imageName);
                }
            }
        }

        void RefrashFaceList()
        {
            faceList = new List<string>();
            if (t.settings.fgImageDefine == null) return;
            faceList.Add("None");
            for (int i = 0; i < t.settings.fgImageDefine.faceGroup.Length; i++)
            {
                var faceGroup = t.settings.fgImageDefine.faceGroup[i];
                if (GetBodyGroupByBodyName(bodyList[bodySelected]).bodyGroupName == faceGroup.relativeBodyGroup)
                {
                    for (int j = 0; j < faceGroup.faces.Length; j++)
                    {
                        faceList.Add(faceGroup.faces[j].imageName);
                    }
                    return;
                }
            }
        }

      
        AdventureGameFgImageBodyGroup GetBodyGroupByBodyName(string bodyName)
        {
            for (int i = 0; i < t.settings.fgImageDefine.bodyGroup.Length; i++)
            {
                var bodyGroup = t.settings.fgImageDefine.bodyGroup[i];
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

        void RefrashAppendList()
        {
            appendList = new List<string>();
            if (t.settings.fgImageDefine == null) return;
            appendList.Add("None");
            for (int i = 0; i < t.settings.fgImageDefine.appendFace.Length; i++)
            {
                var appendface = t.settings.fgImageDefine.appendFace[i];
                appendList.Add(appendface.imageName);
            }
        }
    }
}