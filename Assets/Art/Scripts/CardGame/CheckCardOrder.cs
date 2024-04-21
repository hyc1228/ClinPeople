using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCardOrder : MonoBehaviour
{
    public GameObject[] cards; // �˿�������
    public GameObject uiPanel; // ����UI Panel
    public AudioSource victorySound; // ʤ����Ч��AudioSource����

    private Vector3[] initialPositions; // �洢��ʼλ��

    void Start()
    {
        // �洢���п�Ƭ�ĳ�ʼλ��
        initialPositions = new Vector3[cards.Length];
        for (int i = 0; i < cards.Length; i++)
        {
            initialPositions[i] = cards[i].transform.position;
        }
    }

    void Update()
    {
        if (IsCorrectOrder())
        {
            Debug.Log("Correct Order! Event Triggered.");
            victorySound.Play(); // ����ʤ����Ч
            DestroyPanel();
        }
    }

    public void ResetCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].transform.position = initialPositions[i];
            cards[i].transform.localScale = Vector3.one; // Reset scale if modified
        }
    }

    bool IsCorrectOrder()
    {
        for (int i = 0; i < cards.Length - 1; i++)
        {
            if (cards[i].transform.position.x > cards[i + 1].transform.position.x)
                return false;
        }
        return true;
    }

    void DestroyPanel()
    {
        if (uiPanel != null)
        {
            Destroy(uiPanel);
        }
    }
}