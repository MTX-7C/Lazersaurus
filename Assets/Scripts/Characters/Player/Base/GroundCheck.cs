using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GroundCheck : MonoBehaviour
{
    public Player player;
    public LayerMask layerMask;

    public List<GameObject> gameObjects = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            player.isGrounded = true;
            gameObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            gameObjects.Remove(other.gameObject);
        }
        if (gameObjects.Count <= 0)
        {
            player.isGrounded = false;
        }
    }
}
