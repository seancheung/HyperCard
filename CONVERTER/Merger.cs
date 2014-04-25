using System;
using System.IO;
using MODEL;

namespace CONVERTER
{
    internal class Merger
    {
        /// <summary>
        /// Merge multiple files
        /// </summary>
        /// <param name="inputFiles">Full paths of files to merge</param>
        /// <param name="outputFile">Output file full path</param>
        public static void Merge(string[] inputFiles, string outputFile)
        {
            try
            {
                using (var fs = File.Create(outputFile))
                {
                    using (var bw = new BinaryWriter(fs))
                    {
                        foreach (var file in inputFiles)
                        {
                            //Mark file breaking point
                            bw.Write(true);
                            //Mark file legal name
                            bw.Write(Path.GetFileName(file));
                            //Size length of the file
                            var data = File.ReadAllBytes(file);
                            //Track length
                            bw.Write(data.Length);
                            //Save file data
                            bw.Write(data);
                        }
                        //Mark end
                        bw.Write(false);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerError.Log("Merging File Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Split File to multiple
        /// </summary>
        /// <param name="inputFile">File to split</param>
        /// <param name="outputPath">Path to split to</param>
        public static void Split(string inputFile, string outputPath)
        {
            try
            {
                using (var br = new BinaryReader(File.OpenRead(inputFile)))
                {
                    while (br.ReadBoolean())
                    {
                        using (var fs = File.Create(string.Format(@"{0}\{1}", outputPath, br.ReadString())))
                        {
                            using (var bw = new BinaryWriter(fs))
                            {
                                var len = br.ReadInt32();
                                var data = br.ReadBytes(len);
                                bw.Write(data);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LoggerError.Log("Splitting File Error: " + ex.Message);
            }
        }
    }
}
