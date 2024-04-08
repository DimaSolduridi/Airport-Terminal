using System.Net.Http;
using System.Text;
using t = System.Timers;

public class Program
{
    static Random rnd = new Random();
    static HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7151") };
    static t.Timer timer = new t.Timer(5000);

    static void Main(string[] args)
    {
        timer.Elapsed += (s, e) => Timer_Elapsed();
        timer.Start();

        Console.ReadLine();
    }

    private static async void Timer_Elapsed()
    {
        timer.Interval = rnd.Next(5000, 15000);
        string planeNumber = GenerateRandomFlightNumber();
        string passengerCount = GenerateRandomPassengersNumber();

        string jsonPayload = @"
                    {
                      ""number"": """ + planeNumber + @""",
                      ""flightStatus"": 1,
                      ""passengersCount"": """ + passengerCount + @""",
                      ""currentLeg"": {
                        ""id"": 0,
                        ""number"": 0,
                        ""crossingTime"": 0,
                        ""legStatus"": 0,
                        ""legStage"": 0,
                        ""isOccupied"": true
                      } 
                    }";
        StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        await client.PostAsync("Flights", content);

    }

    #region RandomGenerators
    public static string GenerateRandomFlightNumber()
    {

        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Random random = new Random();
        string randomLetters = new string(Enumerable.Repeat(letters, 4)
            .Select(s => s[random.Next(s.Length)]).ToArray());


        string digits = "0123456789";
        string randomDigits = new string(Enumerable.Repeat(digits, 3)
            .Select(s => s[random.Next(s.Length)]).ToArray());


        string randomFlightNumber = randomLetters + randomDigits;
        return randomFlightNumber;
    }



    public static string GenerateRandomPassengersNumber()
    {
        int minPassengers = 0;
        int maxPassengers = 300;
        int randomPassengers = rnd.Next(minPassengers, maxPassengers + 1);
        return randomPassengers.ToString();
    }
    #endregion
}
