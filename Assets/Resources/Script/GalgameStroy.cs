using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalgameStroy : MonoBehaviour {
    public GameManger GameManger;
    public bool isAuto = false;

    private int ActionIndex = 0;
    private List<GalgameAction> actions;
    // Use this for initialization
    void Start () {
        GalgamePart [] parts = this.GetComponentsInChildren<GalgamePart>(true);
        StartPart(parts[0]);
    }

    //从头开始一个Part
    void StartPart(GalgamePart part)
    {
        ActionIndex = 0;
        GalgamePartDefine define = part.GalgamePartDefine;
        GameManger.PlayBgm(define.StartBgm);
        GameManger.ChangeBg(define.BaseBg);
        actions = define.Actions;
        StartAction();
    }

    //执行一个Action
    void StartAction()
    {
        if (ActionIndex == actions.Count)
        {
            Debug.Log("part 结束");
            return;
        }

        if (GameManger.isActive)
        {
            GameManger.FinishTyperEffect();
            GameManger.EndPerform();
            return;
        }
        else
        {
            GameManger.ChangeUIContent(actions[ActionIndex].Serifu);
            GameManger.Perform(actions[ActionIndex].Keyframe);
        }

        GameManger.ChangeUIName(actions[ActionIndex].Character.ToString());
        GameManger.PlayVoice(actions[ActionIndex].Voice,()=>{
            if (isAuto)
            {
                StartAction();
            }
        });
       // GameManger.Perform(actions[ActionIndex].Keyframe);
        ActionIndex++;
    }

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (isAuto)
            {
                GameManger.ClearVoiceCallback();
            }
            StartAction();
        }
    }
}
