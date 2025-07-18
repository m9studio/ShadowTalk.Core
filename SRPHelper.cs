﻿using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace M9Studio.ShadowTalk.Core
{
    public static class SRPHelper
    {
        /// <summary>Генерация случайных байт заданной длины</summary>
        public static byte[] GenerateRandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            RandomNumberGenerator.Fill(bytes);
            return bytes;
        }

        /// <summary>Хэширует BigInteger через SHA256</summary>
        public static byte[] Hash(BigInteger value)
        {
            return SHA256.Create().ComputeHash(value.ToByteArrayUnsigned());
        }

        /// <summary>Вычисляет H(A | B)</summary>
        public static BigInteger ComputeU(BigInteger A, BigInteger B, BigInteger N)
        {
            byte[] aBytes = A.ToByteArrayUnsigned();
            byte[] bBytes = B.ToByteArrayUnsigned();

            byte[] concat = new byte[aBytes.Length + bBytes.Length];
            Buffer.BlockCopy(aBytes, 0, concat, 0, aBytes.Length);
            Buffer.BlockCopy(bBytes, 0, concat, aBytes.Length, bBytes.Length);

            byte[] hash = SHA256.Create().ComputeHash(concat);
            return new BigInteger(hash, isUnsigned: true, isBigEndian: true) % N;
        }

        /// <summary>Вычисляет M1 = H(A | B | K)</summary>
        public static string ComputeM1(BigInteger A, BigInteger B, byte[] K)
        {
            byte[] aBytes = A.ToByteArrayUnsigned();
            byte[] bBytes = B.ToByteArrayUnsigned();

            byte[] concat = new byte[aBytes.Length + bBytes.Length + K.Length];
            Buffer.BlockCopy(aBytes, 0, concat, 0, aBytes.Length);
            Buffer.BlockCopy(bBytes, 0, concat, aBytes.Length, bBytes.Length);
            Buffer.BlockCopy(K, 0, concat, aBytes.Length + bBytes.Length, K.Length);

            byte[] hash = SHA256.Create().ComputeHash(concat);
            return Convert.ToHexString(hash);
        }

        /// <summary>Вычисляет M2 = H(A | M1 | K)</summary>
        public static string ComputeM2(BigInteger A, string M1Hex, byte[] K)
        {
            byte[] aBytes = A.ToByteArrayUnsigned();
            byte[] m1Bytes = Convert.FromHexString(M1Hex);

            byte[] concat = new byte[aBytes.Length + m1Bytes.Length + K.Length];
            Buffer.BlockCopy(aBytes, 0, concat, 0, aBytes.Length);
            Buffer.BlockCopy(m1Bytes, 0, concat, aBytes.Length, m1Bytes.Length);
            Buffer.BlockCopy(K, 0, concat, aBytes.Length + m1Bytes.Length, K.Length);

            byte[] hash = SHA256.Create().ComputeHash(concat);
            return Convert.ToHexString(hash);
        }

        /// <summary>Вычисление HMAC</summary>
        public static string ComputeHMAC(byte[] key, string message)
        {
            using var hmac = new HMACSHA256(key);
            var bytes = Encoding.UTF8.GetBytes(message);
            return Convert.ToHexString(hmac.ComputeHash(bytes));
        }




        public static (BigInteger a, string A_hex) GenerateA()
        {
            // Получаем SRP-константы (N и g должны совпадать с сервером)
            BigInteger N = SRPConstants.N;
            BigInteger g = SRPConstants.g;

            // Генерируем a (приватное случайное значение)
            byte[] aBytes = new byte[32];
            RandomNumberGenerator.Fill(aBytes);
            BigInteger a = new BigInteger(aBytes, isUnsigned: true, isBigEndian: true);

            // Вычисляем A = g^a mod N
            BigInteger A = BigInteger.ModPow(g, a, N);

            // Возвращаем и A (hex-строку для отправки), и a (для вычисления K позже)
            return (a, A.ToString("X"));
        }
    }
}
