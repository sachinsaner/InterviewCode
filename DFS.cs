using System;
using System.Collections.Generic;

namespace CodingPractice
{
    public class DFS
    {
        public DFS()
        {

        }

        public bool SolveMaze(int[,] maze, int N)
        {
            int[,] sol = new int[N, N];
            for(int i = 0; i < N; i++)
            {
                for(int j = 0; j < N; j++)
                {
                    sol[i, j] = 0;
                }
            }

            if (MazeUtil(maze, sol, 0, 0, 3))
            {
                return true;
            }

            return false;
        }

        private bool MazeUtil(int[,] maze, int[,] sol, int x, int y, int N)
        {
            //Base condition, since X,Y is the goal
            if (x == N - 1 && y == N - 1 && maze[x, y] == 1)
            {
                sol[x, y] = 1;
                return true;
            }

            if (IsSafe(N, x, y, maze))
            {
                if (sol[x, y] == 1) return false;

                sol[x, y] = 1;

                if (MazeUtil(maze, sol, x + 1, y, N))
                {
                    return true;
                }

                if (MazeUtil(maze, sol, x, y + 1, N))
                {
                    return true;
                }

                sol[x, y] = 0;
                return false;
            }

            return false;
        }

        private bool IsSafe(int N, int x, int y, int[,] maze)
        {
            if (x >= 0 && x < N && y >= 0 && y < N && maze[x, y] == 1) 
            {
               return true;
            }

            return false;
        }


        public int NumIslands(ref List<List<int>> grid)
        {
            int count = 0;
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[0].Count; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        count++;
                        DFS_1(grid, i, j);
                    }
                }
            }

            return count;
            // Code here
        }

        private void DFS_1(List<List<int>> grid, int i, int j)
        {
            if (i >= 0 && i < grid.Count && j >= 0 && j < grid[0].Count && grid[i][j] == 1)
            {
                grid[i][j] = 0;
                DFS_1(grid, i + 1, j);
                DFS_1(grid, i, j + 1);
                DFS_1(grid, i - 1, j);
                DFS_1(grid, i, j - 1);
            }
        }

        public int UniquePaths(int m, int n)
        {
            var grid = new int[m, n];

            for (int i = 0; i < m; i++)
            {
                grid[i, 0] = 1;
            }
            for (int i = 0; i < n; i++)
            {
                grid[0, i] = 1;
            }

            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    grid[i, j] = grid[i - 1, j] + grid[i, j - 1];
                }
            }

            return grid[m - 1, n - 1];
        }

        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int rows = obstacleGrid.Length;
            int cols = obstacleGrid[0].Length;

            if (obstacleGrid[0][0] == 1)
            {
                return 0;
            }

            obstacleGrid[0][0] = 1;

            for (int i = 1; i < rows; i++)
            {
                obstacleGrid[i][0] = obstacleGrid[i][0] == 0 && obstacleGrid[i - 1][0] == 1 ? 1 : 0;
            }

            for (int i = 1; i < cols; i++)
            {
                obstacleGrid[0][i] = obstacleGrid[0][i] == 0 && obstacleGrid[0][i - 1] == 1 ? 1 : 0;
            }

            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < cols; j++)
                {
                    if (obstacleGrid[i][j] == 0)
                    {
                        obstacleGrid[i][j] = obstacleGrid[i - 1][j] + obstacleGrid[i][j - 1];
                    }
                    else
                    {
                        obstacleGrid[i][j] = 0;
                    }
                }
            }

            return obstacleGrid[rows - 1][cols - 1];
        }
    }
}
