using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextKiller : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Die());
    }

    private void Update()
    {
        transform.position=new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
       
    }
}
