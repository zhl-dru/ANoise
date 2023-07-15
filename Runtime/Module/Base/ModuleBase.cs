using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using System.Collections.Generic;

namespace ANoise
{
    public abstract partial class ModuleBase
    {
        #region Get
        public abstract JobHandle Get(NativeArray<double2> inputs, NativeArray<double> outputs, JobHandle dependsOn = default);
        public abstract JobHandle Get(NativeArray<double3> inputs, NativeArray<double> outputs, JobHandle dependsOn = default);
        public abstract JobHandle Get(NativeArray<double4> inputs, NativeArray<double> outputs, JobHandle dependsOn = default);
        public abstract JobHandle Get(NativeArray<double6> inputs, NativeArray<double> outputs, JobHandle dependsOn = default);
        #endregion

        #region Cache
        protected NativeArray<T> CreateCache<T>(int length) where T : unmanaged
        {
            return new NativeArray<T>(length, Allocator.TempJob);
        }
        protected void DisposeCache<T>(JobHandle inputDeps, params NativeArray<T>[] caches)
            where T : unmanaged
        {
            for (int i = 0; i < caches.Length; i++)
            {
                caches[i].Dispose(inputDeps);
            }
        }
        #endregion
    }
}