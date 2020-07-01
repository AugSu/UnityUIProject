using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel:BasePanel
{
    private CanvasGroup canvasGroup;
    private UIPanelType uiType;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    /// <summary>
    /// 点击按钮进入面板;
    /// 将页面屏蔽
    /// </summary>
    /// <param name="uiPanelTypeString"></param>
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        Debug.Log("执行");
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 按钮的监听事件
    /// </summary>
    /// <param name="uiPanelTypeString"></param>
    public void ButtonClick(string uiPanelTypeString)
    {
        uiType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), uiPanelTypeString);
        UIManager.Instance.PushUIPanel(uiType);
    }
}
