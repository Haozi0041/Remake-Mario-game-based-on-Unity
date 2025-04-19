using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new Camera camera;
    private Vector2 velocity;
    private float inputAxis;
    
    public float moveSpeed = 8f;
    public float MaxJumpHight = 5f;//�߶�
    public float MaxJumpTime = 1f;//ʱ��


    public float jumpForce => (2f * MaxJumpHight) / (MaxJumpTime / 2);
    public float Gravity => (-2f * MaxJumpHight) /Mathf .Pow( (MaxJumpTime / 2),2);


    public bool jumping { get; private set; }
    public bool Grounded { get; private set; }

    public bool running => Mathf.Abs(velocity.x) > 0.1f || Mathf.Abs(inputAxis)>0.1f;
    //public bool sliding => (inputAxis * velocity.x < 0f)&&(Mathf.Abs(velocity.x)>6f);
    public bool sliding => ((inputAxis>0f&&velocity.x<0f)||(inputAxis<0f&&velocity.x>0f)) ;

    private void OnDisable()
    {
        //velocity = Vector2.zero;
        jumping = false;
        rigidbody.isKinematic=true;
    }
    private void OnEnable()
    {
        rigidbody.isKinematic = false; 
    }
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();//��ȡ����
        camera = Camera.main;//��ȡ�����
    }
    private void Update()
    {
        HorizontalMovement();
        Grounded = rigidbody.Raycast(Vector2.down);//�ж��ŵ�״̬
        if (Grounded)//�����ŵ�״̬����
        {
            GroundedMovement();
        }
        ApplyGravity();
    }
    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis* moveSpeed, moveSpeed * Time.deltaTime);//MoveTowards(a,b,c) a��b ������c ����������ƽ���ƶ�
        if (rigidbody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0;
        }
        if (velocity.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        if(velocity.x<0||(inputAxis*velocity.x<0))
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);//����״̬
        jumping = velocity.y > 0f;//�ж��Ƿ�������Ծ
        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
            velocity.y = jumpForce;//ʩ����
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");//�ж�����״̬
        float mul = falling ? 2f : 1f;//��������״̬�����ʵ���������
        velocity.y += Gravity * mul * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, Gravity / 2f);//������ÿ֡���м�������������������������һ����ֵ��������������
    }

    private void FixedUpdate()//��������һ����fixedUpdate �и���  �����м������ٶ���������ʵ��Ӧ��
    {
        Vector2 position = rigidbody.position;

        position += velocity * Time.fixedDeltaTime;

        Vector2 Leftedge = camera.ScreenToWorldPoint(Vector2.zero);//��ȡ��������Ե����
        Vector2 Rightedge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));//��ȡ������ұ�Ե����
        position.x = Mathf.Clamp(position.x, Leftedge.x+0.5f, Rightedge.x+0.5f);
        rigidbody.MovePosition(position);//��������µ��ƶ���ʽ����ֱ��ͨ��ʩ������ʵ���ƶ�

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(collision.transform.DotTest(transform, Vector2.up))
            { velocity.y = jumpForce / 1.75f; }
            
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }
}
