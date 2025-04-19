using UnityEngine;
/// <summary>
/// ������������  ���ô�����
/// </summary>
public class Deathbarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.RestLevel(2f);

        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
