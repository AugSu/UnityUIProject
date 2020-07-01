using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    /// <summary>
    /// 关闭按钮
    /// 将当前面板出栈
    /// 
    /// </summary>
    public void OnButtonClick()
    {
        UIManager.Instance.PopPanel();
    }

    /// <summary>
    /// 当前面板的退出事件
    /// </summary>
    public override void OnExit()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    public override void OnEnter()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
