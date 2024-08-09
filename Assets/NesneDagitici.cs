using UnityEngine;

public class NesneDagitici : MonoBehaviour
{
    public GameObject[] objects;
    public float spacing = 0.5f; 

    void Start()
    {
        GameObject[] plates = GameObject.FindGameObjectsWithTag("Plate");

        if (plates.Length == 0 || objects.Length == 0)
        {
            Debug.LogWarning("Tabakalar veya nesneler bulunamadý.");
            return;
        }

        int objectsPerPlate = objects.Length / plates.Length;
        int extraObjects = objects.Length % plates.Length;
        int objectIndex = 0;

        foreach (GameObject plate in plates)
        {
            int objectsToPlace = objectsPerPlate + (extraObjects > 0 ? 1 : 0);
            extraObjects--;

            for (int i = 0; i < objectsToPlace; i++)
            {
                if (objectIndex >= objects.Length)
                    break;

                Vector3 position = plate.transform.position + Vector3.up * (i * spacing);
                objects[objectIndex].transform.position = position;

                objectIndex++;
            }
        }
    }
}
