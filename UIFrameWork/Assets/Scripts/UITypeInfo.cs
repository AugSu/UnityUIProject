using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存储面板的路径/类型的信息
/// </summary>
[System.Serializable]
public class UITypeInfo :ISerializationCallbackReceiver
{
    /// <summary>
    /// panel面板的类型
    /// </summary>
    [System.NonSerialized]
    public UIPanelType uiType;
    /// <summary>
    /// 对应Panel面板的路径;
    /// </summary>
    public string uiPanelPath;
    public string uiTypeString;

    public void OnAfterDeserialize()
    {
        UIPanelType type =(UIPanelType)System.Enum.Parse(typeof(UIPanelType), uiTypeString);
        uiType = type;
    }

    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }
}
