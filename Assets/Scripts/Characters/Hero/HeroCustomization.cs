using UnityEngine;

public class HeroCustomization : MonoBehaviour
{
    [SerializeField] GameObject[] headObjects;
    [SerializeField] GameObject[] tailObjects;
    [SerializeField] GameObject[] backObjects;

    GameObject[] activeObjects = new GameObject[3];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pickup")
        {
            CustomizItem customizItem = collision.gameObject.GetComponent<CustomizItem>();
            if(customizItem.bodyLoc == 0)
            {
                activeObjects[0].SetActive(false);
                activeObjects[0] = headObjects[customizItem.activeObject];
                activeObjects[0].SetActive(true);
            }
            else if(customizItem.bodyLoc == 1)
            {
                activeObjects[1].SetActive(false);
                activeObjects[1] = headObjects[customizItem.activeObject];
                activeObjects[1].SetActive(true);
            }
            else if(customizItem.bodyLoc == 2)
            {
                activeObjects[2].SetActive(false);
                activeObjects[2] = headObjects[customizItem.activeObject];
                activeObjects[2].SetActive(true);
            }
        }
    }
}
