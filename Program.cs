using System.Net;
using System.Text.Json.Nodes;

Console.Clear();
DrawPrice();

void DrawPrice()
{
    List<double> listPriceUsd = new List<double>();

    // Variables for storing cursor positioning.
    var leftCursor = 0;
    var topCursor = 10;
    Console.CursorVisible = false;

    // Variables for storing price data.
    var lastPrice = 0.0;
    var nowPrice = 0.0;
    var lablePrice = "Unknown";

    // Delay (ms) before new request.
    var timeout = 1500;
    
    const string urlPrice = "https://data.messari.io/api/v1/assets/btc/metrics";
    
    for ( int i = 0 ;; i++ )
    {
        // Getting the price for the currency.
        try
        {
            var requestPrice = WebRequest.Create(urlPrice);
            var responsePrice = requestPrice.GetResponse();
            using (Stream stream = responsePrice.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var priceJson = JsonObject.Parse(reader.ReadToEnd());
                    listPriceUsd.Add((double)priceJson["data"]["market_data"]["price_usd"]);
                    lablePrice = priceJson["data"]["symbol"].ToString();
                }
            }
            responsePrice.Close();
        
        }
        catch (System.Exception)
        {
            continue;
            throw;
        }
        

        // Get the new and last known price for the currency.
        nowPrice = listPriceUsd[listPriceUsd.Count-1];
        lastPrice = i == 0 ? listPriceUsd[0] : listPriceUsd[i-1];

        // Drawing a window.
        DrawFrameW(100, 0, 0);
        DrawHeadline(nowPrice, lastPrice, lablePrice);
        DrawFrameW(100, 0, 20);
        DrawFrameH(21, 100, 0);

        // If the graph exceeds the margins, then clear.
        if (topCursor == 1 || leftCursor == 99 || topCursor == 19)
        {
            Console.Clear();
            topCursor = 10;
            leftCursor = 0;
            continue;
        }
        if(nowPrice >= lastPrice) 
        { 
            topCursor -= 1;
        }
        else if(nowPrice <= lastPrice)
        { 
            topCursor += 1;
        }

        Console.SetCursorPosition(leftCursor, topCursor); 
        Console.WriteLine("░");
        leftCursor++;

        Thread.Sleep(timeout);
    }
}

// Drawing a frame to the width.
void DrawFrameW(int widthFrame, int leftCursor, int topCursor) 
{
    Console.SetCursorPosition(leftCursor, topCursor);
    for (int i = 0; i < widthFrame; i++)
    {
        Console.Write("—");
    }
}

// Drawing a frame to the height.
void DrawFrameH(int heightFrame, int leftCursor, int topCursor)
{
    for (int i = 0; i < heightFrame; i++)
    {
        Console.SetCursorPosition(leftCursor, topCursor);
        Console.WriteLine("|");
        topCursor++;
    }
}

// Draw a headline.
void DrawHeadline(double nowPrice, double lastPrice, string lable)
{
    Console.SetCursorPosition(0,0);
    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.Red;
    Console.Write($" {lable} ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.Green;
    Console.Write($" {(int)nowPrice}$ ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.Yellow;
    Console.Write($" {(int)lastPrice}$ ");
    Console.ForegroundColor = ConsoleColor.Black;
    Console.BackgroundColor = ConsoleColor.White;
    Console.Write($" {(int)nowPrice-(int)lastPrice}$ "); // <-- Difference
    Console.ResetColor();
}