﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private int TotalTurnsAmount = 100;
    [SerializeField] public float BaseTime;
    [SerializeField] private TMP_InputField TurnInputField;
    [SerializeField] private TextMeshProUGUI SpeedText;
    [SerializeField] private TextMeshProUGUI TurnText;
    [SerializeField] private GameObject LeftChatMessagesCanvas;
    [SerializeField] private GameObject RightChatMessagesCanvas;
    private GameManager GameManager;
    public float Speed = 1;
    private float LastApplyTime = 0;
    private int CurrentTurn = 0;

    private void Awake()
    {
        GameManager = GameManager.Instance;
        Instance = this;
    }

    private void Start()
    {
        TotalTurnsAmount = GameManager.MaxTurns;
        Debug.Log("TotalTurnsAmount  " + TotalTurnsAmount);
        SetTurnText("0");

        LeftChatMessagesCanvas.SetActive(false);
        RightChatMessagesCanvas.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (Time.time > LastApplyTime + BaseTime && CurrentTurn < TotalTurnsAmount)
        {
            CurrentTurn += 1;
            ApplyTurn(CurrentTurn.ToString(), true);
            Debug.Log("apply turn " + CurrentTurn + " ui update");
        }
    }

    public void ApplyTurnButtonClicked()
    {
        string turnToGo = TurnInputField.text;
        if (int.Parse(turnToGo) <= TotalTurnsAmount && int.Parse(turnToGo) > 0)
        {
            CurrentTurn = int.Parse(turnToGo);
            ApplyTurn(turnToGo, false);
        }

        TurnInputField.text = "";
    }

    private void ApplyTurn(string turnToGo, bool isAnim)
    {
        SetTurnText(turnToGo);
        LastApplyTime = Time.time;
        ChatManager.Instance.ClearChatBoxes();
        GameManager.ApplyLog(CurrentTurn, isAnim);
    }

    private void SetTurnText(string turnToGo)
    {
        TurnText.text = turnToGo + " / " + TotalTurnsAmount;
    }

    public void OnNextTurnButtonClick()
    {
        int turnToGo = CurrentTurn + 1;
        if (turnToGo <= TotalTurnsAmount && turnToGo > 0)
        {
            CurrentTurn = turnToGo;
            ApplyTurn(turnToGo.ToString(), false);
        }
    }

    public void OnPreviousTurnButtonClick()
    {
        int turnToGo = CurrentTurn - 1;
        if (turnToGo <= TotalTurnsAmount && turnToGo > 0)
        {
            CurrentTurn = turnToGo;
            ApplyTurn(turnToGo.ToString(), false);
        }
    }

    public void OnChangeSpeedButtonClicked(float factor)
    {
        Speed *= factor;
        if (Speed > 2)
            Speed = 2;
        if (Speed < 0.25)
            Speed = (float) 0.25;
        if (Time.timeScale > 0)
            Time.timeScale = Speed;
        SpeedText.text = Speed.ToString("F2");
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