namespace THH.Core.Excel.Npoi
{
    internal class FilePathResult
    {
        private string generateFileName;
        private string v;

        public FilePathResult(string generateFileName, string v)
        {
            this.generateFileName = generateFileName;
            this.v = v;
        }

        public string FileDownloadName { get; set; }
    }
}