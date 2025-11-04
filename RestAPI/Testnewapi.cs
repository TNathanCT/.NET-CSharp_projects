using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

public class DataOfUsers{
    public List<string> user = new List<string>();
    public List<string> post = new List<string>();
    public string todo;
    public int durationMs;

    public DataOfUsers(DataOfUsersJSON data){
        user = data.user;
        post = data.post;
        todo = data.todo;
        durationMs = data.durationMS;
    }
}

    public struct DataOfUsersJSON{
        public List<string> user = new List<string>();
        public List<string> post = new List<string>();
        public string todo;
        public int durationMs;

    }


public class ConsoleApp{
        
        public DataOfUsers UserData = new DataOfUsers();
        public Dictionary<string, DataOfUsers> dictionaryofUsers = new Dictionary<string, DataOfUsers>();
    

        public int Main(){}



        //functionName is the name of the function we wish to call after the code block is done running
        public void UserLoginData(DataOfUsers dataJSON, string functionName){
            string JsonStr = JsonUtility.ToJson(dataJSON);
            string requestString = "https://jsonplaceholder.typicode.com/posts/1";
            StartCoroutine(SendAPIRequest<DataOfUsers>(functionName, RequestType.GET, requestString, JsonStr));

        }

        public IEnumerator SendAPIRequest<T>(GameObject obj, string returnFunction, RequestType reqType, string reqURL, string payloadString = "",int timeout = -1) where T : {
            //First we need to check if we have internet
            if(CheckInternetConnection() == false){
                yield break;

            }
            object resultofRecovery = null;
             string jsonRes = req.downloadHandler.text;

            UnityWebRequest req = RequestType.GET:
            newRequest = UnityWebRequest.Get(reqURL);
            UnityWebRequest req = BuildBasicWebRequest(newRequest);
            bool succescheck = false;
            
            string status = targetRequest.responseCode.ToString();
            string jsonRes = req.downloadHandler.text;

            if (req.uploadHandler != null && rea.uploadHandler.data != null){
                string payload = System.Text.Encoding.UTF8.GetString(targetRequest.uploadHandler.data);
            }

            if(!System.String.IsNullOrWhiteSpace(targetRequest.error)){
                succescheck = false;
                Debug.Log("Error");
            }else{
                succescheck = true;
            }

            if(succescheck){
                result = JsonConvert.DeserializeObject<T>(jsonRes,jsonSerializerConfig);
            }

            //grab a gameObject and use SendMessage() to trigger a function
            SetMessage(obj, returnFunction, result);
        }

        public void SetMessage(GameObject targetObject, string targetFunction, object resultValue){

            if (resultValue is responseFailJSON){ //IF RESPONSE FAILED, CHECK IF DUE TO UNAUTHORISED TOKEN
                allowSend = CheckFailedResponse((responseFailJSON)resultValue);
            }       
            
            if (allowSend){ //FORCE IMMEDIATE SENDING OF RESULT MESSAGE
                if(targetObject != null && targetObject.activeInHierarchy){
                    targetObject.SendMessage(targetFunction,resultValue);
                }

            }
        }




        public bool CheckInternetConnection(int timeoutMs = 1000, string url = null){
            try { 
                url = "www.google.com";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = true;
                request.Timeout = timeoutMs;
                using(var response = (HttpWebResponse)request.GetResponse()){
                    return true;
                }
            }
            catch{
                return false;
            }
        }


        public UnityWebRequest BuildCustomWebRequest(string reqURL){
            newRequest = UnityWebRequest.Get(reqURL);
            newRequest.downloadHandler = new DownloadHandlerBuffer();
            
            if (reqTimeout > 0){
                newRequest.timeout = reqTimeout;
            }else{
                newRequest.timeout = 15;
            }
            return newRequest;

        }

    
}
