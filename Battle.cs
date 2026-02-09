using FinalBattlee.Items;
using FinalBattlee.UnitActions;
using FinalBattlee.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBattlee
{
    class Battle
    {
        public Party FirstParty { get; }
        public Party SecondParty { get; }
        public UIManager UI { get; }

        public Battle(Party firstParty, Party secondParty)
        {
            FirstParty = firstParty;
            SecondParty = secondParty;
            UI = new UIManager(this);

            RunBattleLoop();
        }
        public void RunBattleLoop()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;

                if (FirstParty.Units.Count == 0)
                {
                    Console.WriteLine($"\n{SecondParty.PartyName} WINS!!!!");
                    Thread.Sleep(2000);
                    break;
                }
                else if (SecondParty.Units.Count == 0)
                {
                    Console.WriteLine($"\n{FirstParty.PartyName} WINS!!!!");
                    Thread.Sleep(2000);
                    break;
                }

                if (FirstParty.PlayerOwner.PlayerType == PlayerType.Computer)                  
                {
                    ComputerTakeTurn(FirstParty);                                  // gonna make a separate func for the computer so TakeTurn isn't massive
                }
                else { TakeTurn(FirstParty); }
                
                if (SecondParty.PlayerOwner.PlayerType == PlayerType.Computer)
                {
                    ComputerTakeTurn(SecondParty);
                }
                else { TakeTurn(SecondParty); }
                    
            }
        }

        public void TakeTurn(Party party)
        {

            bool targetChosen = false;

            foreach (Unit unit in party.Units)
            {

                while (!targetChosen)
                {
                    UI.PrintGameStatus();
                    UnitAction action = UI.GetUnitAction(unit);
                     
                    if (action is DoNothing)
                    {
                        action.RunAction(unit, unit);      
                    }
                    else if (action is UseItem)
                    {
                        Item item = UI.GetItemChoice(party);
                        //Console.WriteLine($"DEBUG: {item.Name} is item selected");

                        if (item == null) continue;

                        var potentialTargets = GetFriendlyPartyForUnit(unit);
                        Unit target = UI.GetTarget(potentialTargets);                   

                        if (target == null) continue;


                        item.Use(target);
                        party.Inventory.Remove(item);                                   

                        Thread.Sleep(1000);
                    }
                    else
                    {
                        var potentialTargets = GetEnemyPartyForUnit(unit);
                        Unit target = UI.GetTarget(potentialTargets);

                        if (target == null) continue;

                        action.RunAction(unit, target);
                        Thread.Sleep(1000);
                        DeathCheck(target);
                    }

                    targetChosen = true;
                }
                
            }
        }

        public void ComputerTakeTurn(Party party)                                                              
        {
            foreach (Unit u in party.Units)                                                                     // this has to be bad and is obviously NOT sustainable in any way... but i'm not sure how else to do it
            {
                UI.PrintGameStatus();
                UI.PrintComputerTurn(u);

                switch (u)
                {
                    case Skeleton skeleton:
                        {
                            //Console.WriteLine("DEBUG: this is a skeleton");
                            if (skeleton.CurrentHP <= skeleton.MaxHP / 2 && new Random().Next(4) == 0 && party.Inventory.Any(item => item is HealthPotion))            // yes I asked Gemini for this, yes this is 10x cleaner
                            {
                                var potionAction = skeleton.Actions.OfType<UseItem>().FirstOrDefault();               // and this because I forgot that I could use LINQ for this. Thanks paptreek
                                potionAction?.RunAction(skeleton, skeleton);                                          // I also realize that the UseItem action doesn't actually do anything. 
                                                                                                                      // I just don't get how to reference the usable Item in the UnitAction UseItem without passing everything in and having the same problems I had before.
                                var potion = party.Inventory.OfType<HealthPotion>().FirstOrDefault();
                                potion?.Use(skeleton);
                                party.Inventory.Remove(potion);
                            }
                            else
                            {
                                var boneCrunch = skeleton.Actions.OfType<BoneCrunch>().FirstOrDefault();
                                var targets = GetEnemyPartyForUnit(skeleton);
                                //Console.WriteLine($"DEBUG: making sure code reaches here");
                                boneCrunch?.RunAction(skeleton, targets[0]);
                                Thread.Sleep(1000);
                                DeathCheck(targets[0]);
                            }
                            break;
                        }
                    
                    case UncodedOne uncoded:
                        {
                            //Console.WriteLine("DEBUG: this is the uncoded one");

                            var unravel = uncoded.Actions.OfType<Unraveling>().FirstOrDefault();
                            var targets = GetEnemyPartyForUnit(uncoded);
                            unravel?.RunAction(uncoded, targets[0]);
                            Thread.Sleep(1000);
                            DeathCheck(targets[0]);

                            break;
                        }
                    case TrueProgrammer programmer:
                        Thread.Sleep(1000);
                        break;
                                
                }



                //if (u is Skeleton)                      // maybe a switch case would be better here
                //{
                //    //Console.WriteLine("DEBUG: it IS a skeleton");
                //    if (u.CurrentHP <= u.MaxHP / 2)
                //    {
                //        Random rng = new Random();
                //        int potionChance = rng.Next(4);


                //        if (potionChance == 0)
                //        {

                //            for (int i = 0; i < u.Actions.Count; i++)          // holy hell this seems obtuse and ugly. there must be a better way
                //            {
                //                if (u.Actions[i] is UseItem)                              // i guess this does make it so if the Actions list ever expands or gets mixed up, this will still work
                //                {
                //                    action = u.Actions[i];                    // but at the same time, I feel that we should KNOW a skeleton will have BoneCrunch and UseItem. 
                //                    action.RunAction(u, u);
                //                    break;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            for (int i = 0; i < u.Actions.Count; i++)
                //            {
                //                if (u.Actions[i] is BoneCrunch)
                //                {
                //                    action = u.Actions[i];
                //                    var targets = GetEnemyPartyForUnit(u);
                //                    action.RunAction(u, targets[0]);
                //                    break;
                //                }
                                
                //            }

                            
                            
                //        }


                //    }
                //    else
                //    {
                //        //Console.WriteLine("DEBUG: skeleton health is more than half");
                //        for (int i = 0; i < u.Actions.Count; i++)                      // this is an exact repeat of above, which prob means it deserves a method, but...
                //        {
                //            if (u.Actions[i] is BoneCrunch)
                //            {
                //                //Console.WriteLine("DEBUG: we found bone crunch");
                //                action = u.Actions[i];
                //                var targets = GetEnemyPartyForUnit(u);
                //                action.RunAction(u, targets[0]);
                //                break;
                //            }

                //        }
                //    }
            }
        }

        public List<Unit> GetEnemyPartyForUnit(Unit unit)
        {
            List<Unit> enemies = new List<Unit>();

            foreach (Unit u in FirstParty.Units)
            {
                if (u.Party != unit.Party) { enemies.Add(u); }
            }

            foreach (Unit u in SecondParty.Units)
            {
                if (u.Party != unit.Party)
                {
                    enemies.Add(u);
                }
            }

            //foreach (Unit u in enemies) { Console.WriteLine(u.Name); }  // debugging
            //foreach (Unit u in enemies) { Console.WriteLine(u.ID); }  // testing if ID works
            return enemies;
        }

        public List<Unit> SimpleFriends(Unit unit) => unit.Party.Units.ToList();        // I looked this up but I don't get it. This is already a list of Units. Why Do I need to ToList() this to make it work? Apparently because of the ReadOnly List vs Enumerable.
        public List<Unit> SimpleEnemies(Unit unit) => (unit.Party == FirstParty ? SecondParty : FirstParty).Units.ToList(); // I'm not using these because I totally cheated and don't want to take credit
        public List<Unit> GetFriendlyPartyForUnit(Unit unit)
        {
            List<Unit> friends = new List<Unit>();

            foreach (Unit u in FirstParty.Units)
            {
                if (u.Party == unit.Party) { friends.Add(u); }
            }

            foreach (Unit u in SecondParty.Units)
            {
                if (u.Party == unit.Party)
                {
                    friends.Add(u);
                }
            }

            //foreach (Unit u in enemies) { Console.WriteLine(u.Name); }  // debugging
            //foreach (Unit u in enemies) { Console.WriteLine(u.ID); }  // testing if ID works
            return friends;
        }

        public void DeathCheck(Unit target)
        {
            if (target.CurrentHP == 0)
            {
                Console.WriteLine($"{target.Name} has been DEFEATED!");
                target.Party.Remove(target);
            }
            else return;
        }
    }
}

