using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Quester
{
    internal class QrcReader
    {
        private const byte LetterLineTerminator = 0xfc;
        private const byte LineTerminator = 0xfd;
        private const byte RecordTerminator = 0xfe;
        private const byte SubRecordTerminator = 0xff;

        private static QrcHeader ReadHeader(BinaryReader reader)
        {
            QrcHeader header = new QrcHeader();
            header.TextRecordHeaderLength = reader.ReadUInt16();
            int textRecordCount = (header.TextRecordHeaderLength / 6) - 1;
            header.TextRecordOffsets = new Dictionary<int, uint>();
            for (int i = 0; i < textRecordCount; i++)
            {
                ushort textRecordId = reader.ReadUInt16();
                header.TextRecordOffsets[textRecordId] = reader.ReadUInt32();
            }

            return header;
        }

        public static Dictionary<int, List<string>> ReadTextRecords(BinaryReader reader)
        {
            Dictionary<int, List<string>> records = new Dictionary<int, List<string>>();
            QrcHeader header = ReadHeader(reader);
            foreach (KeyValuePair<int, uint> offsets in header.TextRecordOffsets)
            {
                records[offsets.Key] = ReadTextRecord(reader, offsets.Value);
            }

            return records;
        }

        private static List<string> ReadTextRecord(BinaryReader reader, uint offset)
        {
            reader.BaseStream.Seek(offset, 0);
            List<string> subRecords = new List<string>();
            byte currentByte;
            StringBuilder sb = new StringBuilder();
            do
            {
                currentByte = reader.ReadByte();
                while (currentByte >= 32 && currentByte < 128)
                {
                    sb.Append((char) currentByte);
                    currentByte = reader.ReadByte();
                }

                if (currentByte == SubRecordTerminator)
                {
                    AddToSubrecord(sb, subRecords);
                    sb.Clear();
                }
                else if (currentByte == LineTerminator)
                {
                    sb.Append(" ");
                }
//                else if (currentByte == LetterLineTerminator)
//                {
//                    sb.Append("\\n");
//                }
            } while (currentByte != RecordTerminator);

            AddToSubrecord(sb, subRecords);
            return subRecords;
        }

        private static void AddToSubrecord(StringBuilder sb, List<string> subRecords)
        {
            var str = sb.ToString().Trim();
            if (str.Length > 0)
                subRecords.Add(str);
        }
    }

    internal struct QrcHeader
    {
        public ushort TextRecordHeaderLength;
        public Dictionary<int, uint> TextRecordOffsets;
    }
}