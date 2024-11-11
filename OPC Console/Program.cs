using Opc.UaFx.Client;
using static System.Net.Mime.MediaTypeNames;

namespace OPC_Console
{
    internal class Program
    {
        private static bool _exit = false;

        static void Main(string[] args)
        {
            string OPCServerConnectionString = "opc.tcp://172.26.134.121:49320";
            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);

            using (var opcClient = new OpcClient(OPCServerConnectionString))
            {
                try
                {
                    opcClient.Connect();
                    Console.WriteLine("Connected to OPC Server");
                    string RampSampleTag = "ns=2;s=Channel1.Device1.Tag1";
                    string BooleanSampleTag = "ns=2;s=Channel1.Device1.BoolVal";
                    // Initial output to set up the console
                    Console.WriteLine("Ramp Value: ");
                    Console.WriteLine("Boolean Value: ");
                    while (!_exit)
                    {

                        var status = opcClient.ReadNode(RampSampleTag);
                        bool boolStatus = (bool)opcClient.ReadNode(BooleanSampleTag).Value;
                        // Update the console output in place
                        Console.SetCursorPosition(12, 1); // Position for Ramp Value
                        Console.Write(status.Value.ToString().PadRight(10)); // PadRight to clear previous value
                        Console.SetCursorPosition(15, 2); // Position for Boolean Value
                        Console.Write(boolStatus.ToString().PadRight(10)); // PadRight to clear previous value
                        opcClient.WriteNode(BooleanSampleTag, !boolStatus);

                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(0, 3);
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    Console.SetCursorPosition(0, 4);
                    opcClient.Disconnect();
                    Console.WriteLine("Disconnected from OPC Server");
                }

            }

        }
        protected static void OnExit(object? sender, ConsoleCancelEventArgs args)
        {
            _exit = true;
            args.Cancel = true; // Prevent the process from terminating immediately
        }
    }
}
