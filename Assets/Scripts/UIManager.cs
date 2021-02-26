using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private int TotalTurnsAmount = 100;

    [SerializeField] private float BaseTime;
    [SerializeField] private TMP_InputField TurnInputField;
    [SerializeField] private TextMeshProUGUI SpeedText;
    [SerializeField] private TextMeshProUGUI TurnText;
    [SerializeField] private GameObject LeftChatMessagesCanvas;
    [SerializeField] private GameObject RightChatMessagesCanvas;
    private GameManager GameManager;
    private float Speed = 1;
    private float LastApplyTime = 0;
    private int CurrentTurn = 0;

    private void Awake()
    {
        GameManager = (GameManager) FindObjectOfType(typeof(GameManager));
        Instance = this;
    }

    private void Start()
    {
        TotalTurnsAmount = GameManager.MaxTurns;
        SetTurnText("0");

        LeftChatMessagesCanvas.SetActive(false);
        RightChatMessagesCanvas.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (Time.time > LastApplyTime + BaseTime && CurrentTurn < TotalTurnsAmount)
        {
            CurrentTurn += 1;
            SetTurnText(CurrentTurn.ToString());
            LastApplyTime = Time.time;
            Debug.Log(CurrentTurn);
            GameManager.ApplyLog(CurrentTurn);
        }
    }

    public void ApplyTurnButtonClicked()
    {
        string turnToGo = TurnInputField.text;
        if (int.Parse(turnToGo) <= TotalTurnsAmount && int.Parse(turnToGo) > 0)
        {
            CurrentTurn = int.Parse(turnToGo);
            SetTurnText(turnToGo);
            LastApplyTime = Time.time;
            GameManager.ApplyLog(CurrentTurn);
        }

        TurnInputField.text = "";
    }

    private void SetTurnText(string turnToGo)
    {
        TurnText.text = turnToGo + " / " + TotalTurnsAmount;
    }

    public void OnChangeSpeedButtonClicked(float factor)
    {
        Speed *= factor;
        if (Speed > 4)
            Speed = 4;
        if (Speed < 0.25)
            Speed = (float) 0.25;
        if (Time.timeScale > 0)
            Time.timeScale = Speed;
        SpeedText.text = Speed.ToString("F2");
        //call function from GameManager
    }

    public void OnLeftToggleChatButtonClicked()
    {
        LeftChatMessagesCanvas.SetActive(!LeftChatMessagesCanvas.activeSelf);
    }
    public void OnRightToggleChatButtonClicked()
    {
        RightChatMessagesCanvas.SetActive(!RightChatMessagesCanvas.activeSelf);
    }

    public void OnPlayButtonClicked(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = Speed;
        }
    }
}