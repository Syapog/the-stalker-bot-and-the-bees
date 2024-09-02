using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnPointBee : MonoBehaviour
{
    private const int N = 3;
    private Vector3 pos;

    private Vector3[][] Route = new Vector3[N][]; // Маршруты следования пчел - N маршрутов до M точек в каждом (ниже задано 3 маршрута, от 3 до 5 точек в каждом)   
    public GameObject Bee;                        // Префаб пчелы


    // Создание пчелы в точке спавна
    private void CreateBee()
    {
        int Num = Random.Range(0, N);                       // Генерируем номер маршрута для создаваемой пчелы
        GameObject Bee_OBJ = Instantiate(Bee, transform.position, transform.rotation) as GameObject;
        Bee_OBJ.GetComponent<FlyBee>().Route = Route[Num];  // Задаем маршрут пчелы согласно сгенерированному номеру
        Bee_OBJ.GetComponent<FlyBee>().ok = true;           // Разрешаем начать обработку полета пчелы (нужно, чтобы успел передаться маршрут)
    }


    private IEnumerator StartCreate()
    {
        while (true)
        {
            CreateBee();
            int temp = Random.Range(1, 5);
            yield return new WaitForSeconds(temp);
        }
    }


    void Start()
    {
        pos = gameObject.transform.position;  // Начальная позиция пчелы совпадает с точкой спавна

        // Задаем каждый из возможных маршрутов полета пчелы
        Route[0] = new Vector3[4] { pos, new Vector3(2f, 8.5f, -20f), new Vector3(2.8f, 9f, -18.8f), new Vector3(1f, 8.5f, -17f) };
        Route[1] = new Vector3[3] { pos, new Vector3(-0.2f, 8f, -21f), new Vector3(-1f, 9f, -22f), };
        Route[2] = new Vector3[5] { pos, new Vector3(2f, 8f, -21f), new Vector3(3f, 9f, -22f), new Vector3(5f, 8f, -21f), new Vector3(4f, 9f, -20f) };

        StartCoroutine(StartCreate());
    }
}
