using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorImage : MonoBehaviour
{
    public Canvas m_canvasRoot;
    public RectTransform m_rtParent;
    public RectTransform m_rtTarget;

    void Update()
    {
        Vector2 MousePos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            m_rtParent,
            Input.mousePosition ,
            m_canvasRoot.worldCamera,
            out MousePos);

        m_rtTarget.anchoredPosition = new Vector2(
            MousePos.x,
            MousePos.y);

    }
}
