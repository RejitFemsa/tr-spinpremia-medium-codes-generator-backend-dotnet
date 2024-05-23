using System.Collections.Concurrent;
namespace CardsGeneratorBackend.Services
{
    public class CardGeneratorC
    {
        private readonly int _cardLength = 13;
        private static readonly ThreadLocal<Random> RandomProvider = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));
        public HashSet<string> Generate(int count, string prefix, bool log = false)
        {
            int min = 72000001;
            int max = 999999999;

            var generatedNumbers = new ConcurrentBag<string>();
            int numThreads = Environment.ProcessorCount;
            int totalRange = max - min + 1;
            int rangePerThread = totalRange / numThreads;
            int remainingRange = totalRange % numThreads;

            Parallel.For(0, numThreads, (threadIndex) =>
            {
                var random = RandomProvider.Value;
                var localSet = new HashSet<string>();
                // Calculating the range for each thread
                int threadStart = min + threadIndex * rangePerThread;
                int threadEnd = threadIndex == numThreads - 1 ? max + 1 : threadStart + rangePerThread;
                var numberOfMediumsInCurrentThread = (count + numThreads - 1) / numThreads;

                if (threadIndex == numThreads - 1 && remainingRange > 0)
                {
                    threadEnd += remainingRange;
                    numberOfMediumsInCurrentThread = count - ((numThreads - 1) * numberOfMediumsInCurrentThread);
                }

                while (localSet.Count != numberOfMediumsInCurrentThread)
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    int randomNumber = random.Next(threadStart, threadEnd);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    string complement = randomNumber.ToString().PadLeft(_cardLength - prefix.Length - 1, '0');
                    string cardNumber = Verifier(prefix + complement, log);

                    if (cardNumber.Length == _cardLength && !localSet.Contains(cardNumber))
                    {
                        localSet.Add(cardNumber);
                    }
                }
                foreach (var number in localSet)
                {
                    generatedNumbers.Add(number);
                }
            });

            if (log)
            {
                Console.WriteLine($"realcount: {generatedNumbers.Count}\nexpedtedCount {count}\nprefix: {prefix}\nmin: {min}\nmax: {max}");
            }

            return new HashSet<string>(generatedNumbers);
        }

        private string Verifier(string token, bool log)
        {
            int validator;
            int sum = 0;
            int factor = 2;

            for (int i = token.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(token[i]) && int.TryParse(token[i].ToString(), out int n))
                {
                    sum += n * factor;
                    if (log) Console.WriteLine($"n: {n}\nfactor: {factor}\nsum: {sum}");
                    factor = (factor == 7) ? 2 : factor + 1;
                }
            }

            validator = 11 - (sum % 11);
            if (validator == 10) validator = 0;
            return token + validator;
        }
    }
}