using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public UnityEvent actions;
    bool focused;
    Shadow shadow;
    new RectTransform transform;
    MaskableGraphic background;
    Color color;
    public static Color MainColor;
    [Range(0, 1)]
    [SerializeField] float colorDivider;
    [SerializeField] Color[] colors;
    private void Awake()
    {
        transform = GetComponent<RectTransform>();
        foreach (var s in GetComponents<Shadow>())
            if (!(s is Outline))
                shadow = s;
        background = GetComponent<MaskableGraphic>();
        color = background.color;
        MainColor = color;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (focused)
            return;
        focused = true;
        shadow.enabled = false;
        transform.localPosition += new Vector3(shadow.effectDistance.x, shadow.effectDistance.y, 0);
        background.color = new Color(color.r * colorDivider, color.g * colorDivider, color.b * colorDivider, color.a);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!focused)
            return;
        focused = false;
        shadow.enabled = true;
        transform.localPosition -= new Vector3(shadow.effectDistance.x, shadow.effectDistance.y, 0);
        background.color = color;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (focused)
        {
            OnPointerExit(null);
            actions.Invoke();
            color = colors[UnityEngine.Random.Range(0, colors.Length)];
            color.a = 1;
            background.color = color;
            MainColor = color;
        }
    }
}