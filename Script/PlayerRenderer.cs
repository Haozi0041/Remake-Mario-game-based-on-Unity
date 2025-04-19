using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    public  SpriteRenderer SpriteRenderer { get; private set; }
    private PlyerMovement movement;//���������ű���ȡ��ɫ״̬

    public AnimationRenderer run;  //���ö�����Ⱦ�ű�
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
        run.enabled = movement.running;//ͨ���ܲ�״̬  �������Ⱦ�ű�
        if(movement.jumping)
        {
            SpriteRenderer.sprite = jump;//��Ծ״̬��Ϊ����
        }
        else if (movement.sliding)
        {
            SpriteRenderer.sprite = slide;
        }
        else if(!movement.running)//��Ϊ��Ⱦ����Ϊ����ʽ  ��Ĭ��Ϊ�ܲ�״̬  ����Ҫ�Ӷ����ж�
        {
            SpriteRenderer.sprite = idle;
        }
    }

}
