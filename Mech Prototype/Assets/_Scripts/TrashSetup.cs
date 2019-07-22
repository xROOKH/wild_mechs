using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSetup : MonoBehaviour
{

    public static GameObject trashcan;
    // Сохранение ссылки на корзину для дочерних объектов на сцене
    void Start()
    {
        trashcan = GameObject.Find("Trash");
        trashcan.SetActive(false);
    }


}
