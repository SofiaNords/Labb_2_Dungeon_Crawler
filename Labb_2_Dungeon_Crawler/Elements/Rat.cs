﻿using System.Numerics;

public class Rat : Enemy
{
    public Rat(int x, int y)
    {
        ClassChar = 'r';
        Color = ConsoleColor.Red;
        PositionX = x;
        PositionY = y;
        Name = "Rat";
        HP = 10;
    }

    private LevelData _levelData;

    public override void Update()
    {
        int direction;
        int newX = this.PositionX;
        int newY = this.PositionY;

        int up = 1;
        int down = 2;
        int left = 3;
        int right = 4;

        Random random = new Random();

        direction = random.Next(1, 4);

        if (direction == up)
        {
            newY--;
        } 
        else if (direction == down)
        {
            newY++;
        }
        else if (direction == left)
        {
            newX--;
        }
        else if (direction == right)
        {
            newX++;
        }

        if (IsValidMove(newX, newY))
        {
            this.PositionX = newX;
            this.PositionY = newY;
        }
    }

    private bool IsValidMove(int x, int y)
    {
        foreach (var element in _levelData.Elements)
        {
            if (element.PositionX == x && element.PositionY == y && (element is Wall || element is Enemy || element is Player))
            {
                return false;
            }
        }
        return true;
    }
}