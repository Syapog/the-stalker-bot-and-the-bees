using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBee : MonoBehaviour
{
    [HideInInspector]
    public Vector3[] Route;

    [HideInInspector]
    public bool ok = false;

    private float velocity;         // Скорость полета пчелы
    private float timeDeltaFly;     // Время полета между двумя точками на основе расстояния и заданной скорости
    private int count;              // Количество точек маршрута в массиве Route
    private int index;              // Текущий индекс элемента маршрута, от которого идет перемещение
    private float va, vb;           // Диапазон разброса скорости полета пчел
    private float a, b;             // Диапазон разброса вокруг целевой точки маршрута
    Vector3 temp1, temp2;           // Для учета значения разброса вокруг целевой точки маршрута


    // Следование пчелы по прямому маршруту
    private IEnumerator Moving()
    {
        temp2 = new Vector3(Random.Range(a, b), Random.Range(a, b), Random.Range(a, b));              // Создаем некоторый разброс вокруг целевой точки маршрута

        timeDeltaFly = Vector3.Distance(Route[index] + temp1, Route[index + 1] + temp2) / velocity;   // Вычисляем время полета между двумя точками по расстоянию и скорости полета      
        transform.rotation = Quaternion.LookRotation(Route[index + 1] + temp2 - transform.position);  // Поворачиваем пчелу по направлению движения

        for (float i = 0; i <= timeDeltaFly; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(Route[index] + temp1, Route[index + 1] + temp2, i / timeDeltaFly);
            yield return null;
        }
        transform.position = Route[index + 1] + temp2;
        index++;

        if (index < count - 1)
        {
            temp1 = temp2;              // На следующем отрезке полет должен начаться от текущей точки положения пчелы с учетом разброса
            StartCoroutine(Moving());
        }

        if (index == count - 1)
        {
            temp1 = temp2;
            StartCoroutine(MovingReverse());  // Запускаем полет пчелы по обратному маршруту
            StopCoroutine(Moving());
        }
    }


    // Следование пчелы по обратному маршруту
    private IEnumerator MovingReverse()
    {
        if (index == 1) temp2 = new Vector3(0f, 0f, 0f);                                              // Для конечной точки маршрута разброса не должно быть - пчела залетает в улей
        else temp2 = new Vector3(Random.Range(a, b), Random.Range(a, b), Random.Range(a, b));      // Создаем некоторый разброс вокруг целевой точки маршрута

        timeDeltaFly = Vector3.Distance(Route[index] + temp1, Route[index - 1] + temp2) / velocity;   // Вычисляем время полета между двумя точками по расстоянию и скорости полета      
        transform.rotation = Quaternion.LookRotation(Route[index - 1] + temp2 - transform.position);  // Поворачиваем пчелу по направлению движения

        for (float i = 0; i <= timeDeltaFly; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(Route[index] + temp1, Route[index - 1] + temp2, i / timeDeltaFly);
            yield return null;
        }
        transform.position = Route[index - 1] + temp2;
        index--;

        if (index > 0)
        {
            temp1 = temp2;
            StartCoroutine(MovingReverse());
        }

        if (index == 0) Destroy(gameObject);  // Уничтожаем вернувшуюся в улей пчелу
    }


    private void Update()
    {
        if (ok)
        {
            ok = false;
            index = 0;
            a = -0.5f; b = 0.5f;
            va = 1f; vb = 1.5f;
            velocity = Random.Range(va, vb);
            temp1 = new Vector3(0f, 0f, 0f);
            count = Route.Length;
            StartCoroutine(Moving());
        }
    }

}
