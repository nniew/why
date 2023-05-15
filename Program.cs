using System;

class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int InfectionLevel { get; set; }
    public int ConnectedCityId { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("กรุณากรอกจำนวนเมืองที่แสดงในแบบจำลอง: ");
        int cityCount = ReadPositiveInteger();

        City[] cities = new City[cityCount];
        for (int i = 0; i < cityCount; i++)
        {
            City city = new City();
            city.Id = i;
            Console.Write($"เมืองที่ {i} ชื่อ: ");
            city.Name = Console.ReadLine();
            city.InfectionLevel = 0;

            if (i > 0)
            {
                Console.Write($"จำนวนเมืองที่ติดต่อกับเมือง {i}: ");
                int connectedCityId = ReadNonNegativeNumber();

                while (connectedCityId == i || connectedCityId > i)
                {
                    Console.WriteLine("Invalid ID");
                    Console.Write($"จำนวนเมืองที่ติดต่อกับเมือง {i}: ");
                    connectedCityId = ReadNonNegativeNumber();
                }

                city.ConnectedCityId = connectedCityId;
            }
            else
            {
                city.ConnectedCityId = -1;
            }

            cities[i] = city;
        }

        Console.WriteLine();
        Console.WriteLine("รับเหตุการณ์ที่เกิดขึ้นระหว่างการระบาดของโรค COVID-19");

        while (true)
        {
            Console.Write("เหตุการณ์: ");
            string eventText = Console.ReadLine();

            if (eventText == "Outbreak" || eventText == "Vaccinate" || eventText == "Lockdown")
            {
                Console.Write("หมายเลขประจำเมืองที่เกิดเหตุการณ์: ");
                int eventCityId = ReadNonNegativeNumber();

                if (eventCityId >= cityCount || eventCityId < 0)
                {
                    Console.WriteLine("Invalid");
                    continue;
                }

                if (eventText == "Outbreak")
                {
                    if (cities[eventCityId].InfectionLevel < 3)
                    {
                        cities[eventCityId].InfectionLevel += 2;
                    }
                    else
                    {
                        cities[eventCityId].InfectionLevel = 3;
                    }

                    if (cities[eventCityId].ConnectedCityId >= 0 && cities[eventCityId].InfectionLevel < 3)
                    {
                        if (cities[cities[eventCityId].ConnectedCityId].InfectionLevel < 3)
                        {
                            cities[cities[eventCityId].ConnectedCityId].InfectionLevel += 1;
                        }
                        else
                        {
                            cities[cities[eventCityId].ConnectedCityId].InfectionLevel = 3;
                        }
                    }
                }
                else if (eventText == "Vaccinate");