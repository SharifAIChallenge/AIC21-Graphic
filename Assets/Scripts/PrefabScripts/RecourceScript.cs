using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecourceScript : MonoBehaviour
{
    public Text Text;

    public void SetAmount(int amount)
    {
        Text.text = amount.ToString();
    }
}
