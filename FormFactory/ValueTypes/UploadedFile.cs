using System;
using System.IO;

namespace FormFactory.ValueTypes
{
    public class UploadedFile
    {
        private Func<Stream> _getStream;
        public string ContentType { get; set; }
        public string Id { get; set; }
        public int ContentLength { get; set; }
        public string FileName { get; set; }
        public Stream GetStream()
        {
            return _getStream();
        }
        internal void SetGetStream(Func<Stream> getStream)
        {
            _getStream = getStream;
        }
    }

}