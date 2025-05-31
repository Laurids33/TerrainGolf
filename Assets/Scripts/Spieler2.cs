using UnityEngine;
using TMPro;

public class Spieler2 : MonoBehaviour
{
    Rigidbody rb;
    readonly float bremsFaktor = 0.995f;
    readonly float grenzGeschwindigkeit = 10;
    Vector3 downPosition;
    bool schlagBegonnen = false;
    int schlagAnzahl = 0;
    public TextMeshProUGUI schlagAnzeige;

    readonly Vector3[] ziel = new Vector3[9];
    int zielIndex = 0;

    Vector3 startPosition = new Vector3(940, 34, 50);
    public Landschaft2 landschaft2;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ziel[0] = new Vector3(940, 46, 150);
        ziel[1] = new Vector3(60, 58, 250);
        ziel[2] = new Vector3(940, 70, 350);
        ziel[3] = new Vector3(60, 82, 450);
        ziel[4] = new Vector3(940, 94, 550);
        ziel[5] = new Vector3(60, 106, 650);
        ziel[6] = new Vector3(940, 118, 750);
        ziel[7] = new Vector3(60, 130, 850);
        ziel[8] = new Vector3(940, 142, 950);
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
            transform.position.y > 250
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
            if (zielIndex <= 7)
            {
                zielIndex++;
                collision.gameObject.transform.position = ziel[zielIndex];
                Debug.Log(zielIndex);
            }
            else
            {
                Destroy(collision.gameObject);
                schlagAnzeige.text = "Gewonnen Anzahl: " + schlagAnzahl;
            }
        }
    }
}
