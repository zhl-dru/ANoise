using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using ANoise;
using System.Diagnostics;
using System;
using System.IO;

public class Example : MonoBehaviour
{
    public RawImage Image;
    public int Width = 512;

    public NoiseSet NoiseSet;

    private NativeArray<double2> coords;
    private NativeArray<double> values;
    private Stopwatch stopwatch = new Stopwatch();

    private string path => Directory.GetCurrentDirectory();

    [Button]
    protected virtual void Generate()
    {
        int length = Width * Width;
        coords = new NativeArray<double2>(length, Allocator.TempJob);
        values = new NativeArray<double>(length, Allocator.TempJob);

        CoordsJob.JobHandle(Width, Width, coords).Complete();
    }

    protected void Complete(ModuleBase module)
    {
        var job = module.Get(coords, values);
        job.Complete();
    }
    protected void DrawImage()
    {
        int length = Width * Width;
        var colors = new NativeArray<Color>(length, Allocator.TempJob);
        Map2DHelper.GetColors(values, colors).Complete();
        Texture2D texture = new Texture2D(Width, Width);
        texture.SetPixels(colors.ToArray());
        texture.Apply();
        Image.texture = texture;
        colors.Dispose();
    }

    protected void ModuleRun(Action action)
    {
        stopwatch.Restart();
        action.Invoke();
        stopwatch.Stop();
        UnityEngine.Debug.Log(string.Format("{0}สนำรมห{1}ms", GetType().Name, stopwatch.Elapsed.TotalMilliseconds));
    }

    protected void Dispose()
    {
        coords.Dispose();
        values.Dispose();
    }
}

[Serializable]
public class NoiseSet
{
    public int Seed = 10000;
    public int Octaves = 8;
    public double Frequency = 1.0;
    public double Lacunarity = 2.0;
    public double DerivSpacing = 0.0001;
    public EBasisTypes BasisType = EBasisTypes.SIMPLEX;
    public EFractalTypes FractalType = EFractalTypes.MULTI;
    public EInterpTypes InterpType = EInterpTypes.QUINTIC;
}

[BurstCompile]
public struct CoordsJob : IJobParallelForBatch
{
    public int Width;
    public int Height;
    [WriteOnly]
    public NativeArray<double2> Coords;

    public void Execute(int startIndex, int count)
    {
        for (int i = startIndex; i < startIndex + count; i++)
        {
            int x = i % Width;
            int y = i / Width;
            double cx = x / (double)Width;
            double cy = y / (double)Height;
            Coords[i] = new double2(cx, cy);
        }
    }

    public static JobHandle JobHandle(int width, int height, NativeArray<double2> coords)
    {
        return new CoordsJob()
        {
            Width = width,
            Height = height,
            Coords = coords
        }.ScheduleBatch(coords.Length, coords.Length / Constant.JobBatchCount);
    }
}
