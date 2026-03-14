using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject pickUpPrefab;
    private GameObject[] pickUps;
    private int totalPickUps = 50;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        pickUps = new GameObject[totalPickUps];
        for (int i = 0; i < totalPickUps; i++)

        {
            pickUps[i] = Instantiate(pickUpPrefab);
            pickUps[i].transform.position = new Vector3(

                Random.Range(-9f, 9f),
                1f,
                Random.Range(-9f, 9f)

            );
        }

        SetCountText();
        winTextObject.SetActive(false);
    }
    
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= totalPickUps)
        {
            winTextObject.SetActive(true);
        }
    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

       rb.AddForce(movement * speed); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }
}
