using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public static ChatManager Instance;

    [SerializeField]
    private TextMeshProUGUI[] LeftTexts;
    private int LeftTextsCount;

    [SerializeField]
    private TextMeshProUGUI[] RightTexts;
    private int RightTextsCount;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ClearChatBoxes();
    }

    public void ClearChatBoxes()
    {
        LeftTextsCount = LeftTexts.Length;
        for (int i = 0; i < LeftTextsCount; i++)
        {
            LeftTexts[i].text = "";
        }

        RightTextsCount = RightTexts.Length;
        for (int i = 0; i < RightTextsCount; i++)
        {
            RightTexts[i].text = "";
        }
    }

    public void SetLeftChatMessages(Chat[] importantMessages, Chat[] trivialMessages)
    {
        int importantMessagesCount = importantMessages.Length;
        int trivialMessagesCount = trivialMessages.Length;
        int total = importantMessagesCount + trivialMessagesCount;

        int i;
        for (i = 0; i < LeftTextsCount - total; i++)
        {
            LeftTexts[i].text = LeftTexts[i + total].text;
            LeftTexts[i].color = LeftTexts[i + total].color;
        }
        for (i = 0; i < Math.Min(importantMessagesCount, LeftTextsCount); i++)
        {
            LeftTexts[i].text = importantMessages[importantMessagesCount - LeftTextsCount + i].ToString();
            LeftTexts[i].color = Color.yellow;
        }
        int pivot = i;
        for (; i < Math.Min(total, LeftTextsCount); i++)
        {
            LeftTexts[i].text = trivialMessages[i - pivot].ToString();
            LeftTexts[i].color = Color.grey;
        }
    }

    public void SetRightChatMessages(Chat[] importantMessages, Chat[] trivialMessages)
    {
        int importantMessagesCount = importantMessages.Length;
        int trivialMessagesCount = trivialMessages.Length;
        int total = importantMessagesCount + trivialMessagesCount;

        int i;
        for (i = 0; i < RightTextsCount - total; i++)
        {
            RightTexts[i].text = RightTexts[i + total].text;
            RightTexts[i].color = RightTexts[i + total].color;
        }
        for (i = 0; i < Math.Min(importantMessagesCount, RightTextsCount); i++)
        {
            RightTexts[i].text = importantMessages[importantMessagesCount - RightTextsCount + i].ToString();
            RightTexts[i].color = Color.yellow;
        }
        int pivot = i;
        for (; i < Math.Min(total, RightTextsCount); i++)
        {
            RightTexts[i].text = trivialMessages[i - pivot].ToString();
            RightTexts[i].color = Color.grey;
        }
    }
}
