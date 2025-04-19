using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRenderer : MonoBehaviour
{
    public Sprite[] sprites;  //�����Ⱦ��������
    public float framerate = 1f / 6f;//��Ⱦ֡��  �������

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animation), framerate, framerate);//�ú��������Թ̶��������ָ������
    }

    private void OnDisable()
    {
        CancelInvoke();//ȡ������
    }

    private void Animation()//ѭ���л�������ʾ
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
