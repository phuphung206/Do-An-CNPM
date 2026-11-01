
using System;
using System.Collections.Generic;

namespace SecureDataBackupSystem.CoreEngine
{
    public sealed class BackupManifest
    {
        public string BackupId { get; set; } = "";
        public DateTime CreatedUtc { get; set; }

        public List<BackupManifestEntry> Files { get; set; }
            = new List<BackupManifestEntry>();
    }

    public sealed class BackupManifestEntry
    {
        public string RelativePath { get; set; } = "";
        public string StoredFileName { get; set; } = "";
        public string Sha256 { get; set; } = "";
        public long SizeBytes { get; set; }
        public FileChangeType ChangeType { get; set; }
    }
}
