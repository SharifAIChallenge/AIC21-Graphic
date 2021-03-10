using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonScript : MonoBehaviour
{
    private Vector3 temp;
    private bool move;
    private float reachTime;
    public void Fire(int x1,int y1,int x,int y,float time)
    {
        transform.position = FindObjectOfType<GameManager>().ConvertPosition(x1,y1);
        move = true;
        temp = FindObjectOfType<GameManager>().ConvertPosition(x,y);
        reachTime = Time.time + time;

    }
    private void Update()
    {
        if (move)
        {
            float d = Vector2.Distance(transform.position, temp);
            Debug.Log(d);
            if (d < 0.5)
            {
                move = false;
                Destroy(gameObject);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, temp,
                    ( Mathf.Abs(d / (reachTime-Time.time))) * Time.deltaTime);
                Vector3 dir = temp - transform.position;
                LookTo(dir);
            }
        }
    }
    
    private void LookTo(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
