using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Singleton<Bird>
{
    [SerializeField] private Rigidbody2D rb;

    public float xSpeed;
    public float minYSpeed;
    public float maxYSpeed;

    public GameObject deathVFX;

    private bool moveLeftOnStart;
    private bool isDead;
    private void Start()
    {
        RandomMovingDirection();
    }
    private void Update()
    {
        Flip();
        rb.velocity = moveLeftOnStart ? new Vector2(-xSpeed, Random.Range(minYSpeed, maxYSpeed)) : new Vector2(xSpeed, Random.Range(minYSpeed, maxYSpeed));
    }

    public void RandomMovingDirection()
    {
        moveLeftOnStart = transform.position.x > 0 ? true : false;
    }
    public void Flip()
    {
        if (moveLeftOnStart)
        {
            if (transform.localScale.x < 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            if (transform.localScale.x > 0) { return; }
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
        GameManager.Instance.PointCounter++;
        if (deathVFX)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            UIManager.Instance.UpdatePointCouter(GameManager.Instance.PointCounter);
        }
    }
}
