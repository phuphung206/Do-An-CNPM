
using System;
using System.IO;
using System.IO.Compression;

namespace SecureDataBackupSystem.CoreEngine
{
    public static class CompressionHelper
    {
        public static void CompressFile(
            string sourcePath,
            string outputPath)
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException(
                    "Khong tim thay file nguon.", sourcePath);

            if (string.Equals(
                Path.GetFullPath(sourcePath),
                Path.GetFullPath(outputPath),
                StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException(
                    "File nguon va file nen phai khac nhau.");

            using (FileStream input =
                File.OpenRead(sourcePath))
            using (FileStream output =
                new FileStream(outputPath, FileMode.CreateNew))
            using (GZipStream gzip =
                new GZipStream(
                    output,
                    CompressionMode.Compress))
            {
                input.CopyTo(gzip);
            }
        }
       
public static void DecompressFile(
    string compressedPath,
    string outputPath)
{
    if (!File.Exists(compressedPath))
        throw new FileNotFoundException(
            "Khong tim thay file nen.", compressedPath);

    if (string.Equals(
        Path.GetFullPath(compressedPath),
        Path.GetFullPath(outputPath),
        StringComparison.OrdinalIgnoreCase))
        throw new ArgumentException(
            "File nen va file giai nen phai khac nhau.");

    using (FileStream input =
        File.OpenRead(compressedPath))
    using (GZipStream gzip =
        new GZipStream(input, CompressionMode.Decompress))
    using (FileStream output =
        new FileStream(outputPath, FileMode.CreateNew))
    {
        gzip.CopyTo(output);
    }
}
 
    }
}
