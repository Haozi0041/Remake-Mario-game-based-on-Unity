using System.Collections;

using UnityEngine;


/// <summary>
/// ���ƽ�ɫ��������
/// </summary>
public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite deathSprite;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()//һ��ʼ��ɫ���������ú��� Ҫ�Ƚ��� �����ú� ������Ⱦ���� ������ԭ��������ϵͳ
    {
        DisablePhysics();
        UpdateSprite();
        
        StartCoroutine(Animate());//Э�̴���  ���ƶ��̣߳� ������������������ĵ���
    }

    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();//һ�������Ͽ��ܺ��ж����ײ��  �������йر�
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;//ͬʱ�ر���ѧ���
        PlyerMovement plyer = GetComponent<PlyerMovement>();//�ر��ƶ��ű�
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
        spriteRenderer.enabled = true;//����BUG����Ⱦ
        spriteRenderer.sortingOrder = 10;//����Ⱦͼ���������ͼ�� ������ײ  ʵ�ֵ���ȥ�Ķ���
        if(deathSprite != null)
        {
            spriteRenderer.sprite = deathSprite;
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;//��������ʱ��

        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsed<duration)//�ڶ�����Ⱦ�ĳ���ʱ���ڽ���  ������Ⱦ
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

  

}
