using System;
using System.Threading.Tasks;


namespace R5T.O0006.Construction
{
    class Program
    {
        static async Task Main()
        {
            await Demonstrations.Instance.StandardizeVariances();
        }
    }
}