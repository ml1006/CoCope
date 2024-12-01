using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollison : MonoBehaviour
{
    private bool col_inter = false;
    private int col_id= -1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Interaction"))
        {
            col_inter = true;
            col_id = collision.transform.GetComponent<InteractionData>().id;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Interaction"))
        {
            col_inter = false;
            col_id = -1;
        }
    }

    public bool CollidesWithInteraction()
    {
        return col_inter;
    }
    public int ColliderId()
    {
        return col_id;
    }
}
