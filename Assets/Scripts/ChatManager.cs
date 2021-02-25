using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public static ChatManager Instance;

    [SerializeField]
    private TextMeshProUGUI[] Texts;
    private int TextsCount;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TextsCount = Texts.Length;
    }

    public void SetChatMessages(string[] messages)
    {
        int messagesCount = messages.Length;
        if (messagesCount >= TextsCount)
        {
            for (int i = 0; i < TextsCount ; i++)
            {
                Texts[i].text = messages[messagesCount - TextsCount + i];
            }
        }
        else
        {
            for (int i = 0; i < TextsCount - messagesCount; i++)
            {
                Texts[i].text = Texts[i + messagesCount].text;
            }
            for (int i = TextsCount - messagesCount; i < TextsCount; i++)
            {
                Texts[i].text = messages[messagesCount - TextsCount + i];
            }
        }
    }
}
