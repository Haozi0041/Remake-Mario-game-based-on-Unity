using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpMushroom : MonoBehaviour
{
    EntityMove entity;

    public enum Type
    {
        UpMushroom,
        BigMushroom,
        StarPower,
    }


    public Type type;
    private void Start()
    {
        entity = GetComponent<EntityMove>();
        GameObject player = GameObject.FindWithTag("Player");
        Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, 0f);
        entity.direction = direction.normalized;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Collect(collision.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.BigMushroom:
                player.GetComponent<Player>().Grow();
                break;
            case Type.StarPower:
                player.GetComponent<Player>().Strapower();
                break;
            case Type.UpMushroom:
                GameManager.Instance.AddLives();
                break;
        }
        Destroy(gameObject);
    }



}
