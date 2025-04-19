
using UnityEngine;


public static class Extensions
{
    private static LayerMask laymask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rig ,Vector2 direction)// ��һ������Ͷ������ײ
    {
        if (rig.isKinematic)
        {
            return false;
        }
        float radius = 0.25f;
        float distance = 0.375f;
        //RaycastHit2D hit = Physics2D.CircleCast(rig.position, radius, direction.normalized, distance);
        RaycastHit2D hit = Physics2D.CircleCast(rig.position, radius, direction.normalized, distance,laymask);
        return hit.collider != null && hit.rigidbody != rig;
    }
    public static bool DotTest(this Transform transform,Transform other ,Vector2 direction)//���е�������ж� �ó���ĳһ�����ϵ��ٶ�����ײ������ �ĵ�����ж�
    {
        Vector2 dir = other.position - transform.position;
        return Vector2.Dot(dir.normalized, direction) > 0.25f;//Խ����1 Խ�ӽ��������   Խ����0 Խ��ֱ
    }
}
