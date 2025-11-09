using UnityEngine;

public class BackgroundCamera : MonoBehaviour
{
    [SerializeField] Transform camMain;
    [SerializeField] Transform camBackground;

    Vector3 lastPos;
    Vector3 change;

    void FixedUpdate()
    {
        change = camMain.position - lastPos;
        lastPos = camMain.position;
        camBackground.position = new Vector3(0, camMain.position.y - 5.76f, camBackground.position.z + (change.x * -1));
        change = new Vector3(0,0,0);
    }
}
