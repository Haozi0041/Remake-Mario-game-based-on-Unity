using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRenderer : MonoBehaviour
{
    public Sprite[] sprites;  //存放渲染动画数组
    public float framerate = 1f / 6f;//渲染帧率  （间隔）

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animation), framerate, framerate);//该函数可以以固定间隔调用指定函数
    }

    private void OnDisable()
    {
        CancelInvoke();//取消调用
    }

    private void Animation()//循环切换动画显示
    {
        frame++;
        if (frame >= sprites.Length)
        {
            frame = 0;
        }
        if(frame>=0&&frame<sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }

    }


}
