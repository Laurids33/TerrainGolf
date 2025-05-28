using UnityEngine;

public class Landschaft : MonoBehaviour
{
    void Start()
    {
        Terrain terrainKomponente = GetComponent<Terrain>();
        TerrainData terrainDaten = terrainKomponente.terrainData;
        int aufloesung = terrainDaten.heightmapResolution;
        float[,] hMap = new float[aufloesung, aufloesung];

        // Drei Ebenen
        for (int z = 0; z < aufloesung; z++)
        {
            for (int x = 0; x < aufloesung; x++)
            {
                if (z < 300 && x < 200)
                {
                    hMap[z, x] = 0.02f;
                }
                else if (z < 400 && x < 300)
                {
                    hMap[z, x] = 0.04f;
                }
                else
                {
                    hMap[z, x] = 0.06f;
                }
            }
        }

        // Rand links und rechts
        for (int z = 0; z < aufloesung; z++)
        {
            for (int x = 0; x < 5; x++)
            {
                hMap[z, x] = 0.08f;
            }
            for (int x = aufloesung - 5; x < aufloesung; x++)
            {
                hMap[z, x] = 0.08f;
            }
        }

        // Rand unten und oben
        for (int x = 0; x < aufloesung; x++)
        {
            for (int z = 0; z < 5; z++)
            {
                hMap[z, x] = 0.08f;
            }
            for (int z = aufloesung - 5; z < aufloesung; z++)
                hMap[z, x] = 0.08f;
        }

        // Externe Rampe in Z-Richtung
        for (int z = 250; z <= 300; z++)
        {
            for (int x = 100; x <= 150; x++)
            {
                hMap[z, x] = 0.02f + 0.02f * (z - 250) / 50;
            }
        }

        // Interne Rampe in X-Richtung
        for (int z = 100; z <= 150; z++)
        {
            for (int x = 300; x <= 350; x++)
            {
                hMap[z, x] = 0.04f + 0.02f * (x - 300) / 50;
            }
        }

        terrainDaten.SetHeights(0, 0, hMap);
    }
}
