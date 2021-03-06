﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GreenPro.PayPalSystem
{
    public static class Configuration
    {
        public static Dictionary<string, string> GetAcctAndConfig()
        {
            Dictionary<string, string> configMap = new Dictionary<string, string>();

            configMap = GetConfig();


            // sand box keys
            configMap.Add("account1.apiUsername", Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["apiUsername"]));
            configMap.Add("account1.apiPassword", Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["apiPassword"]));
            configMap.Add("account1.apiSignature", Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["apiSignature"]));



            // live keys
            //configMap.Add("account1.apiUsername", "iaya_api1.gprowash.com");
            //configMap.Add("account1.apiPassword", "PCAPTWDW7VJFT847");
            //configMap.Add("account1.apiSignature", "AwpNBt8VI5CEx3Oo5VDAsqCqNr20AxIpZFyJRR4U.D2ZBNO5Mi2m-RYR");





            // Optional
            //configMap.Add("account1.subject", "testSubject");

            // Sample Certificate Credential
            // configMap.Add("account2.apiUsername", "certuser_biz_api1.paypal.com");
            // configMap.Add("account2.apiPassword", "D6JNKKULHN3G5B8A");
            // configMap.Add("account2.apiCertificate", "resource/sdk-cert.p12");
            // configMap.Add("account2.privateKeyPassword", "password");

            // Optional
            // configMap.Add("account2.subject", "");

            return configMap;
        }

        // Creates a configuration map containing mode and other required configuration parameters
        public static Dictionary<string, string> GetConfig()
        {
            Dictionary<string, string> configMap = new Dictionary<string, string>();

            // Endpoints are varied depending on whether sandbox OR live is chosen for mode
            configMap.Add("mode", Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["mode"]));
            
            //configMap.Add("mode", "live");

            // These values are defaulted in SDK. If you want to override default values, uncomment it and add your value.
            // configMap.Add("connectionTimeout", "5000");
            // configMap.Add("requestRetries", "2");

            return configMap;
        }
    }
}
