﻿using System;

namespace MazeLibrary
{
    public class MazeSolver
    {
        #region Private fields
        private readonly int startX, startY;
        private int[,] maze;

        private int step = 1;
        private bool finish = false;
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="MazeSolver"/> class.
        /// </summary>
        /// <param name="mazeModel">The maze model.</param>
        /// <param name="startX">The start x.</param>
        /// <param name="startY">The start y.</param>
        /// <exception cref="ArgumentNullException">Null array argument was sent</exception>
        /// <exception cref="ArgumentException">Index out of range</exception>
        /// <exception cref="ArgumentException">There is no finish point in maze</exception>
        public MazeSolver(int[,] mazeModel, int startX, int startY)
        {
            if (mazeModel == null)
            {
                throw new ArgumentNullException($"Null array argument was sent");
            }

            if (startX >= mazeModel.GetLength(0) || startX < 0)
            {
                throw new ArgumentException($"Index {startX} out of range");

            }

            if (startY >= mazeModel.GetLength(1) || startY < 0)
            {
                throw new ArgumentException($"Index {startY} out of range");
            }

            if (!IsFinishExists(mazeModel))
            {
                throw new ArgumentException("There is no finish point in maze");
            }

            this.startX = startX;
            this.startY = startY;
            maze = new int[mazeModel.GetLength(0), mazeModel.GetLength(1)];
            Copy2dArray(mazeModel, maze);
        }


        public int[,] MazeWithPass() { return maze; }

        /// <summary>
        /// Passes the maze to find finish.
        /// </summary>
        public void PassMaze()
        {
            MakeStep(startX, startY, 0);
            ChangeToZero();
        }

        /// <summary>
        /// Recursive method to make every step
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="finishSide">The finish side.</param>
        private void MakeStep(int x, int y, int finishSide)
        {
            if (y == finishSide && maze[x, y] == 0)
            {
                // finish has been founded
                maze[x, y] = step++;
                finish = true;
            }

            if (x != maze.GetLength(0) - 1 && maze[x + 1, y] == 0)
            {
                maze[x, y] = step++;
                MakeStep(x + 1, y, finishSide);
            }

            if (x != 0 && maze[x - 1, y] == 0)
            {
                maze[x, y] = step++;
                MakeStep(x - 1, y, finishSide);
            }

            if (y + 1 != maze.GetLength(1) && maze[x, y + 1] == 0)
            {
                maze[x, y] = step++;
                MakeStep(x, y + 1, finishSide);
            }

            if (y != 0 && maze[x, y - 1] == 0)
            {
                maze[x, y] = step++;
                MakeStep(x, y - 1, finishSide);
            }

            step -= 1;
            if (!finish)
            {
                maze[x, y] = -2;
            }
        }

        private void ChangeToZero()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j] == -2)
                    {
                        maze[i, j] = 0;
                    }
                }
            }
        }

        private bool IsFinishExists(int[,] mazeModel)
        {
            for (int i = 0; i < mazeModel.GetLength(0); i++)
            {
                if (mazeModel[i, 0] == 0 || mazeModel[i, mazeModel.GetLength(1) - 1] == 0)
                {
                    return true;
                }
            }

            for (int i = 0; i < mazeModel.GetLength(1); i++)
            {
                if (mazeModel[0, i] == 0 || mazeModel[mazeModel.GetLength(0) - 1, i] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void Copy2dArray(int[,] from, int[,] to)
        {
            for (int i = 0; i < from.GetLength(0); i++)
            {
                for (int j = 0; j < from.GetLength(1); j++)
                {
                    to[i, j] = from[i, j];
                }
            }
        }
    }
}
