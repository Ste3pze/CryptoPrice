# 📊 CryptoPrice

➖**CryptoPrice** is an application written in C# programming language. <br> Its main functionality is the display of the cryptocurrency price chart in the terminal.

# 🎬 Preview of work
![mh9tu8Yxo0](https://user-images.githubusercontent.com/105447428/189231425-b73ff21d-a01c-4b66-8e0d-c01b88b15318.gif)

# 🤔 How does it work? 
The work of this application is quite trivial.
First we send an API request to get the json data from the server. <br>
Then we pull the strings we need from the response, and based on them we build a graph,<br> 
based on whether the exchange rate went up or down, the graph will display these price jumps. 

# 🚀 Build & Run 
Before you can run an application, you have to build it with a console command:<br>
`$ dotnet build`<br>
<br>
If there are no errors after assembly (ignore the warnings), then we can run it<br>
`$ dotnet run`

# 🗺 RoadMap 
Task  | Status
------------- | -------------
Remove headline flicker  | ❌
Add multithreading  | ❌
Add currency selection option  | ❌
Get rid of server error  | ❌
