using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level
{
    public string Name;
    public int IntValue;
    public string strValue;
    public string[] Passwords;

    public Level (string name, string[] passwords)
    {
        Name = name;
        Passwords = passwords;
    }
}

public class Hacker : MonoBehaviour
{
    // Game configuration data
    private readonly Level[] Levels =
    {
        new Level("Wikipedia", new string[] { "history", "science", "law", "math", "music" }),
        new Level("Google", new string[] { "gmail", "drive", "calendar", "youtube", "translate", "search" }),
        new Level("Path of Exile", new string[] { "slayer", "gladiator", "champion", "assassin", "saboteur", "trickster", "juggernaut", "berserker", "chieftain", "necromancer", "occultist", "elementalist", "deadeye", "raider", "pathfinder", "inquisitor", "hierophant", "guardian", "ascendant" })
    };

    enum Screen
    {
        MainMenu,
        Password,
        Win
    }

    // Game State
    Screen CurrentScreen;
    Level CurrentLevel;
    string CurrentPassword;

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
        for (int i = 0; i < Levels.Length; i++)
        {
            Terminal.WriteLine($"Enter {i} for {Levels[i].Name}");
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
        else if (CurrentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        if (int.TryParse(input, out int index) && Levels.ElementAtOrDefault(index) != null)
        {
            CurrentLevel = Levels[index];
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
        int randInt = Random.Range(0, CurrentLevel.Passwords.Length);
        CurrentPassword = CurrentLevel.Passwords[randInt];

        Terminal.ClearScreen();
        Terminal.WriteLine("Please enter your password: ");
    }

    void CheckPassword(string input)
    {
        if (input.ToLower() == CurrentPassword)
        {
            Terminal.WriteLine("Congratulations!");
        } 
        else
        {
            Terminal.WriteLine("Please enter another password.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
