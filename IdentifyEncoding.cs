using System;
using System.IO;
using System.Net;

namespace FileEncodingTransform
{
   

    public class IdentifyEncoding
    {
        internal static int[][] Big5Freq = new int[0x5e][];
        internal static int[][] EUC_TWFreq = new int[0x5e][];
        internal static int[][] GBFreq = new int[0x5e][];
        internal static int[][] GBKFreq = new int[0x7e][];
        internal static string[] nicename = new string[] { "GB2312", "GBK", "HZ", "Big5", "CNS 11643", "ISO 2022CN", "UTF-8", "Unicode", "ASCII", "UTF-8(BOM)","OTHER" };

        public IdentifyEncoding()
        {
            this.Initialize_Frequencies();
        }

        internal virtual int ASCIIProbability(sbyte[] rawtext)
        {
            int num = 70;
            int length = rawtext.Length;
            for (int i = 0; i < length; i++)
            {
                if (rawtext[i] < 0)
                {
                    num -= 5;
                }
                else if (rawtext[i] == 0x1b)
                {
                    num -= 5;
                }
            }
            return num;
        }

        internal virtual int BIG5Probability(sbyte[] rawtext)
        {
            int length = 0;
            int num3 = 1;
            int num4 = 1;
            float num5 = 0f;
            float num6 = 0f;
            long num7 = 0L;
            long num8 = 1L;
            length = rawtext.Length;
            for (int i = 0; i < (length - 1); i++)
            {
                if (rawtext[i] < 0)
                {
                    num3++;
                    if (((((sbyte)Identity((long)0xa1L)) <= rawtext[i]) && (rawtext[i] <= ((sbyte)Identity((long)0xf9L)))) && (((0x40 <= rawtext[i + 1]) && (rawtext[i + 1] <= 0x7e)) || ((((sbyte)Identity((long)0xa1L)) <= rawtext[i + 1]) && (rawtext[i + 1] <= ((sbyte)Identity((long)0xfeL))))))
                    {
                        int num10;
                        num4++;
                        num8 += 500L;
                        int index = (rawtext[i] + 0x100) - 0xa1;
                        if ((0x40 <= rawtext[i + 1]) && (rawtext[i + 1] <= 0x7e))
                        {
                            num10 = rawtext[i + 1] - 0x40;
                        }
                        else
                        {
                            num10 = (rawtext[i + 1] + 0x100) - 0x61;
                        }
                        if (Big5Freq[index][num10] != 0)
                        {
                            num7 += Big5Freq[index][num10];
                        }
                        else if ((3 <= index) && (index <= 0x25))
                        {
                            num7 += 200L;
                        }
                    }
                    i++;
                }
            }
            num5 = 50f * (((float)num4) / ((float)num3));
            num6 = 50f * (((float)num7) / ((float)num8));
            return (int)(num5 + num6);
        }

        internal virtual int ENCTWProbability(sbyte[] rawtext)
        {
            int length = 0;
            int num3 = 1;
            int num4 = 1;
            long num5 = 0L;
            long num6 = 1L;
            float num7 = 0f;
            float num8 = 0f;
            length = rawtext.Length;
            for (int i = 0; i < (length - 1); i++)
            {
                if (rawtext[i] < 0)
                {
                    num3++;
                    if ((((((i + 3) < length) && (((sbyte)Identity((long)0x8eL)) == rawtext[i])) && ((((sbyte)Identity((long)0xa1L)) <= rawtext[i + 1]) && (rawtext[i + 1] <= ((sbyte)Identity((long)0xb0L))))) && (((((sbyte)Identity((long)0xa1L)) <= rawtext[i + 2]) && (rawtext[i + 2] <= ((sbyte)Identity((long)0xfeL)))) && (((sbyte)Identity((long)0xa1L)) <= rawtext[i + 3]))) && (rawtext[i + 3] <= ((sbyte)Identity((long)0xfeL))))
                    {
                        num4++;
                        i += 3;
                    }
                    else if ((((((sbyte)Identity((long)0xa1L)) <= rawtext[i]) && (rawtext[i] <= ((sbyte)Identity((long)0xfeL)))) && (((sbyte)Identity((long)0xa1L)) <= rawtext[i + 1])) && (rawtext[i + 1] <= ((sbyte)Identity((long)0xfeL))))
                    {
                        num4++;
                        num6 += 500L;
                        int index = (rawtext[i] + 0x100) - 0xa1;
                        int num10 = (rawtext[i + 1] + 0x100) - 0xa1;
                        if (EUC_TWFreq[index][num10] != 0)
                        {
                            num5 += EUC_TWFreq[index][num10];
                        }
                        else if ((0x23 <= index) && (index <= 0x5c))
                        {
                            num5 += 150L;
                        }
                        i++;
                    }
                }
            }
            num7 = 50f * (((float)num4) / ((float)num3));
            num8 = 50f * (((float)num5) / ((float)num6));
            return (int)(num7 + num8);
        }

        public static long FileLength(FileInfo file)
        {
            if (Directory.Exists(file.FullName))
            {
                return 0L;
            }
            return file.Length;
        }

        internal virtual int GB2312Probability(sbyte[] rawtext)
        {
            int length = 0;
            int num3 = 1;
            int num4 = 1;
            long num5 = 0L;
            long num6 = 1L;
            float num7 = 0f;
            float num8 = 0f;
            length = rawtext.Length;
            for (int i = 0; i < (length - 1); i++)
            {
                if (rawtext[i] < 0)
                {
                    num3++;
                    if ((((((sbyte)Identity((long)0xa1L)) <= rawtext[i]) && (rawtext[i] <= ((sbyte)Identity((long)0xf7L)))) && (((sbyte)Identity((long)0xa1L)) <= rawtext[i + 1])) && (rawtext[i + 1] <= ((sbyte)Identity((long)0xfeL))))
                    {
                        num4++;
                        num6 += 500L;
                        int index = (rawtext[i] + 0x100) - 0xa1;
                        int num10 = (rawtext[i + 1] + 0x100) - 0xa1;
                        if (GBFreq[index][num10] != 0)
                        {
                            num5 += GBFreq[index][num10];
                        }
                        else if ((15 <= index) && (index < 0x37))
                        {
                            num5 += 200L;
                        }
                    }
                    i++;
                }
            }
            num7 = 50f * (((float)num4) / ((float)num3));
            num8 = 50f * (((float)num5) / ((float)num6));
            return (int)(num7 + num8);
        }

        internal virtual int GBKProbability(sbyte[] rawtext)
        {
            int length = 0;
            int num3 = 1;
            int num4 = 1;
            long num5 = 0L;
            long num6 = 1L;
            float num7 = 0f;
            float num8 = 0f;
            length = rawtext.Length;
            for (int i = 0; i < (length - 1); i++)
            {
                if (rawtext[i] < 0)
                {
                    int num9;
                    int num10;
                    num3++;
                    if ((((((sbyte)Identity((long)0xa1L)) <= rawtext[i]) && (rawtext[i] <= ((sbyte)Identity((long)0xf7L)))) && (((sbyte)Identity((long)0xa1L)) <= rawtext[i + 1])) && (rawtext[i + 1] <= ((sbyte)Identity((long)0xfeL))))
                    {
                        num4++;
                        num6 += 500L;
                        num9 = (rawtext[i] + 0x100) - 0xa1;
                        num10 = (rawtext[i + 1] + 0x100) - 0xa1;
                        if (GBFreq[num9][num10] != 0)
                        {
                            num5 += GBFreq[num9][num10];
                        }
                        else if ((15 <= num9) && (num9 < 0x37))
                        {
                            num5 += 200L;
                        }
                    }
                    else if (((((sbyte)Identity((long)0x81L)) <= rawtext[i]) && (rawtext[i] <= ((sbyte)Identity((long)0xfeL)))) && (((((sbyte)Identity((long)0x80L)) <= rawtext[i + 1]) && (rawtext[i + 1] <= ((sbyte)Identity((long)0xfeL)))) || ((0x40 <= rawtext[i + 1]) && (rawtext[i + 1] <= 0x7e))))
                    {
                        num4++;
                        num6 += 500L;
                        num9 = (rawtext[i] + 0x100) - 0x81;
                        if ((0x40 <= rawtext[i + 1]) && (rawtext[i + 1] <= 0x7e))
                        {
                            num10 = rawtext[i + 1] - 0x40;
                        }
                        else
                        {
                            num10 = (rawtext[i + 1] + 0x100) - 0x80;
                        }
                        if (GBKFreq[num9][num10] != 0)
                        {
                            num5 += GBKFreq[num9][num10];
                        }
                    }
                    i++;
                }
            }
            num7 = 50f * (((float)num4) / ((float)num3));
            num8 = 50f * (((float)num5) / ((float)num6));
            return (((int)(num7 + num8)) - 1);
        }

        public virtual string GetEncodingName(FileInfo testfile)
        {
            FileStream sourceStream = null;
            sbyte[] target = new sbyte[(int)FileLength(testfile)];
            try
            {
                sourceStream = new FileStream(testfile.FullName, FileMode.Open, FileAccess.Read);
                ReadInput(sourceStream, ref target, 0, target.Length);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (null != sourceStream)
                {
                    sourceStream.Close();
                }
            }

            return this.GetEncodingName(target);
        }

        public virtual string GetEncodingName(Uri testurl)
        {
            sbyte[] target = new sbyte[0x400];
            int num = 0;
            int start = 0;
            try
            {
                Stream responseStream = WebRequest.Create(testurl.AbsoluteUri).GetResponse().GetResponseStream();
                while ((num = ReadInput(responseStream, ref target, start, target.Length - start)) > 0)
                {
                    start += num;
                }
                responseStream.Close();
            }
            catch
            {
                throw;
            }
            return this.GetEncodingName(target);
        }
        /// <summary>
        /// 判断是否是不带 BOM 的 UTF8 格式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool IsUTF8Bytes(sbyte[] data)
        {
            int charByteCounter = 1;　 //计算当前正分析的字符应还有的字节数
            byte curByte; //当前分析的字节.
            for (int i = 0; i < data.Length; i++)
            {
                curByte = (byte)data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式!");
            }
            return true;
        }
   
    public virtual string GetEncodingName(sbyte[] rawtext)
        {
            int num2 = 0;
            int index = 0;
            int[] numArray = new int[] { this.GB2312Probability(rawtext), this.GBKProbability(rawtext), this.HZProbability(rawtext), this.BIG5Probability(rawtext), this.ENCTWProbability(rawtext), this.ISO2022CNProbability(rawtext), this.UTF8Probability(rawtext), this.UnicodeProbability(rawtext), this.ASCIIProbability(rawtext), 0 };
            for (int i = 0; i < 10; i++)
            {
                if (numArray[i] > num2)
                {
                    index = i;
                    num2 = numArray[i];
                }
            }
            if (index == 6) {
                if ((byte)rawtext[0] == 0xEF && (byte)rawtext[1] == 0xBB && (byte)rawtext[2] == 0xBF)
                {
                    index = 9;
                }
            }
            if (num2 <= 50)
            {
                index = 10;
            }
            
            return nicename[index];
        }

        internal virtual int HZProbability(sbyte[] rawtext)
        {
            int num3 = 0;
            int num4 = 1;
            long num5 = 0L;
            long num6 = 1L;
            float num7 = 0f;
            float num8 = 0f;
            int num9 = 0;
            int num10 = 0;
            int length = rawtext.Length;
            for (int i = 0; i < length; i++)
            {
                if (rawtext[i] == 0x7e)
                {
                    if (rawtext[i + 1] == 0x7b)
                    {
                        num9++;
                        i += 2;
                        while (i < (length - 1))
                        {
                            int num11;
                            int num12;
                            if ((rawtext[i] == 10) || (rawtext[i] == 13))
                            {
                                break;
                            }
                            if ((rawtext[i] == 0x7e) && (rawtext[i + 1] == 0x7d))
                            {
                                num10++;
                                i++;
                                break;
                            }
                            if (((0x21 <= rawtext[i]) && (rawtext[i] <= 0x77)) && ((0x21 <= rawtext[i + 1]) && (rawtext[i + 1] <= 0x77)))
                            {
                                num3 += 2;
                                num11 = rawtext[i] - 0x21;
                                num12 = rawtext[i + 1] - 0x21;
                                num6 += 500L;
                                if (GBFreq[num11][num12] != 0)
                                {
                                    num5 += GBFreq[num11][num12];
                                }
                                else if ((15 <= num11) && (num11 < 0x37))
                                {
                                    num5 += 200L;
                                }
                            }
                            else if (((0xa1 <= (byte)rawtext[i]) && ((byte)rawtext[i] <= 0xf7)) && ((0xa1 <= (byte)rawtext[i + 1]) && ((byte)rawtext[i + 1] <= 0xf7)))
                            {
                                num3 += 2;
                                num11 = (rawtext[i] + 0x100) - 0xa1;
                                num12 = (rawtext[i + 1] + 0x100) - 0xa1;
                                num6 += 500L;
                                if (GBFreq[num11][num12] != 0)
                                {
                                    num5 += GBFreq[num11][num12];
                                }
                                else if ((15 <= num11) && (num11 < 0x37))
                                {
                                    num5 += 200L;
                                }
                            }
                            num4 += 2;
                            i += 2;
                        }
                    }
                    else if (rawtext[i + 1] == 0x7d)
                    {
                        num10++;
                        i++;
                    }
                    else if (rawtext[i + 1] == 0x7e)
                    {
                        i++;
                    }
                }
            }
            if (num9 > 4)
            {
                num7 = 50f;
            }
            else if (num9 > 1)
            {
                num7 = 41f;
            }
            else if (num9 > 0)
            {
                num7 = 39f;
            }
            else
            {
                num7 = 0f;
            }
            num8 = 50f * (((float)num5) / ((float)num6));
            return (int)(num7 + num8);
        }

        public static double Identity(double literal)
        {
            return literal;
        }

        public static long Identity(long literal)
        {
            return literal;
        }

        public static float Identity(float literal)
        {
            return literal;
        }

        public static ulong Identity(ulong literal)
        {
            return literal;
        }

        internal virtual void Initialize_Frequencies()
        {
            int num;
            if (GBFreq[0] == null)
            {
                for (num = 0; num < 0x5e; num++)
                {
                    GBFreq[num] = new int[0x5e];
                }
                GBFreq[0x31][0x1a] = 0x256;
                GBFreq[0x29][0x26] = 0x255;
                GBFreq[0x11][0x1a] = 0x254;
                GBFreq[0x20][0x2a] = 0x253;
                GBFreq[0x27][0x2a] = 0x252;
                GBFreq[0x2d][0x31] = 0x251;
                GBFreq[0x33][0x39] = 0x250;
                GBFreq[50][0x2f] = 0x24f;
                GBFreq[0x2a][90] = 590;
                GBFreq[0x34][0x41] = 0x24d;
                GBFreq[0x35][0x2f] = 0x24c;
                GBFreq[0x13][0x52] = 0x24b;
                GBFreq[0x1f][0x13] = 0x24a;
                GBFreq[40][0x2e] = 0x249;
                GBFreq[0x18][0x59] = 0x248;
                GBFreq[0x17][0x55] = 0x247;
                GBFreq[20][0x1c] = 0x246;
                GBFreq[0x2a][20] = 0x245;
                GBFreq[0x22][0x26] = 580;
                GBFreq[0x2d][9] = 0x243;
                GBFreq[0x36][50] = 0x242;
                GBFreq[0x19][0x2c] = 0x241;
                GBFreq[0x23][0x42] = 0x240;
                GBFreq[20][0x37] = 0x23f;
                GBFreq[0x12][0x55] = 0x23e;
                GBFreq[20][0x1f] = 0x23d;
                GBFreq[0x31][0x11] = 0x23c;
                GBFreq[0x29][0x10] = 0x23b;
                GBFreq[0x23][0x49] = 570;
                GBFreq[20][0x22] = 0x239;
                GBFreq[0x1d][0x2c] = 0x238;
                GBFreq[0x23][0x26] = 0x237;
                GBFreq[0x31][9] = 0x236;
                GBFreq[0x2e][0x21] = 0x235;
                GBFreq[0x31][0x33] = 0x234;
                GBFreq[40][0x59] = 0x233;
                GBFreq[0x1a][0x40] = 0x232;
                GBFreq[0x36][0x33] = 0x231;
                GBFreq[0x36][0x24] = 560;
                GBFreq[0x27][4] = 0x22f;
                GBFreq[0x35][13] = 0x22e;
                GBFreq[0x18][0x5c] = 0x22d;
                GBFreq[0x1b][0x31] = 0x22c;
                GBFreq[0x30][6] = 0x22b;
                GBFreq[0x15][0x33] = 0x22a;
                GBFreq[30][40] = 0x229;
                GBFreq[0x2a][0x5c] = 0x228;
                GBFreq[0x1f][0x4e] = 0x227;
                GBFreq[0x19][0x52] = 550;
                GBFreq[0x2f][0] = 0x225;
                GBFreq[0x22][0x13] = 0x224;
                GBFreq[0x2f][0x23] = 0x223;
                GBFreq[0x15][0x3f] = 0x222;
                GBFreq[0x2b][0x4b] = 0x221;
                GBFreq[0x15][0x57] = 0x220;
                GBFreq[0x23][0x3b] = 0x21f;
                GBFreq[0x19][0x22] = 0x21e;
                GBFreq[0x15][0x1b] = 0x21d;
                GBFreq[0x27][0x1a] = 540;
                GBFreq[0x22][0x1a] = 0x21b;
                GBFreq[0x27][0x34] = 0x21a;
                GBFreq[50][0x39] = 0x219;
                GBFreq[0x25][0x4f] = 0x218;
                GBFreq[0x1a][0x18] = 0x217;
                GBFreq[0x16][1] = 0x216;
                GBFreq[0x12][40] = 0x215;
                GBFreq[0x29][0x21] = 0x214;
                GBFreq[0x35][0x1a] = 0x213;
                GBFreq[0x36][0x56] = 530;
                GBFreq[20][0x10] = 0x211;
                GBFreq[0x2e][0x4a] = 0x210;
                GBFreq[30][0x13] = 0x20f;
                GBFreq[0x2d][0x23] = 0x20e;
                GBFreq[0x2d][0x3d] = 0x20d;
                GBFreq[30][9] = 0x20c;
                GBFreq[0x29][0x35] = 0x20b;
                GBFreq[0x29][13] = 0x20a;
                GBFreq[50][0x22] = 0x209;
                GBFreq[0x35][0x56] = 520;
                GBFreq[0x2f][0x2f] = 0x207;
                GBFreq[0x16][0x1c] = 0x206;
                GBFreq[50][0x35] = 0x205;
                GBFreq[0x27][70] = 0x204;
                GBFreq[0x26][15] = 0x203;
                GBFreq[0x2a][0x58] = 0x202;
                GBFreq[0x10][0x1d] = 0x201;
                GBFreq[0x1b][90] = 0x200;
                GBFreq[0x1d][12] = 0x1ff;
                GBFreq[0x2c][0x16] = 510;
                GBFreq[0x22][0x45] = 0x1fd;
                GBFreq[0x18][10] = 0x1fc;
                GBFreq[0x2c][11] = 0x1fb;
                GBFreq[0x27][0x5c] = 0x1fa;
                GBFreq[0x31][0x30] = 0x1f9;
                GBFreq[0x1f][0x2e] = 0x1f8;
                GBFreq[0x13][50] = 0x1f7;
                GBFreq[0x15][14] = 0x1f6;
                GBFreq[0x20][0x1c] = 0x1f5;
                GBFreq[0x12][3] = 500;
                GBFreq[0x35][9] = 0x1f3;
                GBFreq[0x22][80] = 0x1f2;
                GBFreq[0x30][0x58] = 0x1f1;
                GBFreq[0x2e][0x35] = 0x1f0;
                GBFreq[0x16][0x35] = 0x1ef;
                GBFreq[0x1c][10] = 0x1ee;
                GBFreq[0x2c][0x41] = 0x1ed;
                GBFreq[20][10] = 0x1ec;
                GBFreq[40][0x4c] = 0x1eb;
                GBFreq[0x2f][8] = 490;
                GBFreq[50][0x4a] = 0x1e9;
                GBFreq[0x17][0x3e] = 0x1e8;
                GBFreq[0x31][0x41] = 0x1e7;
                GBFreq[0x1c][0x57] = 0x1e6;
                GBFreq[15][0x30] = 0x1e5;
                GBFreq[0x16][7] = 0x1e4;
                GBFreq[0x13][0x2a] = 0x1e3;
                GBFreq[0x29][20] = 0x1e2;
                GBFreq[0x1a][0x37] = 0x1e1;
                GBFreq[0x15][0x5d] = 480;
                GBFreq[0x1f][0x4c] = 0x1df;
                GBFreq[0x22][0x1f] = 0x1de;
                GBFreq[20][0x42] = 0x1dd;
                GBFreq[0x33][0x21] = 0x1dc;
                GBFreq[0x22][0x56] = 0x1db;
                GBFreq[0x25][0x43] = 0x1da;
                GBFreq[0x35][0x35] = 0x1d9;
                GBFreq[40][0x58] = 0x1d8;
                GBFreq[0x27][10] = 0x1d7;
                GBFreq[0x18][3] = 470;
                GBFreq[0x1b][0x19] = 0x1d5;
                GBFreq[0x1a][15] = 0x1d4;
                GBFreq[0x15][0x58] = 0x1d3;
                GBFreq[0x34][0x3e] = 0x1d2;
                GBFreq[0x2e][0x51] = 0x1d1;
                GBFreq[0x26][0x48] = 0x1d0;
                GBFreq[0x11][30] = 0x1cf;
                GBFreq[0x34][0x5c] = 0x1ce;
                GBFreq[0x22][90] = 0x1cd;
                GBFreq[0x15][7] = 460;
                GBFreq[0x24][13] = 0x1cb;
                GBFreq[0x2d][0x29] = 0x1ca;
                GBFreq[0x20][5] = 0x1c9;
                GBFreq[0x1a][0x59] = 0x1c8;
                GBFreq[0x17][0x57] = 0x1c7;
                GBFreq[20][0x27] = 0x1c6;
                GBFreq[0x1b][0x17] = 0x1c5;
                GBFreq[0x19][0x3b] = 0x1c4;
                GBFreq[0x31][20] = 0x1c3;
                GBFreq[0x36][0x4d] = 450;
                GBFreq[0x1b][0x43] = 0x1c1;
                GBFreq[0x2f][0x21] = 0x1c0;
                GBFreq[0x29][0x11] = 0x1bf;
                GBFreq[0x13][0x51] = 0x1be;
                GBFreq[0x10][0x42] = 0x1bd;
                GBFreq[0x2d][0x1a] = 0x1bc;
                GBFreq[0x31][0x51] = 0x1bb;
                GBFreq[0x35][0x37] = 0x1ba;
                GBFreq[0x10][0x1a] = 0x1b9;
                GBFreq[0x36][0x3e] = 440;
                GBFreq[20][70] = 0x1b7;
                GBFreq[0x2a][0x23] = 0x1b6;
                GBFreq[20][0x39] = 0x1b5;
                GBFreq[0x22][0x24] = 0x1b4;
                GBFreq[0x2e][0x3f] = 0x1b3;
                GBFreq[0x13][0x2d] = 0x1b2;
                GBFreq[0x15][10] = 0x1b1;
                GBFreq[0x34][0x5d] = 0x1b0;
                GBFreq[0x19][2] = 0x1af;
                GBFreq[30][0x39] = 430;
                GBFreq[0x29][0x18] = 0x1ad;
                GBFreq[0x1c][0x2b] = 0x1ac;
                GBFreq[0x2d][0x56] = 0x1ab;
                GBFreq[0x33][0x38] = 0x1aa;
                GBFreq[0x25][0x1c] = 0x1a9;
                GBFreq[0x34][0x45] = 0x1a8;
                GBFreq[0x2b][0x5c] = 0x1a7;
                GBFreq[0x29][0x1f] = 0x1a6;
                GBFreq[0x25][0x57] = 0x1a5;
                GBFreq[0x2f][0x24] = 420;
                GBFreq[0x10][0x10] = 0x1a3;
                GBFreq[40][0x38] = 0x1a2;
                GBFreq[0x18][0x37] = 0x1a1;
                GBFreq[0x11][1] = 0x1a0;
                GBFreq[0x23][0x39] = 0x19f;
                GBFreq[0x1b][50] = 0x19e;
                GBFreq[0x1a][14] = 0x19d;
                GBFreq[50][40] = 0x19c;
                GBFreq[0x27][0x13] = 0x19b;
                GBFreq[0x13][0x59] = 410;
                GBFreq[0x1d][0x5b] = 0x199;
                GBFreq[0x11][0x59] = 0x198;
                GBFreq[0x27][0x4a] = 0x197;
                GBFreq[0x2e][0x27] = 0x196;
                GBFreq[40][0x1c] = 0x195;
                GBFreq[0x2d][0x44] = 0x194;
                GBFreq[0x2b][10] = 0x193;
                GBFreq[0x2a][13] = 0x192;
                GBFreq[0x2c][0x51] = 0x191;
                GBFreq[0x29][0x2f] = 400;
                GBFreq[0x30][0x3a] = 0x18f;
                GBFreq[0x2b][0x44] = 0x18e;
                GBFreq[0x10][0x4f] = 0x18d;
                GBFreq[0x13][5] = 0x18c;
                GBFreq[0x36][0x3b] = 0x18b;
                GBFreq[0x11][0x24] = 0x18a;
                GBFreq[0x12][0] = 0x189;
                GBFreq[0x29][5] = 0x188;
                GBFreq[0x29][0x48] = 0x187;
                GBFreq[0x10][0x27] = 390;
                GBFreq[0x36][0] = 0x185;
                GBFreq[0x33][0x10] = 0x184;
                GBFreq[0x1d][0x24] = 0x183;
                GBFreq[0x2f][5] = 0x182;
                GBFreq[0x2f][0x33] = 0x181;
                GBFreq[0x2c][7] = 0x180;
                GBFreq[0x23][30] = 0x17f;
                GBFreq[0x1a][9] = 0x17e;
                GBFreq[0x10][7] = 0x17d;
                GBFreq[0x20][1] = 380;
                GBFreq[0x21][0x4c] = 0x17b;
                GBFreq[0x22][0x5b] = 0x17a;
                GBFreq[0x34][0x24] = 0x179;
                GBFreq[0x1a][0x4d] = 0x178;
                GBFreq[0x23][0x30] = 0x177;
                GBFreq[40][80] = 0x176;
                GBFreq[0x29][0x5c] = 0x175;
                GBFreq[0x1b][0x5d] = 0x174;
                GBFreq[15][0x11] = 0x173;
                GBFreq[0x10][0x4c] = 370;
                GBFreq[0x33][12] = 0x171;
                GBFreq[0x12][20] = 0x170;
                GBFreq[15][0x36] = 0x16f;
                GBFreq[50][5] = 0x16e;
                GBFreq[0x21][0x16] = 0x16d;
                GBFreq[0x25][0x39] = 0x16c;
                GBFreq[0x1c][0x2f] = 0x16b;
                GBFreq[0x2a][0x1f] = 0x16a;
                GBFreq[0x12][2] = 0x169;
                GBFreq[0x2b][0x40] = 360;
                GBFreq[0x17][0x2f] = 0x167;
                GBFreq[0x1c][0x4f] = 0x166;
                GBFreq[0x19][0x2d] = 0x165;
                GBFreq[0x17][0x5b] = 0x164;
                GBFreq[0x16][0x13] = 0x163;
                GBFreq[0x19][0x2e] = 0x162;
                GBFreq[0x16][0x24] = 0x161;
                GBFreq[0x36][0x55] = 0x160;
                GBFreq[0x2e][20] = 0x15f;
                GBFreq[0x1b][0x25] = 350;
                GBFreq[0x1a][0x51] = 0x15d;
                GBFreq[0x2a][0x1d] = 0x15c;
                GBFreq[0x1f][90] = 0x15b;
                GBFreq[0x29][0x3b] = 0x15a;
                GBFreq[0x18][0x41] = 0x159;
                GBFreq[0x2c][0x54] = 0x158;
                GBFreq[0x18][90] = 0x157;
                GBFreq[0x26][0x36] = 0x156;
                GBFreq[0x1c][70] = 0x155;
                GBFreq[0x1b][15] = 340;
                GBFreq[0x1c][80] = 0x153;
                GBFreq[0x1d][8] = 0x152;
                GBFreq[0x2d][80] = 0x151;
                GBFreq[0x35][0x25] = 0x150;
                GBFreq[0x1c][0x41] = 0x14f;
                GBFreq[0x17][0x56] = 0x14e;
                GBFreq[0x27][0x2d] = 0x14d;
                GBFreq[0x35][0x20] = 0x14c;
                GBFreq[0x26][0x44] = 0x14b;
                GBFreq[0x2d][0x4e] = 330;
                GBFreq[0x2b][7] = 0x149;
                GBFreq[0x2e][0x52] = 0x148;
                GBFreq[0x1b][0x26] = 0x147;
                GBFreq[0x10][0x3e] = 0x146;
                GBFreq[0x18][0x11] = 0x145;
                GBFreq[0x16][70] = 0x144;
                GBFreq[0x34][0x1c] = 0x143;
                GBFreq[0x17][40] = 0x142;
                GBFreq[0x1c][50] = 0x141;
                GBFreq[0x2a][0x5b] = 320;
                GBFreq[0x2f][0x4c] = 0x13f;
                GBFreq[15][0x2a] = 0x13e;
                GBFreq[0x2b][0x37] = 0x13d;
                GBFreq[0x1d][0x54] = 0x13c;
                GBFreq[0x2c][90] = 0x13b;
                GBFreq[0x35][0x10] = 0x13a;
                GBFreq[0x16][0x5d] = 0x139;
                GBFreq[0x22][10] = 0x138;
                GBFreq[0x20][0x35] = 0x137;
                GBFreq[0x2b][0x41] = 310;
                GBFreq[0x1c][7] = 0x135;
                GBFreq[0x23][0x2e] = 0x134;
                GBFreq[0x15][0x27] = 0x133;
                GBFreq[0x2c][0x12] = 0x132;
                GBFreq[40][10] = 0x131;
                GBFreq[0x36][0x35] = 0x130;
                GBFreq[0x26][0x4a] = 0x12f;
                GBFreq[0x1c][0x1a] = 0x12e;
                GBFreq[15][13] = 0x12d;
                GBFreq[0x27][0x22] = 300;
                GBFreq[0x27][0x2e] = 0x12b;
                GBFreq[0x2a][0x42] = 0x12a;
                GBFreq[0x21][0x3a] = 0x129;
                GBFreq[15][0x38] = 0x128;
                GBFreq[0x12][0x33] = 0x127;
                GBFreq[0x31][0x44] = 0x126;
                GBFreq[30][0x25] = 0x125;
                GBFreq[0x33][0x54] = 0x124;
                GBFreq[0x33][9] = 0x123;
                GBFreq[40][70] = 290;
                GBFreq[0x29][0x54] = 0x121;
                GBFreq[0x1c][0x40] = 0x120;
                GBFreq[0x20][0x58] = 0x11f;
                GBFreq[0x18][5] = 0x11e;
                GBFreq[0x35][0x17] = 0x11d;
                GBFreq[0x2a][0x1b] = 0x11c;
                GBFreq[0x16][0x26] = 0x11b;
                GBFreq[0x20][0x56] = 0x11a;
                GBFreq[0x22][30] = 0x119;
                GBFreq[0x26][0x3f] = 280;
                GBFreq[0x18][0x3b] = 0x117;
                GBFreq[0x16][0x51] = 0x116;
                GBFreq[0x20][11] = 0x115;
                GBFreq[0x33][0x15] = 0x114;
                GBFreq[0x36][0x29] = 0x113;
                GBFreq[0x15][50] = 0x112;
                GBFreq[0x17][0x59] = 0x111;
                GBFreq[0x13][0x57] = 0x110;
                GBFreq[0x1a][7] = 0x10f;
                GBFreq[30][0x4b] = 270;
                GBFreq[0x2b][0x54] = 0x10d;
                GBFreq[0x33][0x19] = 0x10c;
                GBFreq[0x10][0x43] = 0x10b;
                GBFreq[0x20][9] = 0x10a;
                GBFreq[0x30][0x33] = 0x109;
                GBFreq[0x27][7] = 0x108;
                GBFreq[0x2c][0x58] = 0x107;
                GBFreq[0x34][0x18] = 0x106;
                GBFreq[0x17][0x22] = 0x105;
                GBFreq[0x20][0x4b] = 260;
                GBFreq[0x13][10] = 0x103;
                GBFreq[0x1c][0x5b] = 0x102;
                GBFreq[0x20][0x53] = 0x101;
                GBFreq[0x19][0x4b] = 0x100;
                GBFreq[0x35][0x2d] = 0xff;
                GBFreq[0x1d][0x55] = 0xfe;
                GBFreq[0x35][0x3b] = 0xfd;
                GBFreq[0x10][2] = 0xfc;
                GBFreq[0x13][0x4e] = 0xfb;
                GBFreq[15][0x4b] = 250;
                GBFreq[0x33][0x2a] = 0xf9;
                GBFreq[0x2d][0x43] = 0xf8;
                GBFreq[15][0x4a] = 0xf7;
                GBFreq[0x19][0x51] = 0xf6;
                GBFreq[0x25][0x3e] = 0xf5;
                GBFreq[0x10][0x37] = 0xf4;
                GBFreq[0x12][0x26] = 0xf3;
                GBFreq[0x17][0x17] = 0xf2;
                GBFreq[0x26][30] = 0xf1;
                GBFreq[0x11][0x1c] = 240;
                GBFreq[0x2c][0x49] = 0xef;
                GBFreq[0x17][0x4e] = 0xee;
                GBFreq[40][0x4d] = 0xed;
                GBFreq[0x26][0x57] = 0xec;
                GBFreq[0x1b][0x13] = 0xeb;
                GBFreq[0x26][0x52] = 0xea;
                GBFreq[0x25][0x16] = 0xe9;
                GBFreq[0x29][30] = 0xe8;
                GBFreq[0x36][9] = 0xe7;
                GBFreq[0x20][30] = 230;
                GBFreq[30][0x34] = 0xe5;
                GBFreq[40][0x54] = 0xe4;
                GBFreq[0x35][0x39] = 0xe3;
                GBFreq[0x1b][0x1b] = 0xe2;
                GBFreq[0x26][0x40] = 0xe1;
                GBFreq[0x12][0x2b] = 0xe0;
                GBFreq[0x17][0x45] = 0xdf;
                GBFreq[0x1c][12] = 0xde;
                GBFreq[50][0x4e] = 0xdd;
                GBFreq[50][1] = 220;
                GBFreq[0x1a][0x58] = 0xdb;
                GBFreq[0x24][40] = 0xda;
                GBFreq[0x21][0x59] = 0xd9;
                GBFreq[0x29][0x1c] = 0xd8;
                GBFreq[0x1f][0x4d] = 0xd7;
                GBFreq[0x2e][1] = 0xd6;
                GBFreq[0x2f][0x13] = 0xd5;
                GBFreq[0x23][0x37] = 0xd4;
                GBFreq[0x29][0x15] = 0xd3;
                GBFreq[0x1b][10] = 210;
                GBFreq[0x20][0x4d] = 0xd1;
                GBFreq[0x1a][0x25] = 0xd0;
                GBFreq[20][0x21] = 0xcf;
                GBFreq[0x29][0x34] = 0xce;
                GBFreq[0x20][0x12] = 0xcd;
                GBFreq[0x26][13] = 0xcc;
                GBFreq[20][0x12] = 0xcb;
                GBFreq[20][0x18] = 0xca;
                GBFreq[0x2d][0x13] = 0xc9;
                GBFreq[0x12][0x35] = 200;
            }
            if (GBKFreq[0] == null)
            {
                for (num = 0; num < 0x7e; num++)
                {
                    GBKFreq[num] = new int[0xbf];
                }
                GBKFreq[0x49][0x87] = 0x257;
                GBKFreq[0x31][0x7b] = 0x256;
                GBKFreq[0x4d][0x92] = 0x255;
                GBKFreq[0x51][0x7b] = 0x254;
                GBKFreq[0x52][0x90] = 0x253;
                GBKFreq[0x33][0xb3] = 0x252;
                GBKFreq[0x53][0x9a] = 0x251;
                GBKFreq[0x47][0x8b] = 0x250;
                GBKFreq[0x40][0x8b] = 0x24f;
                GBKFreq[0x55][0x90] = 590;
                GBKFreq[0x34][0x7d] = 0x24d;
                GBKFreq[0x58][0x19] = 0x24c;
                GBKFreq[0x51][0x6a] = 0x24b;
                GBKFreq[0x51][0x94] = 0x24a;
                GBKFreq[0x3e][0x89] = 0x249;
                GBKFreq[0x5e][0] = 0x248;
                GBKFreq[1][0x40] = 0x247;
                GBKFreq[0x43][0xa3] = 0x246;
                GBKFreq[20][190] = 0x245;
                GBKFreq[0x39][0x83] = 580;
                GBKFreq[0x1d][0xa9] = 0x243;
                GBKFreq[0x48][0x8f] = 0x242;
                GBKFreq[0][0xad] = 0x241;
                GBKFreq[11][0x17] = 0x240;
                GBKFreq[0x3d][0x8d] = 0x23f;
                GBKFreq[60][0x7b] = 0x23e;
                GBKFreq[0x51][0x72] = 0x23d;
                GBKFreq[0x52][0x83] = 0x23c;
                GBKFreq[0x43][0x9c] = 0x23b;
                GBKFreq[0x47][0xa7] = 570;
                GBKFreq[20][50] = 0x239;
                GBKFreq[0x4d][0x84] = 0x238;
                GBKFreq[0x54][0x26] = 0x237;
                GBKFreq[0x1a][0x1d] = 0x236;
                GBKFreq[0x4a][0xbb] = 0x235;
                GBKFreq[0x3e][0x74] = 0x234;
                GBKFreq[0x43][0x87] = 0x233;
                GBKFreq[5][0x56] = 0x232;
                GBKFreq[0x48][0xba] = 0x231;
                GBKFreq[0x4b][0xa1] = 560;
                GBKFreq[0x4e][130] = 0x22f;
                GBKFreq[0x5e][30] = 0x22e;
                GBKFreq[0x54][0x48] = 0x22d;
                GBKFreq[1][0x43] = 0x22c;
                GBKFreq[0x4b][0xac] = 0x22b;
                GBKFreq[0x4a][0xb9] = 0x22a;
                GBKFreq[0x35][160] = 0x229;
                GBKFreq[0x7b][14] = 0x228;
                GBKFreq[0x4f][0x61] = 0x227;
                GBKFreq[0x55][110] = 550;
                GBKFreq[0x4e][0xab] = 0x225;
                GBKFreq[0x34][0x83] = 0x224;
                GBKFreq[0x38][100] = 0x223;
                GBKFreq[50][0xb6] = 0x222;
                GBKFreq[0x5e][0x40] = 0x221;
                GBKFreq[0x6a][0x4a] = 0x220;
                GBKFreq[11][0x66] = 0x21f;
                GBKFreq[0x35][0x7c] = 0x21e;
                GBKFreq[0x18][3] = 0x21d;
                GBKFreq[0x56][0x94] = 540;
                GBKFreq[0x35][0xb8] = 0x21b;
                GBKFreq[0x56][0x93] = 0x21a;
                GBKFreq[0x60][0xa1] = 0x219;
                GBKFreq[0x52][0x4d] = 0x218;
                GBKFreq[0x3b][0x92] = 0x217;
                GBKFreq[0x54][0x7e] = 0x216;
                GBKFreq[0x4f][0x84] = 0x215;
                GBKFreq[0x55][0x7b] = 0x214;
                GBKFreq[0x47][0x65] = 0x213;
                GBKFreq[0x55][0x6a] = 530;
                GBKFreq[6][0xb8] = 0x211;
                GBKFreq[0x39][0x9c] = 0x210;
                GBKFreq[0x4b][0x68] = 0x20f;
                GBKFreq[50][0x89] = 0x20e;
                GBKFreq[0x4f][0x85] = 0x20d;
                GBKFreq[0x4c][0x6c] = 0x20c;
                GBKFreq[0x39][0x8e] = 0x20b;
                GBKFreq[0x54][130] = 0x20a;
                GBKFreq[0x34][0x80] = 0x209;
                GBKFreq[0x2f][0x2c] = 520;
                GBKFreq[0x34][0x98] = 0x207;
                GBKFreq[0x36][0x68] = 0x206;
                GBKFreq[30][0x2f] = 0x205;
                GBKFreq[0x47][0x7b] = 0x204;
                GBKFreq[0x34][0x6b] = 0x203;
                GBKFreq[0x2d][0x54] = 0x202;
                GBKFreq[0x6b][0x76] = 0x201;
                GBKFreq[5][0xa1] = 0x200;
                GBKFreq[0x30][0x7e] = 0x1ff;
                GBKFreq[0x43][170] = 510;
                GBKFreq[0x2b][6] = 0x1fd;
                GBKFreq[70][0x70] = 0x1fc;
                GBKFreq[0x56][0xae] = 0x1fb;
                GBKFreq[0x54][0xa6] = 0x1fa;
                GBKFreq[0x4f][130] = 0x1f9;
                GBKFreq[0x39][0x8d] = 0x1f8;
                GBKFreq[0x51][0xb2] = 0x1f7;
                GBKFreq[0x38][0xbb] = 0x1f6;
                GBKFreq[0x51][0xa2] = 0x1f5;
                GBKFreq[0x35][0x68] = 500;
                GBKFreq[0x7b][0x23] = 0x1f3;
                GBKFreq[70][0xa9] = 0x1f2;
                GBKFreq[0x45][0xa4] = 0x1f1;
                GBKFreq[0x6d][0x3d] = 0x1f0;
                GBKFreq[0x49][130] = 0x1ef;
                GBKFreq[0x3e][0x86] = 0x1ee;
                GBKFreq[0x36][0x7d] = 0x1ed;
                GBKFreq[0x4f][0x69] = 0x1ec;
                GBKFreq[70][0xa5] = 0x1eb;
                GBKFreq[0x47][0xbd] = 490;
                GBKFreq[0x17][0x93] = 0x1e9;
                GBKFreq[0x33][0x8b] = 0x1e8;
                GBKFreq[0x2f][0x89] = 0x1e7;
                GBKFreq[0x4d][0x7b] = 0x1e6;
                GBKFreq[0x56][0xb7] = 0x1e5;
                GBKFreq[0x3f][0xad] = 0x1e4;
                GBKFreq[0x4f][0x90] = 0x1e3;
                GBKFreq[0x54][0x9f] = 0x1e2;
                GBKFreq[60][0x5b] = 0x1e1;
                GBKFreq[0x42][0xbb] = 480;
                GBKFreq[0x49][0x72] = 0x1df;
                GBKFreq[0x55][0x38] = 0x1de;
                GBKFreq[0x47][0x95] = 0x1dd;
                GBKFreq[0x54][0xbd] = 0x1dc;
                GBKFreq[0x68][0x1f] = 0x1db;
                GBKFreq[0x53][0x52] = 0x1da;
                GBKFreq[0x44][0x23] = 0x1d9;
                GBKFreq[11][0x4d] = 0x1d8;
                GBKFreq[15][0x9b] = 0x1d7;
                GBKFreq[0x53][0x99] = 470;
                GBKFreq[0x47][1] = 0x1d5;
                GBKFreq[0x35][190] = 0x1d4;
                GBKFreq[50][0x87] = 0x1d3;
                GBKFreq[3][0x93] = 0x1d2;
                GBKFreq[0x30][0x88] = 0x1d1;
                GBKFreq[0x42][0xa6] = 0x1d0;
                GBKFreq[0x37][0x9f] = 0x1cf;
                GBKFreq[0x52][150] = 0x1ce;
                GBKFreq[0x3a][0xb2] = 0x1cd;
                GBKFreq[0x40][0x66] = 460;
                GBKFreq[0x10][0x6a] = 0x1cb;
                GBKFreq[0x44][110] = 0x1ca;
                GBKFreq[0x36][14] = 0x1c9;
                GBKFreq[60][140] = 0x1c8;
                GBKFreq[0x5b][0x47] = 0x1c7;
                GBKFreq[0x36][150] = 0x1c6;
                GBKFreq[0x4e][0xb1] = 0x1c5;
                GBKFreq[0x4e][0x75] = 0x1c4;
                GBKFreq[0x68][12] = 0x1c3;
                GBKFreq[0x49][150] = 450;
                GBKFreq[0x33][0x8e] = 0x1c1;
                GBKFreq[0x51][0x91] = 0x1c0;
                GBKFreq[0x42][0xb7] = 0x1bf;
                GBKFreq[0x33][0xb2] = 0x1be;
                GBKFreq[0x4b][0x6b] = 0x1bd;
                GBKFreq[0x41][0x77] = 0x1bc;
                GBKFreq[0x45][0xb0] = 0x1bb;
                GBKFreq[0x3b][0x7a] = 0x1ba;
                GBKFreq[0x4e][160] = 0x1b9;
                GBKFreq[0x55][0xb7] = 440;
                GBKFreq[0x69][0x10] = 0x1b7;
                GBKFreq[0x49][110] = 0x1b6;
                GBKFreq[0x68][0x27] = 0x1b5;
                GBKFreq[0x77][0x10] = 0x1b4;
                GBKFreq[0x4c][0xa2] = 0x1b3;
                GBKFreq[0x43][0x98] = 0x1b2;
                GBKFreq[0x52][0x18] = 0x1b1;
                GBKFreq[0x49][0x79] = 0x1b0;
                GBKFreq[0x53][0x53] = 0x1af;
                GBKFreq[0x52][0x91] = 430;
                GBKFreq[0x31][0x85] = 0x1ad;
                GBKFreq[0x5e][13] = 0x1ac;
                GBKFreq[0x3a][0x8b] = 0x1ab;
                GBKFreq[0x4a][0xbd] = 0x1aa;
                GBKFreq[0x42][0xb1] = 0x1a9;
                GBKFreq[0x55][0xb8] = 0x1a8;
                GBKFreq[0x37][0xb7] = 0x1a7;
                GBKFreq[0x47][0x6b] = 0x1a6;
                GBKFreq[11][0x62] = 0x1a5;
                GBKFreq[0x48][0x99] = 420;
                GBKFreq[2][0x89] = 0x1a3;
                GBKFreq[0x3b][0x93] = 0x1a2;
                GBKFreq[0x3a][0x98] = 0x1a1;
                GBKFreq[0x37][0x90] = 0x1a0;
                GBKFreq[0x49][0x7d] = 0x19f;
                GBKFreq[0x34][0x9a] = 0x19e;
                GBKFreq[70][0xb2] = 0x19d;
                GBKFreq[0x4f][0x94] = 0x19c;
                GBKFreq[0x3f][0x8f] = 0x19b;
                GBKFreq[50][140] = 410;
                GBKFreq[0x2f][0x91] = 0x199;
                GBKFreq[0x30][0x7b] = 0x198;
                GBKFreq[0x38][0x6b] = 0x197;
                GBKFreq[0x54][0x53] = 0x196;
                GBKFreq[0x3b][0x70] = 0x195;
                GBKFreq[0x7c][0x48] = 0x194;
                GBKFreq[0x4f][0x63] = 0x193;
                GBKFreq[3][0x25] = 0x192;
                GBKFreq[0x72][0x37] = 0x191;
                GBKFreq[0x55][0x98] = 400;
                GBKFreq[60][0x2f] = 0x18f;
                GBKFreq[0x41][0x60] = 0x18e;
                GBKFreq[0x4a][110] = 0x18d;
                GBKFreq[0x56][0xb6] = 0x18c;
                GBKFreq[50][0x63] = 0x18b;
                GBKFreq[0x43][0xba] = 0x18a;
                GBKFreq[0x51][0x4a] = 0x189;
                GBKFreq[80][0x25] = 0x188;
                GBKFreq[0x15][60] = 0x187;
                GBKFreq[110][12] = 390;
                GBKFreq[60][0xa2] = 0x185;
                GBKFreq[0x1d][0x73] = 0x184;
                GBKFreq[0x53][130] = 0x183;
                GBKFreq[0x34][0x88] = 0x182;
                GBKFreq[0x3f][0x72] = 0x181;
                GBKFreq[0x31][0x7f] = 0x180;
                GBKFreq[0x53][0x6d] = 0x17f;
                GBKFreq[0x42][0x80] = 0x17e;
                GBKFreq[0x4e][0x88] = 0x17d;
                GBKFreq[0x51][180] = 380;
                GBKFreq[0x4c][0x68] = 0x17b;
                GBKFreq[0x38][0x9c] = 0x17a;
                GBKFreq[0x3d][0x17] = 0x179;
                GBKFreq[4][30] = 0x178;
                GBKFreq[0x45][0x9a] = 0x177;
                GBKFreq[100][0x25] = 0x176;
                GBKFreq[0x36][0xb1] = 0x175;
                GBKFreq[0x17][0x77] = 0x174;
                GBKFreq[0x47][0xab] = 0x173;
                GBKFreq[0x54][0x92] = 370;
                GBKFreq[20][0xb8] = 0x171;
                GBKFreq[0x56][0x4c] = 0x170;
                GBKFreq[0x4a][0x84] = 0x16f;
                GBKFreq[0x2f][0x61] = 0x16e;
                GBKFreq[0x52][0x89] = 0x16d;
                GBKFreq[0x5e][0x38] = 0x16c;
                GBKFreq[0x5c][30] = 0x16b;
                GBKFreq[0x13][0x75] = 0x16a;
                GBKFreq[0x30][0xad] = 0x169;
                GBKFreq[2][0x88] = 360;
                GBKFreq[7][0xb6] = 0x167;
                GBKFreq[0x4a][0xbc] = 0x166;
                GBKFreq[14][0x84] = 0x165;
                GBKFreq[0x3e][0xac] = 0x164;
                GBKFreq[0x19][0x27] = 0x163;
                GBKFreq[0x55][0x81] = 0x162;
                GBKFreq[0x40][0x62] = 0x161;
                GBKFreq[0x43][0x7f] = 0x160;
                GBKFreq[0x48][0xa7] = 0x15f;
                GBKFreq[0x39][0x8f] = 350;
                GBKFreq[0x4c][0xbb] = 0x15d;
                GBKFreq[0x53][0xb5] = 0x15c;
                GBKFreq[0x54][10] = 0x15b;
                GBKFreq[0x37][0xa6] = 0x15a;
                GBKFreq[0x37][0xbc] = 0x159;
                GBKFreq[13][0x97] = 0x158;
                GBKFreq[0x3e][0x7c] = 0x157;
                GBKFreq[0x35][0x88] = 0x156;
                GBKFreq[0x6a][0x39] = 0x155;
                GBKFreq[0x2f][0xa6] = 340;
                GBKFreq[0x6d][30] = 0x153;
                GBKFreq[0x4e][0x72] = 0x152;
                GBKFreq[0x53][0x13] = 0x151;
                GBKFreq[0x38][0xa2] = 0x150;
                GBKFreq[60][0xb1] = 0x14f;
                GBKFreq[0x58][9] = 0x14e;
                GBKFreq[0x4a][0xa3] = 0x14d;
                GBKFreq[0x34][0x9c] = 0x14c;
                GBKFreq[0x47][180] = 0x14b;
                GBKFreq[60][0x39] = 330;
                GBKFreq[0x48][0xad] = 0x149;
                GBKFreq[0x52][0x5b] = 0x148;
                GBKFreq[0x33][0xba] = 0x147;
                GBKFreq[0x4b][0x56] = 0x146;
                GBKFreq[0x4b][0x4e] = 0x145;
                GBKFreq[0x4c][170] = 0x144;
                GBKFreq[60][0x93] = 0x143;
                GBKFreq[0x52][0x4b] = 0x142;
                GBKFreq[80][0x94] = 0x141;
                GBKFreq[0x56][150] = 320;
                GBKFreq[13][0x5f] = 0x13f;
                GBKFreq[0][11] = 0x13e;
                GBKFreq[0x54][190] = 0x13d;
                GBKFreq[0x4c][0xa6] = 0x13c;
                GBKFreq[14][0x48] = 0x13b;
                GBKFreq[0x43][0x90] = 0x13a;
                GBKFreq[0x54][0x2c] = 0x139;
                GBKFreq[0x48][0x7d] = 0x138;
                GBKFreq[0x42][0x7f] = 0x137;
                GBKFreq[60][0x19] = 310;
                GBKFreq[70][0x92] = 0x135;
                GBKFreq[0x4f][0x87] = 0x134;
                GBKFreq[0x36][0x87] = 0x133;
                GBKFreq[60][0x68] = 0x132;
                GBKFreq[0x37][0x84] = 0x131;
                GBKFreq[0x5e][2] = 0x130;
                GBKFreq[0x36][0x85] = 0x12f;
                GBKFreq[0x38][190] = 0x12e;
                GBKFreq[0x3a][0xae] = 0x12d;
                GBKFreq[80][0x90] = 300;
                GBKFreq[0x55][0x71] = 0x12b;
            }
            if (Big5Freq[0] == null)
            {
                for (num = 0; num < 0x5e; num++)
                {
                    Big5Freq[num] = new int[0x9e];
                }
                Big5Freq[11][15] = 0x257;
                Big5Freq[3][0x42] = 0x256;
                Big5Freq[6][0x79] = 0x255;
                Big5Freq[3][0] = 0x254;
                Big5Freq[5][0x52] = 0x253;
                Big5Freq[3][0x2a] = 0x252;
                Big5Freq[5][0x22] = 0x251;
                Big5Freq[3][8] = 0x250;
                Big5Freq[3][6] = 0x24f;
                Big5Freq[3][0x43] = 590;
                Big5Freq[7][0x8b] = 0x24d;
                Big5Freq[0x17][0x89] = 0x24c;
                Big5Freq[12][0x2e] = 0x24b;
                Big5Freq[4][8] = 0x24a;
                Big5Freq[4][0x29] = 0x249;
                Big5Freq[0x12][0x2f] = 0x248;
                Big5Freq[12][0x72] = 0x247;
                Big5Freq[6][1] = 0x246;
                Big5Freq[0x16][60] = 0x245;
                Big5Freq[5][0x2e] = 580;
                Big5Freq[11][0x4f] = 0x243;
                Big5Freq[3][0x17] = 0x242;
                Big5Freq[7][0x72] = 0x241;
                Big5Freq[0x1d][0x66] = 0x240;
                Big5Freq[0x13][14] = 0x23f;
                Big5Freq[4][0x85] = 0x23e;
                Big5Freq[3][0x1d] = 0x23d;
                Big5Freq[4][0x6d] = 0x23c;
                Big5Freq[14][0x7f] = 0x23b;
                Big5Freq[5][0x30] = 570;
                Big5Freq[13][0x68] = 0x239;
                Big5Freq[3][0x84] = 0x238;
                Big5Freq[0x1a][0x40] = 0x237;
                Big5Freq[7][0x13] = 0x236;
                Big5Freq[4][12] = 0x235;
                Big5Freq[11][0x7c] = 0x234;
                Big5Freq[7][0x59] = 0x233;
                Big5Freq[15][0x7c] = 0x232;
                Big5Freq[4][0x6c] = 0x231;
                Big5Freq[0x13][0x42] = 560;
                Big5Freq[3][0x15] = 0x22f;
                Big5Freq[0x18][12] = 0x22e;
                Big5Freq[0x1c][0x6f] = 0x22d;
                Big5Freq[12][0x6b] = 0x22c;
                Big5Freq[3][0x70] = 0x22b;
                Big5Freq[8][0x71] = 0x22a;
                Big5Freq[5][40] = 0x229;
                Big5Freq[0x1a][0x91] = 0x228;
                Big5Freq[3][0x30] = 0x227;
                Big5Freq[3][70] = 550;
                Big5Freq[0x16][0x11] = 0x225;
                Big5Freq[0x10][0x2f] = 0x224;
                Big5Freq[3][0x35] = 0x223;
                Big5Freq[4][0x18] = 0x222;
                Big5Freq[0x20][120] = 0x221;
                Big5Freq[0x18][0x31] = 0x220;
                Big5Freq[0x18][0x8e] = 0x21f;
                Big5Freq[0x12][0x42] = 0x21e;
                Big5Freq[0x1d][150] = 0x21d;
                Big5Freq[5][0x7a] = 540;
                Big5Freq[5][0x72] = 0x21b;
                Big5Freq[3][0x2c] = 0x21a;
                Big5Freq[10][0x80] = 0x219;
                Big5Freq[15][20] = 0x218;
                Big5Freq[13][0x21] = 0x217;
                Big5Freq[14][0x57] = 0x216;
                Big5Freq[3][0x7e] = 0x215;
                Big5Freq[4][0x35] = 0x214;
                Big5Freq[4][40] = 0x213;
                Big5Freq[9][0x5d] = 530;
                Big5Freq[15][0x89] = 0x211;
                Big5Freq[10][0x7b] = 0x210;
                Big5Freq[4][0x38] = 0x20f;
                Big5Freq[5][0x47] = 0x20e;
                Big5Freq[10][8] = 0x20d;
                Big5Freq[5][0x10] = 0x20c;
                Big5Freq[5][0x92] = 0x20b;
                Big5Freq[0x12][0x58] = 0x20a;
                Big5Freq[0x18][4] = 0x209;
                Big5Freq[20][0x2f] = 520;
                Big5Freq[5][0x21] = 0x207;
                Big5Freq[9][0x2b] = 0x206;
                Big5Freq[20][12] = 0x205;
                Big5Freq[20][13] = 0x204;
                Big5Freq[5][0x9c] = 0x203;
                Big5Freq[0x16][140] = 0x202;
                Big5Freq[8][0x92] = 0x201;
                Big5Freq[0x15][0x7b] = 0x200;
                Big5Freq[4][90] = 0x1ff;
                Big5Freq[5][0x3e] = 510;
                Big5Freq[0x11][0x3b] = 0x1fd;
                Big5Freq[10][0x25] = 0x1fc;
                Big5Freq[0x12][0x6b] = 0x1fb;
                Big5Freq[14][0x35] = 0x1fa;
                Big5Freq[0x16][0x33] = 0x1f9;
                Big5Freq[8][13] = 0x1f8;
                Big5Freq[5][0x1d] = 0x1f7;
                Big5Freq[9][7] = 0x1f6;
                Big5Freq[0x16][14] = 0x1f5;
                Big5Freq[8][0x37] = 500;
                Big5Freq[0x21][9] = 0x1f3;
                Big5Freq[0x10][0x40] = 0x1f2;
                Big5Freq[7][0x83] = 0x1f1;
                Big5Freq[0x22][4] = 0x1f0;
                Big5Freq[7][0x65] = 0x1ef;
                Big5Freq[11][0x8b] = 0x1ee;
                Big5Freq[3][0x87] = 0x1ed;
                Big5Freq[7][0x66] = 0x1ec;
                Big5Freq[0x11][13] = 0x1eb;
                Big5Freq[3][20] = 490;
                Big5Freq[0x1b][0x6a] = 0x1e9;
                Big5Freq[5][0x58] = 0x1e8;
                Big5Freq[6][0x21] = 0x1e7;
                Big5Freq[5][0x8b] = 0x1e6;
                Big5Freq[6][0] = 0x1e5;
                Big5Freq[0x11][0x3a] = 0x1e4;
                Big5Freq[5][0x85] = 0x1e3;
                Big5Freq[9][0x6b] = 0x1e2;
                Big5Freq[0x17][0x27] = 0x1e1;
                Big5Freq[5][0x17] = 480;
                Big5Freq[3][0x4f] = 0x1df;
                Big5Freq[0x20][0x61] = 0x1de;
                Big5Freq[3][0x88] = 0x1dd;
                Big5Freq[4][0x5e] = 0x1dc;
                Big5Freq[0x15][0x3d] = 0x1db;
                Big5Freq[0x17][0x7b] = 0x1da;
                Big5Freq[0x1a][0x10] = 0x1d9;
                Big5Freq[0x18][0x89] = 0x1d8;
                Big5Freq[0x16][0x12] = 0x1d7;
                Big5Freq[5][1] = 470;
                Big5Freq[20][0x77] = 0x1d5;
                Big5Freq[3][7] = 0x1d4;
                Big5Freq[10][0x4f] = 0x1d3;
                Big5Freq[15][0x69] = 0x1d2;
                Big5Freq[3][0x90] = 0x1d1;
                Big5Freq[12][80] = 0x1d0;
                Big5Freq[15][0x49] = 0x1cf;
                Big5Freq[3][0x13] = 0x1ce;
                Big5Freq[8][0x6d] = 0x1cd;
                Big5Freq[3][15] = 460;
                Big5Freq[0x1f][0x52] = 0x1cb;
                Big5Freq[3][0x2b] = 0x1ca;
                Big5Freq[0x19][0x77] = 0x1c9;
                Big5Freq[0x10][0x6f] = 0x1c8;
                Big5Freq[7][0x4d] = 0x1c7;
                Big5Freq[3][0x5f] = 0x1c6;
                Big5Freq[0x18][0x52] = 0x1c5;
                Big5Freq[7][0x34] = 0x1c4;
                Big5Freq[9][0x97] = 0x1c3;
                Big5Freq[3][0x81] = 450;
                Big5Freq[5][0x57] = 0x1c1;
                Big5Freq[3][0x37] = 0x1c0;
                Big5Freq[8][0x99] = 0x1bf;
                Big5Freq[4][0x53] = 0x1be;
                Big5Freq[3][0x72] = 0x1bd;
                Big5Freq[0x17][0x93] = 0x1bc;
                Big5Freq[15][0x1f] = 0x1bb;
                Big5Freq[3][0x36] = 0x1ba;
                Big5Freq[11][0x7a] = 0x1b9;
                Big5Freq[4][4] = 440;
                Big5Freq[0x22][0x95] = 0x1b7;
                Big5Freq[3][0x11] = 0x1b6;
                Big5Freq[0x15][0x40] = 0x1b5;
                Big5Freq[0x1a][0x90] = 0x1b4;
                Big5Freq[4][0x3e] = 0x1b3;
                Big5Freq[8][15] = 0x1b2;
                Big5Freq[0x23][80] = 0x1b1;
                Big5Freq[7][110] = 0x1b0;
                Big5Freq[0x17][0x72] = 0x1af;
                Big5Freq[3][0x6c] = 430;
                Big5Freq[3][0x3e] = 0x1ad;
                Big5Freq[0x15][0x29] = 0x1ac;
                Big5Freq[15][0x63] = 0x1ab;
                Big5Freq[5][0x2f] = 0x1aa;
                Big5Freq[4][0x60] = 0x1a9;
                Big5Freq[20][0x7a] = 0x1a8;
                Big5Freq[5][0x15] = 0x1a7;
                Big5Freq[4][0x9d] = 0x1a6;
                Big5Freq[0x10][14] = 0x1a5;
                Big5Freq[3][0x75] = 420;
                Big5Freq[7][0x81] = 0x1a3;
                Big5Freq[4][0x1b] = 0x1a2;
                Big5Freq[5][30] = 0x1a1;
                Big5Freq[0x16][0x10] = 0x1a0;
                Big5Freq[5][0x40] = 0x19f;
                Big5Freq[0x11][0x63] = 0x19e;
                Big5Freq[0x11][0x39] = 0x19d;
                Big5Freq[8][0x69] = 0x19c;
                Big5Freq[5][0x70] = 0x19b;
                Big5Freq[20][0x3b] = 410;
                Big5Freq[6][0x81] = 0x199;
                Big5Freq[0x12][0x11] = 0x198;
                Big5Freq[3][0x5c] = 0x197;
                Big5Freq[0x1c][0x76] = 0x196;
                Big5Freq[3][0x6d] = 0x195;
                Big5Freq[0x1f][0x33] = 0x194;
                Big5Freq[13][0x74] = 0x193;
                Big5Freq[6][15] = 0x192;
                Big5Freq[0x24][0x88] = 0x191;
                Big5Freq[12][0x4a] = 400;
                Big5Freq[20][0x58] = 0x18f;
                Big5Freq[0x24][0x44] = 0x18e;
                Big5Freq[3][0x93] = 0x18d;
                Big5Freq[15][0x54] = 0x18c;
                Big5Freq[0x10][0x20] = 0x18b;
                Big5Freq[0x10][0x3a] = 0x18a;
                Big5Freq[7][0x42] = 0x189;
                Big5Freq[0x17][0x6b] = 0x188;
                Big5Freq[9][6] = 0x187;
                Big5Freq[12][0x56] = 390;
                Big5Freq[0x17][0x70] = 0x185;
                Big5Freq[0x25][0x17] = 0x184;
                Big5Freq[3][0x8a] = 0x183;
                Big5Freq[20][0x44] = 0x182;
                Big5Freq[15][0x74] = 0x181;
                Big5Freq[0x12][0x40] = 0x180;
                Big5Freq[12][0x8b] = 0x17f;
                Big5Freq[11][0x9b] = 0x17e;
                Big5Freq[4][0x9c] = 0x17d;
                Big5Freq[12][0x54] = 380;
                Big5Freq[0x12][0x31] = 0x17b;
                Big5Freq[0x19][0x7d] = 0x17a;
                Big5Freq[0x19][0x93] = 0x179;
                Big5Freq[15][110] = 0x178;
                Big5Freq[0x13][0x60] = 0x177;
                Big5Freq[30][0x98] = 0x176;
                Big5Freq[6][0x1f] = 0x175;
                Big5Freq[0x1b][0x75] = 0x174;
                Big5Freq[3][10] = 0x173;
                Big5Freq[6][0x83] = 370;
                Big5Freq[13][0x70] = 0x171;
                Big5Freq[0x24][0x9c] = 0x170;
                Big5Freq[4][60] = 0x16f;
                Big5Freq[15][0x79] = 0x16e;
                Big5Freq[4][0x70] = 0x16d;
                Big5Freq[30][0x8e] = 0x16c;
                Big5Freq[0x17][0x9a] = 0x16b;
                Big5Freq[0x1b][0x65] = 0x16a;
                Big5Freq[9][140] = 0x169;
                Big5Freq[3][0x59] = 360;
                Big5Freq[0x12][0x94] = 0x167;
                Big5Freq[4][0x45] = 0x166;
                Big5Freq[0x10][0x31] = 0x165;
                Big5Freq[6][0x75] = 0x164;
                Big5Freq[0x24][0x37] = 0x163;
                Big5Freq[5][0x7b] = 0x162;
                Big5Freq[4][0x7e] = 0x161;
                Big5Freq[4][0x77] = 0x160;
                Big5Freq[9][0x5f] = 0x15f;
                Big5Freq[5][0x18] = 350;
                Big5Freq[0x10][0x85] = 0x15d;
                Big5Freq[10][0x86] = 0x15c;
                Big5Freq[0x1a][0x3b] = 0x15b;
                Big5Freq[6][0x29] = 0x15a;
                Big5Freq[6][0x92] = 0x159;
                Big5Freq[0x13][0x18] = 0x158;
                Big5Freq[5][0x71] = 0x157;
                Big5Freq[10][0x76] = 0x156;
                Big5Freq[0x22][0x97] = 0x155;
                Big5Freq[9][0x48] = 340;
                Big5Freq[0x1f][0x19] = 0x153;
                Big5Freq[0x12][0x7e] = 0x152;
                Big5Freq[0x12][0x1c] = 0x151;
                Big5Freq[4][0x99] = 0x150;
                Big5Freq[3][0x54] = 0x14f;
                Big5Freq[0x15][0x12] = 0x14e;
                Big5Freq[0x19][0x81] = 0x14d;
                Big5Freq[6][0x6b] = 0x14c;
                Big5Freq[12][0x19] = 0x14b;
                Big5Freq[0x11][0x6d] = 330;
                Big5Freq[7][0x4c] = 0x149;
                Big5Freq[15][15] = 0x148;
                Big5Freq[4][14] = 0x147;
                Big5Freq[0x17][0x58] = 0x146;
                Big5Freq[0x12][2] = 0x145;
                Big5Freq[6][0x58] = 0x144;
                Big5Freq[0x10][0x54] = 0x143;
                Big5Freq[12][0x30] = 0x142;
                Big5Freq[7][0x44] = 0x141;
                Big5Freq[5][50] = 320;
                Big5Freq[13][0x36] = 0x13f;
                Big5Freq[7][0x62] = 0x13e;
                Big5Freq[11][6] = 0x13d;
                Big5Freq[9][80] = 0x13c;
                Big5Freq[0x10][0x29] = 0x13b;
                Big5Freq[7][0x2b] = 0x13a;
                Big5Freq[0x1c][0x75] = 0x139;
                Big5Freq[3][0x33] = 0x138;
                Big5Freq[7][3] = 0x137;
                Big5Freq[20][0x51] = 310;
                Big5Freq[4][2] = 0x135;
                Big5Freq[11][0x10] = 0x134;
                Big5Freq[10][4] = 0x133;
                Big5Freq[10][0x77] = 0x132;
                Big5Freq[6][0x8e] = 0x131;
                Big5Freq[0x12][0x33] = 0x130;
                Big5Freq[8][0x90] = 0x12f;
                Big5Freq[10][0x41] = 0x12e;
                Big5Freq[11][0x40] = 0x12d;
                Big5Freq[11][130] = 300;
                Big5Freq[9][0x5c] = 0x12b;
                Big5Freq[0x12][0x1d] = 0x12a;
                Big5Freq[0x12][0x4e] = 0x129;
                Big5Freq[0x12][0x97] = 0x128;
                Big5Freq[0x21][0x7f] = 0x127;
                Big5Freq[0x23][0x71] = 0x126;
                Big5Freq[10][0x9b] = 0x125;
                Big5Freq[3][0x4c] = 0x124;
                Big5Freq[0x24][0x7b] = 0x123;
                Big5Freq[13][0x8f] = 290;
                Big5Freq[5][0x87] = 0x121;
                Big5Freq[0x17][0x74] = 0x120;
                Big5Freq[6][0x65] = 0x11f;
                Big5Freq[14][0x4a] = 0x11e;
                Big5Freq[7][0x99] = 0x11d;
                Big5Freq[3][0x65] = 0x11c;
                Big5Freq[9][0x4a] = 0x11b;
                Big5Freq[3][0x9c] = 0x11a;
                Big5Freq[4][0x93] = 0x119;
                Big5Freq[9][12] = 280;
                Big5Freq[0x12][0x85] = 0x117;
                Big5Freq[4][0] = 0x116;
                Big5Freq[7][0x9b] = 0x115;
                Big5Freq[9][0x90] = 0x114;
                Big5Freq[0x17][0x31] = 0x113;
                Big5Freq[5][0x59] = 0x112;
                Big5Freq[10][11] = 0x111;
                Big5Freq[3][110] = 0x110;
                Big5Freq[3][40] = 0x10f;
                Big5Freq[0x1d][0x73] = 270;
                Big5Freq[9][100] = 0x10d;
                Big5Freq[0x15][0x43] = 0x10c;
                Big5Freq[0x17][0x91] = 0x10b;
                Big5Freq[10][0x2f] = 0x10a;
                Big5Freq[4][0x1f] = 0x109;
                Big5Freq[4][0x51] = 0x108;
                Big5Freq[0x16][0x3e] = 0x107;
                Big5Freq[4][0x1c] = 0x106;
                Big5Freq[0x1b][0x27] = 0x105;
                Big5Freq[0x1b][0x36] = 260;
                Big5Freq[0x20][0x2e] = 0x103;
                Big5Freq[4][0x4c] = 0x102;
                Big5Freq[0x1a][15] = 0x101;
                Big5Freq[12][0x9a] = 0x100;
                Big5Freq[9][150] = 0xff;
                Big5Freq[15][0x11] = 0xfe;
                Big5Freq[5][0x81] = 0xfd;
                Big5Freq[10][40] = 0xfc;
                Big5Freq[13][0x25] = 0xfb;
                Big5Freq[0x1f][0x68] = 250;
                Big5Freq[3][0x98] = 0xf9;
                Big5Freq[5][0x16] = 0xf8;
                Big5Freq[8][0x30] = 0xf7;
                Big5Freq[4][0x4a] = 0xf6;
                Big5Freq[6][0x11] = 0xf5;
                Big5Freq[30][0x52] = 0xf4;
                Big5Freq[4][0x74] = 0xf3;
                Big5Freq[0x10][0x2a] = 0xf2;
                Big5Freq[5][0x37] = 0xf1;
                Big5Freq[4][0x40] = 240;
                Big5Freq[14][0x13] = 0xef;
                Big5Freq[0x23][0x52] = 0xee;
                Big5Freq[30][0x8b] = 0xed;
                Big5Freq[0x1a][0x98] = 0xec;
                Big5Freq[0x20][0x20] = 0xeb;
                Big5Freq[0x15][0x66] = 0xea;
                Big5Freq[10][0x83] = 0xe9;
                Big5Freq[9][0x80] = 0xe8;
                Big5Freq[3][0x57] = 0xe7;
                Big5Freq[4][0x33] = 230;
                Big5Freq[10][15] = 0xe5;
                Big5Freq[4][150] = 0xe4;
                Big5Freq[7][4] = 0xe3;
                Big5Freq[7][0x33] = 0xe2;
                Big5Freq[7][0x9d] = 0xe1;
                Big5Freq[4][0x92] = 0xe0;
                Big5Freq[4][0x5b] = 0xdf;
                Big5Freq[7][13] = 0xde;
                Big5Freq[0x11][0x74] = 0xdd;
                Big5Freq[0x17][0x15] = 220;
                Big5Freq[5][0x6a] = 0xdb;
                Big5Freq[14][100] = 0xda;
                Big5Freq[10][0x98] = 0xd9;
                Big5Freq[14][0x59] = 0xd8;
                Big5Freq[6][0x8a] = 0xd7;
                Big5Freq[12][0x9d] = 0xd6;
                Big5Freq[10][0x66] = 0xd5;
                Big5Freq[0x13][0x5e] = 0xd4;
                Big5Freq[7][0x4a] = 0xd3;
                Big5Freq[0x12][0x80] = 210;
                Big5Freq[0x1b][0x6f] = 0xd1;
                Big5Freq[11][0x39] = 0xd0;
                Big5Freq[3][0x83] = 0xcf;
                Big5Freq[30][0x17] = 0xce;
                Big5Freq[30][0x7e] = 0xcd;
                Big5Freq[4][0x24] = 0xcc;
                Big5Freq[0x1a][0x7c] = 0xcb;
                Big5Freq[4][0x13] = 0xca;
                Big5Freq[9][0x98] = 0xc9;
            }
            if (EUC_TWFreq[0] == null)
            {
                for (num = 0; num < 0x5e; num++)
                {
                    EUC_TWFreq[num] = new int[0x5e];
                }
                EUC_TWFreq[0x23][0x41] = 0x256;
                EUC_TWFreq[0x29][0x1b] = 0x255;
                EUC_TWFreq[0x23][0] = 0x254;
                EUC_TWFreq[0x27][0x13] = 0x253;
                EUC_TWFreq[0x23][0x2a] = 0x252;
                EUC_TWFreq[0x26][0x42] = 0x251;
                EUC_TWFreq[0x23][8] = 0x250;
                EUC_TWFreq[0x23][6] = 0x24f;
                EUC_TWFreq[0x23][0x42] = 590;
                EUC_TWFreq[0x2b][14] = 0x24d;
                EUC_TWFreq[0x45][80] = 0x24c;
                EUC_TWFreq[50][0x30] = 0x24b;
                EUC_TWFreq[0x24][0x47] = 0x24a;
                EUC_TWFreq[0x25][10] = 0x249;
                EUC_TWFreq[60][0x34] = 0x248;
                EUC_TWFreq[0x33][0x15] = 0x247;
                EUC_TWFreq[40][2] = 0x246;
                EUC_TWFreq[0x43][0x23] = 0x245;
                EUC_TWFreq[0x26][0x4e] = 580;
                EUC_TWFreq[0x31][0x12] = 0x243;
                EUC_TWFreq[0x23][0x17] = 0x242;
                EUC_TWFreq[0x2a][0x53] = 0x241;
                EUC_TWFreq[0x4f][0x2f] = 0x240;
                EUC_TWFreq[0x3d][0x52] = 0x23f;
                EUC_TWFreq[0x26][7] = 0x23e;
                EUC_TWFreq[0x23][0x1d] = 0x23d;
                EUC_TWFreq[0x25][0x4d] = 0x23c;
                EUC_TWFreq[0x36][0x43] = 0x23b;
                EUC_TWFreq[0x26][80] = 570;
                EUC_TWFreq[0x34][0x4a] = 0x239;
                EUC_TWFreq[0x24][0x25] = 0x238;
                EUC_TWFreq[0x4a][8] = 0x237;
                EUC_TWFreq[0x29][0x53] = 0x236;
                EUC_TWFreq[0x24][0x4b] = 0x235;
                EUC_TWFreq[0x31][0x3f] = 0x234;
                EUC_TWFreq[0x2a][0x3a] = 0x233;
                EUC_TWFreq[0x38][0x21] = 0x232;
                EUC_TWFreq[0x25][0x4c] = 0x231;
                EUC_TWFreq[0x3e][0x27] = 560;
                EUC_TWFreq[0x23][0x15] = 0x22f;
                EUC_TWFreq[70][0x13] = 0x22e;
                EUC_TWFreq[0x4d][0x58] = 0x22d;
                EUC_TWFreq[0x33][14] = 0x22c;
                EUC_TWFreq[0x24][0x11] = 0x22b;
                EUC_TWFreq[0x2c][0x33] = 0x22a;
                EUC_TWFreq[0x26][0x48] = 0x229;
                EUC_TWFreq[0x4a][90] = 0x228;
                EUC_TWFreq[0x23][0x30] = 0x227;
                EUC_TWFreq[0x23][0x45] = 550;
                EUC_TWFreq[0x42][0x56] = 0x225;
                EUC_TWFreq[0x39][20] = 0x224;
                EUC_TWFreq[0x23][0x35] = 0x223;
                EUC_TWFreq[0x24][0x57] = 0x222;
                EUC_TWFreq[0x54][0x43] = 0x221;
                EUC_TWFreq[70][0x38] = 0x220;
                EUC_TWFreq[0x47][0x36] = 0x21f;
                EUC_TWFreq[60][70] = 0x21e;
                EUC_TWFreq[80][1] = 0x21d;
                EUC_TWFreq[0x27][0x3b] = 540;
                EUC_TWFreq[0x27][0x33] = 0x21b;
                EUC_TWFreq[0x23][0x2c] = 0x21a;
                EUC_TWFreq[0x30][4] = 0x219;
                EUC_TWFreq[0x37][0x18] = 0x218;
                EUC_TWFreq[0x34][4] = 0x217;
                EUC_TWFreq[0x36][0x1a] = 0x216;
                EUC_TWFreq[0x24][0x1f] = 0x215;
                EUC_TWFreq[0x25][0x16] = 0x214;
                EUC_TWFreq[0x25][9] = 0x213;
                EUC_TWFreq[0x2e][0] = 530;
                EUC_TWFreq[0x38][0x2e] = 0x211;
                EUC_TWFreq[0x2f][0x5d] = 0x210;
                EUC_TWFreq[0x25][0x19] = 0x20f;
                EUC_TWFreq[0x27][8] = 0x20e;
                EUC_TWFreq[0x2e][0x49] = 0x20d;
                EUC_TWFreq[0x26][0x30] = 0x20c;
                EUC_TWFreq[0x27][0x53] = 0x20b;
                EUC_TWFreq[60][0x5c] = 0x20a;
                EUC_TWFreq[70][11] = 0x209;
                EUC_TWFreq[0x3f][0x54] = 520;
                EUC_TWFreq[0x26][0x41] = 0x207;
                EUC_TWFreq[0x2d][0x2d] = 0x206;
                EUC_TWFreq[0x3f][0x31] = 0x205;
                EUC_TWFreq[0x3f][50] = 0x204;
                EUC_TWFreq[0x27][0x5d] = 0x203;
                EUC_TWFreq[0x44][20] = 0x202;
                EUC_TWFreq[0x2c][0x54] = 0x201;
                EUC_TWFreq[0x42][0x22] = 0x200;
                EUC_TWFreq[0x25][0x3a] = 0x1ff;
                EUC_TWFreq[0x27][0] = 510;
                EUC_TWFreq[0x3b][1] = 0x1fd;
                EUC_TWFreq[0x2f][8] = 0x1fc;
                EUC_TWFreq[0x3d][0x11] = 0x1fb;
                EUC_TWFreq[0x35][0x57] = 0x1fa;
                EUC_TWFreq[0x43][0x1a] = 0x1f9;
                EUC_TWFreq[0x2b][0x2e] = 0x1f8;
                EUC_TWFreq[0x26][0x3d] = 0x1f7;
                EUC_TWFreq[0x2d][9] = 0x1f6;
                EUC_TWFreq[0x42][0x53] = 0x1f5;
                EUC_TWFreq[0x2b][0x58] = 500;
                EUC_TWFreq[0x55][20] = 0x1f3;
                EUC_TWFreq[0x39][0x24] = 0x1f2;
                EUC_TWFreq[0x2b][6] = 0x1f1;
                EUC_TWFreq[0x56][0x4d] = 0x1f0;
                EUC_TWFreq[0x2a][70] = 0x1ef;
                EUC_TWFreq[0x31][0x4e] = 0x1ee;
                EUC_TWFreq[0x24][40] = 0x1ed;
                EUC_TWFreq[0x2a][0x47] = 0x1ec;
                EUC_TWFreq[0x3a][0x31] = 0x1eb;
                EUC_TWFreq[0x23][20] = 490;
                EUC_TWFreq[0x4c][20] = 0x1e9;
                EUC_TWFreq[0x27][0x19] = 0x1e8;
                EUC_TWFreq[40][0x22] = 0x1e7;
                EUC_TWFreq[0x27][0x4c] = 0x1e6;
                EUC_TWFreq[40][1] = 0x1e5;
                EUC_TWFreq[0x3b][0] = 0x1e4;
                EUC_TWFreq[0x27][70] = 0x1e3;
                EUC_TWFreq[0x2e][14] = 0x1e2;
                EUC_TWFreq[0x44][0x4d] = 0x1e1;
                EUC_TWFreq[0x26][0x37] = 480;
                EUC_TWFreq[0x23][0x4e] = 0x1df;
                EUC_TWFreq[0x54][0x2c] = 0x1de;
                EUC_TWFreq[0x24][0x29] = 0x1dd;
                EUC_TWFreq[0x25][0x3e] = 0x1dc;
                EUC_TWFreq[0x41][0x43] = 0x1db;
                EUC_TWFreq[0x45][0x42] = 0x1da;
                EUC_TWFreq[0x49][0x37] = 0x1d9;
                EUC_TWFreq[0x47][0x31] = 0x1d8;
                EUC_TWFreq[0x42][0x57] = 0x1d7;
                EUC_TWFreq[0x26][0x21] = 470;
                EUC_TWFreq[0x40][0x3d] = 0x1d5;
                EUC_TWFreq[0x23][7] = 0x1d4;
                EUC_TWFreq[0x2f][0x31] = 0x1d3;
                EUC_TWFreq[0x38][14] = 0x1d2;
                EUC_TWFreq[0x24][0x31] = 0x1d1;
                EUC_TWFreq[50][0x51] = 0x1d0;
                EUC_TWFreq[0x37][0x4c] = 0x1cf;
                EUC_TWFreq[0x23][0x13] = 0x1ce;
                EUC_TWFreq[0x2c][0x2f] = 0x1cd;
                EUC_TWFreq[0x23][15] = 460;
                EUC_TWFreq[0x52][0x3b] = 0x1cb;
                EUC_TWFreq[0x23][0x2b] = 0x1ca;
                EUC_TWFreq[0x49][0] = 0x1c9;
                EUC_TWFreq[0x39][0x53] = 0x1c8;
                EUC_TWFreq[0x2a][0x2e] = 0x1c7;
                EUC_TWFreq[0x24][0] = 0x1c6;
                EUC_TWFreq[70][0x58] = 0x1c5;
                EUC_TWFreq[0x2a][0x16] = 0x1c4;
                EUC_TWFreq[0x2e][0x3a] = 0x1c3;
                EUC_TWFreq[0x24][0x22] = 450;
                EUC_TWFreq[0x27][0x18] = 0x1c1;
                EUC_TWFreq[0x23][0x37] = 0x1c0;
                EUC_TWFreq[0x2c][0x5b] = 0x1bf;
                EUC_TWFreq[0x25][0x33] = 0x1be;
                EUC_TWFreq[0x24][0x13] = 0x1bd;
                EUC_TWFreq[0x45][90] = 0x1bc;
                EUC_TWFreq[0x37][0x23] = 0x1bb;
                EUC_TWFreq[0x23][0x36] = 0x1ba;
                EUC_TWFreq[0x31][0x3d] = 0x1b9;
                EUC_TWFreq[0x24][0x43] = 440;
                EUC_TWFreq[0x58][0x22] = 0x1b7;
                EUC_TWFreq[0x23][0x11] = 0x1b6;
                EUC_TWFreq[0x41][0x45] = 0x1b5;
                EUC_TWFreq[0x4a][0x59] = 0x1b4;
                EUC_TWFreq[0x25][0x1f] = 0x1b3;
                EUC_TWFreq[0x2b][0x30] = 0x1b2;
                EUC_TWFreq[0x59][0x1b] = 0x1b1;
                EUC_TWFreq[0x2a][0x4f] = 0x1b0;
                EUC_TWFreq[0x45][0x39] = 0x1af;
                EUC_TWFreq[0x24][13] = 430;
                EUC_TWFreq[0x23][0x3e] = 0x1ad;
                EUC_TWFreq[0x41][0x2f] = 0x1ac;
                EUC_TWFreq[0x38][8] = 0x1ab;
                EUC_TWFreq[0x26][0x4f] = 0x1aa;
                EUC_TWFreq[0x25][0x40] = 0x1a9;
                EUC_TWFreq[0x40][0x40] = 0x1a8;
                EUC_TWFreq[0x26][0x35] = 0x1a7;
                EUC_TWFreq[0x26][0x1f] = 0x1a6;
                EUC_TWFreq[0x38][0x51] = 0x1a5;
                EUC_TWFreq[0x24][0x16] = 420;
                EUC_TWFreq[0x2b][4] = 0x1a3;
                EUC_TWFreq[0x24][90] = 0x1a2;
                EUC_TWFreq[0x26][0x3e] = 0x1a1;
                EUC_TWFreq[0x42][0x55] = 0x1a0;
                EUC_TWFreq[0x27][1] = 0x19f;
                EUC_TWFreq[0x3b][40] = 0x19e;
                EUC_TWFreq[0x3a][0x5d] = 0x19d;
                EUC_TWFreq[0x2c][0x2b] = 0x19c;
                EUC_TWFreq[0x27][0x31] = 0x19b;
                EUC_TWFreq[0x40][2] = 410;
                EUC_TWFreq[0x29][0x23] = 0x199;
                EUC_TWFreq[60][0x16] = 0x198;
                EUC_TWFreq[0x23][0x5b] = 0x197;
                EUC_TWFreq[0x4e][1] = 0x196;
                EUC_TWFreq[0x24][14] = 0x195;
                EUC_TWFreq[0x52][0x1d] = 0x194;
                EUC_TWFreq[0x34][0x56] = 0x193;
                EUC_TWFreq[40][0x10] = 0x192;
                EUC_TWFreq[0x5b][0x34] = 0x191;
                EUC_TWFreq[50][0x4b] = 400;
                EUC_TWFreq[0x40][30] = 0x18f;
                EUC_TWFreq[90][0x4e] = 0x18e;
                EUC_TWFreq[0x24][0x34] = 0x18d;
                EUC_TWFreq[0x37][0x57] = 0x18c;
                EUC_TWFreq[0x39][5] = 0x18b;
                EUC_TWFreq[0x39][0x1f] = 0x18a;
                EUC_TWFreq[0x2a][0x23] = 0x189;
                EUC_TWFreq[0x45][50] = 0x188;
                EUC_TWFreq[0x2d][8] = 0x187;
                EUC_TWFreq[50][0x57] = 390;
                EUC_TWFreq[0x45][0x37] = 0x185;
                EUC_TWFreq[0x5c][3] = 0x184;
                EUC_TWFreq[0x24][0x2b] = 0x183;
                EUC_TWFreq[0x40][10] = 0x182;
                EUC_TWFreq[0x38][0x19] = 0x181;
                EUC_TWFreq[60][0x44] = 0x180;
                EUC_TWFreq[0x33][0x2e] = 0x17f;
                EUC_TWFreq[50][0] = 0x17e;
                EUC_TWFreq[0x26][30] = 0x17d;
                EUC_TWFreq[50][0x55] = 380;
                EUC_TWFreq[60][0x36] = 0x17b;
                EUC_TWFreq[0x49][6] = 0x17a;
                EUC_TWFreq[0x49][0x1c] = 0x179;
                EUC_TWFreq[0x38][0x13] = 0x178;
                EUC_TWFreq[0x3e][0x45] = 0x177;
                EUC_TWFreq[0x51][0x42] = 0x176;
                EUC_TWFreq[40][0x20] = 0x175;
                EUC_TWFreq[0x4c][0x1f] = 0x174;
                EUC_TWFreq[0x23][10] = 0x173;
                EUC_TWFreq[0x29][0x25] = 370;
                EUC_TWFreq[0x34][0x52] = 0x171;
                EUC_TWFreq[0x5b][0x48] = 0x170;
                EUC_TWFreq[0x25][0x1d] = 0x16f;
                EUC_TWFreq[0x38][30] = 0x16e;
                EUC_TWFreq[0x25][80] = 0x16d;
                EUC_TWFreq[0x51][0x38] = 0x16c;
                EUC_TWFreq[70][3] = 0x16b;
                EUC_TWFreq[0x4c][15] = 0x16a;
                EUC_TWFreq[0x2e][0x2f] = 0x169;
                EUC_TWFreq[0x23][0x58] = 360;
                EUC_TWFreq[0x3d][0x3a] = 0x167;
                EUC_TWFreq[0x25][0x25] = 0x166;
                EUC_TWFreq[0x39][0x16] = 0x165;
                EUC_TWFreq[0x29][0x17] = 0x164;
                EUC_TWFreq[90][0x42] = 0x163;
                EUC_TWFreq[0x27][60] = 0x162;
                EUC_TWFreq[0x26][0] = 0x161;
                EUC_TWFreq[0x25][0x57] = 0x160;
                EUC_TWFreq[0x2e][2] = 0x15f;
                EUC_TWFreq[0x26][0x38] = 350;
                EUC_TWFreq[0x3a][11] = 0x15d;
                EUC_TWFreq[0x30][10] = 0x15c;
                EUC_TWFreq[0x4a][4] = 0x15b;
                EUC_TWFreq[40][0x2a] = 0x15a;
                EUC_TWFreq[0x29][0x34] = 0x159;
                EUC_TWFreq[0x3d][0x5c] = 0x158;
                EUC_TWFreq[0x27][50] = 0x157;
                EUC_TWFreq[0x2f][0x58] = 0x156;
                EUC_TWFreq[0x58][0x24] = 0x155;
                EUC_TWFreq[0x2d][0x49] = 340;
                EUC_TWFreq[0x52][3] = 0x153;
                EUC_TWFreq[0x3d][0x24] = 0x152;
                EUC_TWFreq[60][0x21] = 0x151;
                EUC_TWFreq[0x26][0x1b] = 0x150;
                EUC_TWFreq[0x23][0x53] = 0x14f;
                EUC_TWFreq[0x41][0x18] = 0x14e;
                EUC_TWFreq[0x49][10] = 0x14d;
                EUC_TWFreq[0x29][13] = 0x14c;
                EUC_TWFreq[50][0x1b] = 0x14b;
                EUC_TWFreq[0x3b][50] = 330;
                EUC_TWFreq[0x2a][0x2d] = 0x149;
                EUC_TWFreq[0x37][0x13] = 0x148;
                EUC_TWFreq[0x24][0x4d] = 0x147;
                EUC_TWFreq[0x45][0x1f] = 0x146;
                EUC_TWFreq[60][7] = 0x145;
                EUC_TWFreq[40][0x58] = 0x144;
                EUC_TWFreq[0x39][0x38] = 0x143;
                EUC_TWFreq[50][50] = 0x142;
                EUC_TWFreq[0x2a][0x25] = 0x141;
                EUC_TWFreq[0x26][0x52] = 320;
                EUC_TWFreq[0x34][0x19] = 0x13f;
                EUC_TWFreq[0x2a][0x43] = 0x13e;
                EUC_TWFreq[0x30][40] = 0x13d;
                EUC_TWFreq[0x2d][0x51] = 0x13c;
                EUC_TWFreq[0x39][14] = 0x13b;
                EUC_TWFreq[0x2a][13] = 0x13a;
                EUC_TWFreq[0x4e][0] = 0x139;
                EUC_TWFreq[0x23][0x33] = 0x138;
                EUC_TWFreq[0x29][0x43] = 0x137;
                EUC_TWFreq[0x40][0x17] = 310;
                EUC_TWFreq[0x24][0x41] = 0x135;
                EUC_TWFreq[0x30][50] = 0x134;
                EUC_TWFreq[0x2e][0x45] = 0x133;
                EUC_TWFreq[0x2f][0x59] = 0x132;
                EUC_TWFreq[0x29][0x30] = 0x131;
                EUC_TWFreq[60][0x38] = 0x130;
                EUC_TWFreq[0x2c][0x52] = 0x12f;
                EUC_TWFreq[0x2f][0x23] = 0x12e;
                EUC_TWFreq[0x31][3] = 0x12d;
                EUC_TWFreq[0x31][0x45] = 300;
                EUC_TWFreq[0x2d][0x5d] = 0x12b;
                EUC_TWFreq[60][0x22] = 0x12a;
                EUC_TWFreq[60][0x52] = 0x129;
                EUC_TWFreq[0x3d][0x3d] = 0x128;
                EUC_TWFreq[0x56][0x2a] = 0x127;
                EUC_TWFreq[0x59][60] = 0x126;
                EUC_TWFreq[0x30][0x1f] = 0x125;
                EUC_TWFreq[0x23][0x4b] = 0x124;
                EUC_TWFreq[0x5b][0x27] = 0x123;
                EUC_TWFreq[0x35][0x13] = 290;
                EUC_TWFreq[0x27][0x48] = 0x121;
                EUC_TWFreq[0x45][0x3b] = 0x120;
                EUC_TWFreq[0x29][7] = 0x11f;
                EUC_TWFreq[0x36][13] = 0x11e;
                EUC_TWFreq[0x2b][0x1c] = 0x11d;
                EUC_TWFreq[0x24][6] = 0x11c;
                EUC_TWFreq[0x2d][0x4b] = 0x11b;
                EUC_TWFreq[0x24][0x3d] = 0x11a;
                EUC_TWFreq[0x26][0x15] = 0x119;
                EUC_TWFreq[0x2d][14] = 280;
                EUC_TWFreq[0x3d][0x2b] = 0x117;
                EUC_TWFreq[0x24][0x3f] = 0x116;
                EUC_TWFreq[0x2b][30] = 0x115;
                EUC_TWFreq[0x2e][0x33] = 0x114;
                EUC_TWFreq[0x44][0x57] = 0x113;
                EUC_TWFreq[0x27][0x1a] = 0x112;
                EUC_TWFreq[0x2e][0x4c] = 0x111;
                EUC_TWFreq[0x24][15] = 0x110;
                EUC_TWFreq[0x23][40] = 0x10f;
                EUC_TWFreq[0x4f][60] = 270;
                EUC_TWFreq[0x2e][7] = 0x10d;
                EUC_TWFreq[0x41][0x48] = 0x10c;
                EUC_TWFreq[0x45][0x58] = 0x10b;
                EUC_TWFreq[0x2f][0x12] = 0x10a;
                EUC_TWFreq[0x25][0] = 0x109;
                EUC_TWFreq[0x25][0x31] = 0x108;
                EUC_TWFreq[0x43][0x25] = 0x107;
                EUC_TWFreq[0x24][0x5b] = 0x106;
                EUC_TWFreq[0x4b][0x30] = 0x105;
                EUC_TWFreq[0x4b][0x3f] = 260;
                EUC_TWFreq[0x53][0x57] = 0x103;
                EUC_TWFreq[0x25][0x2c] = 0x102;
                EUC_TWFreq[0x49][0x36] = 0x101;
                EUC_TWFreq[0x33][0x3d] = 0x100;
                EUC_TWFreq[0x2e][0x39] = 0xff;
                EUC_TWFreq[0x37][0x15] = 0xfe;
                EUC_TWFreq[0x27][0x42] = 0xfd;
                EUC_TWFreq[0x2f][11] = 0xfc;
                EUC_TWFreq[0x34][8] = 0xfb;
                EUC_TWFreq[0x52][0x51] = 250;
                EUC_TWFreq[0x24][0x39] = 0xf9;
                EUC_TWFreq[0x26][0x36] = 0xf8;
                EUC_TWFreq[0x2b][0x51] = 0xf7;
                EUC_TWFreq[0x25][0x2a] = 0xf6;
                EUC_TWFreq[40][0x12] = 0xf5;
                EUC_TWFreq[80][90] = 0xf4;
                EUC_TWFreq[0x25][0x54] = 0xf3;
                EUC_TWFreq[0x39][15] = 0xf2;
                EUC_TWFreq[0x26][0x57] = 0xf1;
                EUC_TWFreq[0x25][0x20] = 240;
                EUC_TWFreq[0x35][0x35] = 0xef;
                EUC_TWFreq[0x59][0x1d] = 0xee;
                EUC_TWFreq[0x51][0x35] = 0xed;
                EUC_TWFreq[0x4b][3] = 0xec;
                EUC_TWFreq[0x53][0x49] = 0xeb;
                EUC_TWFreq[0x42][13] = 0xea;
                EUC_TWFreq[0x30][7] = 0xe9;
                EUC_TWFreq[0x2e][0x23] = 0xe8;
                EUC_TWFreq[0x23][0x56] = 0xe7;
                EUC_TWFreq[0x25][20] = 230;
                EUC_TWFreq[0x2e][80] = 0xe5;
                EUC_TWFreq[0x26][0x18] = 0xe4;
                EUC_TWFreq[0x29][0x44] = 0xe3;
                EUC_TWFreq[0x2a][0x15] = 0xe2;
                EUC_TWFreq[0x2b][0x20] = 0xe1;
                EUC_TWFreq[0x26][20] = 0xe0;
                EUC_TWFreq[0x25][0x3b] = 0xdf;
                EUC_TWFreq[0x29][0x4d] = 0xde;
                EUC_TWFreq[0x3b][0x39] = 0xdd;
                EUC_TWFreq[0x44][0x3b] = 220;
                EUC_TWFreq[0x27][0x2b] = 0xdb;
                EUC_TWFreq[0x36][0x27] = 0xda;
                EUC_TWFreq[0x30][0x1c] = 0xd9;
                EUC_TWFreq[0x36][0x1c] = 0xd8;
                EUC_TWFreq[0x29][0x2c] = 0xd7;
                EUC_TWFreq[0x33][0x40] = 0xd6;
                EUC_TWFreq[0x2f][0x48] = 0xd5;
                EUC_TWFreq[0x3e][0x43] = 0xd4;
                EUC_TWFreq[0x2a][0x2b] = 0xd3;
                EUC_TWFreq[0x3d][0x26] = 210;
                EUC_TWFreq[0x4c][0x19] = 0xd1;
                EUC_TWFreq[0x30][0x5b] = 0xd0;
                EUC_TWFreq[0x24][0x24] = 0xcf;
                EUC_TWFreq[80][0x20] = 0xce;
                EUC_TWFreq[0x51][40] = 0xcd;
                EUC_TWFreq[0x25][5] = 0xcc;
                EUC_TWFreq[0x4a][0x45] = 0xcb;
                EUC_TWFreq[0x24][0x52] = 0xca;
                EUC_TWFreq[0x2e][0x3b] = 0xc9;
            }
        }

        internal virtual int ISO2022CNProbability(sbyte[] rawtext)
        {
            int length = 0;
            int num3 = 1;
            int num4 = 1;
            long num5 = 0L;
            long num6 = 1L;
            float num7 = 0f;
            float num8 = 0f;
            length = rawtext.Length;
            for (int i = 0; i < (length - 1); i++)
            {
                if ((rawtext[i] == 0x1b) && ((i + 3) < length))
                {
                    int num9;
                    int num10;
                    if (((rawtext[i + 1] == 0x24) && (rawtext[i + 2] == 0x29)) && (rawtext[i + 3] == 0x41))
                    {
                        i += 4;
                        while (rawtext[i] != 0x1b)
                        {
                            num3++;
                            if (((0x21 <= rawtext[i]) && (rawtext[i] <= 0x77)) && ((0x21 <= rawtext[i + 1]) && (rawtext[i + 1] <= 0x77)))
                            {
                                num4++;
                                num9 = rawtext[i] - 0x21;
                                num10 = rawtext[i + 1] - 0x21;
                                num6 += 500L;
                                if (GBFreq[num9][num10] != 0)
                                {
                                    num5 += GBFreq[num9][num10];
                                }
                                else if ((15 <= num9) && (num9 < 0x37))
                                {
                                    num5 += 200L;
                                }
                                i++;
                            }
                            i++;
                        }
                    }
                    else if (((((i + 3) < length) && (rawtext[i + 1] == 0x24)) && (rawtext[i + 2] == 0x29)) && (rawtext[i + 3] == 0x47))
                    {
                        i += 4;
                        while (rawtext[i] != 0x1b)
                        {
                            num3++;
                            if ((((0x21 <= rawtext[i]) && (rawtext[i] <= 0x7e)) && (0x21 <= rawtext[i + 1])) && (rawtext[i + 1] <= 0x7e))
                            {
                                num4++;
                                num6 += 500L;
                                num9 = rawtext[i] - 0x21;
                                num10 = rawtext[i + 1] - 0x21;
                                if (EUC_TWFreq[num9][num10] != 0)
                                {
                                    num5 += EUC_TWFreq[num9][num10];
                                }
                                else if ((0x23 <= num9) && (num9 <= 0x5c))
                                {
                                    num5 += 150L;
                                }
                                i++;
                            }
                            i++;
                        }
                    }
                    if ((((rawtext[i] == 0x1b) && ((i + 2) < length)) && (rawtext[i + 1] == 40)) && (rawtext[i + 2] == 0x42))
                    {
                        i += 2;
                    }
                }
            }
            num7 = 50f * (((float)num4) / ((float)num3));
            num8 = 50f * (((float)num5) / ((float)num6));
            return (int)(num7 + num8);
        }

        public static int ReadInput(Stream sourceStream, ref sbyte[] target, int start, int count)
        {
            if (target.Length == 0)
            {
                return 0;
            }
            byte[] buffer = new byte[target.Length];
            int num = sourceStream.Read(buffer, start, count);
            if (num == 0)
            {
                return -1;
            }
            for (int i = start; i < (start + num); i++)
            {
                target[i] = (sbyte)buffer[i];
            }
            return num;
        }

        public static int ReadInput(TextReader sourceTextReader, ref sbyte[] target, int start, int count)
        {
            if (target.Length == 0)
            {
                return 0;
            }
            char[] buffer = new char[target.Length];
            int num = sourceTextReader.Read(buffer, start, count);
            if (num == 0)
            {
                return -1;
            }
            for (int i = start; i < (start + num); i++)
            {
                target[i] = (sbyte)buffer[i];
            }
            return num;
        }

        public static byte[] ToByteArray(object[] tempObjectArray)
        {
            byte[] buffer = new byte[tempObjectArray.Length];
            for (int i = 0; i < tempObjectArray.Length; i++)
            {
                buffer[i] = (byte)tempObjectArray[i];
            }
            return buffer;
        }

        public static byte[] ToByteArray(sbyte[] sbyteArray)
        {
            byte[] buffer = new byte[sbyteArray.Length];
            for (int i = 0; i < sbyteArray.Length; i++)
            {
                buffer[i] = (byte)sbyteArray[i];
            }
            return buffer;
        }

        public static byte[] ToByteArray(string sourceString)
        {
            byte[] buffer = new byte[sourceString.Length];
            for (int i = 0; i < sourceString.Length; i++)
            {
                buffer[i] = (byte)sourceString[i];
            }
            return buffer;
        }

        public static sbyte[] ToSByteArray(byte[] byteArray)
        {
            sbyte[] numArray = new sbyte[byteArray.Length];
            for (int i = 0; i < byteArray.Length; i++)
            {
                numArray[i] = (sbyte)byteArray[i];
            }
            return numArray;
        }

        internal virtual int UnicodeProbability(sbyte[] rawtext)
        {
            if (rawtext.Length < 1) {
                return 100;
            }
            if (((((sbyte)Identity((long)0xfeL)) == rawtext[0]) && (((sbyte)Identity((long)0xffL)) == rawtext[1])) || ((((sbyte)Identity((long)0xffL)) == rawtext[0]) && (((sbyte)Identity((long)0xfeL)) == rawtext[1])))
            {
                return 100;
            }
            return 0;
        }

        internal virtual int UTF8Probability(sbyte[] rawtext)
        {
            int num = 0;
            int length = 0;
            int num4 = 0;
            int num5 = 0;
            length = rawtext.Length;
            for (int i = 0; i < length; i++)
            {
                if ((rawtext[i] & 0x7f) == rawtext[i])
                {
                    num5++;
                }
                else if ((((-64 <= rawtext[i]) && (rawtext[i] <= -33)) && (((i + 1) < length) && (-128 <= rawtext[i + 1]))) && (rawtext[i + 1] <= -65))
                {
                    num4 += 2;
                    i++;
                }
                else if (((((-32 <= rawtext[i]) && (rawtext[i] <= -17)) && (((i + 2) < length) && (-128 <= rawtext[i + 1]))) && ((rawtext[i + 1] <= -65) && (-128 <= rawtext[i + 2]))) && (rawtext[i + 2] <= -65))
                {
                    num4 += 3;
                    i += 2;
                }
            }
            if (num5 != length)
            {
                num = (int)(100f * (((float)num4) / ((float)(length - num5))));
                if (num > 0x62)
                {
                    return num;
                }
                if ((num > 0x5f) && (num4 > 30))
                {
                    return num;
                }
            }
            return 0;
        }
    }
}
