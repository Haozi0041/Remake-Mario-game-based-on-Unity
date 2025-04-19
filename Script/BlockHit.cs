using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public int maxHits = -1;

    public GameObject item;
    public Sprite emptyBlock;

    private bool animating;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!animating && maxHits!=0&& collision.gameObject.CompareTag("Player"))//检测是否有玩家顶方块
        {
            if(collision.transform.DotTest(transform,Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        maxHits--;
       if(maxHits==0)//检测可以顶的次数
        {
            GetComponent<SpriteRenderer>().sprite = emptyBlock;
            Vector3 upPosition = transform.position + Vector3.up * 0.5f;
            Instantiate(item,upPosition,Quaternion.identity );//无旋转生成
            GetComponent<SpriteRenderer>().enabled = true;
        }
        StartCoroutine(Animate());//执行被顶的动画
    }


    private IEnumerator Animate()
    {
        animating = true;

        Vector3 resPosition = transform.localPosition;
        Vector3 upPosition = resPosition + Vector3.up * 0.5f;
        yield return Move(resPosition, upPosition);
        yield return Move(upPosition, resPosition);

        animating = false;
    }


    private IEnumerator Move(Vector3 from ,Vector3 to)//将坐标线性在两个坐标之间移动
    {
        float elapsed = 0f;
        float duration = 0.125f;
        while(elapsed<duration)
        {
            float t = elapsed / duration;
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = to;
    }

}
