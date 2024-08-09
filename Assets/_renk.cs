using UnityEngine;
using System.Collections.Generic;

public class _renk : MonoBehaviour
{
    public int benimRengim;
    public Camera mainCamera;
    private GameObject selectedObject;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool hasSelected = false;

    private GameObject[] plates;
    private List<Transform> stackedObjects = new List<Transform>(); 
    void Start()
    {
        Renderer yuzey = GetComponent<Renderer>();
        benimRengim = Random.Range(0, 5);
        renkAta(benimRengim, yuzey);

       
        plates = GameObject.FindGameObjectsWithTag("Plate");
    }

    void renkAta(int renk, Renderer yuzey)
    {
        yuzey.material = new Material(Shader.Find("Specular"));
        switch (renk)
        {
            case 0:
                Color sarýrenk = new Color(0.956f, 0.769f, 0.188f);
                yuzey.material.SetColor("_Color", sarýrenk);
                break;
            case 1:
                Color kýrmýzýreng = Color.red;
                yuzey.material.SetColor("_Color", kýrmýzýreng);
                break;
            case 2:
                Color mavirenk = Color.blue;
                yuzey.material.SetColor("_Color", mavirenk);
                break;
            case 3:
                Color yesilrenk = Color.green;
                yuzey.material.SetColor("_Color", yesilrenk);
                break;
            case 4:
                Color turuncurenk = new Color(1f, 0.302f, 0f);
                yuzey.material.SetColor("_Color", turuncurenk);
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
