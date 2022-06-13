using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid(5, 5);
            CharacterClass playerCharacterClass;
            CharacterClass enemyCharacterClass;
            GridBox PlayerCurrentLocation;
            GridBox EnemyCurrentLocation;
            Character PlayerCharacter;
            Character EnemyCharacter;
            List<Character> AllPlayers = new List<Character>();
            int currentTurn = 0;
            int numberOfPossibleTiles = grid.grids.Count;
            Setup(); 


            void Setup()
            {

                GetPlayerChoice();
            }

            void GetPlayerChoice()
            {
                //asks for the player to choose between for possible classes via console.
                Console.WriteLine("Choose Between One of this Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                //store the player choice in a variable
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "2":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "3":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "4":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    default:
                        GetPlayerChoice();
                        break;
                }
            }

            void CreatePlayerCharacter(int classIndex)
            {

                playerCharacterClass = (CharacterClass)classIndex;
                Console.WriteLine($"Player Class Choice: {playerCharacterClass.ToString()}");
                PlayerCharacter = new Character(playerCharacterClass);
                PlayerCharacter.PlayerIndex = 0;
                
                CreateEnemyCharacter();

            }

            void CreateEnemyCharacter()
            {
                //randomly choose the enemy class and set up vital variables
                var rand = new Random();
                int randomInteger = rand.Next(1, 4);
                enemyCharacterClass = (CharacterClass)randomInteger;
                Console.WriteLine($"Enemy Class Choice: {enemyCharacterClass}");
                EnemyCharacter = new Character(enemyCharacterClass);
                EnemyCharacter.PlayerIndex = 1;
                Setup_class_info(playerCharacterClass, enemyCharacterClass);
                StartGame();

            }

            void StartGame()
            {
                //populates the character variables and targets
                EnemyCharacter.Target = PlayerCharacter;
                PlayerCharacter.Target = EnemyCharacter;
                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);
                AlocatePlayers();
                StartTurn();

            }

            void Setup_class_info(CharacterClass player_class, CharacterClass enemy_class)
            {
                switch (player_class)
                {
                    case CharacterClass.Paladin:
                        PlayerCharacter.Health = 120;
                        PlayerCharacter.BaseDamage = 10;
                        PlayerCharacter.DamageMultiplier = 1.5f;
                        break;
                    case CharacterClass.Warrior:
                        PlayerCharacter.Health = 100;
                        PlayerCharacter.BaseDamage = 20;
                        PlayerCharacter.DamageMultiplier = 1.6f;
                        break;
                    case CharacterClass.Cleric:
                        PlayerCharacter.Health = 150;
                        PlayerCharacter.BaseDamage = 5;
                        PlayerCharacter.DamageMultiplier = 1.1f;
                        break;
                    case CharacterClass.Archer:
                        PlayerCharacter.Health = 70;
                        PlayerCharacter.BaseDamage = 20;
                        PlayerCharacter.DamageMultiplier = 2;
                        break;
                }

                switch (enemy_class)
                {
                    case CharacterClass.Paladin:
                        EnemyCharacter.Health = 120;
                        EnemyCharacter.BaseDamage = 10;
                        EnemyCharacter.DamageMultiplier = 1.5f;
                        break;
                    case CharacterClass.Warrior:
                        EnemyCharacter.Health = 100;
                        EnemyCharacter.BaseDamage = 20;
                        EnemyCharacter.DamageMultiplier = 1.6f;
                        break;
                    case CharacterClass.Cleric:
                        EnemyCharacter.Health = 150;
                        EnemyCharacter.BaseDamage = 5;
                        EnemyCharacter.DamageMultiplier = 1.1f;
                        break;
                    case CharacterClass.Archer:
                        EnemyCharacter.Health = 70;
                        EnemyCharacter.BaseDamage = 20;
                        EnemyCharacter.DamageMultiplier = 2;
                        break;
                }
            }

            void StartTurn(){


                foreach(Character character in AllPlayers)
                {
                    character.StartTurn(grid, PlayerCharacter, EnemyCharacter);
                }

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                if(PlayerCharacter.Health <= 0)
                {
                    Console.Write("Lose");
                    return;
                } else if (EnemyCharacter.Health <= 0)
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    Console.Write("Win");

                    Console.Write(Environment.NewLine + Environment.NewLine);

                    return;
                } else
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key to start the next turn...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    ConsoleKeyInfo key = Console.ReadKey();
                    StartTurn();
                }
            }

            int GetRandomInt(int min, int max)
            {
                var rand = new Random();
                int index = rand.Next(min, max);
                return index;
            }

            void AlocatePlayers()
            {
                AlocatePlayerCharacter();

            }

            void AlocatePlayerCharacter()
            {
                int random = GetRandomInt(0,grid.grids.Count);
                GridBox RandomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!RandomLocation.ocupied)
                {
                    GridBox PlayerCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    grid.grids[random] = RandomLocation;
                    PlayerCharacter.currentBox = grid.grids[random];
                    
                    AlocateEnemyCharacter();
                } else
                {
                    AlocatePlayerCharacter();
                }
            }

            void AlocateEnemyCharacter()
            {
                int random = GetRandomInt(0, grid.grids.Count);
                GridBox RandomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!RandomLocation.ocupied)
                {
                    EnemyCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    grid.grids[random] = RandomLocation;
                    EnemyCharacter.currentBox = grid.grids[random];
                    
                    grid.drawBattlefield(5 , 5);
                }
                else
                {
                    AlocateEnemyCharacter();
                }

                
            }

        }
    }
}
