using System.Text;
using SecureDataBackupSystem.CoreEngine;

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("=== CORE BACKUP ENGINE - BUOC 1 ===");

string? source = args.Length > 0 ? args[0] : null;
if (string.IsNullOrWhiteSpace(source))
{
    Console.Write("Nhap duong dan file/thu muc: ");
    source = Console.ReadLine();
}

if (string.IsNullOrWhiteSpace(source))
{
    Console.WriteLine("Chua nhap duong dan.");
    return;
}

try
{
    var files = FileScanner.Scan(source.Trim().Trim('"'));
    Console.WriteLine("So file da quet: " + files.Count);
    foreach (var f in files)
    {
        Console.WriteLine("----------------------------");
        Console.WriteLine("File: " + f.RelativePath);
        Console.WriteLine("Size: " + f.SizeBytes + " bytes");
        Console.WriteLine("Modified UTC: " + f.LastWriteTimeUtc.ToString("u"));
        Console.WriteLine("SHA-256: " + f.Sha256);
    }
}
catch (Exception ex)
{
    Console.Error.WriteLine("Loi khi quet file: " + ex.Message);
    Environment.ExitCode = 1;
}



Console.WriteLine("\n=== INCREMENTAL BACKUP ===");

// Duong dan luu trang thai file
string stateFolder = Path.Combine(
    Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData),
    "SecureBackup.Core.Demo");

Directory.CreateDirectory(stateFolder);

string statePath = Path.Combine(
    stateFolder, "last-scan.json");

// Doc danh sach file cua lan backup truoc
var oldFiles = new List<BackupFileMetadata>();

if (File.Exists(statePath))
{
    string oldJson = File.ReadAllText(statePath);

    oldFiles = System.Text.Json.JsonSerializer
        .Deserialize<List<BackupFileMetadata>>(oldJson)
        ?? new List<BackupFileMetadata>();

    Console.WriteLine("Da doc du lieu backup cu.");
}
else
{
    Console.WriteLine("Lan backup dau tien.");
}

// Quet trang thai hien tai
var newFiles = FileScanner.Scan(source.Trim().Trim('"'));

// So sanh 2 lan backup
var changes = ChangeDetector.Compare(oldFiles, newFiles);

var filesToBackup = changes
    .Where(c => c.Type == FileChangeType.New ||
                c.Type == FileChangeType.Modified)
    .ToList();

Console.WriteLine("\n=== FILE CAN BACKUP ===");

foreach (var file in filesToBackup)
{
    Console.WriteLine(file.RelativePath);
}

Console.WriteLine(
    $"Tong so file can backup: {filesToBackup.Count}");

foreach (var change in changes)
{
    Console.WriteLine($"{change.RelativePath}: {change.Type}");
}

// Luu trang thai moi cho lan chay sau
string json = System.Text.Json.JsonSerializer.Serialize(
    newFiles,
    new System.Text.Json.JsonSerializerOptions
    {
        WriteIndented = true
    });

// Chi cap nhat metadata sau khi backup thanh cong.
// File.WriteAllText(statePath, json);

Console.WriteLine("Chua cap nhat metadata - cho backup thanh cong.");


Console.WriteLine("\n=== INCREMENTAL COMPRESSION ===");

var manifest = new BackupManifest
{
    BackupId = Guid.NewGuid().ToString("N"),
    CreatedUtc = DateTime.UtcNow
};

foreach (var change in filesToBackup)
{
    // Tim file moi hoac da thay doi
    var file = newFiles.First(f =>
        string.Equals(
            f.RelativePath,
            change.RelativePath,
            StringComparison.OrdinalIgnoreCase));

    // Tao file nen trong thu muc tam
    string compressedPath = Path.Combine(
        Path.GetTempPath(),
        $"backup-{Guid.NewGuid():N}.gz");

    // Nen file
    CompressionHelper.CompressFile(
        file.FullPath,
        compressedPath);

    Console.WriteLine("Da nen: " + file.RelativePath);
    Console.WriteLine("Kich thuoc goc: " +
        file.SizeBytes + " bytes");
    Console.WriteLine("Kich thuoc nen: " +
        new FileInfo(compressedPath).Length + " bytes");

    // Giai nen thu de kiem tra du lieu
    string restoredPath = Path.Combine(
        Path.GetTempPath(),
        $"restored-{Guid.NewGuid():N}.bin");

    CompressionHelper.DecompressFile(
        compressedPath,
        restoredPath);

    
using (var stream = File.OpenRead(restoredPath))
{
    string hash = Convert.ToHexString(
        System.Security.Cryptography.SHA256.HashData(stream));

    if (!string.Equals(hash, file.Sha256,
        StringComparison.OrdinalIgnoreCase))
    {
        throw new InvalidDataException(
            "Du lieu khong khop SHA-256!");
    }

    Console.WriteLine("Kiem tra du lieu: OK");
}

    // Tao duong dan kho Backup
    string backupStore = Path.Combine(
        Environment.GetFolderPath(
            Environment.SpecialFolder.UserProfile),
        "BackupStore");

    Directory.CreateDirectory(backupStore);

    // Tao ten file duy nhat de tranh ghi de
    string savedPath = Path.Combine(
        backupStore,
        $"{Guid.NewGuid():N}.gz");

    // Chuyen file nen vao kho Backup
    File.Move(compressedPath, savedPath);

    Console.WriteLine("Da luu Backup: " + savedPath);
    
    manifest.Files.Add(new BackupManifestEntry
    {
        RelativePath = file.RelativePath,
        StoredFileName = Path.GetFileName(savedPath),
        Sha256 = file.Sha256,
        SizeBytes = file.SizeBytes,
        ChangeType = change.Type
    });

    Console.WriteLine("Da ghi nhan file vao Manifest.");

} 

 // Ghi nhan cac file da bi xoa
foreach (var change in changes.Where(
    c => c.Type == FileChangeType.Deleted))
{
    var oldFile = oldFiles.FirstOrDefault(f =>
        string.Equals(
            f.RelativePath,
            change.RelativePath,
            StringComparison.OrdinalIgnoreCase));

    manifest.Files.Add(new BackupManifestEntry
    {
        RelativePath = change.RelativePath,
        StoredFileName = "",
        Sha256 = oldFile?.Sha256 ?? "",
        SizeBytes = oldFile?.SizeBytes ?? 0,
        ChangeType = FileChangeType.Deleted
    });

    Console.WriteLine(
        "Da ghi nhan file bi xoa: " + change.RelativePath);
}

if (manifest.Files.Count > 0)

{
    string manifestFolder = Path.Combine(
        Environment.GetFolderPath(
            Environment.SpecialFolder.UserProfile),
        "BackupStore");

    Directory.CreateDirectory(manifestFolder);

    string manifestPath = Path.Combine(
        manifestFolder,
        $"manifest-{manifest.BackupId}.json");

    string manifestJson =
        System.Text.Json.JsonSerializer.Serialize(
            manifest,
            new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });

    File.WriteAllText(manifestPath, manifestJson);

    Console.WriteLine("Da luu Manifest: " + manifestPath);
    
    File.WriteAllText(statePath, json);

    Console.WriteLine(
        "Backup thanh cong! Da cap nhat trang thai.");

}
else
{
    Console.WriteLine("Khong co file can backup.");
}

