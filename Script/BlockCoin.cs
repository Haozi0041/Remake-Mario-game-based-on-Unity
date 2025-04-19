using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{

    private void Start()
    {
        GameManager.Instance.AddCoins();
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Vector3 resPosition = transform.localPosition;
        Vector3 upPosition = resPosition + Vector3.up * 2f;
        yield return Move(resPosition, upPosition);
        yield return Move(upPosition, resPosition);
        Destroy(gameObject);
    }


    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.2f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = to;
    }

}
