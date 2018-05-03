using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementSpeed = 10f;
    public float rotationTime = 2f;

    private Direction facing = Direction.Right;
    private int rotationCount = 0;
    private Rigidbody rb;
    private LinkedList<int> movingActions;
    private Transform camTransform;
    private Vector3 camOffset;
    private Transform bodyTransform;

    public GameObject lightSource;
    public GameObject target;
    public GameObject Shoulder;
    private ArmController armController; 
    private Animation armWaving;
    private GameObject weapon;


    enum Direction {
        Front = 0,
        Back = 1,
        Left = 2,
        Right = 3
    }

    private Dictionary<int, Vector3> InputToVelocity = new Dictionary<int, Vector3> {
        {0, new Vector3(0f, 0f, 1f) },
        {1, new Vector3(0f, 0f, -1f) },
        {2, new Vector3(-1f, 0f, 0f) },
        {3, new Vector3(1f, 0f, 0f) }
    };

    private Dictionary<int, float> DirectionAngles = new Dictionary<int, float>() {
        { 0, 270f },
        { 1, 90f },
        { 2, 0f},
        { 3, 180f},
    };

    private List<int> directions = new List<int> { 0, 1, 2, 3 };
    private bool[] buttonDowns;
    private bool[] buttonUps;

    void Start() {
        camTransform = Camera.main.transform;
        camOffset = camTransform.position - transform.position;
        rb = GetComponent<Rigidbody>();
        armController = Shoulder.GetComponent<ArmController>();
        movingActions = new LinkedList<int>();
        buttonDowns = new bool[] { false, false, false, false };
        buttonUps = new bool[] { false, false, false, false };
        bodyTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update() {
        Actions();
        Move();
        CameraFollow();
    }

    private void Actions() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            lightSource.SetActive(!lightSource.activeSelf);
        }

        armController.attacking = Input.GetKey(KeyCode.Space);
    }

        private Vector3 Velocity() {

        buttonDowns[0] = Input.GetKeyDown(KeyCode.UpArrow);
        buttonDowns[1] = Input.GetKeyDown(KeyCode.DownArrow);
        buttonDowns[2] = Input.GetKeyDown(KeyCode.LeftArrow);
        buttonDowns[3] = Input.GetKeyDown(KeyCode.RightArrow);

        buttonUps[0] = Input.GetKeyUp(KeyCode.UpArrow);
        buttonUps[1] = Input.GetKeyUp(KeyCode.DownArrow);
        buttonUps[2] = Input.GetKeyUp(KeyCode.LeftArrow);
        buttonUps[3] = Input.GetKeyUp(KeyCode.RightArrow);

        foreach (int direction in directions) {
            if (buttonDowns[direction]) {
                if (movingActions.Contains(direction)) {
                    movingActions.Remove(direction);
                }
                movingActions.AddFirst(direction);
            }
            if (buttonUps[direction]) {
                movingActions.Remove(direction);
            }
        }

        if (movingActions.Count != 0) {
            return InputToVelocity[movingActions.First.Value];
        } else {
            return Vector3.zero;
        }
    }

    private void Move() {

        Vector3 inputs = Velocity();
        float hor = inputs.x;
        float vert = inputs.z;

        rb.velocity = new Vector3(hor * movementSpeed, rb.velocity.y, vert * movementSpeed);

        if (vert > 0 && !(facing == Direction.Front)) {
            StartCoroutine(TurnAround(rotationCount, 0));
            facing = Direction.Front;
        } else if (vert < 0 && !(facing == Direction.Back)) {
            StartCoroutine(TurnAround(rotationCount, 1));
            facing = Direction.Back;
        }

        if (hor > 0 && !(facing == Direction.Right)) {
            StartCoroutine(TurnAround(rotationCount, 2));
            facing = Direction.Right;
        } else if (hor < 0 && !(facing == Direction.Left)) {
            StartCoroutine(TurnAround(rotationCount, 3));
            facing = Direction.Left;
        }
        rb.angularVelocity = Vector3.zero;
    }

    private void CameraFollow() {
        camTransform.position = camOffset + transform.position;
    }

    IEnumerator TurnAround(int count, int dir) {
        rotationCount++;
        float target = DirectionAngles[dir];
        float offset = 0.01f;
        for (var t = 0f; t < 1 && rotationCount == count + 1; t += Time.deltaTime / rotationTime) {
            bodyTransform.rotation = Quaternion.Lerp(bodyTransform.rotation, Quaternion.Euler(0, target + offset, 0), t);
            yield return null;
        }
        if (rotationCount == count + 1) {
            bodyTransform.rotation = Quaternion.Euler(0, target, 0);
            rb.angularVelocity = Vector3.zero;
        }
    }

}
