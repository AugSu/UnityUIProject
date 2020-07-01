using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }

    private Transform canvas;
    public Transform Canvas
    {
        get
        {
            if (canvas==null)
            {
                canvas = GameObject.Find("Canvas").transform;
            }
            return canvas;
        }
    }

    /// <summary>
    /// json解析的数组对象解析在这里面;
    /// 解析对象以类存储需要先序列化,将格式转化为数组支持的类型
    /// </summary>
    [Serializable]
    class UiPanelJson
    {
        public List<UITypeInfo> infoList;
    }
    /// <summary>
    /// 存储不同面板的路径信息;
    /// </summary>
    public Dictionary<UIPanelType, string> panelPathDic;
    /// <summary>
    /// 存储当前页面存在的面板
    /// </summary>
    public Dictionary<UIPanelType, BasePanel> curPanelDict;
    /// <summary>
    /// 存储页面上当前的面板
    /// </summary>
    public Stack<BasePanel> panelStack;
    /// <summary>
    /// 面板存入栈中;
    /// </summary>
    public void PushUIPanel (UIPanelType uIPanelType)
    {
        BasePanel  topPanel;
        if (panelStack==null)
        {
            panelStack = new Stack<BasePanel>();
        }
        //判断栈内是否有元素,有元素取出栈顶,禁用
        if (panelStack.Count!=0)
        {
            topPanel = panelStack.Peek();
            topPanel.OnPause();
        }
        BasePanel panel = GetBasePanel(uIPanelType);
        panel.OnEnter();
        panelStack.Push(panel);
    }
    /// <summary>
    /// 栈顶面板出栈
    /// 出站后再获取栈顶面板设置为可见
    /// </summary>
    public void PopPanel()
    {
        if (panelStack.Count!=0)
        {
            BasePanel topPanel = panelStack.Pop();
            topPanel.OnExit();
        }
        //找到当前面板,继续事件
        BasePanel currentPanel = panelStack.Peek();
        Debug.Log(currentPanel.name);
        currentPanel.OnResume();
    }

    /// <summary>
    /// 一旦UImanger被实例化就解析json
    /// </summary>
    public UIManager()
    {
        ParseJson();
    }

    /// <summary>
    /// json文件解析
    /// </summary>
    public void ParseJson()
    {
        panelPathDic = new Dictionary<UIPanelType, string>();
        //加载json文本信息
        TextAsset ta = Resources.Load<TextAsset>("UITypeJson");
        UiPanelJson uijson = JsonUtility.FromJson<UiPanelJson>(ta.text);
        foreach (UITypeInfo item in uijson.infoList)
        {
            //遍历字典存储每一个对应面板的路径信息;
            panelPathDic.Add(item.uiType, item.uiPanelPath);
        }
    }

    /// <summary>
    /// 获取字典中存储的面板;
    /// </summary>
    /// <param name="uIPanelType"></param>
    /// <returns></returns>
    public BasePanel GetBasePanel(UIPanelType uIPanelType)
    {


        //存储字典的面板为空
        if (curPanelDict==null)
        {
            curPanelDict = new Dictionary<UIPanelType, BasePanel>();
        }
        BasePanel panel;
        curPanelDict.TryGetValue(uIPanelType, out panel);
        //场景中没有该面板,在资源中实例化面板
        if (panel==null)
        {
            //找到面板,然后将面板设置在Canvas下面;
            //先加载资源,然后实例化.加载预制体资源需要实例化;
            GameObject go = GameObject.Instantiate(Resources.Load(panelPathDic[uIPanelType])) as GameObject;
            go.transform.SetParent(Canvas, false);
            curPanelDict.Add(uIPanelType, go.GetComponent<BasePanel>());
            return go.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }
    }

}
