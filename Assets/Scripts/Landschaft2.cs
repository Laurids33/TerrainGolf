using UnityEngine;

public class Landschaft2 : MonoBehaviour
{
    public int aufloesung;
    void Start()
    {
        Terrain terrainKomponente = GetComponent<Terrain>();
        TerrainData terrainDaten = terrainKomponente.terrainData;
        aufloesung = terrainDaten.heightmapResolution;
        float[,] hMap = new float[aufloesung, aufloesung];
        int faktor = 1;

        // Alle Ebenen
        for (int z = 0; z < aufloesung; z++)
        {
            // Zur naechsten Ebene
            if (z % 50 == 0)
            {
                faktor++;
            }

            // Ebene
            for (int x = 0; x < aufloesung; x++)
            {
                hMap[z, x] = 0.02f * faktor;
            }

            // Rand links
            for (int x = 0; x < 5; x++)
            {
                hMap[z, x] = 0.02f * faktor + 0.02f;
            }

            // Rand rechts
            for (int x = aufloesung - 5; x < aufloesung; x++)
            {
                hMap[z, x] = 0.02f * faktor + 0.02f;
            }
        }

        // Rampen links
        for (int k = 0; k < 5; k++)
        {
            for (int z = 100 * k + 25; z <= 100 * k + 75; z++)
            {
                for (int x = 50; x <= 100; x++)
                {
                    hMap[z, x] = 0.02f * (2 * k + 2) + 0.02f * (z - (100 * k + 25)) / 50;
                }
            }
        }

        // Rampen rechts
        for (int k = 0; k < 4; k++)
        {
            for (int z = 100 * k + 75; z <= 100 * k + 125; z++)
            {
                for (int x = 400; x <= 450; x++)
                {
                    hMap[z, x] = 0.02f * (2 * k + 2) + 0.02f * (z - (100 * k + 25)) / 50;
                }
            }
        }


        // Rand unten
        for (int z = 0; z < 5; z++)
        {
            for (int x = 0; x < aufloesung; x++)
            {
                hMap[z, x] = 0.06f;
            }
        }

        // Rand oben
        for (int z = aufloesung - 13; z < aufloesung; z++)
        {
            for (int x = 0; x < aufloesung; x++)
            {
                hMap[z, x] = 0.02f * faktor;
            }
        }

        terrainDaten.SetHeights(0, 0, hMap);

    }
}
