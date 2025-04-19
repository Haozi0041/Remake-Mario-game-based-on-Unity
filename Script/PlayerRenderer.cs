using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    public  SpriteRenderer SpriteRenderer { get; private set; }
    private PlyerMovement movement;//引用其他脚本读取角色状态

    public AnimationRenderer run;  //引用动画渲染脚本
    public Sprite idle;
    public Sprite jump;
    public Sprite slide;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlyerMovement>();
    }

    private void OnEnable()
    {
        SpriteRenderer.enabled = true;
    }
    private void OnDisable()
    {
        SpriteRenderer.enabled = false;
        run.enabled = false;
    }
    private void LateUpdate()
    {
        run.enabled = movement.running;//通过跑步状态  激活动画渲染脚本
        if(movement.jumping)
        {
            SpriteRenderer.sprite = jump;//跳跃状态更为优先
        }
        else if (movement.sliding)
        {
            SpriteRenderer.sprite = slide;
        }
        else if(!movement.running)//因为渲染机制为覆盖式  即默认为跑步状态  所以要加额外判断
        {
            SpriteRenderer.sprite = idle;
        }
    }

}
