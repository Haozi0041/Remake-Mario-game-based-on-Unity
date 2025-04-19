using System.Collections;

using UnityEngine;

public class Pole : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;

    public float movespeed = 6f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            StartCoroutine(MoveTo(flag, (poleBottom.position-new Vector3 (0.5f,-0.75f,0f))));
            StartCoroutine(Leave(collision.transform));
        }
    }

    private IEnumerator MoveTo(Transform subject,Vector3 destination)
    {
        while (Vector3.Distance(subject.position, destination) > 0.5f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, movespeed * Time.deltaTime);
            yield return null;
        }
        subject.position = destination;
    }

    private IEnumerator Leave(Transform player)
    {
        player.GetComponent<PlyerMovement>().enabled = false;
        
        yield return MoveTo(player, poleBottom.position+Vector3.up);
        yield return MoveTo(player, player.position+Vector3.right);
        yield return MoveTo(player, player.position + Vector3.right+Vector3.down);
        yield return MoveTo(player, castle.position+Vector3.right);

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        GameManager.Instance.RestLevel();
    }

}
