using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public List<Slot> slots; 
    public List<CardColor> availableColors; 

    void Start()
    {
        AssignRandomColorsToSlots();
    }

    void AssignRandomColorsToSlots()
    {
       
        List<CardColor> selectedColors = new List<CardColor>();

        while (selectedColors.Count < slots.Count)
        {
            CardColor randomColor = availableColors[Random.Range(0, availableColors.Count)];
            if (!selectedColors.Contains(randomColor))
            {
                selectedColors.Add(randomColor);
            }
        }

    
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].AssignColor(selectedColors[i]);
        }
    }
}
