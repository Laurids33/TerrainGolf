using UnityEngine;
using TMPro;

public class Spieler1 : MonoBehaviour
{
    Rigidbody rb;
    readonly float bremsFaktor = 0.995f;
    readonly float grenzGeschwindigkeit = 10;
    Vector3 downPosition;
    bool schlagBegonnen = false;
    int schlagAnzahl = 0;
    public TextMeshProUGUI schlagAnzeige;

    readonly Vector3[] ziel = new Vector3[7];
    int zielIndex = 0;

    Vector3 startPosition = new Vector3(260, 22, 100);
    public Landschaft1 landschaft1;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ziel[0] = new Vector3(100, 22, 300);
        ziel[1] = new Vector3(100, 34, 700);
        ziel[2] = new Vector3(500, 34, 700);
        ziel[3] = new Vector3(500, 34, 100);
        ziel[4] = new Vector3(800, 64, 100);
        ziel[5] = new Vector3(800, 64, 900);
        ziel[6] = new Vector3(100, 64, 900);
    }

    void Update()
    {
        rb.linearVelocity *= bremsFaktor;

        if (rb.linearVelocity.magnitude < grenzGeschwindigkeit)
        {
            if (Input.GetMouseButtonDown(0) && !schlagBegonnen)
            {
                downPosition = Input.mousePosition;
                schlagBegonnen = true;
            }

            if (Input.GetMouseButtonUp(0) && schlagBegonnen)
            {
                Vector3 schlag = Input.mousePosition - downPosition;
                schlag.z = schlag.y;
                schlag.y = 0;
                rb.AddForce(50 * schlag);
                schlagAnzahl++;
                schlagBegonnen = false;
                schlagAnzeige.text = $"Anzahl: {schlagAnzahl}";
            }
        }

        if (
            transform.position.x < 0 ||
            transform.position.x > 1000 ||
            transform.position.z < 0 ||
            transform.position.z > 1000 ||
            transform.position.y > 100
        )
        {
            rb.linearVelocity = new Vector3(0, 0, 0);
            if (zielIndex == 0)
            {
                transform.position = startPosition;
            }
            else
            {
                transform.position = ziel[zielIndex - 1];
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ziel"))
        {
            if (zielIndex <= 5)
            {
                zielIndex++;
                collision.gameObject.transform.position = ziel[zielIndex];
            }
            else
            {
                Destroy(collision.gameObject);
                schlagAnzeige.text = "Gewonnen Anzahl: " + schlagAnzahl;
            }
        }
    }
}
