
using System.IO;
namespace DwfTools.W2d.Opcodes
{
    public struct DrawContourSetLong : IOpcode
    {
        public class Factory : OpcodeFactory, ISingleByteOpcodeFactory
        {
            public int OpcodeId { get { return DrawContourSetLong.Id; } }

            public override IOpcode ReadOpcode(Stream stream)
            {
                int contourCount = ReadUnsignedByte(stream);

                //extended count
                if (contourCount == 0)
                {
                    contourCount = 256 + ReadUnsignedShort(stream);
                }

                for (int i = 0; i < contourCount; i++)
                {
                    //each contour has a list of points
                    int pointsCount = ReadUnsignedByte(stream);

                    //points extended count
                    if (pointsCount == 0)
                    {
                        pointsCount = 256 + ReadUnsignedShort(stream);
                    }

                    //first vertex
                    ReadSignedLong(stream);
                    ReadSignedLong(stream);

                    for (int j = 0; j < pointsCount; j++)
                    {
                        //vertex(j)
                        ReadSignedLong(stream);
                        ReadSignedLong(stream);
                    }
                }

                return new DrawContourSetLong();
            }
        }
        
        public static readonly byte Id = 0x6B;

        //TODO: Not supported for now

        public CoordinatesType CoordinatesType { get { return CoordinatesType.Relative; } }
    }
}
