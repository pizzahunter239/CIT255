using System;
using System.Collections.Generic;
using System.Linq;

namespace MauiHangman
{
    public class HangmanGame
    {
        private string _wordToGuess;
        private HashSet<char> _guessedLetters = new();
        public int MaxAttempts { get; private set; } = 6;
        public int AttemptsLeft { get; private set; }

        public string CurrentGuess { get; private set; }
        public bool GameOver => AttemptsLeft == 0 || _wordToGuess == CurrentGuess;
        public string WordToGuess => _wordToGuess;

        public HangmanGame(string wordToGuess)
        {
            _wordToGuess = wordToGuess.ToLower();
            AttemptsLeft = MaxAttempts;
            UpdateCurrentGuess();
        }

        public bool GuessLetter(char letter)
        {
            letter = char.ToLower(letter);

            if (_guessedLetters.Contains(letter))
                return false; // Already guessed

            _guessedLetters.Add(letter);

            if (_wordToGuess.Contains(letter))
            {
                UpdateCurrentGuess();
                return true;
            }
            else
            {
                AttemptsLeft--;
                return false;
            }
        }

        private void UpdateCurrentGuess()
        {
            CurrentGuess = new string(_wordToGuess.Select(c => _guessedLetters.Contains(c) ? c : '_').ToArray());
        }
    }
}