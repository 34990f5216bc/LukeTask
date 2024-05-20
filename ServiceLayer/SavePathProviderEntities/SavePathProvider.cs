using System.IO.Abstractions;

namespace ServiceLayer.SavePathProviderEntities
{
    public class SavePathProvider : ISavePathProvider
    {
        public string SavePath { get => _SavePathResult.Value; }

        Lazy<string> _SavePathResult;
        string? _FileName;
        IFileSystem _FileSystem;

        public SavePathProvider(SavePathConfig savePath, IFileSystem fileSystem)
        {
            _FileName = savePath?.FileName;
            _SavePathResult = new Lazy<string>(GetAbsoluteSavePath);
            _FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        private string GetAbsoluteSavePath()
        {
            string absoluteSavePath;

            if(!IsValidFileName())
            {
                absoluteSavePath = Path.Combine(_FileSystem.Directory.GetCurrentDirectory(), GetRandomFileName());
            }
            else
            {
                absoluteSavePath = Path.Combine(_FileSystem.Directory.GetCurrentDirectory(), _FileName);
            }

            return absoluteSavePath;
        }

        private string GetRandomFileName()
        {
            return Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".txt";
        }

        private bool IsValidFileName()
        {
            if (string.IsNullOrEmpty(_FileName))
                return false;
            var invalidChars = Path.GetInvalidFileNameChars().ToHashSet();
            return !_FileName.Any(c => invalidChars.Contains(c));
        }
    }
}
