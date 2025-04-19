using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 该脚本决定所有实体的基础移动 例如板栗仔 蘑菇 等
/// </summary>
public class EntityMove : MonoBehaviour
{


    public float speed = 1f;
    public Vector2 direction = Vector2.left;//定义统一 默认方向 为左边
    
    
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;//初始化禁用该组件 当然也可以在项目中手动禁用
    }
    private void OnBecameVisible()//当物体对象进入摄像机视野后调用   激活脚本
    {
        enabled = true;
    }
    private void OnBecameInvisible()//当物体对象离开摄像机视野后调用   关闭脚本
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
