using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ItemData item;
    private float radius = 0.1f;

    // appelé avant la première frame
    private void Start()
    {
        UnityEngine.Vector2 vec = new UnityEngine.Vector2(transform.position.x, transform.position.y);

        Collider2D hit = Physics2D.OverlapCircle(vec, radius);
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        //Debug.Log(hit.gameObject.tag);

        if (hit != null && hit.gameObject.tag != "Collectible")
        {
            player.inventory.Add(this);
            Destroy(gameObject);
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = item.imageMap;
    } // Start()

    // lorsque le joueur entre dans la zone du coffre
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player && item != null && item.isCollectable)
        {
            player.inventory.Add(this);
            Destroy(gameObject);
        }
    } // OnTriggerEnter2D(Collider2D)

    // set l'item
    public void setItem(ItemData i)
    {
        this.item = i;
    } // setItem(ItemData)
}

