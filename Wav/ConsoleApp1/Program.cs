using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {

        #region MyRegion
        static string path = @"C:\Users\Jaffa\Desktop\Wav\";
        static List<string> files = new List<string> { path + "a.wav", path + "b.wav", path + "c.wav", path + "d.wav" };
        static List<string> Vowels = new List<string>()
        {
            "ा", "ि", "ी", "ु", "ू",
            "ृ", "ॄ", "ॢ", "ॣ",
            "े", "ै", "ो", "ौ", "ं", "ः"
        };
        static List<string> Consonants = new List<string>()
        {
            "क", "ख", "ग", "घ", "ङ",
            "च", "छ", "ज", "झ", "ञ",
            "ट", "ठ", "ड", "ढ", "ण",
            "त", "थ", "द", "ध", "न",
            "प", "फ", "ब", "भ", "म",
            "य", "र", "ल", "व", "श",
            "ष", "स", "ह"
        };
        #endregion

        static void Main(string[] args)
        {
            string[] test = new string[] { "a", "b", "c" };
            Wave wave = new Wave();
            foreach (var item in new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m" })
            {
                File.Delete(path + item + " ( 1 ).wav");
            }

            foreach (var item in Directory.GetFiles(@"C:\Users\Jaffa\Desktop\Wav"))
            {
                wave.Splitter(item, new Timer[]
                {
                    new Timer()
                    {
                        From = new TimeSpan(0, 0, 0, 0, 0),
                        To= new TimeSpan(0, 0, 0, 1, 000)
                    }
                });
            }
        }
    }

    #region Classes

    class Wave
    {

        #region WAV

        #region RIFF

        /*1 - 4 */
        //Marks the file as a riff file. Characters are each 1 byte long.
        public char[] ChunkID = new char[4] { 'R', 'I', 'F', 'F' };

        /*5 - 8 */
        //Size of the overall file - 8 bytes, in bytes (32-bit integer).
        public int ChunkSize = 0;//8byte 32 bit int

        /*9 -12 */
        //File Type Header. For our purposes, it always equals "WAVE".
        public char[] ChunkFormat = new char[4] { 'W', 'A', 'V', 'E' };

        #endregion

        #region Format

        /* 13 - 16 */
        //Format chunk marker. Includes trailing null
        public char[] FormatChunkID = new char[4] { 'f', 'm', 't', ' ' };

        /* 17 - 20 */
        //Length of format data as listed above
        public int FormatChunkSize = 16;

        /* 21 - 22 */
        //Type of format (1 is PCM) - 2 byte integer
        public short AudioFormat = 1;

        /* 23 - 24 */
        //Number of Channels - 2 byte integer
        public short NumChannels = 0;

        /* 25 - 28 */
        //Sample Rate - 32 byte integer
        //Common values are 44100 (CD), 48000 (DAT)
        //Sample Rate = Number of Samples per second, or Hertz.
        public int SampleRate;

        /* 29 - 32 */
        //(Sample Rate * BitsPerSample * Channels) / 8.
        public int ByteRate = 0;

        /* 33 - 34 */
        //(BitsPerSample * Channels) / 8.1 - 8 bit mono2 - 8 bit stereo/16 bit mono4 - 16 bit stereo
        public short BlockAlign = 0;

        /* 35 - 36 */
        //2 byte integer
        public short BitsPerSample = 0;

        #region Extra Bytes

        public short ExtraBytesSize = 0;

        public short ValidBitsPerSample = 0;

        public float ChannelMask = 0;

        public byte[] SubFormat;

        public bool IsExtraByteExist = false;

        #endregion

        #endregion

        #region Fact

        public char[] FactID = new char[4] { 'f', 'a', 'c', 't' };

        public int FactChunkSize = 4;

        public float SampleLength = 0;

        public bool IsFactExist = false;

        #endregion

        #region LIST

        public char[] ListChunkID = new char[4] { 'L', 'I', 'S', 'T' };

        public int ListChunkSize = 0;

        public string ListChunkID1 = "";

        public string ListChunkMetaData = "";

        public int ListChunkMetaDataStart = 0;

        public string ListChunkMetaDataInfo = "";

        public bool IsListChunkExist = false;

        #endregion

        public char[] CueID = new char[4] { 'c', 'u', 'e', ' ' };

        //4 + (24 * NumCuePoints)
        public int CueSize = 0;

        public int NumCuePoints = 0;

        public int CuePointID = 0;

        public int PlayOrderPosition = 0;

        public char[] CueDataChunkID = new char[4] { 'd', 'a', 't', 'a' };

        public int ChunkStart = 0;

        public int BlockStart = 0;

        public int FrameOffset = 0;

        #region Data

        /* 37 - 40 */
        //"data" chunk header. Marks the beginning of the data section.
        public char[] DataChunkID = new char[4] { 'd', 'a', 't', 'a' };

        /* 41 - 44 */
        //Size of the data section.
        public int DataChunkSize = 0;

        /* 45+ */
        //content
        public List<byte> Data = new List<byte>();

        //if DataSize is even then padByte = 0 else 1
        public byte PadByte = 0;

        #endregion

        #region Misc

        //Minutes = ((DataChunkSize / ByteRate) / 60)
        public double Minutes = 0;

        //Seconds = ((double)DataChunkSize / (double)ByteRate) - ((double)Minutes * 60)
        public double Seconds = 0;

        //To.TotalMilliseconds - From.TotalMilliseconds
        public TimeSpan Duration = new TimeSpan();

        //FromBytes - (FromBytes % BlockAlign)
        public double StartPosition = 0;

        //ToBytes - (ToBytes % BlockAlign)
        public double EndPosition = 0;

        //From.TotalMilliseconds * (ByteRate / 1000)
        public double FromBytes = 0;

        //To.TotalMilliseconds * (ByteRate / 1000)
        public double ToBytes = 0;

        public int Samples = 0;

        public float[] Left;

        public float[] Right;

        public long DataPosition = 0;

        public string FileName = "";

        #endregion

        #endregion

        #region Public

        public void WaveHeaderIN(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    ChunkSize = (int)fs.Length - 8;

                    fs.Position = 22;
                    NumChannels = br.ReadInt16();

                    fs.Position = 24;
                    SampleRate = br.ReadInt32();

                    fs.Position = 34;
                    BitsPerSample = br.ReadInt16();

                    DataChunkSize = (int)fs.Length - 44;
                };
            };
        }

        public void WaveOUT(string file)
        {
            using (FileStream fs = File.OpenWrite(file))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {

                    fs.Position = 0;

                    bw.Write(ChunkID);

                    bw.Write(ChunkSize);

                    bw.Write(ChunkFormat);

                    bw.Write(FormatChunkID);

                    bw.Write(FormatChunkSize);

                    bw.Write(AudioFormat);

                    bw.Write(NumChannels);

                    bw.Write(SampleRate);

                    bw.Write(GetByteRate());

                    bw.Write(GetBlockAlign());

                    bw.Write(BitsPerSample);

                    #region List
                    //if (IsListChunkExist)
                    //{
                    //    bw.Write(ListChunkID);

                    //    bw.Write(ListChunkSize);

                    //    bw.Write(ListChunkID1);

                    //    bw.Write(ListChunkMetaData);

                    //    bw.Write(ListChunkMetaDataStart);

                    //    bw.Write(ListChunkMetaDataInfo);
                    //} 
                    #endregion

                    #region ExtraBytes
                    //if (IsExtraByteExist)
                    //{

                    //    bw.Write(ExtraBytes);

                    //    bw.Write(ExtraBytesData);

                    //} 
                    #endregion

                    bw.Write(DataChunkID);

                    bw.Write(DataChunkSize);

                    bw.Write(Data.ToArray());
                };
            };
        }

        public void Merge(List<string> files, string result)
        {
            Wave wa_IN = new Wave();
            Wave wa_out = new Wave();

            foreach (var item in files)
            {
                //header
                wa_IN.WaveHeaderIN(item);
                wa_out.DataChunkSize += wa_IN.DataChunkSize;
                wa_out.ChunkSize += wa_IN.ChunkSize;

                //body
                wa_out.Data.AddRange(File.ReadAllBytes(item).Skip(44).ToList());
            }

            wa_out.BitsPerSample = wa_IN.BitsPerSample;
            wa_out.NumChannels = wa_IN.NumChannels;
            wa_out.SampleRate = wa_IN.SampleRate;
            wa_out.WaveOUT(result);

        }

        public void Splitter(string file, Timer[] timer)
        {
            Wave wa_IN = new Wave();
            Wave wa_OUT = new Wave();

            using (FileStream fs = File.OpenRead(file))
            {
                using (BinaryReader bw = new BinaryReader(fs))
                {
                    SetRIFF(wa_IN, bw);

                    SetFormatChunk(wa_IN, bw);

                    SetDataChunk(wa_IN, bw);

                    SetMisc(wa_IN, bw, fs.Name);
                };
            };

            //Silence(wa_IN);

            WriteToOUT(wa_IN, wa_OUT, timer, file);

        }

        #endregion

        #region Private

        private void SetRIFF(Wave wa_IN, BinaryReader bw)
        {
            wa_IN.ChunkID = bw.ReadChars(4);

            wa_IN.ChunkSize = bw.ReadInt32();

            wa_IN.ChunkFormat = bw.ReadChars(4);
        }

        private void SetFormatChunk(Wave wa_IN, BinaryReader bw)
        {
            wa_IN.FormatChunkID = bw.ReadChars(4);

            wa_IN.FormatChunkSize = bw.ReadInt32();

            wa_IN.AudioFormat = bw.ReadInt16();

            wa_IN.NumChannels = bw.ReadInt16();

            wa_IN.SampleRate = bw.ReadInt32();

            wa_IN.ByteRate = bw.ReadInt32();

            wa_IN.BlockAlign = bw.ReadInt16();

            wa_IN.BitsPerSample = bw.ReadInt16();

            SetExtraBytes(wa_IN, bw);
        }

        private void SetDataChunk(Wave wa_IN, BinaryReader bw)
        {
            wa_IN.DataChunkID = bw.ReadChars(4);

            wa_IN.IsListChunkExist = false;

            wa_IN.IsFactExist = false;

            if (string.Join("", wa_IN.DataChunkID) == "LIST")
            {
                SetLISTChunk(wa_IN, bw);
            }
            else if (string.Join("", wa_IN.DataChunkID) == "fact")
            {
                SetFactChunk(wa_IN, bw);
            }
            else if (string.Join("", wa_IN.DataChunkID) == "cue ")
            {
                wa_IN.CueID = new char[4] { 'c', 'u', 'e', ' ' };

                wa_IN.CueSize = bw.ReadInt32();

                //wa_IN.NumCuePoints = bw.ReadInt32();
                //wa_IN.CuePointID = bw.ReadInt32();
                //wa_IN.PlayOrderPosition = bw.ReadInt32();
                //wa_IN.CueDataChunkID= bw.ReadChars(4);
                //wa_IN.ChunkStart = bw.ReadInt32();
                //wa_IN.BlockStart = bw.ReadInt32();
                //wa_IN.FrameOffset = bw.ReadInt32();
                //if (wa_IN.NumCuePoints > 0)
                //{

                //}
                bw.ReadBytes(wa_IN.CueSize);

                wa_IN.DataChunkID = bw.ReadChars(4);
            }

            wa_IN.DataChunkSize = bw.ReadInt32();

            wa_IN.DataPosition = bw.BaseStream.Position;

            wa_IN.Data = bw.ReadBytes((int)bw.BaseStream.Length - (int)wa_IN.DataPosition).ToList();

            wa_IN.PadByte = (byte)(((wa_IN.Data.Count & 1) == 0) ? 0 : 1);
        }

        private void SetFactChunk(Wave wa_IN, BinaryReader bw)
        {
            wa_IN.IsFactExist = true;

            wa_IN.FactID = new char[4] { 'f', 'a', 'c', 't' };

            wa_IN.FactChunkSize = bw.ReadInt32();

            wa_IN.SampleLength = bw.ReadSingle();

            wa_IN.DataChunkID = bw.ReadChars(4);
        }

        private void SetLISTChunk(Wave wa_IN, BinaryReader bw)
        {
            wa_IN.IsListChunkExist = true;

            wa_IN.ListChunkID = new char[4] { 'L', 'I', 'S', 'T' };

            wa_IN.ListChunkSize = bw.ReadInt32();

            wa_IN.ListChunkID1 = BitConverter.ToString(bw.ReadBytes(4));

            wa_IN.ListChunkMetaData = BitConverter.ToString(bw.ReadBytes(4));

            wa_IN.ListChunkMetaDataStart = bw.ReadInt32();

            wa_IN.ListChunkMetaDataInfo = BitConverter.ToString(bw.ReadBytes(14));

            wa_IN.DataChunkID = bw.ReadChars(4);
        }

        private void SetExtraBytes(Wave wa_IN, BinaryReader bw)
        {
            wa_IN.IsExtraByteExist = false;

            if (wa_IN.FormatChunkSize > 16)
            {
                wa_IN.IsExtraByteExist = true;

                wa_IN.ExtraBytesSize = bw.ReadInt16();

                if (wa_IN.ExtraBytesSize == 22)
                {
                    wa_IN.ValidBitsPerSample = bw.ReadInt16();

                    wa_IN.ChannelMask = bw.ReadSingle();

                    wa_IN.SubFormat = bw.ReadBytes(16);
                }
            }
        }

        private void SetMisc(Wave wa_IN, BinaryReader bw, string fileName)
        {
            wa_IN.Samples = GetSamples(wa_IN);

            wa_IN.FileName = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName);

            //SetAudioChannels(wa_IN, GetAudioChannels(wa_IN));
        }

        private bool SetAudioChannels(Wave wa_IN, float[] asFloat)
        {
            switch (wa_IN.NumChannels)
            {
                //Mono
                case 1:
                    {
                        wa_IN.Left = asFloat;
                        wa_IN.Right = null;
                        return true;
                    }
                //Stereo    
                case 2:
                    {
                        wa_IN.Left = new float[wa_IN.Samples];
                        wa_IN.Right = new float[wa_IN.Samples];
                        try
                        {
                            for (int i = 0, j = 0; i < wa_IN.Samples; i++)
                            {
                                wa_IN.Left[i] = asFloat[j++];
                                wa_IN.Right[i] = asFloat[j++];
                            }
                        }
                        catch (Exception)
                        { }
                        return true;
                    }
                //dual mono
                case 3:
                    {
                        return true;
                    }
                //joint stereo
                case 4:
                    {
                        return true;
                    }
                default:
                    return false;
            }
        }

        private float[] GetAudioChannels(Wave wa_IN)
        {
            float[] asFloat = null;

            switch (wa_IN.BitsPerSample)
            {
                case 64:
                    {
                        double[] asDouble = new double[wa_IN.Samples];
                        Buffer.BlockCopy(wa_IN.Data.ToArray(), 0, asDouble, 0, wa_IN.DataChunkSize);
                        asFloat = Array.ConvertAll(asDouble, e => (float)e);
                        break;
                    }
                case 32:
                    {
                        asFloat = new float[wa_IN.Samples];
                        Buffer.BlockCopy(wa_IN.Data.ToArray(), 0, asFloat, 0, wa_IN.DataChunkSize);
                        break;
                    }
                case 16:
                    {
                        Int16[] asInt16 = new Int16[wa_IN.Samples];
                        Buffer.BlockCopy(wa_IN.Data.ToArray(), 0, asInt16, 0, wa_IN.DataChunkSize);
                        asFloat = Array.ConvertAll(asInt16, e => e / (float)Int16.MaxValue);
                        break;
                    }
                default:
                    break;
            }

            return asFloat;
        }

        private int GetSamples(Wave wa_IN)
        {
            return (wa_IN.DataChunkSize / (wa_IN.BitsPerSample / 8));
        }

        private void WriteToOUT(Wave wa_IN, Wave wa_OUT, Timer[] timer, string file)
        {
            wa_OUT = (Wave)wa_IN.MemberwiseClone();

            int count = 0;

            foreach (var item in timer)
            {
                count++;

                wa_OUT.StartPosition = GetPostion(GetBytes(item.From, wa_IN.ByteRate), wa_IN.BlockAlign);

                wa_OUT.EndPosition = GetPostion(GetBytes(item.To, wa_IN.ByteRate), wa_IN.BlockAlign);

                wa_OUT.Data = GetData(wa_IN, wa_OUT);

                wa_OUT.DataChunkSize = GetDataLength(wa_IN, GetDuration(item.From, item.To), wa_IN.ByteRate);

                wa_OUT.ChunkSize = GetChunkSize(wa_OUT);

                wa_OUT.FormatChunkSize = 16;

                //WriteToFile(wa_IN, wa_OUT.FileName + " ( " + count + " ).txt");

                wa_OUT.WaveOUT(wa_OUT.FileName + " ( " + count + " ).wav");
            }
        }

        private int GetChunkSize(Wave wave)
        {
            return (36 + wave.Data.Count);
        }

        private List<byte> GetData(Wave wa_IN, Wave wa_OUT)
        {
            return wa_IN.Data.GetRange((int)wa_OUT.StartPosition, ((int)wa_OUT.EndPosition - (int)wa_OUT.StartPosition));
        }

        private int GetDataLength(Wave wave, TimeSpan duration, int byteRate)
        {
            return (int)GetBytes(duration, byteRate);
        }

        private short GetBlockAlign()
        {
            return (short)((BitsPerSample * NumChannels) / 8);
        }

        private int GetByteRate()
        {
            return (SampleRate * ((BitsPerSample * NumChannels) / 8));
        }

        private double GetMinutes(int dataChunkSize, int byteRate)
        {
            return dataChunkSize / (double)byteRate / 60d;
        }

        private double GetSeconds(int dataChunkSize, int byteRate)
        {
            return DataChunkSize / (double)ByteRate - (GetMinutes(dataChunkSize, byteRate) * 60);
        }

        private TimeSpan GetDuration(TimeSpan from, TimeSpan to)
        {
            return to - from;
        }

        private double GetPostion(double bytes, short blockAlign)
        {
            return bytes - (bytes % blockAlign);
        }

        private double GetBytes(TimeSpan timeSpan, int byteRate)
        {
            return timeSpan.TotalMilliseconds * (byteRate / 1000d);
        }

        private void Silence(Wave wa_In)
        {
            short FILTER_FREQ_LOW = -10000;
            short FILTER_FREQ_HIGH = 10000;
            int startpos = 0, endpos = 0;
            try
            {
                for (int j = 0; j < wa_In.Data.Count; j += 2)
                {
                    short snd = ComplementToSigned(ref wa_In.Data, j);
                    if (snd > FILTER_FREQ_LOW && snd < FILTER_FREQ_HIGH) startpos = j;
                    else
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            //At end
            for (int k = wa_In.Data.Count - 1; k >= 0; k -= 2)
            {
                short snd = ComplementToSigned(ref wa_In.Data, k - 1);
                if (snd > FILTER_FREQ_LOW && snd < FILTER_FREQ_HIGH) endpos = k;
                else
                    break;
            }
        }

        private short ComplementToSigned(ref List<byte> bytArr, int intPos) // 2's complement to normal signed value
        {
            short snd = BitConverter.ToInt16(bytArr.ToArray(), intPos);
            if (snd != 0)
                snd = Convert.ToInt16((~snd | 1));
            return snd;
        }

        private void WriteToFile(Wave wa_IN, string fileName)
        {
            File.WriteAllText(fileName,
                string.Format(
                    "ChunkID = {0}\r\nChunkSize = {1}\r\nChunkFormat = {2}\r\nFormatChunkID = {3}\r\nFormatChunkSize = {4}\r\nAudioFormat = {5}\r\nNumChannels = {6}\r\nSampleRate = {7}\r\nByteRate = {8}\r\nBlockAlign = {9}\r\nBitsPerSample = {10}\r\nExtraBytesSize = {11}\r\nValidBitsPerSample = {12}\r\nChannelMask = {13}\r\nSubFormat = {14}\r\nIsExtraByteExist = {15}\r\nFactID = {16}\r\nFactChunkSize = {17}\r\nSampleLength = {18}\r\nIsFactExist = {19}\r\nListChunkID = {20}\r\nListChunkSize = {21}\r\nListChunkID1 = {22}\r\nListChunkMetaData = {23}\r\nListChunkMetaDataStart = {24}\r\nListChunkMetaDataInfo = {25}\r\nIsListChunkExist = {26}\r\nDataChunkID = {27}\r\nDataChunkSize = {28}\r\nData = {29}\r\nPadByte = {30}\r\nSamples = {31}\r\nDataPosition = {32}",
                    wa_IN.ChunkID, wa_IN.ChunkSize, wa_IN.ChunkFormat, wa_IN.FormatChunkID, wa_IN.FormatChunkSize, wa_IN.AudioFormat, wa_IN.NumChannels, wa_IN.SampleRate, wa_IN.ByteRate, wa_IN.BlockAlign, wa_IN.BitsPerSample, wa_IN.ExtraBytesSize, wa_IN.ValidBitsPerSample, wa_IN.ChannelMask, wa_IN.SubFormat, wa_IN.IsExtraByteExist, wa_IN.FactID, wa_IN.FactChunkSize, wa_IN.SampleLength, wa_IN.IsFactExist, wa_IN.ListChunkID, wa_IN.ListChunkSize, wa_IN.ListChunkID1, wa_IN.ListChunkMetaData, wa_IN.ListChunkMetaDataStart, wa_IN.ListChunkMetaDataInfo, wa_IN.IsListChunkExist, wa_IN.DataChunkID, wa_IN.DataChunkSize, wa_IN.Data, wa_IN.PadByte, wa_IN.Samples, wa_IN.DataPosition
                    ));
        }

        #endregion

    }

    class Timer
    {
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }

    static class Test
    {
        public static string Path { get; set; }
    }

    #endregion
}

#region MyRegion
//foreach (var co in Consonants)
//{
//    foreach (var vo in Vowels)
//    {

//    }
//}
//wave.Merge(files.GetRange(0, 2), files[2]); 
#endregion