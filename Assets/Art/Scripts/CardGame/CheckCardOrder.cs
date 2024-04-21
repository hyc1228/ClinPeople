using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCardOrder : MonoBehaviour
{
    public GameObject[] cards; // 扑克牌数组
    public GameObject uiPanel; // 引用UI Panel
    public AudioSource victorySound; // 胜利音效的AudioSource引用

    private Vector3[] initialPositions; // 存储初始位置

    void Start()
    {
        // 存储所有卡片的初始位置
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
            victorySound.Play(); // 播放胜利音效
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