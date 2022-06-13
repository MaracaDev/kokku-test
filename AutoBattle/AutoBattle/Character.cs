using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Character
    {
        public string Name { get; set; }
        public float Health;
        public float BaseDamage;
        public int rounds_stunned;
        public float DamageMultiplier { get; set; }
        public GridBox currentBox;
        public int PlayerIndex;
        public Character Target { get; set; } 
        public Character(CharacterClass characterClass)
        {

        }


        public bool TakeDamage(float amount)
        {
            if((Health -= amount) <= 0)
            {
                Die();
                return true;
            }
            return false;
        }

        public void Die()
        {
            Console.WriteLine("\n end game \n");
        }

        public void WalkTO(bool CanWalk)
        {

        }

        public void StartTurn(Grid battlefield, Character player, Character enemy)
        {
            
            if (CheckCloseTargets(battlefield)) 
            {
                if (rounds_stunned <= 0)
                {
                    Attack(Target, player, enemy);
                    return;
                }

                else
                {
                    rounds_stunned--;
                }
                
            }
            else
            {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if(this.currentBox.xIndex > Target.currentBox.xIndex)
                {
                    
                    if ((battlefield.grids.Exists(x => x.Index == currentBox.Index - 1)))
                    {
                        currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
                        currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {PlayerIndex} walked up\n");
                        battlefield.drawBattlefield(5, 5);

                        return;
                    }
                } else if(currentBox.xIndex < Target.currentBox.xIndex)
                {
                    if ((battlefield.grids.Exists(x => x.Index == currentBox.Index + 1)))
                    {
                        currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                        currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {PlayerIndex} walked down\n");
                        battlefield.drawBattlefield(5, 5);
                        return;
                    }
                       
                    
                }

                if (this.currentBox.yIndex > Target.currentBox.yIndex)
                {
                    if ((battlefield.grids.Exists(y => y.Index == currentBox.Index - 1)))
                    {
                        this.currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        this.currentBox = (battlefield.grids.Find(y => y.Index == currentBox.Index - battlefield.yLength));
                        this.currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {PlayerIndex} walked left\n");
                        battlefield.drawBattlefield(5, 5);
                        return;
                    }
                    
                }
                else if(this.currentBox.yIndex < Target.currentBox.yIndex)
                {
                    if ((battlefield.grids.Exists(y => y.Index == currentBox.Index + 1)))
                    {
                        this.currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = this.currentBox;
                        this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght));
                        this.currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {PlayerIndex} walked right\n");
                        battlefield.drawBattlefield(5, 5);
                    }
                    

                    return;
                }
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            bool up = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1).ocupied);
            bool down = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1).ocupied);
            bool left = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght).ocupied);
            bool right = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght).ocupied);
            

            if (left || right || up || down) 
            {
                
                return true;
                
            }
            return false; 
        }

        

        public void Attack (Character target, Character player, Character enemy)
        {
            Random random_rate = new Random();
            int num = random_rate.Next(0, 10);

            if (num >= 9)
            {
                Console.WriteLine($" \n Player {PlayerIndex} stunned player {Target.PlayerIndex} and did {BaseDamage} damage\n");
                target.TakeDamage(BaseDamage * DamageMultiplier);
                target.rounds_stunned = 2;
            }

            target.TakeDamage(BaseDamage);
            Console.WriteLine($" \n Player {PlayerIndex} is attacking the player {Target.PlayerIndex} and did {BaseDamage} damage\n");
            Console.WriteLine("\n Player health " + player.Health);
            Console.WriteLine("\n Enemy health " + enemy.Health);

        }
    }
}
