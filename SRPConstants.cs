using System.Numerics;

namespace M9Studio.ShadowTalk.Core
{
    public static class SRPConstants
    {
        // Большое безопасное простое число (RFC 5054 Group 1024-bit или 2048-bit)
        public static readonly BigInteger N = new BigInteger(
    Convert.FromHexString(
        "EEAF0AB9ADB38DD69C33F80AFA8FC5E8" +
        "60D0539C6E3C9FBE117577A615D6C770" +
        "988C0BAD946E208E24FA074E5AB3143C" +
        "FEEA31BEB5C55DF06F4C52C9DE2BCBF6" +
        "95581718D1C7AAFE"
    ),
    isUnsigned: true,
    isBigEndian: true
);

        // Генератор группы (обычно 2)
        public static readonly BigInteger g = new BigInteger(2);

        // k = H(N || g) — можно просто фиксировать как число
        public static readonly BigInteger k = new BigInteger(3); // Упрощённо, или считай H(N,g)
    }

}
