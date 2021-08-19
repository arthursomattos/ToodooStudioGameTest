using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenTouched : MonoBehaviour
{
    public void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            StartCoroutine(WaitToBeDestroyed());
        }
    }

    IEnumerator WaitToBeDestroyed()
    {
        yield return new WaitForSeconds(1.4f);
        Destroy(gameObject);
    }
}
