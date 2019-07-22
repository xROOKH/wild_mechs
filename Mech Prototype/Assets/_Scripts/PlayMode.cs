using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMode : MonoBehaviour
{
    public Rigidbody playerRB;
    protected Vector3 startingPoint;

    private void Start()
    {
        // Запомнить начальное положение шара на поле
        startingPoint = playerRB.gameObject.transform.position;
    }
    void Update()
    {
        // Войти в игровой режим
        if (Input.GetKey("p"))
        {
            
            playerRB.useGravity = true;
            playerRB.isKinematic = false;
        }
        if (playerRB.gameObject.transform.position.y<-16)
        {
            playerRB.isKinematic = true;
            playerRB.useGravity = false;
            playerRB.position = startingPoint;
        }
    }
}
