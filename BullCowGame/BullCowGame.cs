using System;
using System.Collections.Generic;

namespace BullCowGame
{
    public class BullCowGame
    {
        private string sHidden_word;


        private int iBulls;
        private int iCows;

        public enum GuessStatus
        {
            Invalid,
	        OK,
	        Not_Isogram,
	        Wrong_Lenght
        };

        private int iMaxTries;
        private bool bIsGameWon;
        private int iCurrentTry;

        public string SHidden_word { get => sHidden_word; set => sHidden_word = value; }
        public int IMaxTries { get => iMaxTries; set => iMaxTries = value; }
        public bool BIsGameWon { get => bIsGameWon; set => bIsGameWon = value; }
        public int ICurrentTry { get => iCurrentTry; set => iCurrentTry = value; }
        public int IBulls { get => iBulls; set => iBulls = value; }
        public int ICows { get => iCows; set => iCows = value; }

        public bool IsIsogram(string Guess)
        {
            //treat 0 or 1 letter words as isgorams
            if (Guess.Length <= 1)
            {
                return true;
            }

            //using a key value pair, to cut down the run-time comparison time from O(n^2) [nested for loops, comparing each character] to O(n*log(n))
            Dictionary<char, bool> LetterSeen = new Dictionary<char, bool>();

            foreach (var letter in Guess)
            {
                if (LetterSeen[letter]) // if the letter is "already seen"
                {
                    return false; //we don't have an isogram
                }
                else
                {
                    LetterSeen[letter] = true; //adding the letter to the already seen list
                }
            }

            return true;
        }

        public void SubmitValidGuess(string Guess)
        {
            iCurrentTry++;

            for (int HiddenWordChar = 0; HiddenWordChar < sHidden_word.Length; HiddenWordChar++)
            {
                for (int GuessChar = 0; GuessChar < sHidden_word.Length; GuessChar++)
                {
                    if (Guess[GuessChar] == sHidden_word[HiddenWordChar])
                    {
                        if (HiddenWordChar == GuessChar)
                        {
                            iBulls++;
                        }
                        else
                        {
                            iCows++;
                        }
                    }
                }
            }

            if (iBulls == sHidden_word.Length)
            {
                bIsGameWon = true;
            }
            else
            {
                bIsGameWon = false;
            }
            
        }

        
        public GuessStatus CheckGuessVailidity(string Guess)
        {
            if (! IsIsogram(Guess))
	        {
                return GuessStatus.Not_Isogram;
            }
            else if (Guess.Length != SHidden_word.Length)
            {
                return GuessStatus.Wrong_Lenght;
            }
            else
            {
                return GuessStatus.OK;
            }
        }


        //Constructor sets inital values for game-state
        public BullCowGame (string HiddenWord,int difficulty)
        {
            iBulls = 0;
            iCows = 0;

            if (difficulty <= 3)
            {
                iMaxTries = 2;
            }
            else
            {
                iMaxTries = Convert.ToInt32(difficulty / 2);
            }

            sHidden_word = HiddenWord;
            bIsGameWon = false;
            iCurrentTry = 1;
        }
    }
}
