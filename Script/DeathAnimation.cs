using System.Collections;

using UnityEngine;


/// <summary>
/// 控制角色死亡动画
/// </summary>
public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite deathSprite;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()//一开始角色并不触发该函数 要先禁用 在启用后 更新渲染动画 并禁用原本的物理系统
    {
        DisablePhysics();
        UpdateSprite();
        
        StartCoroutine(Animate());//协程处理  类似多线程？ 在这里进行死亡动画的调用
    }

    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();//一个物体上可能含有多个碰撞箱  遍历进行关闭
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;//同时关闭力学检测
        PlyerMovement plyer = GetComponent<PlyerMovement>();//关闭移动脚本
        EntityMove entityMove = GetComponent<EntityMove>();

        if(plyer != null)
        {
            plyer.enabled = false;
        }
        if (entityMove != null)
        {
            entityMove.enabled = false;
        }

    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;//避免BUG不渲染
        spriteRenderer.sortingOrder = 10;//将渲染图层调制其他图层 避免碰撞  实现调出去的动画
        if(deathSprite != null)
        {
            spriteRenderer.sprite = deathSprite;
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;//动画持续时间

        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsed<duration)//在动画渲染的持续时间内进行  动作渲染
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

  

}
