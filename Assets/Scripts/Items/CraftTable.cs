using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTable : MonoBehaviour
{
    private bool isTrigger;
    [SerializeField]
    private GameObject craftTableUI;
    [SerializeField]
    private GameObject canvas;
    private Player player;
    private bool isOpen;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            isTrigger = true;
            isOpen=false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isOpen)
        {
            craftTableUI.SetActive(false);
            isOpen = false;
        }
        isTrigger = false;
    }

    void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                craftTableUI.SetActive(false);
                isOpen = false;
            }
            else
            {
                craftTableUI.SetActive(true);
                isOpen = true;
            }
    }        

    }
}
