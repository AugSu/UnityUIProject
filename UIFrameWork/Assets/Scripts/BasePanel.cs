using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel:MonoBehaviour
{
    /// <summary>
    /// 进入面板需要处理的事件;
    /// </summary>
    public virtual void OnPause(){}
    public virtual void OnExit() {}
    public virtual void OnResume() {}
    public virtual void OnEnter() {}
}
