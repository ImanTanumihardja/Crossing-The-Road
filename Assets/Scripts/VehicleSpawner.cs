using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject[] vehicles;
    public int max;

    private int count = 0;
    private float timer = 0.0f;
    private float lastTime = 0.0f;
    private float waitingTime = 2.0f;


    public GameObject targetContainer;
    public List<Transform> targets;

    public int minY;
    public int maxY;

    public int minZ;
    public int maxZ;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < targetContainer.transform.childCount; i++)
        {
            targets.Add(targetContainer.transform.GetChild(i));
        }

        SpawnVehicle();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer - lastTime >= waitingTime && count < max)
        {
            SpawnVehicle();
            lastTime = timer;
        }


    }

    void SpawnVehicle()
    {
        Vector3 newPos;
        GameObject go;
        int r;

        if (targets.Count == 0)
        {
            r = Random.Range(0, 2);

            go = Instantiate(vehicles[Random.Range(0, vehicles.Length)]);
            go.transform.SetParent(this.transform);

            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);

            

            if (go.name == "FlyingFastCar(Clone)")
            {
                go.GetComponent<CarMovement>().AddSpeed(Random.Range(10, 15));
            }
            else
            {
                go.GetComponent<CarMovement>().AddSpeed(Random.Range(0, 5));
            }

            if (r == 0)
            {
                //right
                go.GetComponent<CarMovement>().SetEndPos(-70);
                float x = 70;
                newPos = new Vector3(x, y, z);

                go.transform.Rotate(0, 180, 0);
            }
            else
            {
                //left
                go.GetComponent<CarMovement>().SetEndPos(90);
                float x = -70;
                newPos = new Vector3(x, y, z);
            }

        }
        else
        {
            r = Random.Range(0, targets.Count);
            
            go = Instantiate(vehicles[Random.Range(0, vehicles.Length)]);
            go.transform.SetParent(this.transform);

            float y = targets[r].position.y;
            float z = targets[r].position.z;
            float x = targets[r].position.x;

            if (go.name == "FlyingFastCar(Clone)")
            {
                go.GetComponent<CarMovement>().AddSpeed(Random.Range(10, 15));
            }
            else
            {
                go.GetComponent<CarMovement>().AddSpeed(Random.Range(0, 5));
            }

            if (targets[r].tag == "Right")
            {
                //right
                go.GetComponent<CarMovement>().SetEndPos(-90);
                newPos = new Vector3(x, y, z);
                go.transform.Rotate(0, 180, 0);
            }
            else
            {
                //left
                go.GetComponent<CarMovement>().SetEndPos(90);
                newPos = new Vector3(x, y, z);
            }
        }

        go.transform.position = newPos;

        targets.RemoveAt(r);

        count++;

    }
}
