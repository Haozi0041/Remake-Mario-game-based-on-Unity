
using UnityEngine;


public static class Extensions
{
    private static LayerMask laymask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rig ,Vector2 direction)// 向一个方向投射检测碰撞
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
    public static bool DotTest(this Transform transform,Transform other ,Vector2 direction)//进行点积运算判断 得出在某一方向上的速度与碰撞的向量 的点积来判断
    {
        Vector2 dir = other.position - transform.position;
        return Vector2.Dot(dir.normalized, direction) > 0.25f;//越靠近1 越接近这个向量   越靠近0 越垂直
    }
}
