using TMPro;
using UnityEngine;

public class YSorting : MonoBehaviour
{

    void Update()
    {
        float order = -(transform.position.y * 100);
        this.GetComponent<SpriteRenderer>().sortingOrder = (int)order;

    }

}
