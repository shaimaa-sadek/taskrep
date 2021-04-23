using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace Apis_Automating
{

    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void GetAllProductsSuccess()
        {
            RestClient client = new RestClient("http://localhost:3030/products");
            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            //Verify the response returned is same to the OK status code which is 200
            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [TestMethod]
        public void SearchWithNotFoundProduct()
        {
            RestClient client = new RestClient("http://localhost:3030/products/{id}");
            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            //Verify the response returned is same to the NOT FOUND status code which is 404
            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [TestMethod]
        public void GetProductWithID()
        {
            //Creating Request from the client and specifying the http request with the key passed id
            var client = new RestClient();
            string id = "43900";
            string baseURL = "http://localhost:3030/products";
            //key passed s a string 
            string apiURL = baseURL + "/" + id;
            client = new RestClient(apiURL);
            RestRequest request = new RestRequest(Method.GET);
            

            //=Replaces the id placeholder"Key" with "value" set in the below func
           // request.AddParameter("id", "43900");

            IRestResponse response = client.Execute(request);
            //verifying the response returned id is equal to the id searched with

            //translating the json response into object 
            var jObject = JObject.Parse(response.Content);

            //Extracting Node element using Getvalue method
            string idRecievedFromJSonResponse = jObject.GetValue("id").ToString();

            //  print the id variable to see what we got from there

            // Validate the response
           NUnit.Framework.Assert.AreEqual("43900", idRecievedFromJSonResponse,"correct");



        }

        [TestMethod]
        public void CreatePOSTNewProductwithalldata()
        {
            RestClient client = new RestClient("http://localhost:3030/products");

            //defining the json object to add as parameters in the request
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Newprod");
            jObjectbody.Add("Price", "5550");
            jObjectbody.Add("shipping", "usa");
            jObjectbody.Add("upc", "upcsadd");
            jObjectbody.Add("description", "describingtext");
            jObjectbody.Add("manafacturer", "describingftext");
            jObjectbody.Add("model", "model200");
            jObjectbody.Add("url", "dproduct122");
            jObjectbody.Add("image", "describinfkk");

            //add the json body and send the request
            RestRequest restRequest = new RestRequest( Method.POST);

            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
           

            //Validating the status code and the response body
            IRestResponse restResponse = client.Execute(restRequest);

          //  NUnit.Framework.Assert.Contains("Newprod", );
        }
    }
}
