using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite flatSprite;
    public float shellSpeed;

    private bool shelled = false;
    public bool pushed { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)//�����ײ �����ж��Ƿ����ͷ��
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            if (player.strapower)
            {
                HitByShell();
            }
            else if (collision.transform.DotTest(transform, Vector2.down))
            {
               EnterShell();
            }
            else//�����ǲ�ͷ  ִ������
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (shelled && collision.CompareTag("Player"))
        {
            if(!pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - collision.transform.position.x, 0f);//������������� ����
                PushShell(direction);
            }
            else
            {
                if (player.strapower)
                {
                    HitByShell();
                }
                else
                {
                    player.Hit();
                }
                
            }
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {

            HitByShell();
            
        }
    }

    private void EnterShell()//����Ⱦʵ������ʱ ȷ����ʵ��Ķ����Ͷ�����Ⱦ����ر� ͬʱ��������ʱ����Ⱦ���� ���ڶ�����ʱ������
    {
        shelled = true;
        GetComponent<AnimationRenderer>().enabled = false;
        GetComponent<EntityMove>().enabled = false;

        GetComponent<SpriteRenderer>().sprite = flatSprite;
    }

    private void PushShell(Vector2 direction)//�߿Ƿ���  �޸�ʵ���ƶ��ٶ�
    {
        pushed = true;
        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMove entityMove = GetComponent<EntityMove>();
        entityMove.direction = direction.normalized;
        entityMove.speed = shellSpeed;

        entityMove.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void HitByShell()
    {
        GetComponent<DeathAnimation>().enabled = true;

    }
}
