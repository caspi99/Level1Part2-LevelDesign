using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleTag : MonoBehaviour
{
    public List<GameObject> objetosActivos = new List<GameObject>();

    void Update()
    {
        objetosActivos.RemoveAll(obj => obj == null);

        if (objetosActivos.Count == 0)
        {
            this.tag = "DestructibleItem";
        }
    }
}
