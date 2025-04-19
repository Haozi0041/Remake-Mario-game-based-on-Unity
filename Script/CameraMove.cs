using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float hight = 6.2f;
    public float underHight = -10.2f;

    private Transform Player;
    
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
        
    }
    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, Player.position.x);
        transform.position = cameraPosition;
    }

    public void UnderGrouned(bool underGround)
    {
        Vector3 cameraPosition = transform.position;
        
        cameraPosition.y = underGround ? underHight : hight;
        if (underGround == false)
        {
            Debug.Log("fasle");
        }
        else if (underGround == true)
        {
            Debug.Log("true");
        }
        transform.position = cameraPosition;
    }
}
