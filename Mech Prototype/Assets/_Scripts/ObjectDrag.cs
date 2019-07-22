using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectDrag : MonoBehaviour
{
    [Header("ПАРАМЕТРЫ")]
    public string type = "PlankSpawner";
    public float rotSpeed = 60f; // Скорость вращения

    // Необходимые компоненты
    protected Rigidbody rb;
    protected Renderer rnd;

    protected GameObject trash;

    // Состояние перетаскивания и выбрасывания
    protected bool drag = false;
    protected bool destroy = false;
   
    // Трекинг курсора
    protected Vector3 mouseOffset;
    protected float mouseZ;

    // Контроллеры родительского элемента
    protected GameObject spawner;
    protected CreateObject crob;

    // Управление цветом при коллизии
    protected Material mainMat;
    protected Color mainColor;

    private void Awake()
    {
        rnd = gameObject.GetComponent<Renderer>();
        mainMat = rnd.material;
        mainColor = rnd.material.color;
        rb = gameObject.GetComponent<Rigidbody>();
        trash = TrashSetup.trashcan;
        crob = GameObject.Find(type).GetComponent<CreateObject>();
        
    }

    private void Update()
    {
        // Вращение перетаскиваемого объекта на клавиши\
        // !!! переделать для телефонов
        if (drag && Input.GetKey("a"))
        {
            transform.Rotate(new Vector3(0, 0, 1) * 50f * Time.deltaTime);
        }
        else if (drag && Input.GetKey("d"))
        {
            transform.Rotate(new Vector3(0, 0, 1) * -50f * Time.deltaTime);
        }
    }



    //НННННННННННННННННННННННН       ОБРАБОТКА СОБЫТИЙ МЫШИ       НННННННННННННННННННННННН  
    private void OnMouseDown()
    {
        mouseZ = Camera.main.WorldToScreenPoint(transform.position).z;
        mouseOffset = transform.position - MouseWorldPos();
        trash.SetActive(true);
    }

    // Основная функция перетаскивания 
    private void OnMouseDrag()
    {
        transform.position = MouseWorldPos() + mouseOffset;

        drag = true;
        rb.isKinematic = false;
    }

    private void OnMouseUp()
    {
        drag = false;
        
        // Если отпущено во время коллизии, то переместить на свободное место выше, если выше нельзя, то начинает двигаться вправо
        // !!! Пока что теоретически уйти в бесконечный цикл, нужно придумать в каком случае удалять объект, чтобы он не искал себе место вечно
        if(rnd.material.color == Color.red)
        {
            bool quit = false;
            while (!quit)
            {
                Collider[] hit = Physics.OverlapBox(transform.position, new Vector3(2.5f, 0.25f, 1f));
                if (hit.Length > 0)
                {
                    Vector3 pos = transform.position;
                    pos.y += 1f;
                    if (pos.y > 5.8f)
                    {
                        pos.y = -5.2f;
                        pos.x += 0.5f;
                           if(pos.x > 16)
                        {
                            pos.x = -16;
                        }
                    }
                    transform.position = pos;
                }
                else quit = true;
            } 
        }
        rb.isKinematic = true;
        trash.SetActive(false);

        if (destroy)
        {
            crob.addOne();
            Destroy(gameObject);
        }
    }

    Vector3 MouseWorldPos()
    {
        Vector3 pointer = Input.mousePosition;
        pointer.z = mouseZ;

        return Camera.main.ScreenToWorldPoint(pointer);
    }

    //НННННННННННННННННННННННН       КОНЕЦ ОБРАБОТКИ МЫШИ       НННННННННННННННННННННННН        




    // ОБРАБОТКА КОЛЛИЗИЙ
    private void OnCollisionEnter(Collision collision)
    {
        if (drag) rnd.material.color = Color.red;
        
    }

    private void OnCollisionExit(Collision collision)
    {
        rnd.material = mainMat;
        rnd.material.color = mainColor;
    }


    // ОБРАБОТКА ВЫБРАСЫВАНИЯ

    private void OnTriggerEnter(Collider other)
    {
        destroy = true;
    }

    private void OnTriggerExit(Collider other)
    {
        destroy = false;
    }
}
