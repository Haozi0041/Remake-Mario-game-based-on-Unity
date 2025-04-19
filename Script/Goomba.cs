using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)//�����ײ �����ж��Ƿ����ͷ��
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (collision.gameObject.CompareTag("Player"))
        {
            if(player.strapower)
            {
                HitByShell();
            }
            else if (collision.transform.DotTest(transform, Vector2.down))
            {
                FlatSpriteRender();
            }
            else//�����ǲ�ͷ  ִ������
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Koopa koopa = collision.gameObject.GetComponent<Koopa>();

            if(koopa.pushed)
            {
                
                HitByShell();
            }

        }
    }


    private void FlatSpriteRender()//����Ⱦʵ������ʱ ȷ����ʵ��Ķ����Ͷ�����Ⱦ����ر� ͬʱ��������ʱ����Ⱦ���� ���ڶ�����ʱ������
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<AnimationRenderer>().enabled = false;
        GetComponent<EntityMove>().enabled = false;

        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.75f);
    }

   private void  HitByShell()
    {
        GetComponent<DeathAnimation>().enabled = true;

    }



}
