using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public static ChatManager Instance;
    [SerializeField] private int MessageHeight;
    [SerializeField] private Color ImportantMessageColor;
    [SerializeField] private Color TrivialMessageColor;
    [SerializeField] private TextMeshProUGUI TextPrefab;
    [SerializeField] private VerticalLayoutGroup PanelRight;
    [SerializeField] private VerticalLayoutGroup PanelLeft;
    private void Awake()
    {
        Instance = this;
        //hard code for test
        // Chat[] pChat = new Chat[16];
        // pChat[0] = new Chat("salam1", 5, 1);
        // pChat[1] = new Chat("salam2", 2, 2);
        // pChat[2] = new Chat("salam3", 7, 3);
        // pChat[3] = new Chat("salam3", 7, 3);
        // pChat[4] = new Chat("salam3", 7, 3);
        // pChat[5] = new Chat("salam3", 7, 3);
        // pChat[6] = new Chat("salam3", 7, 3);
        // pChat[7] = new Chat("salam3", 7, 3);
        // pChat[8] = new Chat("salam3", 7, 3);
        // pChat[9] = new Chat("salam3", 7, 3);
        // pChat[10] = new Chat("salam3", 7, 3);
        // pChat[11] = new Chat("salam3", 7, 3);
        // pChat[12] = new Chat("salam3", 7, 3);
        // pChat[13] = new Chat("salam3", 7, 3);
        // pChat[14] = new Chat("salam3", 7, 3);
        // pChat[15] = new Chat("salam3", 7, 3);
        // Chat[] tChat = new Chat[3];
        // tChat[0] = new Chat("salaaaaam1", 50, 10);
        // tChat[1] = new Chat("salaaaaam2", 20, 20);
        // tChat[2] = new Chat("salaaaaam3", 70, 30);
        // SetRightChatMessages(pChat, tChat);
    }


    public void ClearChatBoxes()
    {
        for (int i = 0; i < PanelRight.gameObject.transform.childCount; i++)
        {
            Destroy(PanelRight.gameObject.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < PanelLeft.gameObject.transform.childCount; i++)
        {
            Destroy(PanelLeft.gameObject.transform.GetChild(i).gameObject);
        }
    }

    public void SetLeftChatMessages(Chat[] importantMessages, Chat[] trivialMessages)
    {
        SetChatMessages(importantMessages,trivialMessages,PanelLeft);
    }
    public void SetRightChatMessages(Chat[] importantMessages, Chat[] trivialMessages)
    {
        SetChatMessages(importantMessages,trivialMessages,PanelRight);
    }
    public void SetChatMessages(Chat[] importantMessages, Chat[] trivialMessages, VerticalLayoutGroup Panel)
    {
        int importantMessagesCount = importantMessages.Length;
        int trivialMessagesCount = trivialMessages.Length;
        int total = importantMessagesCount + trivialMessagesCount;
        int i;
        // Panel.spacing = total;
        RectTransform rectTransform = Panel.gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, total * MessageHeight);
        rectTransform.Translate(0,-100,0);
        for (i = 0; i < importantMessagesCount; i++)
        {
            TextMeshProUGUI text = Instantiate(TextPrefab, Panel.transform);
            text.text = importantMessages[i].ToString();
            text.color = ImportantMessageColor;
        }
        for (; i < total; i++)
        {
            TextMeshProUGUI text = Instantiate(TextPrefab, Panel.transform);
            text.text = trivialMessages[i - importantMessagesCount].ToString();
            text.color = TrivialMessageColor;
        }
    }
}