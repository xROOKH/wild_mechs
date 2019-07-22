using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CreateObject : MonoBehaviour, IPointerClickHandler

{
    // Создаваемый объект
    public GameObject prefab;

    // Текстовый счетчик оставшихся элементов
    public TextMeshProUGUI counter;

    // Трекинг позиции курсора
    float mouseZ;
    Vector3 mouseOffset;

    // Созданный объект
    private GameObject instance;


    public void OnPointerClick(PointerEventData eventData)
    {
        int amount = int.Parse(counter.text);
        if (amount>0)
        {
            mouseZ = Camera.main.WorldToScreenPoint(transform.position).z;
            mouseOffset = transform.position - MouseWorldPos();
            prefab.transform.position = MouseWorldPos();
            instance = Instantiate<GameObject>(prefab);
            counter.text = "" + --amount;
        }
    }

    Vector3 MouseWorldPos()
    {
        Vector3 pointer = Input.mousePosition;
        pointer.z = mouseZ;

        return Camera.main.ScreenToWorldPoint(pointer);
    }

    private void OnMouseDrag()
    {
        instance.transform.position = MouseWorldPos() + mouseOffset;
    }

    public void addOne()
    {
        int amount = int.Parse(counter.text);
        counter.text = "" + ++amount;
    }
}
