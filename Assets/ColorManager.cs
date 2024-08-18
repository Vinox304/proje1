using UnityEngine;
using System.Collections.Generic;

public class ColorManager : MonoBehaviour
{
    public int myColor;
    public Camera mainCamera;
    private GameObject selectedObject;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool hasSelected = false;

    private GameObject[] plates;
    private List<Transform> stackedObjects = new List<Transform>();

    void Start()
    {
        Renderer surface = GetComponent<Renderer>();
        myColor = Random.Range(0, 5);
        AssignColor(myColor, surface);

        // Find plates
        plates = GameObject.FindGameObjectsWithTag("Plate");
    }

    void AssignColor(int color, Renderer surface)
    {
        surface.material = new Material(Shader.Find("Specular"));
        switch (color)
        {
            case 0:
                Color yellowColor = new Color(0.956f, 0.769f, 0.188f);
                surface.material.SetColor("_Color", yellowColor);
                break;
            case 1:
                Color redColor = Color.red;
                surface.material.SetColor("_Color", redColor);
                break;
            case 2:
                Color blueColor = Color.blue;
                surface.material.SetColor("_Color", blueColor);
                break;
            case 3:
                Color greenColor = Color.green;
                surface.material.SetColor("_Color", greenColor);
                break;
            case 4:
                Color orangeColor = new Color(1f, 0.302f, 0f);
                surface.material.SetColor("_Color", orangeColor);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hasSelected)
                {
                    if (hit.collider.CompareTag("Plate") || hit.collider.CompareTag("Movable"))
                    {
                        targetPosition = hit.collider.transform.position;
                        isMoving = true;
                        hasSelected = false;
                    }
                }
                else if (hit.collider.CompareTag("Movable"))
                {
                    selectedObject = hit.collider.gameObject;
                    hasSelected = true;
                }
            }
        }

        if (isMoving && selectedObject != null)
        {
            selectedObject.transform.position = Vector3.MoveTowards(selectedObject.transform.position, targetPosition, Time.deltaTime * 5f);

            if (Vector3.Distance(selectedObject.transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                StackObject(selectedObject.transform, targetPosition);
                selectedObject = null;
            }
        }
    }

    void StackObject(Transform objectToStack, Vector3 position)
    {
        Vector3 stackPosition = position;
        if (stackedObjects.Count > 0)
        {
            Transform lastStackedObject = stackedObjects[stackedObjects.Count - 1];
            stackPosition = lastStackedObject.position + Vector3.up * lastStackedObject.localScale.y;
        }

        objectToStack.position = stackPosition;


        stackedObjects.Add(objectToStack);
    }
}