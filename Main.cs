using FinalBattlee;
using FinalBattlee.UnitActions;
using FinalBattlee.Units;
using FinalBattlee.Items;



// REVAMP TIME


GameSetup();

void GameSetup()
{

    List<Party> enemyEncounters = new List<Party>();   // for multiple fights in a row

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("It's time for the Final Battlee. Yeah, that's two 'e's. What is your name, True Programmer?");               // there is no input validation for this part, but there is for the rest of the game

    string nameInput = Console.ReadLine();
    TrueProgrammer programmer = new TrueProgrammer(nameInput);

    Console.WriteLine("\nThis can be played with Players or Computers, or both. Here are your options: ");
    Console.WriteLine("#1 Two Computer Players fight each other.\n#2 One Player vs One Computer.\n#3 Two Players fight each other.");
    Console.Write("What is your choice? ");
    int input = Convert.ToInt16(Console.ReadLine());

    if (input == 1) // computer vs computer. I will make this easy and just have it be 2 skeletons duking it out.
    {
        Player player1 = new Player(PlayerType.Computer);
        Player player2 = new Player(PlayerType.Computer);

        Party party1 = new Party("Computer 1", player1);
        Party party2 = new Party("Computer 2", player2);

        Skeleton skeleton = new Skeleton();      
        party1.Add(skeleton);

        Skeleton skeleton2 = new Skeleton();
        party2.Add(skeleton2);

        Battle battle = new Battle(party1, party2);
    }
    else if (input == 2) // human vs computer (this is the actual core challenge)
    {
        Player player1 = new Player(PlayerType.Human);
        Player player2 = new Player(PlayerType.Computer);

        Party mainParty = new Party("True Programmer", player1);
        mainParty.Inventory.Add(new HealthPotion());               // add health potions to hero party here
        mainParty.Inventory.Add(new HealthPotion());
        mainParty.Add(programmer);

        Party party1 = new Party("Computer 1", player2);
        Skeleton skeleton = new Skeleton();
        party1.Add(skeleton);


        Party party2 = new Party("Computer 2", player2);
        Skeleton skeleton2 = new Skeleton();
        Skeleton skeleton3 = new Skeleton();
        party2.Add(skeleton2);
        party2.Add(skeleton3);


        Party party3 = new Party("Computer 3", player2);

        UncodedOne uncodedOne = new UncodedOne();
        party3.Add(uncodedOne);

        enemyEncounters.Add(party1);
        enemyEncounters.Add(party2);
        enemyEncounters.Add(party3);

        foreach (Party party in enemyEncounters)         // this makes it so the fights are back to back. The fight should end when the True Programmer dies, but this has a failsafe just in case
        {
            if (mainParty.Units.Count == 0) { Console.WriteLine("True Programmer has lost. Not moving onto the next battle."); break; }
            else { Console.WriteLine("\n>>> Moving onto the next battle...\n"); }

            Console.WriteLine("------------------------------");
            Console.WriteLine("The next encounter will be: \n");
            foreach (Unit unit in party.Units) { Console.WriteLine($"{unit.Name}"); }
            Console.WriteLine("------------------------------\n");

            Battle battle = new Battle(mainParty, party);
        }
    }

    else if (input == 3)  // player vs player. I'll make 2 true programmers and maybe a skeleton and just have them go at it.
    {
        Player player1 = new Player(PlayerType.Human);
        Player player2 = new Player(PlayerType.Human);

        Party party1 = new Party("Player 1", player1);
        Party party2 = new Party("Player 2", player2);

        Console.WriteLine("\nThere are two True Programmers? Is that even possible? Well, whatever.");
        Console.WriteLine("What is the name of the second True Programmer? ");

        string secondProgrammerName = Console.ReadLine();
        TrueProgrammer secondProgrammer = new TrueProgrammer(secondProgrammerName);

        party1.Add(programmer); party2.Add(secondProgrammer);

        Battle battle = new Battle(party1, party2);
    }
}
