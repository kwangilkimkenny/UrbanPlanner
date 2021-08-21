using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    [SerializeField] public GameObject gioPointPrefab;

    private Camera cam;



    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        SpawnAtMousePos();


    }

    int spawnCounter = 0;

    public void SpawnAtMousePos()
    {
        

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {
                
                GameObject spawnPoint = Instantiate(gioPointPrefab, hit.point, Quaternion.identity);
                spawnPoint.name = "gioPosition" + spawnCounter;
                spawnCounter++;
            }
        }
    }

}
