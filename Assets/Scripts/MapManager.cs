using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {


    public static MapManager Instance;

    public GameObject unitRock;
    public GameObject unitWall;

    private float rockScale;
    private float yPosition;
    private Dictionary<string, GameObject> rocks;


    void Awake() {
        Instance = this;
        rocks = new Dictionary<string, GameObject>();
        rockScale = unitRock.transform.localScale.x;
        yPosition = unitRock.transform.position.y;
        InitializeMap(100);
    }

    private void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    void InitializeMap(int dim) {
        for (int x = 0; x < dim; x++) {
            for (int z = 0; z < dim; z++) {
                if (x == 0 || x == dim - 1 || z == 0 || z == dim - 1) {
                    //Instantiate(
                    //    unitWall,
                    //    new Vector3((float)x * rockScale, yPosition, (float)z * rockScale),
                    //    Quaternion.identity);
                } else {
                    GameObject rock = Instantiate(
                        unitRock,
                        new Vector3((float)x * rockScale, yPosition, (float)z * rockScale),
                        Quaternion.identity);
                    rock.name = "UnitRock" + " (" + x + "," + z + ")";
                    rocks.Add(rock.name, rock);
                }
            }
        }
    }


    public void DestroyRocks(List<string> names) {
        Debug.Log("length " + names.Count);
        foreach (string name in names) {
            if (rocks[name] != null) {
                rocks[name].SetActive(false);
            }
        }
    }


}

