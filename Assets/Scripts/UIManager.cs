using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private int TotalTurnsAmount = 100;

    [SerializeField]
    private TMP_InputField TurnInputField;
    [SerializeField]
    private TextMeshProUGUI TurnText;
    [SerializeField]
    private GameObject ChatMessagesCanvas;

    private void Awake()
    {
        Instance = this;
    }

    public void Init(int totalTurnsAmount)
    {
        TotalTurnsAmount = totalTurnsAmount;
    }

    public void ApplyTurn()
    {
        string turnToGo = TurnInputField.text;
        if (int.Parse(turnToGo) <= TotalTurnsAmount && int.Parse(turnToGo) > 0)
        {
            TurnText.text = turnToGo + " / " + TotalTurnsAmount;
            //call function from GameManager
        }
        TurnInputField.text = "";
    }

    public void OnChangeSpeedButtonClicked(float factor)
    {
        //call function from GameManager
    }

    public void OnToggleChatButtonClicked()
    {
        ChatMessagesCanvas.SetActive(!ChatMessagesCanvas.activeSelf);
    }
}
