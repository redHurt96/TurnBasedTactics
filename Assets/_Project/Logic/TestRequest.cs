using System.Collections;
using System.Text;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;

namespace _Project.Logic
{
    public class TestRequest : MonoBehaviour
    {
/*
        [System.Serializable]
        public class ChatMessage
        {
            public string role;
            public string content;
        }

        [System.Serializable]
        public class ChatRequest
        {
            public string model = "gpt-3.5-turbo";
            public ChatMessage[] messages;
        }

        [System.Serializable]
        public class ChatResponse
        {
            public Choice[] choices;

            [System.Serializable]
            public class Choice
            {
                public ChatMessage message;
            }
        }

        [Button]
        public void SendMessageToGPT(string message)
        {
            StartCoroutine(SendChatRequestCoroutine(message));
        }

        [Button]
        public void SendMessageToDeepSeek(string message)
        {
            StartCoroutine(SendDeepSeekRequest(message));
        }

        private IEnumerator SendChatRequestCoroutine(string userMessage)
        {
            var requestData = new ChatRequest
            {
                messages = new ChatMessage[]
                {
                    new ChatMessage { role = "user", content = userMessage }
                }
            };

            string json = JsonConvert.SerializeObject(requestData);
            byte[] postData = Encoding.UTF8.GetBytes(json);

            using (UnityWebRequest request = new UnityWebRequest("https://api.openai.com/v1/chat/completions", "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(postData);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", $"Bearer {KEY}");

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("OpenAI Error: " + request.error);
                }
                else
                {
                    var responseJson = request.downloadHandler.text;
                    var response = JsonConvert.DeserializeObject<ChatResponse>(responseJson);
                    Debug.Log("GPT Response: " + response.choices[0].message.content);
                }
            }
        }
    
        private IEnumerator SendDeepSeekRequest(string userMessage)
        {
            var requestData = new ChatRequest
            {
                model = "deepseek-chat",
                messages = new[]
                {
                    new ChatMessage { role = "user", content = userMessage }
                }
            };

            string json = JsonConvert.SerializeObject(requestData);
            byte[] postData = Encoding.UTF8.GetBytes(json);

            using (UnityWebRequest request = new UnityWebRequest("https://api.deepseek.com/v1/chat/completions", "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(postData);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", $"Bearer {DEEPSEEK_KEY}");

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("DeepSeek Error: " + request.error);
                }
                else
                {
                    var responseJson = request.downloadHandler.text;
                    var response = JsonConvert.DeserializeObject<ChatResponse>(responseJson);
                    Debug.Log("DeepSeek Response: " + response.choices[0].message.content);
                }
            }
        }*/
    }
}