using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    
    public Transform connection;
    public KeyCode enterKey = KeyCode.S;
    public bool underGround;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(enterKey))
            {
                collision.GetComponent<PlyerMovement>().enabled = false;
                StartCoroutine(Animate(collision.transform));
                
            }
        }          
    }

    private IEnumerator Animate(Transform player)
    {
        //player.GetComponent<PlyerMovement>().enabled = false;
        Vector3 enterPosition = transform.position + enterDirection;
        Vector3 enterScale = Vector3.one * 0.5f;

        yield return Move(player, enterPosition, enterScale);
        underGround = connection.position.y < 0f;
        Camera.main. GetComponent<CameraMove>().UnderGrouned(underGround);
        if (exitDirection != Vector3.zero)
        {
            
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
            yield return new WaitForSeconds(1f);
    
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }
        player.GetComponent<PlyerMovement>().enabled= true;
    }

    private IEnumerator Move(Transform player ,Vector3 endPosition,Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            
            float t = elapsed / duration;
            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        player.position = endPosition;
        player.localScale = endScale;

    }


}
