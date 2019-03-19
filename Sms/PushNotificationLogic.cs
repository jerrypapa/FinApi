﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sms
{
    public static class PushNotificationLogic
    {
        private static Uri FireBasePushNotificationsURL = new Uri("https://fcm.googleapis.com/fcm/send");
        //private static string ServerKey = "AAAAvgqlqyM:APA91bGrOCHA2DYz8lq4iV7OhGAdV9qdLPril_PzMt9FAfI-ylb0oVtlG_Goa-vaElvRN8dK4Ux-PWo03OrXMKFtwcgdO3twYFFX_vz_jK6Lth3v-Cb_HaW3W6Ci_tUBL9JvqgMxYF5b";
        private static string ServerKey = "AAAA8gwdQXA:APA91bFHwdEHFAwym-7fFz77mIdSacRavrwUn2nRp3UY0minoXrb1dNl70a55DGZuC_4Rd8-4qANF8XivmZFD4-SF6P9maWO-Q5uRE_GCdSAJIkWE2LzYdrHwDUM0_IuD06BBo3kD8Su";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public static async Task<bool> SendPushNotification(string[] deviceTokens, string title, string body, object data)
        {
            bool sent = false;

            if (deviceTokens.Count() > 0)
            {
                //Object creation

                var messageInformation = new Message()
                {
                    notification = new Notification()
                    {
                        title = title,
                        text = body
                    },
                    data = data,
                    registration_ids = deviceTokens
                };

                //Object to JSON STRUCTURE => using Newtonsoft.Json;
                string jsonMessage = JsonConvert.SerializeObject(messageInformation);

                /*
                 ------ JSON STRUCTURE ------
                 {
                    notification: {
                                    title: "",
                                    text: ""
                                    },
                    data: {
                            action: "Play",
                            playerId: 5
                            },
                    registration_ids = ["id1", "id2"]
                 }
                 ------ JSON STRUCTURE ------
                 */

                //Create request to Firebase API
                var request = new HttpRequestMessage(HttpMethod.Post, FireBasePushNotificationsURL);

                request.Headers.TryAddWithoutValidation("Authorization", "key=" + ServerKey);
                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = await client.SendAsync(request);
                    sent = sent && result.IsSuccessStatusCode;
                }
            }

            return sent;
        }
    }
}
