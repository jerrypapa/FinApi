using AfricasTalkingCS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Sms
{
    class Program
    {
        private static string apikey = "0ed3cacf4b19036b3139bf7f270b200e01d926e3bf04727c3d77315359051321";
        private static string username = "sandbox";
        private static AfricasTalkingGateway _atGWInstance = new AfricasTalkingGateway(username, apikey);

        static void Main(string[] args)
        {
            /*var phoneNumber = "+254704514301";
            var message = "Hello Derick";
            var gatewayResponse = _atGWInstance.SendMessage(phoneNumber, message);
            var success = gatewayResponse["SMSMessageData"]["Recipients"][0]["status"] == "Success";
            Console.WriteLine(success);

            phoneNumber = "+254704514301";
            var menu = "CON What is your purpose?\n";
            var checkoutToken = _atGWInstance.CreateCheckoutToken(phoneNumber);
            string tkn = checkoutToken["token"];
            gatewayResponse = _atGWInstance.InitiateUssdPushRequest(phoneNumber, menu, tkn);
            success = gatewayResponse["status"] == "Queued" && gatewayResponse["errorMessage"] == "None";

            Console.WriteLine(success);

            //// Specify your login credentials
            string username = "sandbox";
            string apiKey = "0ed3cacf4b19036b3139bf7f270b200e01d926e3bf04727c3d77315359051321";

            // Specify the numbers that you want to send to in a comma-separated list
            // Please ensure you include the country code (+254 for Kenya in this case)
            string recipients = "+254715812661,+254704514301";

            // And of course we want our recipients to know what we really do
            message = "I'm a lumberjack and its ok, I sleep all night and I work all day";

            // Create a new instance of our awesome gateway class
            AfricasTalkingGateway gateway = new AfricasTalkingGateway(username, apiKey);

            // Any gateway errors will be captured by our custom Exception class below,
            // so wrap the call in a try-catch block   
            try
            {

                // Thats it, hit send and we'll take care of the rest

                dynamic results = gateway.SendMessage(recipients, message);
                Console.WriteLine(results.ToString());
                foreach (dynamic result in results)
                {
                    Console.Write((string)result["number"] + ",");
                    Console.Write((string)result["status"] + ","); // status is either "Success" or "error message"
                    Console.Write((string)result["statusCode"] + ",");
                    Console.Write((string)result["messageId"] + ",");
                    Console.WriteLine((string)result["cost"]);
                }
            }
            catch (AfricasTalkingGatewayException e)
            {

                Console.WriteLine("Encountered an error: " + e.Message);

            }*/

            string title = "";
            string body = "";
            var data = new { action = "Play", userId = 5 };

            Console.WriteLine("Hello Everyone!");
            Console.WriteLine("Let's send push notifications!!!");

            Console.Write("Title of Notification: ");
            title = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("Ok, so now I have the title, I'll need a description");
            body = Console.ReadLine();

            Console.WriteLine();

            Console.Write("How many devices are going to receive this notification? ");
            int devicesCount = Int32.Parse(Console.ReadLine());
            //int.TryParse(Console.ReadLine(), out int devicesCount);
            var tokens = new string[devicesCount];

            Console.WriteLine();

            for (int i = 0; i < devicesCount; i++)
            {
                Console.Write($"Token for device number {i + 1}: ");
                tokens[i] = Console.ReadLine();
                Console.WriteLine();
            }

            Console.WriteLine("Do you want to send notifications?");
            Console.WriteLine("1 - Yes!!!!");
            Console.WriteLine("0 - No, I'm wasting my time!!!");
            int sendNotification = Int32.Parse(Console.ReadLine());
            //int.TryParse(Console.ReadLine(), out int sendNotification);
            if (sendNotification == 1)
            {
                var pushSent = PushNotificationLogic.SendPushNotification(tokens, title, body, data);
                Console.WriteLine($"Notification sent");
            }
            else
            {
                Console.WriteLine("BUUUUUU");
            }
            Console.ReadKey();
        }
    }
}
