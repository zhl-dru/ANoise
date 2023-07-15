using System.Runtime.CompilerServices;

namespace ANoise
{
    internal static partial class Noise
    {
        private const uint primeA = 0b10011110001101110111100110110001;
        private const uint primeB = 0b10000101111010111100101001110111;
        private const uint primeC = 0b11000010101100101010111000111101;
        private const uint primeD = 0b00100111110101001110101100101111;
        private const uint primeE = 0b00010110010101100110011110110001;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint RotateLeft(uint data, int steps)
        {
            return (data << steps) | (data >> 32 - steps);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Eat(int data, uint accumulator)
        {
            return RotateLeft(accumulator + (uint)data * primeC, 17) * primeD;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Eat(byte data, uint accumulator)
        {
            return RotateLeft(accumulator + data * primeE, 11) * primeA;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Eat(double data, uint accumulator)
        {
            ulong uldata = 0ul;
            unsafe
            {
                uldata = *(ulong*)&data;
            }
            byte b0 = (byte)(uldata & 0xFF);
            byte b1 = (byte)((uldata >> 8) & 0xFF);
            byte b2 = (byte)((uldata >> 16) & 0xFF);
            byte b3 = (byte)((uldata >> 24) & 0xFF);
            byte b4 = (byte)((uldata >> 32) & 0xFF);
            byte b5 = (byte)((uldata >> 40) & 0xFF);
            byte b6 = (byte)((uldata >> 48) & 0xFF);
            byte b7 = (byte)((uldata >> 56) & 0xFF);

            accumulator = Eat(b0, accumulator);
            accumulator = Eat(b1, accumulator);
            accumulator = Eat(b2, accumulator);
            accumulator = Eat(b3, accumulator);
            accumulator = Eat(b4, accumulator);
            accumulator = Eat(b5, accumulator);
            accumulator = Eat(b6, accumulator);
            accumulator = Eat(b7, accumulator);

            return accumulator;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint hash(uint accumulator)
        {
            uint avalanche = accumulator;
            avalanche ^= avalanche >> 15;
            avalanche *= primeB;
            avalanche ^= avalanche >> 13;
            avalanche *= primeC;
            avalanche ^= avalanche >> 16;
            return
                (byte)((avalanche >> 8) ^ (avalanche & ((1 << 8) - 1)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint hash2(int x, int y, uint seed)
        {
            uint accumulator = seed + primeE;

            accumulator = Eat(x, accumulator);
            accumulator = Eat(y, accumulator);

            return hash(accumulator);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint hash3(int x, int y, int z, uint seed)
        {
            uint accumulator = seed + primeE;
            accumulator = Eat(x, accumulator);
            accumulator = Eat(y, accumulator);
            accumulator = Eat(z, accumulator);

            return hash(accumulator);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint hash4(int x, int y, int z, int w, uint seed)
        {
            uint accumulator = seed + primeE;
            accumulator = Eat(x, accumulator);
            accumulator = Eat(y, accumulator);
            accumulator = Eat(z, accumulator);
            accumulator = Eat(w, accumulator);

            return hash(accumulator);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint hash6(int x, int y, int z, int w, int u, int v, uint seed)
        {
            uint accumulator = seed + primeE;
            accumulator = Eat(x, accumulator);
            accumulator = Eat(y, accumulator);
            accumulator = Eat(z, accumulator);
            accumulator = Eat(w, accumulator);
            accumulator = Eat(u, accumulator);
            accumulator = Eat(v, accumulator);

            return hash(accumulator);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint hash2(double x, double y, uint seed)
        {
            uint accumulator = seed + primeE;
            accumulator = Eat(x, accumulator);
            accumulator = Eat(y, accumulator);

            return hash(accumulator);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint hash3(double x, double y, double z, uint seed)
        {
            uint accumulator = seed + primeE;
            accumulator = Eat(x, accumulator);
            accumulator = Eat(y, accumulator);
            accumulator = Eat(z, accumulator);

            return hash(accumulator);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint hash4(double x, double y, double z, double w, uint seed)
        {
            uint accumulator = seed + primeE;
            accumulator = Eat(x, accumulator);
            accumulator = Eat(y, accumulator);
            accumulator = Eat(z, accumulator);
            accumulator = Eat(w, accumulator);

            return hash(accumulator);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint hash6(double x, double y, double z, double w, double u, double v, uint seed)
        {
            uint accumulator = seed + primeE;
            accumulator = Eat(x, accumulator);
            accumulator = Eat(y, accumulator);
            accumulator = Eat(z, accumulator);
            accumulator = Eat(w, accumulator);
            accumulator = Eat(u, accumulator);
            accumulator = Eat(v, accumulator);

            return hash(accumulator);
        }
    }
}