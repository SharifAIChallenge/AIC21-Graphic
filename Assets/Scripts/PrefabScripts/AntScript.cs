using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class AntScript : MonoBehaviour
{
    [SerializeField] private float MaxDis;
    [SerializeField] private float fighterScaleCorrection;
    [SerializeField] private Color redFighterColorCorrection;
    [SerializeField] private Sprite resource1;
    [SerializeField] private Sprite resource2;
    [SerializeField] private SpriteRenderer resourceSpriteRenderer;
    [SerializeField] private Text IdText;
    [SerializeField] private Text ScorpionIdText;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private RuntimeAnimatorController redWorkerAnimator;
    [SerializeField] private RuntimeAnimatorController redFighterAnimator;
    [SerializeField] private RuntimeAnimatorController blackWorkerAnimator;
    [SerializeField] private RuntimeAnimatorController blackFighterAnimator;

    [SerializeField] private GameObject Poison;
    private float Speed;

    private int x;
    private int y;
    private int id;
    private int health;
    private int recource;
    private int team;
    private int type;
    private float baseTime;
    private Vector3 temp;
    private bool readTemp = false;
    private float reachTime;
    private Animator mainAnimator;
    private Vector3 baseScale;
    [SerializeField] private int turnNumber;
    [SerializeField] private int turnOrder;
    private int turnI = 0;

    private void Awake()
    {
        baseScale = transform.localScale;
        mainAnimator = GetComponent<Animator>();
    }

    public void Set(int x, int y, int team, int type, int health, int recource, int Id, int numbers, int n)
    {
        this.x = x;
        this.y = y;
        id = Id;
        if (type == 0)
            ScorpionIdText.text = id.ToString();
        else
            IdText.text = id.ToString();
        this.recource = recource;
        this.health = health;
        this.team = team;
        this.type = type;
        SetSprite(team, type);
        SetPosition(x, y);
        transform.position = GameManager.Instance.ConvertPosition(x, y) +
                             handleMulty(numbers, n, GameManager.Instance.width);
        if (type == 1)
            SetResource(recource);
        SetHealth(health);
        mainAnimator.Play("Idle");
        readTemp = false;
    }

    private Vector3 handleMulty(int numbers, int n, int width)
    {
        int numbersRoot = (int) Mathf.Pow(numbers, 0.5f);
        float numbersMid = (float) (numbersRoot + 1) / 2;
        int sx = (int) Mathf.Ceil(((float) n / numbersRoot));
        int ys = n % numbersRoot;
        if (ys == 0)
            ys = numbersRoot;
        Vector3 mPosition = new Vector3((-numbersMid + ys) * width / numbersRoot,
            (numbersMid - sx) * width / numbersRoot, 1);
        transform.localScale = baseScale / numbersRoot;
        return mPosition;
    }

    public IEnumerator Go(int x, int y, int health, int recource, float time, int numbers, int n)
    {
        int old_x = this.x;
        int old_y = this.y;
        this.x = x;
        this.y = y;
        this.recource = recource;
        this.health = health;
        if (type == 1)
            SetResource(recource);
        SetHealth(health);
        temp = GameManager.Instance.ConvertPosition(x, y) + handleMulty(numbers, n, GameManager.Instance.width);
        if (math.abs(old_x - x) > 1 || math.abs(old_y - y) > 1)
        {
            //minor mode
            mainAnimator.Play("Idle");
            readTemp = false;
            transform.position = temp;
        }
        else
        {
            mainAnimator.Play("Walk");
            reachTime = time + Time.time;
            readTemp = true;
        }

        yield return null;
    }

    private void LateUpdate()
    {
        if (readTemp)
        {
            float d = Vector2.Distance(transform.position, temp);
            if (d < 0.1f)
            {
                readTemp = false;
                mainAnimator.Play("Idle");
            }
            else
            {
                if (reachTime - Time.time < 0 )
                {
                    Debug.Log("error "+id+" "+reachTime+" "+Time.time);
                }

                float deltaTime = reachTime - Time.time;
                if (deltaTime < 0)
                {
                    deltaTime = 0.0001f;
                }
                float maxDistanceDelta;
                maxDistanceDelta = (d / deltaTime) * Time.deltaTime * turnOrder/turnNumber;
                Debug.Log("id: " + id + "current: " + transform.position + " goal: " + temp + " delta " + d +
                          " max dis: " + maxDistanceDelta);
                maxDistanceDelta = Mathf.Min(MaxDis, maxDistanceDelta);
                Debug.Log("id: "+id+" max dis: "+maxDistanceDelta);
                transform.position = Vector3.MoveTowards(transform.position, temp,
                    maxDistanceDelta);
                Vector3 dir = temp - transform.position;
                LookTo(dir);
            }
        }
    }

    public void Attack(int x, int y, float time)
    {
        GameObject poison = Instantiate(Poison);
        poison.GetComponent<PoisonScript>().Fire(this.x, this.y, x, y, time);
        mainAnimator.Play("Attack");
        LookTo(GameManager.Instance.ConvertPosition(x, y) - transform.position);
    }

    private void LookTo(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public IEnumerator die(float deadTime, float destroyTime)
    {
        yield return new WaitForSecondsRealtime(deadTime * UIManager.Instance.Speed);
        mainAnimator.Play("Die");
        yield return new WaitForSecondsRealtime(destroyTime * UIManager.Instance.Speed);
        Destroy(gameObject);
    }

    private void SetPosition(int x, int y)
    {
        transform.position = GameManager.Instance.ConvertPosition(x, y);
        // Debug.Log(transform.position);
    }

    private void SetSprite(int team, int type)
    {
        //1:worker 0:fighter
        if (team == 0) //red team
        {
            if (type == 1)
            {
                mainAnimator.runtimeAnimatorController = redWorkerAnimator;
                mainAnimator.Play("Idle");
            }
            else
            {
                transform.localScale = new Vector3(fighterScaleCorrection, fighterScaleCorrection, 1);
                baseScale = new Vector3(fighterScaleCorrection, fighterScaleCorrection, 1);
                mainAnimator.runtimeAnimatorController = redFighterAnimator;
                GetComponent<SpriteRenderer>().color = redFighterColorCorrection;
            }
        }
        else //black team
        {
            if (type == 0)
            {
                transform.localScale = new Vector3(fighterScaleCorrection, fighterScaleCorrection, 1);
                baseScale = new Vector3(fighterScaleCorrection, fighterScaleCorrection, 1);
                mainAnimator.runtimeAnimatorController = blackFighterAnimator;
            }
            else
                mainAnimator.runtimeAnimatorController = blackWorkerAnimator;
        }
    }

    private void SetResource(int resource)
    {
        Vector2 size = resourceSpriteRenderer.size;
        switch (resource)
        {
            case 0:
                resourceSpriteRenderer.sprite = resource1;
                break;
            case 1:
                resourceSpriteRenderer.sprite = resource2;
                break;
            case 2:
                resourceSpriteRenderer.sprite = null;
                break;
        }

        resourceSpriteRenderer.size = size;
    }

    private void SetHealth(int health)
    {
        this.healthBar.SetHealth(health);
        this.health = health;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.healthBar.SetMaxHealth(maxHealth);
        this.health = maxHealth;
    }
}