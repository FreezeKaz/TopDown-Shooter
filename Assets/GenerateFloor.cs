using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFloor : MonoBehaviour
{
    [SerializeField] private Transform Container;
    [SerializeField] private GameObject floor;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(floor, new Vector3(i * 5 - 20, j * 5 - 20, 0), Quaternion.Euler(new Vector3(0, 0 , Random.Range(0, 3) * 90)), Container);
            }
        }
    }


}
