using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBonds : MonoBehaviour
{
    SpwanObjects spawnPool;

    private void Start()
    {
        spawnPool = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SpwanObjects>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Static"))
        {
            other.gameObject.SetActive(false);
            spawnPool.objectStatic.Enqueue(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Double"))
        {
            other.gameObject.SetActive(false);
            spawnPool.doubleBox.Enqueue(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Move"))
        {
            other.gameObject.SetActive(false);
            spawnPool.moveObj.Enqueue(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Hole"))
        {
            other.gameObject.SetActive(false);
            spawnPool.holeStatic.Enqueue(other.gameObject);
        }
    }


}
