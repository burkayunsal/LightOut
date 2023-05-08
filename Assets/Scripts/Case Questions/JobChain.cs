using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Random = UnityEngine.Random;


public class JobChain : MonoBehaviour
{

    private void Start()
    {
        NativeArray<int> values = new NativeArray<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, Allocator.Persistent);

        FilterPowerOfTwoValues(values, result =>
        {
            Debug.Log($"Filtered result: {string.Join(",", result)}");
        });

        values.Dispose();
    }

    public void FilterPowerOfTwoValues(NativeArray<int> values, Action<NativeArray<int>> onCompleted)
    {
        NativeArray<int> mask = new NativeArray<int>(values.Length, Allocator.TempJob);
        NativeArray<int> result = new NativeArray<int>(values.Length, Allocator.TempJob);

        GenerateMaskJob generateMaskJob = new GenerateMaskJob
        {
            Values = values,
            Mask = mask
        };
 
        FilterValuesJob filterValuesJob = new FilterValuesJob
        {
            Values = values,
            Mask = mask,
            Result = result
        };
        JobHandle generateMaskJobHandle = generateMaskJob.Schedule(values.Length, 64);
        JobHandle filterValuesJobHandle = filterValuesJob.Schedule(values.Length, 64, generateMaskJobHandle);

        JobHandle completionHandle = JobHandle.CombineDependencies(filterValuesJobHandle, generateMaskJobHandle);

        completionHandle.Complete();
        onCompleted(result);

        mask.Dispose();
        result.Dispose();
    }
    
    [BurstCompile]
    private struct GenerateMaskJob : IJobParallelFor
    {
        public NativeArray<int> Values;
        public NativeArray<int> Mask;

        public void Execute(int index)
        {
            Mask[index] = IsPowerOfTwo(Values[index]) ? 1 : 0;
        }

        private static bool IsPowerOfTwo(int value)
        {
            return (value & (value - 1)) == 0;
        }
    }

    [BurstCompile]
    struct FilterValuesJob : IJobParallelFor
    {
        [ReadOnly]
        public NativeArray<int> Values;

        [ReadOnly]
        public NativeArray<int> Mask;

        public NativeArray<int> Result;

        public void Execute(int i)
        {
            Result[i] = Mask[i] == 1 ? Values[i] : 0;
        }
    }
}

/*
 *
 *
 * Given a NativeArray<int> of size N filled with positive integers, the class below filters the values which are powers of two.
 * The operation is asynchronous and when the operation is completed and the result is ready, the caller is notified through the callback given.
 *
 * The operation is divided into two steps:
 * 
 * The first step is to generate a mask array of size N. The value at an index is 1 if the corresponding value in the source array is a power of two, otherwise 0.
 * 
 * The second step is to filter the values in the source array using the mask generated in the first step.
 * The resulting data should contain the original value if it is a power of two, otherwise 0.
 *
 * The jobs can take as long is they need. The operation should not hang the main thread by waiting for jobs to complete.
 * Burst compilation should be used on the jobs.
 * The operations inside the jobs should be as optimized as possible. Relevant attributes should be used to maximize performance
 * The original array should not be modified.
 * When the result array is ready, invoke the given callback with the result.
 *
 * Note that there is a dependency between the outputs and inputs of the two jobs.
 *
 * Implement the class below and the described jobs, according to the constraints given above.
 * 
 * 
 */