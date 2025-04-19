using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)//检测碰撞 用来判断是否踩在头部
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
            else//若并非踩头  执行受伤
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


    private void FlatSpriteRender()//在渲染实体死亡时 确保该实体的动作和动画渲染组件关闭 同时启用死亡时的渲染动画 并在短暂延时后销毁
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
