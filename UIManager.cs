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

            int i;
            for (i = 0; i < potentialTargets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {potentialTargets[i].Name}");
            }

            i++;
            Console.WriteLine($"{i}. Back to Menu");

            Console.WriteLine("++++++++++++++++++++++++++++++++++++");

            Console.Write("Option: ");
            int choice = ValidateTargetInput(potentialTargets);
            Console.WriteLine("------------------------------------");

            if (choice == potentialTargets.Count + 1) return null;

            else return potentialTargets[choice - 1];

        }

        public Item GetItemChoice(Party party)
        {
            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("Select an Item: ");

            int i;
            for (i = 0; i < party.Inventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {party.Inventory[i].Name}");
            }

            i++;
            Console.WriteLine($"{i}. Back to Menu");

            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            Console.Write("Option: ");
            int choice = ValidateTargetInput(party.Inventory);

            Console.WriteLine("------------------------------------");

            if (choice == party.Inventory.Count + 1) return null;
            else return party.Inventory[choice - 1];
        }

        public int ValidateTargetInput<T>(List<T> targets)                   
        {
            while (true)
            {
                string input = Console.ReadLine();
                bool valid = int.TryParse(input, out int choice);
                //Console.WriteLine($"DEBUG: print valid: {valid}");
                if (!valid) { Console.WriteLine("Not a valid option. Please choose again."); continue; }
                //Console.WriteLine($"DEBUG: print choice: {choice}");

                if (choice > targets.Count + 1 || choice < 0) { Console.WriteLine("Not a valid option. Please choose again."); continue; }
                //Console.WriteLine($"DEBUG: print count {targets.Count}");
                return choice;
            }
        }

    }

}
