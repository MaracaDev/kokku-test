using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>();
        public int xLenght;
        public int yLength;
        public Grid(int Lines, int Columns)
        {
            xLenght = Lines;
            yLength = Columns;
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                    
                for(int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (Columns * i + j));
                    grids.Add(newBox);
                    
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void drawBattlefield(int Lines, int Columns)
        {
            
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    foreach (GridBox current_grid in grids)
                    {
                        if(current_grid.xIndex == i && current_grid.yIndex == j)
                        {
                            if (current_grid.ocupied)
                            {
                                Console.Write("[X]\t");
                            }

                            else if (!current_grid.ocupied)
                            {
                                Console.Write($"[ ]\t");
                            }
                        }
                    }
                    
                        
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

    }
}
