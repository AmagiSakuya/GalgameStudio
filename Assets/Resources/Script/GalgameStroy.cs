using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalgameStroy : MonoBehaviour {
    public GameManger GameManger;
    public bool isAuto = false;
    private int ActionIndex = 0;
    private List<GalgameAction> actions;

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
        //如果正在说话中
        if (GameManger.isActive)
        {
            GameManger.FinishTyperEffect();
            GameManger.EndPerform();
            return;
        }
        //变化台词
        GameManger.ChangeUIContent(actions[ActionIndex].Serifu);
        //变化人物名
        GameManger.ChangeUIName(actions[ActionIndex].Character.ToString());
        //执行演出
        GameManger.Perform(actions[ActionIndex].Keyframe);
        //播放音频
        GameManger.PlayVoice(actions[ActionIndex].Voice,()=>{
            if (isAuto)
            {
                StartAction();
            }
        });
        ActionIndex++;
    }

    void Update () {
        //注册mousedown事件
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
