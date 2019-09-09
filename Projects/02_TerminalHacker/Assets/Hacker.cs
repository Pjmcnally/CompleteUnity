using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Game State
    enum Screen
    {
        MainMenu,
        Password,
        Win
    }
    Screen CurrentScreen;

    enum Level
    {
        Wikipedia = 1,
        Google = 2,
        PathOfExile = 3
    }
    Level CurrentLevel;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        CurrentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Welcome to Terminal Hacker\n");
        Terminal.WriteLine("What would you like to hack into?");
        foreach (Level val in Level.GetValues(typeof(Level)))
        {
            Terminal.WriteLine($"For {val} press {(int) val}");
        }
        Terminal.WriteLine("\nEnter your selection:");
    }

    void OnUserInput(string input)
    {
        if (input.ToLower() == "menu")  // We can always go to main menu
        {
            ShowMainMenu();
        }
        else if (CurrentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
    }

    void RunMainMenu(string input)
    {
        if (input == "1")
        {
            CurrentLevel = Level.Wikipedia;
            StartGame();
        }
        else if (input == "2")
        {
            CurrentLevel = Level.Google;
            StartGame();
        }
        else if (input == "3")
        {
            CurrentLevel = Level.PathOfExile;
            StartGame();
        }
        else
        {
            Terminal.WriteLine("Please enter a valid input:");
        }

    }

    void StartGame()
    {
        CurrentScreen = Screen.Password;
        Terminal.WriteLine($"You have chosen level: {CurrentLevel}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
