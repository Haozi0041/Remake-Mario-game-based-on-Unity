using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerRenderer bigRenderer;
    public PlayerRenderer smallRenderer;
    private PlayerRenderer activeRender;

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsule;
    public bool big => bigRenderer.enabled;
    public bool dead => deathAnimation.enabled;

    public bool strapower { get; private set; }
    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsule = GetComponent<CapsuleCollider2D>();
        activeRender = smallRenderer;
    }

    public void Hit()//通过状态 来判断受伤后 的处理
    {
       if (!strapower&&!dead)
        {
            if (big)
            {
                Shrink();
            }
            else
            {
                Death();
            }
        }
    }

    

    private void Death()//死亡后 关闭大小状态的动作渲染 并启用死亡动画  最后重置关卡
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;

        deathAnimation.enabled = true;

        GameManager.Instance.RestLevel(3f);
    }
   


    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRender = bigRenderer;
        capsule.size = new Vector2(1f, 2f);
        capsule.offset = new Vector2(0f, 0.5f);
        StartCoroutine(Animate());
    }

    public void Shrink()
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRender = smallRenderer;
        capsule.size = new Vector2(1f, 1f);
        capsule.offset = new Vector2(0f, 0f);
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if(Time.frameCount % 10 ==0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled; 
            }
            yield return null;
        }
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRender.enabled = true;
    }


    public void Strapower(float duration = 10f)
    {
        StartCoroutine(StraAnima(duration ));
    }

    private IEnumerator StraAnima(float duration)
    {
        strapower = true;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if(Time.frameCount % 6 ==0)
            {
                activeRender.SpriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }
            yield return null;
        }
        strapower = false;
        activeRender.SpriteRenderer.color = Color.white;
    }

}
