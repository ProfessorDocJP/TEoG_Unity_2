﻿using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Balls : SexualOrgan
{
    [SerializeField]
    private SexualFluid _fluid = new SexualFluid(FluidType.Cum);

    public virtual SexualFluid Fluid
    {
        get
        {
            if (baseSize != lastBase)
            {
                _fluid.FluidCalc(Size);
            }
            return _fluid;
        }
    }
}

public static class BallsExtensions
{
    public static void AddBalls(this List<Balls> balls)
    {
        balls.Add(new Balls());
    }

    public static float Cost(this List<Balls> balls)
    {
        return Mathf.Round(30 * Mathf.Pow(4, balls.Count));
    }

    public static float ReCycle(this List<Balls> balls)
    {
        Balls toShrink = balls[balls.Count - 1];
        if (toShrink.Shrink())
        {
            balls.Remove(toShrink);
            return 30f;
        }
        else
        {
            return toShrink.Cost;
        }
    }

    public static float CumTotal(this List<Balls> balls)
    {
        return balls.Sum(b => b.Fluid.Current);
    }

    public static float CumMax(this List<Balls> balls)
    {
        return balls.Sum(b => b.Fluid.Max);
    }

    public static string Looks(this Balls parBalls, bool capital = true)
    {
        return $"{(capital ? "A" : "a")} pair of {Settings.MorInch(parBalls.Size)} wide balls"
            + $", with {Settings.LorGal(parBalls.Fluid.Current)}";
    }

    public static string Looks(this List<Balls> parBalls)
    {
        StringBuilder builder = new StringBuilder();
        foreach (Balls balls in parBalls)
        {
            builder.Append(balls.Looks() + "\n");
        }
        return builder.ToString();
    }
}