using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    public Text healthText;
    public int maxHealth;

    public int health
    {
        set { health = value; }
    }

    public void setHealth(int health)
    {
        healthText.text = health.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}