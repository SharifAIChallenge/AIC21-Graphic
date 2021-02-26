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

    public void SetLeftChatMessages(string[] messages)
    {
        int messagesCount = messages.Length;
        if (messagesCount >= LeftTextsCount)
        {
            for (int i = 0; i < LeftTextsCount ; i++)
            {
                LeftTexts[i].text = messages[messagesCount - LeftTextsCount + i];
            }
        }
        else
        {
            for (int i = 0; i < LeftTextsCount - messagesCount; i++)
            {
                LeftTexts[i].text = LeftTexts[i + messagesCount].text;
            }
            for (int i = LeftTextsCount - messagesCount; i < LeftTextsCount; i++)
            {
                LeftTexts[i].text = messages[messagesCount - LeftTextsCount + i];
            }
        }
    }

    public void SetRightChatMessages(string[] messages)
    {
        int messagesCount = messages.Length;
        if (messagesCount >= RightTextsCount)
        {
            for (int i = 0; i < RightTextsCount; i++)
            {
                RightTexts[i].text = messages[messagesCount - RightTextsCount + i];
            }
        }
        else
        {
            for (int i = 0; i < RightTextsCount - messagesCount; i++)
            {
                RightTexts[i].text = RightTexts[i + messagesCount].text;
            }
            for (int i = RightTextsCount - messagesCount; i < RightTextsCount; i++)
            {
                RightTexts[i].text = messages[messagesCount - RightTextsCount + i];
            }
        }
    }
}
