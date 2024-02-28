using System.Diagnostics;

Console.Write("Path to file: ");
String path = Console.ReadLine();

var sw = Stopwatch.StartNew();

SequenceProperties properties = new SequenceProperties(path);

Console.WriteLine($"Max: {properties.Max}");
Console.WriteLine($"Min: {properties.Min}");
Console.WriteLine($"Median: {properties.Median}");
Console.WriteLine($"Avg: {properties.Avg}");
Console.WriteLine($"AscendingSequence: {string.Join(", ", properties.AscendingSequence)}");
Console.WriteLine($"DescendingSequence: {string.Join(", ", properties.DescendingSequence)}");

sw.Stop();
Console.WriteLine($"Time consumed: {sw.ElapsedMilliseconds}ms");

class SequenceProperties
{
    public int Max { get => sortedSequence[sortedSequence.Count - 1]; }

    public int Min { get => sortedSequence[0]; }

    public decimal Median { get => (sortedSequence[(int)Math.Floor((sortedSequence.Count - 1) / 2f)] + sortedSequence[(int)Math.Ceiling((sortedSequence.Count - 1) / 2f)]) / 2m; }

    public decimal Avg { get => sum / sortedSequence.Count; }

    public List<int> AscendingSequence { get => ascendingSequence; }

    public List<int> DescendingSequence { get => descendingSequence; }
    
    decimal sum;
    List<int> sortedSequence;
    List<int> descendingSequence;
    List<int> ascendingSequence;

    public SequenceProperties(string path)
    {
        List<int> sequence = new List<int>();
        sum = 0;

        descendingSequence = new List<int>();
        ascendingSequence = new List<int>();
        List<int> sequentialSequence = new List<int>();
        bool ascending = false;

        string[] lines = File.ReadAllLines(path);

        for (int l = 0; l < lines.Length; l++)
        {
            int number = Int32.Parse(lines[l]);
            sum += number;   
            sequence.Add(number);

            // If sequence type determined
            if (sequentialSequence.Count > 1)
            {
                int last = sequentialSequence[sequentialSequence.Count - 1];
                // If the sequence stops ascending/decending
                if (last == number || (last < number) != ascending)
                {
                    // Save a sequence if longer then a saved
                    if (ascending && sequentialSequence.Count > ascendingSequence.Count)
                    {
                        ascendingSequence = sequentialSequence; 
                    }
                    else if (!ascending && sequentialSequence.Count > descendingSequence.Count)
                    {
                        descendingSequence = sequentialSequence;
                    }

                    // Start new sequence
                    sequentialSequence = new List<int>() {last};
                }
            }
            // Determine the type of sequence
            if (sequentialSequence.Count == 1)
            {
                if (sequentialSequence[0] == number)
                {
                    sequentialSequence.Remove(0);
                }
                else
                {
                    ascending = sequentialSequence[0] < number;
                }
            }
            sequentialSequence.Add(number);
        }

        // Save a sequence if longer then a saved
        if (ascending && sequentialSequence.Count > ascendingSequence.Count)
        {
            ascendingSequence = sequentialSequence;
        }
        else if (!ascending && sequentialSequence.Count > descendingSequence.Count)
        {
            descendingSequence = sequentialSequence;
        }

        sequence.Sort();
        sortedSequence = sequence;
    }

}
