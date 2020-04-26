using System;
namespace LibDuelistsOfTheRoses.Interfaces.Data
{
    public interface ICardList
    {
        IDORCard p_DefaultCard { get; set; }

        IDORCard[] p_CardList { get; set; }

        /// <summary>
        /// Returns an array of random cards. The source is p_CardList.
        /// </summary>
        /// <param name="size">Length of array</param>
        /// <returns>An int[] of card indexes. The indexes are from the p_CardList</returns>
        int[] GetRandomCards(int size);

        /// <summary>
        /// Get a card by p_CardList index
        /// </summary>
        /// <param name="cardID">Index, preferrably also IDORCard.CardNumber</param>
        /// <returns>IDORCard or null if it doesn't exist.</returns>
        IDORCard GetCardByID(int cardID);
    }
}
