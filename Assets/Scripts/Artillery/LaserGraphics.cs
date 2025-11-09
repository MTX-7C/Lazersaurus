using UnityEngine;
using System.Collections.Generic;
public class LaserGraphics : MonoBehaviour
{
    LineRenderer lineRenderer;
    public int fps = 12;
    public List<Texture2D> textures = new List<Texture2D> ();
    float t = 0;
    int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.SetTexture("_MainTex", textures[index]);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > (float) 1 / fps)
        {
            
            index++;
            if (index == textures.Count) index = 0;
            lineRenderer.material.SetTexture("_MainTex", textures[index]);

            t = 0;
        }
    }
}
