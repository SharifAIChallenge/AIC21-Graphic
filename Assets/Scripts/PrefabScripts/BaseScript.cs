using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    public Text healthText;
    public HealthBar healthBar;
    private int maxHealth;
    private int health;

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void setHealth(int health)
    {
        try
        {

            healthText.text = health.ToString();
        }
        catch (Exception e)
        {
            Debug.Log(gameObject.name);
        }
        this.health = health;
        healthBar.SetHealth(health);
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