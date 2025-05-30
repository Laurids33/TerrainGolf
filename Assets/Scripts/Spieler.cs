using UnityEngine;
using TMPro;

public class Spieler : MonoBehaviour
{
    Rigidbody rb;
    readonly float bremsFaktor = 0.995f;
    readonly float grenzGeschwindigkeit = 10;
    Vector3 downPosition;
    bool schlagBegonnen = false;
    int schlagAnzahl = 0;
    public TextMeshProUGUI schlagAnzeige;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    }
}
