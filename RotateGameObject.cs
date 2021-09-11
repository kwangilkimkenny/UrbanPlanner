using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGameObject : MonoBehaviour
{

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(new Vector3(0f, 2f, 0f));
    }
}
