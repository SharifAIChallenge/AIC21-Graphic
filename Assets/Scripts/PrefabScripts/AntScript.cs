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
    [SerializeField]
    private RuntimeAnimatorController redWorkerAnimator;
    [SerializeField]
    private RuntimeAnimatorController redFighterAnimator;
    [SerializeField]
    private RuntimeAnimatorController blackWorkerAnimator;
    [SerializeField]
    private RuntimeAnimatorController blackFighterAnimator;
    private GameManager gameManager;
    private float Speed;

    private int x;
    private int y;
    private int health;
    private int recource;
    private int team;
    private int type;
    private float baseTime;
    private Vector3 temp;
    private bool readTemp = false;
    private float reachTime;
    private Animator mainAnimator;

    private void Awake()
    {
        mainAnimator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        // RedAnimator.end
        // baseTime = UIManager.Instance.BaseTime;
        // Set(1, 1, 0, 1, 2, 0);
        // Debug.Log("start");
        // yield return new WaitForSeconds(1);
        // Debug.Log("go");
        // StartCoroutine(Go(3, 2, 2, 0, 2));
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
        mainAnimator.Play("Idle");
        readTemp = false;
    }

    public IEnumerator Go(int x, int y, int health, int recource, float time)
    {
        int tempX = this.x;
        int tempY = this.y;
        this.x = x;
        this.y = y;
        this.recource = recource;
        this.health = health;
        // SetPosition(x, y);
        if (type == 1)
            SetResource(recource);
        SetHealth(health);
        yield return new WaitForSeconds(baseTime / 2);
        temp = GameManager.Instance.ConvertPosition(x, y);
        mainAnimator.Play("Walk");
        readTemp = true;
        reachTime = time+Time.time;
    }

    private void Update()
    {
        if (readTemp)
        {
            float d = Vector2.Distance(transform.position, temp);
            // Debug.Log(d);
            if (d < 0.5)
            {
                readTemp = false;
                mainAnimator.Play("Idle");
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, temp,
                   ( d / (reachTime-Time.time)) * Time.deltaTime);
                Vector3 dir = temp - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            }
        }
    }

    public IEnumerator die(float time)
    {
        mainAnimator.Play("Die");
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    private void SetPosition(int x, int y)
    {
        transform.position = GameManager.Instance.ConvertPosition(x, y);
        Debug.Log(transform.position);
    }

    private void SetSprite(int team, int type)
    {
        if (team == 0) //red team
        {
            if (type == 1)
            {
                mainAnimator.runtimeAnimatorController = redWorkerAnimator;
                mainAnimator.Play("RedAntIdle");
            }
            else
            {
                transform.localScale = new Vector3(fighterScaleCorrection,fighterScaleCorrection,1);
                mainAnimator.runtimeAnimatorController = redFighterAnimator;
                GetComponent<SpriteRenderer>().color = redFighterColorCorrection;
            }
        }
        else//black team
        {
            if (type == 1)
            {
                transform.localScale = new Vector3(fighterScaleCorrection,fighterScaleCorrection,1);
                mainAnimator.runtimeAnimatorController = blackFighterAnimator;
            }
            else
                mainAnimator.runtimeAnimatorController = blackWorkerAnimator;
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
        //todo slider health
    }
}