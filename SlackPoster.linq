<Query Kind="Program">
  <NuGetReference>HtmlAgilityPack</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Specialized</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Text</Namespace>
</Query>

public void Main()
{
	new SlackClient("placeholder").PostMessage(text: "beep boop", username: "Robot", channel: "placeholder" );
}

// Define other methods and classes here

public class SlackClient
{
	private readonly Uri _uri;
	private readonly Encoding _encoding = new UTF8Encoding();

	public SlackClient(string urlWithAccessToken)
	{
		_uri = new Uri(urlWithAccessToken);
	}

	//Post a message using simple strings
	public void PostMessage(string text, string username = null, string channel = null)
	{
		Payload payload = new Payload()
		{
			Channel = channel,
			Username = username,
			Text = text
		};
		PostMessage(payload);
	}

	//Post a message using a Payload object
	public void PostMessage(Payload payload)
	{
		string payloadJson = JsonConvert.SerializeObject(payload);

		using (WebClient client = new WebClient())
		{
			NameValueCollection data = new NameValueCollection();
			data["payload"] = payloadJson;
			var response = client.UploadValues(_uri, "POST", data);
			string responseText = _encoding.GetString(response);//only for debug for now, perhaps this feature could be expanded :)
		}
	}

	public class Payload
	{
		[JsonProperty("channel")]
		public string Channel { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }
	}
}

