using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BackGround : Singleton<BackGround>
{
    //public Sprite[] sprites;
    //public SpriteRenderer image;
    public GameObject[] bg;
    public Transform TFParent;



    private void Start()
    {
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        //if (image != null && sprites != null && sprites.Length > 0)
        //{
        //    int random = UnityEngine.Random.Range(0, sprites.Length);
        //    if (sprites[random] != null)
        //    {
        //        image.sprite = sprites[random];
        //    }
        //}
        
        if(bg!= null)
        {
            int random = UnityEngine.Random.Range(0,bg.Length);
            if (bg[random] != null)
            {
                Instantiate(bg[random],TFParent.position,Quaternion.identity,TFParent);
            }
        }
    }
}
