using UnityEngine;

public class Slot : MonoBehaviour
{
    public CardColor slotColor; 

    public bool CanAcceptCard(Card card)
    {
        return card.Color == this.slotColor;
    }

   
    public void AssignColor(CardColor newColor)
    {
        slotColor = newColor;

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = ConvertCardColorToUnityColor(newColor);
        }
    }

    private Color ConvertCardColorToUnityColor(CardColor cardColor)
    {
        switch (cardColor)
        {
            case CardColor.Red: return Color.red;
            case CardColor.Blue: return Color.blue;
            case CardColor.Orange: return new Color(1f, 0.5f, 0f); 
            case CardColor.Purple: return new Color(0.5f, 0f, 0.5f);
            case CardColor.Yellow: return Color.yellow;
            case CardColor.None: return Color.white;
            default: return Color.white;
        }
    }
}
