using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiHangman
{
    public partial class MainPage : ContentPage
    {
        private HangmanGame _game;
        private Random _random = new Random();
        private HashSet<char> _guessedLetters = new HashSet<char>();

        private List<string> _words = new List<string>
        {
            "galaxy", "nebula", "asteroid", "comet", "meteor",
            "planet", "saturn", "jupiter", "mercury", "venus",
            "telescope", "astronaut", "rocket", "shuttle", "station",
            "orbit", "lunar", "solar", "eclipse", "crater",
            "universe", "cosmos", "stellar", "supernova", "pulsar",
            "gravity", "satellite", "mission", "launch", "capsule"
        };

        public MainPage()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            string randomWord = _words[_random.Next(_words.Count)];

            _game = new HangmanGame(randomWord);
            _guessedLetters.Clear();

            PlayAgainButton.IsVisible = false;
            GameOverMessage.IsVisible = false;
            EnableLetterButtons();
            UpdateUI();
        }

        private void UpdateUI()
        {
            WordDisplay.Text = string.Join(" ", _game.CurrentGuess.ToUpper().ToCharArray());
            AttemptsLabel.Text = $"Attempts Left: {_game.AttemptsLeft}";

            if (_guessedLetters.Count == 0)
            {
                GuessedLettersLabel.Text = "Used: None";
            }
            else
            {
                GuessedLettersLabel.Text = $"Used: {string.Join(", ", _guessedLetters.OrderBy(c => c).Select(c => c.ToString().ToUpper()))}";
            }

            int incorrectGuesses = _game.MaxAttempts - _game.AttemptsLeft;
            UpdateHangmanImage(incorrectGuesses);

            if (_game.GameOver)
            {
                if (_game.CurrentGuess == _game.WordToGuess.ToLower())
                {
                    GameOverMessage.Text = "You Won!";
                    GameOverMessage.TextColor = Colors.Green;
                }
                else
                {
                    GameOverMessage.Text = $"Game Over! The word was: {_game.WordToGuess.ToUpper()}";
                    GameOverMessage.TextColor = Colors.Red;
                }

                GameOverMessage.IsVisible = true;
                DisableLetterButtons();
                PlayAgainButton.IsVisible = true;
            }
        }

        private void UpdateHangmanImage(int incorrectGuesses)
        {
            string imageName = $"hangman{incorrectGuesses}.gif";
            HangmanImage.Source = ImageSource.FromFile(imageName);
        }

        private void DisableLetterButtons()
        {
            foreach (var view in LetterGrid.Children)
            {
                if (view is Button button)
                {
                    button.IsEnabled = false;
                }
            }
        }

        private void EnableLetterButtons()
        {
            foreach (var view in LetterGrid.Children)
            {
                if (view is Button button)
                {
                    button.IsEnabled = true;
                }
            }
        }

        private void LetterButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            char letter = button.Text[0];

            _guessedLetters.Add(char.ToLower(letter));

            button.IsEnabled = false;

            bool isCorrect = _game.GuessLetter(letter);

            UpdateUI();
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            StartNewGame();
        }
    }
}