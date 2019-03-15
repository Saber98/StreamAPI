using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessEntities.Lookups;
using Newtonsoft.Json.Linq;

namespace JSONGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            AddressTypeModel newAddressTypeModel = new AddressTypeModel();
            PhoneTypeModel newPhoneTypeModel = new PhoneTypeModel();
            StateModel newStateModel = new StateModel();
            AddressModel newAddressModel = new AddressModel();
            PhoneModel newPhoneModel = new PhoneModel();
            ContactModel newContactModel = new ContactModel();
            CustomerModel newCustomerModel = new CustomerModel();

            JObject o = (JObject)JToken.FromObject(newAddressTypeModel);
            Console.WriteLine("AddressType Model");
            Console.WriteLine("************************************");
            Console.Write(o.ToString());

            o = (JObject)JToken.FromObject(newPhoneTypeModel);
            Console.WriteLine();
            Console.WriteLine("PhoneType Model");
            Console.WriteLine("************************************");
            Console.Write(o.ToString());

            o = (JObject)JToken.FromObject(newStateModel);
            Console.WriteLine();
            Console.WriteLine("State Model");
            Console.WriteLine("************************************");
            Console.Write(o.ToString());

            o = (JObject)JToken.FromObject(newAddressModel);
            Console.WriteLine();
            Console.WriteLine("Address Model");
            Console.WriteLine("************************************");
            Console.Write(o.ToString());

            o = (JObject)JToken.FromObject(newPhoneModel);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Phone Model");
            Console.WriteLine("************************************");
            Console.Write(o.ToString());

            o = (JObject)JToken.FromObject(newContactModel);
            Console.WriteLine();
            Console.WriteLine("Contact Model");
            Console.WriteLine("************************************");
            Console.Write(o.ToString());

            o = (JObject) JToken.FromObject(newCustomerModel);
            Console.WriteLine();
            Console.WriteLine("Customer Model");
            Console.WriteLine("************************************");
            Console.Write(o.ToString());

            Console.ReadKey();

        }
    }
}
