using System.Threading.Tasks;
using Common;
using static System.Console;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTaskAsync().Wait();
            ReadLine();
        }

        static async Task RunTaskAsync()
        {
            var service = new RestService();
            var responseLeft = await service.PostAsByteArrayAsync(ServiceConstants.V1_LeftEndpoint, DataService.LeftJson());
            var responseRight = await service.PostAsByteArrayAsync(ServiceConstants.V1_RightEndpoint, DataService.RightJson());

            WriteLine(string.Format("Posted to endpoint [{0}]: {1} \nPosted to endpoint [{2}]: {3}", ServiceConstants.V1_LeftEndpoint, responseLeft, ServiceConstants.V1_RightEndpoint, responseRight));

            var resultResponse = await service.GetAsync<ResultContainer>(ServiceConstants.V1_ResultEndpoint);

            WriteLine("=====================================================================================");
            switch (resultResponse.Status)
            {
                case Status.AreEqual:
                    WriteLine("Diff returned an exact match");
                    break;
                case Status.NotSameSize:
                    WriteLine("Provided data is not of equal size");
                    break;
                case Status.SameSizeNotEqual:
                    {
                        if (resultResponse?.Results?.Count > 0)
                        {
                            foreach (var item in resultResponse.Results)
                            {
                                WriteLine(string.Format("Index: {0} - Left: {1} - Right: {2}", item.Item1, item.Item2, item.Item3));
                            }
                        }
                    }
                    break;
            }
        }
    }
}
