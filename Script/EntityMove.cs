using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ýű���������ʵ��Ļ����ƶ� ��������� Ģ�� ��
/// </summary>
public class EntityMove : MonoBehaviour
{


    public float speed = 1f;
    public Vector2 direction = Vector2.left;//����ͳһ Ĭ�Ϸ��� Ϊ���
    
    
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;//��ʼ�����ø���� ��ȻҲ��������Ŀ���ֶ�����
    }
    private void OnBecameVisible()//�������������������Ұ�����   ����ű�
    {
        enabled = true;
    }
    private void OnBecameInvisible()//����������뿪�������Ұ�����   �رսű�
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        velocity = Vector2.zero;
        rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);

        if(rigidbody.Raycast(direction))
        {
            direction = -direction;
            transform.eulerAngles =new Vector3 (0f, 180f, 0f);

        }
        if(rigidbody.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }




    }

}
