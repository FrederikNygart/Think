using System;
using System.Collections.Generic;
using System.IO;

namespace Pensum
{
    public class ImageReader
    {
        static int DoodleCount = 1000;
        static double TrainingPercentile = 0.8;
        static double TestPercentile = 0.2;
        static int imgSize = 784;

        public enum DATA_SET_TYPE
        {
            training,
            test
        }

        static Dictionary<double, string> DoodleBinPath = new Dictionary<double, string>
        {
            { DoodleType.apple, "C://Users/frede/OneDrive/Dokumenter/Projects/Think/Pensum/apple1000.bin"},
            { DoodleType.bird, "C://Users/frede/OneDrive/Dokumenter/Projects/Think/Pensum/bird1000.bin"},
            { DoodleType.compass, "C://Users/frede/OneDrive/Dokumenter/Projects/Think/Pensum/compass1000.bin"}
        };

        public static byte[] ReadBin(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static Dictionary<DATA_SET_TYPE, List<Doodle>> GetDoodleData(double type)
        {
            var totalData = ReadBin(DoodleBinPath[type]);
            var trainingData = GetTrainingData(totalData, type);
            var testData = GetTestData(totalData, type);

            return new Dictionary<DATA_SET_TYPE, List<Doodle>>
            {
                { DATA_SET_TYPE.training , trainingData},
                { DATA_SET_TYPE.test , testData }
            };
        }

        public static List<Doodle> GetTrainingData(byte[] totalData, double type)
        {
            var trainingData = new List<Doodle>();
            for (var d = 0; d < (DoodleCount * TrainingPercentile); d++)
            {
                var newDoodle = new Doodle();
                var doodlePixel = new List<double>();
                var offset = imgSize * d;
                for (var i = 0; i < imgSize; i++)
                {
                    doodlePixel.Add(totalData[offset + i]);
                }
                newDoodle.Pixels = doodlePixel;
                newDoodle.Labels = new List<double> { type };
                trainingData.Add(newDoodle);
            }
            return trainingData;
        }

        public static List<Doodle> GetTestData(byte[] totalData, double type)
        {
            var testData = new List<Doodle>();
            var testStart = (int)(DoodleCount * TrainingPercentile) * imgSize;
            for (var d = 0; d < (DoodleCount * TestPercentile); d++)
            {
                var newDoodle = new Doodle();
                var doodlePixel = new List<double>();
                var offset = (imgSize * d) + testStart;
                for (var i = 0; i < imgSize; i++)
                {
                    doodlePixel.Add(totalData[offset + i]);
                }
                newDoodle.Pixels = doodlePixel;
                newDoodle.Labels = new List<double> {type};
                testData.Add(newDoodle);
            }
            return testData;
        }

        public static void CreateBin()
        {
            var outIndex = 0;
            var outdata = new Byte[DoodleCount * imgSize];
            var fileName = "C:/Users/frede/OneDrive/Dokumenter/Projects/Think/Pensum/apple1000.bin";
            var data = ReadBin("../Pensum/apple.npy");
            using (
            BinaryWriter bwStream = new BinaryWriter(
                new FileStream(fileName, FileMode.Create)))
            {
                for (var n = 0; n < DoodleCount; n++)
                {
                    var start = 80 + n * imgSize;
                    for (var i = 0; i < imgSize; i++)
                    {
                        var index = start + i;
                        outdata[outIndex] = data[index];
                        outIndex++;
                    }
                }
                bwStream.Write(outdata, 0, outdata.Length);
                bwStream.Close();
            }
        }
    }
}
