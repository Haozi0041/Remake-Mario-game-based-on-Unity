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
    public float MaxJumpHight = 5f;//高度
    public float MaxJumpTime = 1f;//时间


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
        rigidbody = GetComponent<Rigidbody2D>();//获取刚体
        camera = Camera.main;//获取主相机
    }
    private void Update()
    {
        HorizontalMovement();
        Grounded = rigidbody.Raycast(Vector2.down);//判断着地状态
        if (Grounded)//进行着地状态设置
        {
            GroundedMovement();
        }
        ApplyGravity();
    }
    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis* moveSpeed, moveSpeed * Time.deltaTime);//MoveTowards(a,b,c) a以b 增量向c 靠近，用于平滑移动
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
        velocity.y = Mathf.Max(velocity.y, 0f);//限制状态
        jumping = velocity.y > 0f;//判断是否正在跳跃
        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
            velocity.y = jumpForce;//施加力
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");//判断下落状态
        float mul = falling ? 2f : 1f;//根据下落状态给予适当重力增益
        velocity.y += Gravity * mul * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, Gravity / 2f);//由于是每帧进行计算提升下落重力，这里设置一个阈值，以免重力过大
    }

    private void FixedUpdate()//刚体物理一般在fixedUpdate 中更新  将进行计算后的速度向量进行实际应用
    {
        Vector2 position = rigidbody.position;

        position += velocity * Time.fixedDeltaTime;

        Vector2 Leftedge = camera.ScreenToWorldPoint(Vector2.zero);//获取摄像机左边缘坐标
        Vector2 Rightedge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));//获取摄像机右边缘坐标
        position.x = Mathf.Clamp(position.x, Leftedge.x+0.5f, Rightedge.x+0.5f);
        rigidbody.MovePosition(position);//由于马里奥的移动方式，不直接通过施加力来实现移动

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
