using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject ammo;             // будет хранить ссылку на боеприпас (снаряд) - укажем сюда префаб
    public GameObject pointSpawnAmmo;   // позиция появления боеприпаса (снаряда)
    //[HideInInspector]
    private float speed = 50;           // скорость полета снаряда


    void CreateAmmo()
    {
        Vector3 ammoPosition = pointSpawnAmmo.transform.position;  // позиция появления боеприпаса (снаряда)
        Vector3 ammoForce;  // направление силы, которая будет применена к объекту

        // определяем вектор, по которому должен полететь боеприпас (снаряд)
        float x = pointSpawnAmmo.transform.position.x - transform.position.x;
        float y = pointSpawnAmmo.transform.position.y - transform.position.y;
        float z = pointSpawnAmmo.transform.position.z - transform.position.z;

        ammoForce = new Vector3(x, y, z);

        // создаем объект с помощью Instantiate и сохраняем его в переменную createAmmo
        GameObject createAmmo = Instantiate(ammo, ammoPosition, transform.rotation) as GameObject;

        // применяем силу к боеприпасу (снаряду)
        createAmmo.GetComponent<Rigidbody>().AddForce(ammoForce * speed, ForceMode.Impulse);
    }



    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F)) CreateAmmo();
    }
}
