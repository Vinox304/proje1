using UnityEngine;

public class Nest : MonoBehaviour
{
    public int color;
    private Renderer renderer;
    private static int colorCounter = 0;
    private static readonly int[] colors = { 0, 1, 2, 3, 4 };
    private static int nestCount = 0;

    void Start()
    {
        renderer = GetComponent<Renderer>();


        color = colors[Random.Range(0, colors.Length)];
        AssignColor(color);

        nestCount++;
        if (nestCount == 3)
        {
            // If all 3 nests are created, reset the colors randomly
            colors[0] = Random.Range(0, 5);
            colors[1] = Random.Range(0, 5);
            colors[2] = Random.Range(0, 5);
        }
    }

    void AssignColor(int color)
    {
        Color colorValue;
        switch (color)
        {
            case 0:
                colorValue = new Color(0.956f, 0.769f, 0.188f);
                break;
            case 1:
                colorValue = Color.red;
                break;
            case 2:
                colorValue = Color.blue;
                break;
            case 3:
                colorValue = Color.green;
                break;
            case 4:
                colorValue = new Color(1f, 0.302f, 0f);
                break;
            default:
                colorValue = Color.white;
                break;
        }
        renderer.material.color = colorValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Movable"))
        {
            ColorManager movableColor = other.GetComponent<ColorManager>();
            if (movableColor != null && movableColor.myColor == color)
            {

                Destroy(other.gameObject);


                AssignColor(Random.Range(0, 5));


                UpdateAllNests();
            }
        }
    }

    private void UpdateAllNests()
    {

        Nest[] nests = FindObjectsOfType<Nest>();
        foreach (Nest nest in nests)
        {
            nest.AssignColor(nest.color);
        }
    }
}