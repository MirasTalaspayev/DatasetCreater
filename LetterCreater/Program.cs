using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace LetterCreater
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string source = @"";
            string destination = @"";
            await LetterService.ResizeImages(source, destination);            
        }
    }
}
