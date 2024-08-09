using UnityEngine;

public class Yuva : MonoBehaviour
{
    public int renk; 
    private Renderer renderer;
    private static int renkSayaci = 0;
    private static readonly int[] renkler = { 0, 1, 2, 3, 4 };
    private static int yuvaSayisi = 0;

    void Start()
    {
        renderer = GetComponent<Renderer>();

       
        renk = renkler[Random.Range(0, renkler.Length)];
        renkAta(renk);

        yuvaSayisi++;
        if (yuvaSayisi == 3)
        {
            
            renkler[0] = Random.Range(0, 5);
            renkler[1] = Random.Range(0, 5);
            renkler[2] = Random.Range(0, 5);
        }
    }

    void renkAta(int renk)
    {
        Color color;
        switch (renk)
        {
            case 0:
                color = new Color(0.956f, 0.769f, 0.188f); 
                break;
            case 1:
                color = Color.red; 
                break;
            case 2:
                color = Color.blue; 
                break;
            case 3:
                color = Color.green; 
                break;
            case 4:
                color = new Color(1f, 0.302f, 0f);
                break;
            default:
                color = Color.white; 
                break;
        }
        renderer.material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Movable"))
        {
            _renk movableRenk = other.GetComponent<_renk>();
            if (movableRenk != null && movableRenk.benimRengim == renk)
            {
                
                Destroy(other.gameObject);

                
                renkAta(Random.Range(0, 5)); 

               
                UpdateAllYuvas();
            }
        }
    }

    private void UpdateAllYuvas()
    {
        
        Yuva[] yuvalar = FindObjectsOfType<Yuva>();
        foreach (Yuva yuva in yuvalar)
        {
            yuva.renkAta(yuva.renk);
        }
    }
}
