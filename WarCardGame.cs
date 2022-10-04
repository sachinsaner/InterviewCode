using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingPractice
{
    public class Player
    {
        public List<int> Cards { get; set; }
        public int  Score { get; set; }
        private int index;

        public string Name { get; set; }
        public int CurrentCard { get; set; }
        public bool IsOut { get; set; }

        public int GetNextCard()
        {
            if(index < Cards.Count)
            {
                this.CurrentCard = Cards[index++];
                return this.CurrentCard;
            }
            this.IsOut = true;
            return -1;
        }
            
        public Player()
        {
            this.Cards = new List<int>();
        }
    }

    public class WarCardGameMultiPlayer
    {
        private List<Player> players;
        private List<int> cards;
        private Random random;

        public WarCardGameMultiPlayer(List<Player> players, List<int> cards)
        {
            this.cards = cards;
            this.players = players;
            this.random = new Random();
            
            this.ShuffleCard();
            this.AllocateCards();
        }

        private void ShuffleCard()
        {
            cards = cards.OrderBy(c => random.Next()).ToList();
        }

        private void AllocateCards()
        {
            int playerIndex = 0;
            int cardIndex = 0;

            while(cardIndex < cards.Count)
            {
                this.players[playerIndex++].Cards.Add(cards[cardIndex++]);
                if(playerIndex > players.Count - 1)
                {
                    playerIndex = 0;
                }
            }
        }

        public void PlayGame()
        {
            while(players.Count > 1)
            {
                PlayGameRound();
            }

            Console.WriteLine("Winner is {0} with Score {1}", players[0].Name, players[0].Score);
        }

        private void PlayGameRound()
        {
            int index = 0;
            List<Player> outPlayers = new List<Player>();
            foreach(var p in players)
            {
                if (!p.IsOut)
                {
                    p.GetNextCard();
                }
                else
                {
                    outPlayers.Add(p);
                }
                index++;
            }

            foreach(var p in outPlayers)
            {
                players.Remove(p);
            }

            this.players = this.players.OrderByDescending(c => c.CurrentCard).ToList();
            int max = this.players[0].CurrentCard;
                        
            for(int i = 0; i < this.players.Count; i++)
            {
                if(players[i].CurrentCard == max)
                {
                    players[i].Score++;
                }
            }                     
        }
    }

    
    public class WarCardGame2Players
    {
        List<int> cards;
        Random random;

        List<int> player_1_cards;
        List<int> player_2_cards;

        public WarCardGame2Players()
        {
            random = new Random();
            cards = new List<int>();
            for(int i = 0; i < 52; i++)
            {
                cards.Add(i + 1);
            }
        }

        private void ShuffleDeck()
        {
            cards = cards.OrderBy(x => random.Next()).ToList();
        }

        public void AllocateCards()
        {
            this.ShuffleDeck();

            player_1_cards = new List<int>(cards.GetRange(0, cards.Count / 2));
            player_2_cards = new List<int>(cards.GetRange((cards.Count / 2), cards.Count / 2));                    
        }

        public void PlayGame()
        {
            AllocateCards();

            int player1_index = 0;
            int player2_index = 0;
            int player1_score = 0;
            int player2_score = 0;

            while (player1_index < player_1_cards.Count && player2_index < player_2_cards.Count)
            {
                int player_1_card = player_1_cards[player1_index++];
                int player_2_card = player_2_cards[player2_index++];

                if(player_1_card > player_2_card)
                {
                    player1_score+=2;
                }
                if (player_1_card < player_2_card)
                {
                    player2_score += 2;
                }

                // if equal then they keep playing ?
            }

            if(player1_score > player2_score)
            {
                Console.WriteLine("Player 1 is winner: " + player1_score);
            }
            else if (player1_score < player2_score)
            {
                Console.WriteLine("Player 2 is winner: " + player2_score);
            }

        }

    }
}
