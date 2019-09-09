using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level
{
    public string Name;
    public string WinPhrase;
    public string[] Passwords;

    public Level (string name, string winPhrase, string[] passwords)
    {
        Name = name;
        WinPhrase = winPhrase;
        Passwords = passwords;
    }
}

public class Hacker : MonoBehaviour
{
    // Game configuration data
    private readonly Level[] Levels =
    {
        new Level("Wikipedia", "Better get what you want quickly. This page will be probably be locked soon...", new string[] { "history", "science", "law", "math", "music" }),
        new Level("Google", "Google is always watching. Hopefully\nthey didn't notice...", new string[] { "gmail", "drive", "calendar", "youtube", "translate", "search" }),
        new Level("Path of Exile", "Run fast exile. Death is always lurkingjust behind you...", new string[] { "slayer", "gladiator", "champion", "assassin", "saboteur", "trickster", "juggernaut", "berserker", "chieftain", "necromancer", "occultist", "elementalist", "deadeye", "raider", "pathfinder", "inquisitor", "hierophant", "guardian", "ascendant" })
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
    const int StartingGuesses = 3;
    int GuessesRemaining;
    const string MenuHint = "Type menu to return to the menu.";

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
        StopAllCoroutines();
        if (
            input.ToLower() == "menu" || // We can always go to main menu
            CurrentScreen == Screen.Win)  // Any input on win screen takes us back to main menu
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
            SetRandomPassword();
            AskForPassword();
        }
        else
        {
            ShowMainMenu();
        }
    }

    void AskForPassword()
    {
        CurrentScreen = Screen.Password;
        Terminal.ClearScreen();
        Terminal.WriteLine($"{MenuHint}\n");
        Terminal.WriteLine("Enter your password: ");
        Terminal.WriteLine($"--> Hint: {CurrentPassword.Anagram()}");
        Terminal.WriteLine($"--> Guesses Remaining: {GuessesRemaining}");
        
    }

    void SetRandomPassword()
    {
        GuessesRemaining = StartingGuesses;
        int randInt = Random.Range(0, CurrentLevel.Passwords.Length);
        CurrentPassword = CurrentLevel.Passwords[randInt];
    }

    void CheckPassword(string input)
    {
        GuessesRemaining -= 1;

        if (input.ToLower() == CurrentPassword)
        {
            DisplayWinScreen();
        } 
        else if (GuessesRemaining > 0)
        {
            AskForPassword();
        }
        else 
        {
            SetRandomPassword();
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        CurrentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        Terminal.WriteLine($"You hacked into {CurrentLevel.Name}!");
        Terminal.WriteLine("");
        Terminal.WriteLine(CurrentLevel.WinPhrase);
        StartCoroutine(WriteMatrixText());
    }

    IEnumerator WriteMatrixText()
    {
        yield return new WaitForSeconds(5);
        Terminal.ClearScreen();
        while (true)
        {
            System.Guid g = System.Guid.NewGuid();
            Terminal.WriteLine(g.ToString());
            yield return new WaitForSeconds(.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
