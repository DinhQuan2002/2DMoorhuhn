using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Singleton<Gun>
{
    public GameObject gun;
    public float fireRate = 1f;
    public float reloadTime = 3f;
    //int maxBullets = 6;
    int currentBullet;
    float currentTime = 0;
    float curFireRate;
    bool isShooted;

    private void Awake()
    {
        curFireRate = fireRate;
    }

    private void Start()
    {
        currentBullet = -2;
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
        if (gun)
        {
            gun.transform.position = new Vector3(mousePos.x, mousePos.y,0f);
        }
        if (isShooted)
        {
            curFireRate -= Time.deltaTime;
            if(curFireRate <= 0f ) 
            {
                isShooted =false;
                curFireRate = fireRate;
            }
        }
        
        if (currentBullet >= 5)
        {
            isShooted = false;
            currentTime += Time.deltaTime;
            curFireRate -= Time.deltaTime;
            if (currentTime >= reloadTime)
            {
                UIManager.Instance.ReLoadBullet();
                currentTime = 0;
                isShooted = true;
                currentBullet = -1;
            }
        }
        if(currentBullet<=5)
        {
            UIManager.Instance.UpdateBulletCount(currentBullet);
        }

        if (Input.GetMouseButtonDown(0) && !isShooted && currentTime <= 0)
        {
            Shoot(mousePos);
        }
    }

    private void Shoot(Vector3 mousePos)
    {
        currentBullet++;
        isShooted = true;
        //vector huong tu chuot den man hinh
        Vector3 shootDir = Camera.main.transform.position - mousePos;
        shootDir.Normalize();

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos,shootDir);

        if(hits != null)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if(hit.collider && (Vector2.Distance(hit.collider.transform.position, mousePos) <= 0.5f))
                {
                    Bird bird = hit.collider.GetComponent<Bird>();
                    if (bird)
                    {
                        bird.Dead();
                    }
                }
            }
        }
        AudioController.Instance.PlaySound(AudioController.Instance.shooting);

        
        Debug.Log(currentBullet);
    }
}
