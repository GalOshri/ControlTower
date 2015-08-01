using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace ControlTower.BusinessLogic
{
	public class Translator
	{
		// Data contract for the access token
		[DataContract]
		public class AdmAccessToken
	    {
	        [DataMember]
			public string access_token { get; set; }
	        [DataMember]
			public string token_type { get; set; }
	        [DataMember]
			public string expires_in { get; set; }
	        [DataMember]
			public string scope { get; set; }
	    }
		
		
		
		public static string Translate(string text)
		{
			AdmAccessToken token = GetAccessToken();
			
			string[] textSplit = text.Split('-');
			
			if (textSplit.Length < 2)
			{
				return "Make sure the command is split by '-' from the text to translate";
			}
			
			Dictionary<string, string> languageDictionary = new Dictionary<string, string>()
			{
			    {"arabic", "ar"},
				{"chinese", "zh-CHS"},
				{"english", "en"},
				{"french", "fr"},
				{"german", "de"},
				{"greek", "el"},
				{"hebrew", "he"},
				{"hindi", "hi"},
				{"italian", "it"},
				{"japanese", "ja"},
				{"klingon", "tlh"},
				{"korean", "ko"},
				{"polish", "pl"},
				{"romanian", "ro"},
				{"russian", "ru"},
				{"spanish", "es"}
			};
			
			string fromLanguage = null;
			string toLanguage = null;
			
			foreach (string word in textSplit[0].Split(' ')) 
			{
				string wordLower = word.ToLower();
				if (languageDictionary.ContainsKey(wordLower))
				{
					if (fromLanguage == null)
					{
						fromLanguage = languageDictionary[wordLower];
					}
					else
					{
						toLanguage = languageDictionary[wordLower];
					}
				}
			}
			
			if (fromLanguage == null)
			{
				return "You need to specify a language to translate from";
			}
			if (toLanguage == null)
			{
				return "You need to specify a language to translate to";
			}
			
			string textToTranslate = textSplit[1];
			
			string translation = GetTranslation(textToTranslate, fromLanguage, toLanguage, token);
			
			return translation;
			   
		}
		
		private static AdmAccessToken GetAccessToken()
		{
			string clientId = "TAAS";
			string clientSecret= "AVLEBefYBBFFf0ry6Pdbk2SItrnolWpHkviKxSZKH10%3D";
			
			string uri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
			string body = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", clientId, clientSecret);
			byte[] bodyBytes = Encoding.ASCII.GetBytes(body);
			string contentType = "application/x-www-form-urlencoded";
			
			WebRequest request = WebRequest.Create(uri);
			request.Method = "POST";
			request.ContentType = contentType;
			request.ContentLength = bodyBytes.Length;
			
			Stream dataStream = request.GetRequestStream();
			dataStream.Write (bodyBytes, 0, bodyBytes.Length);
			dataStream.Close();
			
			using (WebResponse webResponse = request.GetResponse())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
				//Get deserialized object from JSON stream
                AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
				
				return token;
			}
		}
		
		private static string GetTranslation(string text, string fromLanguage, string toLanguage, AdmAccessToken token)
		{
			// TRANSLATE
			string headerValue = "Bearer " + token.access_token;
			string textEncoded = WebUtility.UrlEncode(text);
			string uri = string.Format("http://api.microsofttranslator.com/v2/Http.svc/Translate?text={0}&from={1}&to={2}", text, fromLanguage, toLanguage);
			
			WebRequest translateRequest = WebRequest.Create(uri);
			translateRequest.Headers.Add("Authorization", headerValue);

			WebResponse response = translateRequest.GetResponse();

			using (Stream stream = response.GetResponseStream())
			{
				System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
		    	string translation = (string)dcs.ReadObject(stream);
				return translation;
				 
			}
		}
		
		
		
		
		
	}
}