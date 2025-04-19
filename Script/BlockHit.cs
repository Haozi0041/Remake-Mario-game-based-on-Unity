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
        if(!animating && maxHits!=0&& collision.gameObject.CompareTag("Player"))//����Ƿ�����Ҷ�����
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
       if(maxHits==0)//�����Զ��Ĵ���
        {
            GetComponent<SpriteRenderer>().sprite = emptyBlock;
            Vector3 upPosition = transform.position + Vector3.up * 0.5f;
            Instantiate(item,upPosition,Quaternion.identity );//����ת����
            GetComponent<SpriteRenderer>().enabled = true;
        }
        StartCoroutine(Animate());//ִ�б����Ķ���
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


    private IEnumerator Move(Vector3 from ,Vector3 to)//��������������������֮���ƶ�
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
