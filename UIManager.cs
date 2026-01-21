using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalBattlee.Units;
using FinalBattlee.UnitActions;
using FinalBattlee.Items;

namespace FinalBattlee
{
    class UIManager
    {
        public Battle Battle;
        public UIManager(Battle battle)
        {
            Battle = battle;
        }

        public void PrintGameStatus()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("===========================================");
            Console.WriteLine("===========================================");
            foreach (Unit unit in Battle.FirstParty.Units)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{unit.Name} ({unit.CurrentHP} / {unit.MaxHP})");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine("===========================================");
            foreach (Unit unit in Battle.SecondParty.Units)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"                            {unit.Name} ({unit.CurrentHP} / {unit.MaxHP})");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine("===========================================");
            Console.WriteLine("===========================================");
        }

        public UnitAction GetUnitAction(Unit unit)
        {
            if (unit.Party == Battle.FirstParty) { Console.ForegroundColor = ConsoleColor.Green; } else {  Console.ForegroundColor = ConsoleColor.Red; }

            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            Console.WriteLine($"{unit.Name} is taking their turn...");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("Actions:");

            int i = 1;
            foreach (UnitAction action in unit.Actions)
            {
                Console.WriteLine($"#{i} {action.Name}");
                i++;
            }
            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            Console.Write("Option: ");
            int choice = ValidateTargetInput(unit.Actions);
            Console.WriteLine("------------------------------------");

            return unit.Actions[choice - 1];
        }

        public void PrintComputerTurn(Unit unit)
        {
            if (unit.Party == Battle.FirstParty) { Console.ForegroundColor = ConsoleColor.Green; } else { Console.ForegroundColor = ConsoleColor.Red; }

            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            Console.WriteLine($"{unit.Name} is taking their turn...");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
        }

        public Unit GetTarget(List<Unit> potentialTargets)
        {
            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("Select a Target: ");

            for (int i = 0; i < potentialTargets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {potentialTargets[i].Name}");
            }
            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            Console.Write("Option: ");
            int choice = ValidateTargetInput(potentialTargets);
            Console.WriteLine("------------------------------------");

            return potentialTargets[choice - 1];
        }

        public Item GetItemChoice(Party party)
        {
            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("Select an Item: ");
            for (int i = 0; i < party.Inventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {party.Inventory[i].Name}");
            }
            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            Console.Write("Option: ");
            int choice = ValidateTargetInput(party.Inventory);
            Console.WriteLine("------------------------------------");

            return party.Inventory[choice - 1];
        }

        public int ValidateTargetInput<T>(List<T> targets)                   // yeah, I looked this up too, though it took some effort to actually make it work
        {
            while (true)
            {
                string input = Console.ReadLine();
                bool valid = int.TryParse(input, out int choice);
                //Console.WriteLine($"DEBUG: print valid: {valid}");
                if (!valid) { Console.WriteLine("Not a valid option. Please choose again."); continue; }
                //Console.WriteLine($"DEBUG: print choice: {choice}");

                if (choice > targets.Count || choice < 0) { Console.WriteLine("Not a valid option. Please choose again."); continue; }
                //Console.WriteLine($"DEBUG: print count {targets.Count}");
                return choice;
            }
        }

        //public int ValidateItemInput(Party party)                   // I think I could maybe use delegates to make it so I can pass in any list and make sure the choice is within the bounds
        //{
        //    while (true)
        //    {
        //        int choice = int.Parse(Console.ReadLine());
        //        if (choice > party.Inventory.Count || choice < party.Inventory.Count) { Console.WriteLine("Not a valid option. Please choose again."); }
        //        else return choice;
        //    }
        //}
    }

}
