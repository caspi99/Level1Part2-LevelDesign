using UnityEngine;

public class MolinoGirar : MonoBehaviour
{
    public float velocidadRotacion = 100f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
    }
}
