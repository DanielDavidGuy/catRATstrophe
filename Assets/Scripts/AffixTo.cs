using UnityEngine;

public class AffixTo : MonoBehaviour
{
    [SerializeField] GameObject other;

    // Update is called once per frame
    void Update()
    {
        transform.position = other.transform.position;
    }
}
