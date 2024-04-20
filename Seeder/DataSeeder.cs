using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;

namespace api.Seeder
{
    public class DataSeeder
    {
        private readonly TestDBContext _context;

    public DataSeeder(TestDBContext context)
    {
        _context = context;
    }

    public void SeedRecordsFromCsv(string csvFilePath)
    {
        try
        {
            if (!_context.Records.Any())
            {
                string[] lines = File.ReadAllLines(csvFilePath);
                int batchSize = 5000; // Define the batch size

                for (int i = 1; i < lines.Length; i += batchSize)
                {
                    var batchLines = lines.Skip(i).Take(batchSize); // Get the next batch of lines
                    
                    var batchRecords = batchLines.Select(line =>
                    {
                        string[] values = line.Split(',');
                        return new Record
                        {
                            Caller_id = !string.IsNullOrEmpty(values[0]) ? long.Parse(values[0]) : (long?)null,
                            Recipient = !string.IsNullOrEmpty(values[1]) ? long.Parse(values[1]) : (long?)null,
                            Call_date = !string.IsNullOrEmpty(values[2]) ? DateOnly.ParseExact(values[2], "dd/MM/yyyy", null) : (DateOnly?)null,
                            End_time = !string.IsNullOrEmpty(values[3]) ? TimeSpan.Parse(values[3]) : (TimeSpan?)null,
                            Duration = !string.IsNullOrEmpty(values[4]) ? int.Parse(values[4]) : (int?)null,
                            Cost = !string.IsNullOrEmpty(values[5]) ? decimal.Parse(values[5]) : (decimal?)null,
                            Reference = values[6],
                            Currency = values[7]
                        };
                    }).ToList();

                    _context.Records.AddRange(batchRecords);
                    _context.SaveChanges(); // Save changes after processing each batch
                }
                Console.WriteLine("Records seeded successfully.");
            }
            else
            {
                Console.WriteLine("Records already seeded.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while seeding records: {ex.Message}");
        }
    }
    }
}