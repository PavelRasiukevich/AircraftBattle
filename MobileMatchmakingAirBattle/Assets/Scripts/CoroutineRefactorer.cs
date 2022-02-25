using System.Collections;
using UnityEngine;

public class CoroutineRefactorer : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(nameof(Custom), 1.0f);
    }

    private IEnumerator Custom(float duration)
    {
        var cachedWaitForSeconds = new WaitForSeconds(duration);

        yield return cachedWaitForSeconds;
        print("Tick");
    }
}
