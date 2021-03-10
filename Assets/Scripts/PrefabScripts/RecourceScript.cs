using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecourceScript : MonoBehaviour
{
    private GameObject res20;
    private GameObject res40;
    private GameObject res60;
    private GameObject res80;
    private GameObject res100;
    


    public Text Text;

    public void SetAmount(int amount)
    {
        if(amount <= 20 )
        {
            res20.SetActive(true);
            res40.SetActive(false);
            res60.SetActive(false);
            res80.SetActive(false);
            res100.SetActive(false);
        }
        if(amount > 20 && amount<=40)
        {
            res20.SetActive(true);
            res40.SetActive(true);
            res60.SetActive(false);
            res80.SetActive(false);
            res100.SetActive(false);
        }
        if (amount > 40 && amount <= 60)
        {
            res20.SetActive(true);
            res40.SetActive(true);
            res60.SetActive(true);
            res80.SetActive(false);
            res100.SetActive(false);
        }
        if (amount > 60 && amount <= 80)
        {
            res20.SetActive(true);
            res40.SetActive(true);
            res60.SetActive(true);
            res80.SetActive(true);
            res100.SetActive(false);
        }
        if (amount > 80 && amount <= 100)
        {
            res20.SetActive(true);
            res40.SetActive(true);
            res60.SetActive(true);
            res80.SetActive(true);
            res100.SetActive(true);
        }
        Text.text = amount.ToString();
    }
}
