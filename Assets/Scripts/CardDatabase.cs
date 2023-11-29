using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    private void Awake()
    {
        cardList.Add(new Card(0, "Labour Rush", "Hire 3 workers for 3 rounds"));
        cardList.Add(new Card(1, "Wheat Season", "Double the Wheat production for 2 rounds"));
        cardList.Add(new Card(2, "Wheat Alchemy", "Use Wheat to cover the difference of another resource to fulfill the next contract"));
        cardList.Add(new Card(3, "Animal Fasting", "The animals don't need to eat Wheat for 3 rounds"));
        cardList.Add(new Card(4, "Regulation Cheating", "Cost no actions to build this round, +1 action"));
        cardList.Add(new Card(5, "Overworking", "Cost no actions to manage workers this round, +1 action"));
        cardList.Add(new Card(6, "Brainstorming", "Shuffle the special cards"));
        cardList.Add(new Card(7, "Temporary Storage", "Get 120 extra storage space for 3 rounds"));
        cardList.Add(new Card(8, "Animal Growth", "Increase the animals amount one extra time"));
        cardList.Add(new Card(9, "Union Crackdown", "Workers don't need to be paid for 3 rounds"));
        cardList.Add(new Card(10, "Money Printer", "Get 300 coins"));

    }

}
