using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private int TotalTurnsAmount;
    private bool GameIsPaused = false;

    [SerializeField]
    private InputField TurnInputField;
    [SerializeField]
    private Text TurnText;
    [SerializeField]
    private Image PauseButtonImage;
    [SerializeField]
    private Button OpenChatButton;
    [SerializeField]
    private GameObject ChatMessagesCanvas;

    public Color ClickedButtonColor;
    public Color UnclickedButtonColor;

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
            TurnText.text = turnToGo + "/" + TotalTurnsAmount;
            //call function from GameManager
        }
    }

    public void OnPauseButtonClicked()
    {
        if (GameIsPaused)
        {
            GameIsPaused = false;
            PauseButtonImage.color = UnclickedButtonColor;
            //call function from GameManager
        }
        else
        {
            GameIsPaused = true;
            PauseButtonImage.color = ClickedButtonColor;
            //call function from GameManager
        }
    }

    public void OnChangeSpeedButtonClicked(float factor)
    {
        //call function from GameManager
    }

    public void OnOpenChatButtonClicked()
    {
        ChatMessagesCanvas.SetActive(!ChatMessagesCanvas.activeSelf);
    }
}
