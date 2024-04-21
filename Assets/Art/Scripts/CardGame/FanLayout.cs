using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FanLayout : MonoBehaviour
{
    public float radius; // ���εİ뾶
    public float angleOffset; // ���ε���ʼ�Ƕ�
    public float angleRange; // ���εĽǶȷ�Χ
    public RectTransform container; // �������������RectTransform

    void Start()
    {
        ArrangeElements();
    }

    void ArrangeElements()
    {
        int elementsCount = container.childCount; // ��ȡ���������
        float angleStep = angleRange / (elementsCount - 1); // ����ÿ��Ԫ��֮��ĽǶȲ�

        for (int i = 0; i < elementsCount; i++)
        {
            RectTransform child = container.GetChild(i) as RectTransform;
            if (child != null)
            {
                // ����ÿ��Ԫ�ص�λ��
                float angle = angleOffset + angleStep * i;
                float xPosition = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
                float yPosition = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;

                // ��Overlayģʽ������anchoredPosition
                child.anchoredPosition = new Vector2(xPosition, yPosition);

                // ��ѡ��������Ҫ����Ԫ�ص���ת
                // child.localEulerAngles = new Vector3(0, 0, -angle);
            }
        }
    }
}
