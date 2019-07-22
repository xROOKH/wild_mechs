using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCheck : MonoBehaviour
{
    // Проверка коллизии с финишным объектом
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
