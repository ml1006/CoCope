using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpriteMaskScript : MonoBehaviour
{
    public static GameObject target;

    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("GrayCircle");
    }

    void Update()
    {
        if (target == null) return;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform g = transform.GetChild(i);

            MaterialPropertyBlock mbp = new MaterialPropertyBlock();

            SpriteRenderer spriteRenderer = g.GetComponent<SpriteRenderer>();
            TilemapRenderer tilemapRenderer = g.GetComponent<TilemapRenderer>();

            if (spriteRenderer != null) spriteRenderer.GetPropertyBlock(mbp);
            else tilemapRenderer.GetPropertyBlock(mbp);

            mbp.SetFloat("_RenderDistance", GameManager.instance.distance);
            mbp.SetFloat("_MaskTargetX", target.transform.position.x);
            mbp.SetFloat("_MaskTargetY", target.transform.position.y);

            if (spriteRenderer != null) spriteRenderer.SetPropertyBlock(mbp);
            else tilemapRenderer.SetPropertyBlock(mbp);
        }
    }
}
