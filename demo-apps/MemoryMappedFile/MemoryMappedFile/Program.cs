using System;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace MemoryMappedFileProgram
{
    class Program
    {
        static IntPtr _pointer;
        static unsafe void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                var path = args[0];
                var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

                var length = stream.Length;
                var mappedFile = MemoryMappedFile.CreateFromFile(stream, null, length, MemoryMappedFileAccess.Read,
                    HandleInheritability.None, false);

                var viewAccessor = mappedFile.CreateViewAccessor(0, length, MemoryMappedFileAccess.Read);
                byte* memoryPtr = null;
                viewAccessor.SafeMemoryMappedViewHandle.AcquirePointer(ref memoryPtr);

                _pointer = new IntPtr(memoryPtr);
                ////

                long position = 0;
                while (true)
                {
                    var chunks = Math.Min(1024 * 1024 +1, length - position);
                    var data = AsSpan(position, (int)chunks);
                    position += data.Length;
                    int i = 0;
                    foreach (var b in data)
                    {
                        var bb = b;
                        i++;
                    }
                    Console.WriteLine(i);

                    if (position == length)
                        break;
                }

                ////



                _pointer = IntPtr.Zero;

                viewAccessor.SafeMemoryMappedViewHandle.ReleasePointer();
                viewAccessor.Dispose();
                mappedFile.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        static unsafe Span<byte> AsSpan(long offset, int length)
        {
            return new Span<byte>((byte*)_pointer + offset, length);
        }
    }
}
