using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamraControl : MonoBehaviour
{
    [SerializeField]
    public float camzoom = 0.75f;

    [SerializeField]
    private Transform oa;
    [SerializeField]
    private Transform ob;

    // Update is called once per frame
    void Update()
    {
        Vector3 avg = (oa.position + ob.position) * 0.5f;
        avg.z = -5.0f;

        float dist = Vector3.Distance(oa.position, ob.position);
        dist = dist < 4.0f ? 4.0f : dist;

        GetComponent<Camera>().orthographicSize = dist * camzoom;
        transform.position = avg;
    }
}
