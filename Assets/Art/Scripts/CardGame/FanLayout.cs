using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FanLayout : MonoBehaviour
{
    public float radius; // 扇形的半径
    public float angleOffset; // 扇形的起始角度
    public float angleRange; // 扇形的角度范围
    public RectTransform container; // 包含子项的容器RectTransform

    void Start()
    {
        ArrangeElements();
    }

    void ArrangeElements()
    {
        int elementsCount = container.childCount; // 获取子项的数量
        float angleStep = angleRange / (elementsCount - 1); // 计算每个元素之间的角度差

        for (int i = 0; i < elementsCount; i++)
        {
            RectTransform child = container.GetChild(i) as RectTransform;
            if (child != null)
            {
                // 计算每个元素的位置
                float angle = angleOffset + angleStep * i;
                float xPosition = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
                float yPosition = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;

                // 在Overlay模式下设置anchoredPosition
                child.anchoredPosition = new Vector2(xPosition, yPosition);

                // 可选：根据需要调整元素的旋转
                // child.localEulerAngles = new Vector3(0, 0, -angle);
            }
        }
    }
}
