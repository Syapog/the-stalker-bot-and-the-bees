using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorr : MonoBehaviour
{
    private Animator _animator = null;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider collider)
    {
        _animator.SetBool("isOpened", true);
    }
    void OnTriggerExit(Collider collider)
    {
        _animator.SetBool("isOpened", false);
    }


    //public GameObject Door;

    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.F))
    //        Door.GetComponent<Animator>().Play(OpenDoor);
    //}




    ////void OnTriggerEnter(Collider col)
    ////{
    ////	if (col.tag == "Player")
    ////	{
    ////		Door.GetComponent<Animation>().Play(OpenDoor);
    ////	}
    ////}
}
