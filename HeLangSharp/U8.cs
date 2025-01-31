﻿using System.Collections.Immutable;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
namespace HeLangSharp
{
    public class U8CentralFiniteCurve
    {
        public static readonly U8Start 典 = new();
        public static readonly U8Start dian = new();
        public static readonly U8End 乐 = new();
        public static readonly U8End le = new();

        public static void Test5G()
        {
            var rnd = new Random();
            var songs = new[]
            {
                "Zood", "“Libera me” from hell" /*私货*/, "Rap God", "Like That", "Out On My Own", "Breathe Again",
                "I Know What I Want", "We Do It Right",
                "Dissolve II", "Meridian", "Flirting With June", "Behind The Sun", "Eat Them Apples", "Purusha",
                "Brazooka", "Lenguas", "Loyal", "L.O.V.E. (Instrumental)",
                "Robot Rock (Edit)",
                "Young Dumb & Broke (Originally Performed by Khalid) [Instrumental Karaoke Version]",
                "Finish Line/Drown"
            };
            var suffixes = new[]
            {
                ".flac", ".ogg", ".mp3", ".m4a"
            };
            var player = new SoundPlayer("Eminem - Rap God.wav");
            player.Play();
            Console.WriteLine("Cyber DJ is downloading musics via 5G...");
            var s = new Stopwatch();
            s.Start();
            while (s.Elapsed < TimeSpan.FromSeconds(17))
            {
                var r1 = rnd.Next(songs.Length);
                var r2 = rnd.Next(suffixes.Length);
                var mb = rnd.Next(10, 30);
                Console.Write($"Downloading {songs[r1]}{suffixes[r2]} ({mb}MiB)...");
                using var progress = new ProgressBar();
                for (int i = 0; i <= 100; i+=10) {
                    progress.Report((double) i / 100);
                    Thread.SpinWait(500000);
                }
                Console.WriteLine("Done.");
            }

        }

        public static void SPrint(u8 a)
        {
            for (int i = 0; i < a.nums.Length; i++)
            {
                var num = a.nums[i];
                Console.Write((char)num);
            }
            Console.WriteLine();
        }

        public static void Print(u8 a)
        {
            for (int i = 0; i < a.nums.Length; i++)
            {
                var num = a.nums[i];
                Console.Write(i == 0 ? $"{num}" : $" | {num}");
            }
            Console.WriteLine();
        }

        public static u8 CreateU8(int length)
        {
            if (length <= 0)
            {
                throw new InsufficientMemoryException("内存不够了，star一下repo，抽一个Air Pro");
            }

            var a = new U8Builder(0);
            for (int i = 0; i < length - 1; i++)
            {
                a.Add(0);
            }

            return a.Build();
        }
    }

    public ref struct u8
    {
        public Span<nint> nums { get; set; }

        internal u8(Span<nint> nums) => this.nums = nums;

        public static u8 operator ++(u8 a)
        {
            for (var i = 0; i < a.nums.Length; i++)
            {
                a.nums[i]++;
            }

            return a;
        }

        public static implicit operator u8(Span<nint> a) => new(a);

        public nint this[u8 u]
        {
            set
            {
                foreach (var num in u.nums)
                {
                    nums[(int)num - 1] = value;
                }
            }
        }

        public nint this[nint v]
        {
            get => nums[(int)v - 1];
            set
            {
                if (v == 0)
                {
                    for (int i = 0; i < nums.Length; i++)
                    {
                        nums[i] = value;
                    }
                }
                else nums[(int)v - 1] = value;
            }
        }

    }

    public struct U8Start { public static U8Builder operator |(U8Start _, nint i) => new(i); }

    public struct U8End { }

    public class U8Builder
    {
        private readonly List<nint> Nums = new();

        public U8Builder(nint startNum) { Nums.Add(startNum); }

        public void Add(nint num) => Nums.Add(num);

        public u8 Build() => new(CollectionsMarshal.AsSpan(Nums));

        public static u8 operator |(U8Builder b, U8End _) => b.Build();

        public static U8Builder operator |(U8Builder b, nint i)
        {
            b.Add(i);
            return b;
        }
    }
}