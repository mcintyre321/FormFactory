namespace FormFactory.ValueTypes
{
    public class UploadedFile
    {
        public string ContentType { get; set; }
        public string Id { get; set; }
        public int ContentLength { get; set; }
        public string FileName { get; set; }
        public string Uri { get; set; }
    }
}