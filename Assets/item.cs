using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gamemanager.Instance.PlusScore();
        GameObject.Destroy(gameObject);
    }
}
