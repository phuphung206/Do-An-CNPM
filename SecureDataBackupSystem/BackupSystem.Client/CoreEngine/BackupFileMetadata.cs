using System;

namespace SecureDataBackupSystem.CoreEngine
{
    // Metadata of one file at the time it was scanned.
    public sealed class BackupFileMetadata
    {
        public string FullPath { get; set; } = string.Empty;
        public string RelativePath { get; set; } = string.Empty;
        public string Sha256 { get; set; } = string.Empty;
        public long SizeBytes { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }
        
    }
}
