using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform TF;
    public Transform GunTF;
    float minX = -11;
    float maxX = 11;
    [SerializeField] Vector3 offset;

    private void FixedUpdate()
    {
        float xTF = Mathf.Clamp(TF.position.x,minX,maxX);
        if(Vector2.Distance(new Vector2(TF.position.x,TF.position.y),new Vector2(GunTF.position.x, GunTF.position.y))>=5f)
        {
            TF.position = Vector3.Lerp(new Vector3(xTF,TF.position.y,0f)+offset, new Vector3(GunTF.position.x, 0f) + offset, Time.fixedDeltaTime * 1f);
        }
    }
}
