﻿public abstract class LevelElement
{
    public virtual int PositionX { get; set; }
    public virtual int PositionY { get; set; }

    public char ClassChar { get; set; }
    public ConsoleColor Color { get; set; }

    public void Draw()
    {
        Console.ForegroundColor = Color;
        Console.SetCursorPosition(PositionX, PositionY);
        Console.Write(ClassChar);
        Console.ResetColor();
    }
}