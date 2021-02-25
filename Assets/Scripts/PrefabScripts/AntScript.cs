using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntScript : MonoBehaviour
{
    public Sprite worker1;
    public Sprite worker2;
    public Sprite fighter1;
    public Sprite fighter2;
    public Sprite resource1;
    public Sprite resource2;
    public SpriteRenderer resourceSpriteRenderer;
    public Text healthText;
    private GameManager gameManager;
    private int x;
    private int y;
    private int health;
    private int recource;
    private int team;
    private int type;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Set(int x, int y, int team, int type, int health, int recource)
    {
        this.x = x;
        this.y = y;
        this.recource = recource;
        this.health = health;
        this.team = team;
        this.type = type;
        SetPosition(x, y);
        SetSprite(team, type);
        if (type == 1)
            SetResource(recource);
        SetHealth(health);
    }

    public void Go(int x, int y, int health, int recource)
    {
        this.x = x;
        this.y = y;
        this.recource = recource;
        this.health = health;
        SetPosition(x, y);
        if (type == 1)
            SetResource(recource);
        SetHealth(health);
    }

    private void SetPosition(int x, int y)
    {
        transform.position =
            new Vector3(gameManager.x0 + x * gameManager.width, gameManager.y0 + y * gameManager.haight, 0);
    }

    private void SetSprite(int team, int type)
    {
        if (team == 0)
        {
            if (type == 1)
                SetSprite(worker1);
            else
                SetSprite(fighter1);
        }
        else
        {
            if (type == 1)
                SetSprite(worker2);
            else
                SetSprite(fighter2);
        }
    }


    private void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void SetResource(int resource)
    {
        switch (resource)
        {
            case 1:
                resourceSpriteRenderer.sprite = resource1;
                break;
            case 2:
                resourceSpriteRenderer.sprite = resource2;
                break;
        }

        resourceSpriteRenderer.size = new Vector2(0.5f, 0.5f);
    }

    private void SetHealth(int health)
    {
        healthText.text = health.ToString();
    }
}