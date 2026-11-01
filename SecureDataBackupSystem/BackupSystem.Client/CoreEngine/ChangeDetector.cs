
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecureDataBackupSystem.CoreEngine
{
    public enum FileChangeType
    {
        New,
        Modified,
        Deleted,
        Unchanged
    }

    public sealed class FileChange
    {
        public string RelativePath { get; }
        public FileChangeType Type { get; }

        public FileChange(string path, FileChangeType type)
        {
            RelativePath = path;
            Type = type;
        }
    }

    public static class ChangeDetector
    {
        public static List<FileChange> Compare(
            IEnumerable<BackupFileMetadata> oldFiles,
            IEnumerable<BackupFileMetadata> newFiles)
        {
            var oldDict = oldFiles.ToDictionary(
                f => f.RelativePath,
                StringComparer.OrdinalIgnoreCase);

            var newDict = newFiles.ToDictionary(
                f => f.RelativePath,
                StringComparer.OrdinalIgnoreCase);

            var result = new List<FileChange>();

            // Kiem tra file moi va file thay doi
            foreach (var file in newDict)
            {
                if (!oldDict.TryGetValue(
                    file.Key, out var oldFile))
                {
                    result.Add(new FileChange(
                        file.Key, FileChangeType.New));
                }
                else if (!string.Equals(
                    oldFile.Sha256,
                    file.Value.Sha256,
                    StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(new FileChange(
                        file.Key, FileChangeType.Modified));
                }
                else
                {
                    result.Add(new FileChange(
                        file.Key, FileChangeType.Unchanged));
                }
            }

            // Kiem tra file da bi xoa
            foreach (var file in oldDict)
            {
                if (!newDict.ContainsKey(file.Key))
                {
                    result.Add(new FileChange(
                        file.Key, FileChangeType.Deleted));
                }
            }

            return result;
        }
    }
}
