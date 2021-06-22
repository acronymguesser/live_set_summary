using System;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace LiveSetSummary
{
    public class LiveSetXmlExtractor
    {
        public XmlDocument ExtractXml(string filename)
        {
            var xml = new XmlDocument();

            using (var fs = File.OpenRead(filename))
            using (var rs = new MemoryStream())
            using (var gzips = new GZipStream(fs, CompressionMode.Decompress))
            {
                gzips.CopyTo(rs);
                rs.Seek(0, SeekOrigin.Begin);
                xml.Load(rs);
            }

            return xml;
        }
    }
}
