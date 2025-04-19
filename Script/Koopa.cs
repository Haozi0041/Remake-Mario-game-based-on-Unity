using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite flatSprite;
    public float shellSpeed;

    private bool shelled = false;
    public bool pushed { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)//检测碰撞 用来判断是否踩在头部
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
            else//若并非踩头  执行受伤
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
                Vector2 direction = new Vector2(transform.position.x - collision.transform.position.x, 0f);//检测计算相对坐标 方向
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

    private void EnterShell()//在渲染实体死亡时 确保该实体的动作和动画渲染组件关闭 同时启用死亡时的渲染动画 并在短暂延时后销毁
    {
        shelled = true;
        GetComponent<AnimationRenderer>().enabled = false;
        GetComponent<EntityMove>().enabled = false;

        GetComponent<SpriteRenderer>().sprite = flatSprite;
    }

    private void PushShell(Vector2 direction)//踢壳方法  修改实体移动速度
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
