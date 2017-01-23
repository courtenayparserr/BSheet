﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saastrack.DAL;
using Saastrack.DAL.Operations;
using Newtonsoft.Json;

namespace Saastrack.DAL.Helper
{
    public static class DummyData
    {
        private static string ListOfSubscriptions = "[{\"payments\":[{\"Id\":834,\"TransactionId\":\"ZqB9kMvmn9iLvn660xjLCD1Nxzg3wBhjLkA3J\",\"Amount\":9.99,\"Date\":\"2016-10-03T11:00:00\"},{\"Id\":835,\"TransactionId\":\"vQJRwxoD8Ruk1300QxojS0ALozmD7juVPV9Le\",\"Amount\":9.59,\"Date\":\"2016-09-03T10:00:00\"},{\"Id\":836,\"TransactionId\":\"qVKaQ0wE8aTjdzZZQ0pBiZXEp5qQ73sD5joXe\",\"Amount\":9.59,\"Date\":\"2016-08-03T10:00:00\"},{\"Id\":837,\"TransactionId\":\"E53BVKJLmBsAYzkkxMLXsdwNQgeAb1uQqAv9Q\",\"Amount\":12.99,\"Date\":\"2016-07-03T10:00:00\"},{\"Id\":838,\"TransactionId\":\"3PjgkEObBguYA7338ZLeT5QAbmO6jxCoMvJ50\",\"Amount\":9.59,\"Date\":\"2016-06-03T10:00:00\"}],\"Id\":214,\"Name\":\"Amazon Prime\",\"IsActive\":true,\"Period\":1,\"NextBillingDate\":null,\"PreviousBillingDate\":\"2016-10-03T11:00:00\",\"DateCreated\":\"2016-06-03T10:00:00\",\"MotherSubscriptionName\":\"Amazon Prime\",\"LastPaymentAmount\":9.99,\"MotherSubscriptionLogoUrl\":\"https://www.filepicker.io/api/file/dz3AMjYRviArcezeoY5w\",\"motherSubscriptionId\":124,\"accountid\":2,\"account\":{\"Id\":2,\"_id\":\"1DkAyOzRPAhYKvjj5NP4TvE9nzEeZjFjKRPm8\",\"_item\":\"638E0nB4KEHYjpggeRE6TJv65Nv4KVFy5v8ap\",\"balance_available\":\"0\",\"balance_current\":\"1997.76\",\"institution_type\":\"wells\",\"meta_name\":\"PLATINUM CARD\",\"meta_number\":\"7154\",\"subtype\":\"credit\",\"type\":\"credit\",\"lastdateprocessed\":\"2006-09-11T00:00:00\",\"lastdateadded\":\"2016-05-20T00:00:00\",\"enabled\":true,\"Monthly\":214.59000000000003,\"MonthlyIncreaseDecrease\":-20.099999999999994,\"Annual\":951.25000000000011,\"AnnualIncreaseDecrease\":951.25000000000011,\"Subs\":10}},{\"payments\":[{\"Id\":822,\"TransactionId\":\"8KbyrX15AyfY4EDD5xRnTv3wVRN0YdSxkxO7P\",\"Amount\":5.0,\"Date\":\"2016-09-08T10:00:00\"},{\"Id\":823,\"TransactionId\":\"vQJRwxoD8Ruk1300QxojS0ZaJExp6nSVPyDR3\",\"Amount\":5.0,\"Date\":\"2016-08-08T10:00:00\"},{\"Id\":824,\"TransactionId\":\"KVJDnr930DTvLa44QVwxi3nPapbQrDCEKV5VP\",\"Amount\":5.0,\"Date\":\"2016-07-08T10:00:00\"},{\"Id\":825,\"TransactionId\":\"rOA6nj7d86sEn5KKQj37iKg934XNa0sVj97OY\",\"Amount\":5.0,\"Date\":\"2016-06-08T10:00:00\"},{\"Id\":857,\"TransactionId\":\"av8PaNb0LPcjqzYYZVbrTj55wJQeyYhb5yX4a\",\"Amount\":5.0,\"Date\":\"2016-10-08T00:00:00\"}],\"Id\":211,\"Name\":\"Backblaze\",\"IsActive\":true,\"Period\":1,\"NextBillingDate\":null,\"PreviousBillingDate\":\"2016-10-08T00:00:00\",\"DateCreated\":\"2016-06-08T10:00:00\",\"MotherSubscriptionName\":\"Backblaze\",\"LastPaymentAmount\":5.0,\"MotherSubscriptionLogoUrl\":\"https://www.filepicker.io/api/file/WbxWN30S7aFa6Ucm41w9\",\"motherSubscriptionId\":26,\"accountid\":2,\"account\":{\"Id\":2,\"_id\":\"1DkAyOzRPAhYKvjj5NP4TvE9nzEeZjFjKRPm8\",\"_item\":\"638E0nB4KEHYjpggeRE6TJv65Nv4KVFy5v8ap\",\"balance_available\":\"0\",\"balance_current\":\"1997.76\",\"institution_type\":\"wells\",\"meta_name\":\"PLATINUM CARD\",\"meta_number\":\"7154\",\"subtype\":\"credit\",\"type\":\"credit\",\"lastdateprocessed\":\"2006-09-11T00:00:00\",\"lastdateadded\":\"2016-05-20T00:00:00\",\"enabled\":true,\"Monthly\":214.59000000000003,\"MonthlyIncreaseDecrease\":-20.099999999999994,\"Annual\":951.25000000000011,\"AnnualIncreaseDecrease\":951.25000000000011,\"Subs\":10}},{\"payments\":[{\"Id\":785,\"TransactionId\":\"k5zr93wMArsk6aooq3KASdYJkZYoYZcoZPK0a\",\"Amount\":59.99,\"Date\":\"2016-03-15T11:00:00\"},{\"Id\":786,\"TransactionId\":\"ng1aMkD7jaHqNxRRyk4mTR7mKe7g7eTBXxV71\",\"Amount\":59.99,\"Date\":\"2016-02-16T11:00:00\"},{\"Id\":787,\"TransactionId\":\"KVJDnr930DTvLa44QVwxi3nLQJnwnJsEKVzLd\",\"Amount\":59.99,\"Date\":\"2016-01-13T11:00:00\"},{\"Id\":788,\"TransactionId\":\"0LyZ3VER1Zcom3YY6xReixdB0pdadpSAB7Dvq\",\"Amount\":59.99,\"Date\":\"2015-12-14T11:00:00\"},{\"Id\":789,\"TransactionId\":\"MMo5XaAKn5iJBK11wNL0fNRoqjRBRjSXgNwMz\",\"Amount\":59.99,\"Date\":\"2015-11-13T11:00:00\"},{\"Id\":818,\"TransactionId\":\"qVKaQ0wE8aTjdzZZQ0pJiZoreJLyDDHk1eXOk\",\"Amount\":39.99,\"Date\":\"2016-09-27T10:00:00\"},{\"Id\":819,\"TransactionId\":\"vQJRwxoD8Ruk1300QxojS0ALozmD7juVPV9LP\",\"Amount\":39.99,\"Date\":\"2016-08-27T10:00:00\"},{\"Id\":820,\"TransactionId\":\"zw9715oBV7HO3JLL85ZecLmYBQXerAsvkmqm5\",\"Amount\":39.99,\"Date\":\"2016-07-27T10:00:00\"},{\"Id\":821,\"TransactionId\":\"QnEOLZQ0XOubRX00mKqeTwK8Yp6xe1COoKe7q\",\"Amount\":39.99,\"Date\":\"2016-06-26T10:00:00\"}],\"Id\":204,\"Name\":\"Charter Communications\",\"IsActive\":true,\"Period\":1,\"NextBillingDate\":null,\"PreviousBillingDate\":\"2016-09-27T10:00:00\",\"DateCreated\":\"2015-11-13T11:00:00\",\"MotherSubscriptionName\":\"Charter Communications\",\"LastPaymentAmount\":59.99,\"MotherSubscriptionLogoUrl\":\"https://www.filepicker.io/api/file/DmUL8eohRXWszUUWRGja\",\"motherSubscriptionId\":191,\"accountid\":1,\"account\":{\"Id\":1,\"_id\":\"LOZ1p4myj1sqEQaaLP9bTmPgzZP5KotmwAq0D\",\"_item\":\"638E0nB4KEHYjpggeRE6TJv65Nv4KVFy5v8ap\",\"balance_available\":\"1435.41\",\"balance_current\":\"1924.1\",\"institution_type\":\"wells\",\"meta_name\":\"COMPLETE ADVANTAGERM\",\"meta_number\":\"1530\",\"subtype\":\"checking\",\"type\":\"depository\",\"lastdateprocessed\":\"2006-09-11T00:00:00\",\"lastdateadded\":\"2015-03-18T00:00:00\",\"enabled\":true,\"Monthly\":828.22,\"MonthlyIncreaseDecrease\":-277.57999999999993,\"Annual\":7463.5099999999993,\"AnnualIncreaseDecrease\":6981.8099999999995,\"Subs\":6}},{\"payments\":[{\"Id\":754,\"TransactionId\":\"zw9715oBV7HO3JLL85ZMCwbXJZnZ95sBaJoQ4\",\"Amount\":48.0,\"Date\":\"2016-09-30T10:00:00\"},{\"Id\":755,\"TransactionId\":\"J9aNQjOBvNF6xX55LDgQFxDLnR7zV3sm3mazL\",\"Amount\":28.38,\"Date\":\"2016-08-31T10:00:00\"}],\"Id\":198,\"Name\":\"DigitalOcean\",\"IsActive\":true,\"Period\":null,\"NextBillingDate\":null,\"PreviousBillingDate\":\"2016-09-30T10:00:00\",\"DateCreated\":\"2016-08-31T10:00:00\",\"MotherSubscriptionName\":\"DigitalOcean\",\"LastPaymentAmount\":48.0,\"MotherSubscriptionLogoUrl\":\"https://www.filepicker.io/api/file/ifyYwXhCSSeIFfBXUex8/convert?crop=6,20,393,66\",\"motherSubscriptionId\":507,\"accountid\":2,\"account\":{\"Id\":2,\"_id\":\"1DkAyOzRPAhYKvjj5NP4TvE9nzEeZjFjKRPm8\",\"_item\":\"638E0nB4KEHYjpggeRE6TJv65Nv4KVFy5v8ap\",\"balance_available\":\"0\",\"balance_current\":\"1997.76\",\"institution_type\":\"wells\",\"meta_name\":\"PLATINUM CARD\",\"meta_number\":\"7154\",\"subtype\":\"credit\",\"type\":\"credit\",\"lastdateprocessed\":\"2006-09-11T00:00:00\",\"lastdateadded\":\"2016-05-20T00:00:00\",\"enabled\":true,\"Monthly\":214.59000000000003,\"MonthlyIncreaseDecrease\":-20.099999999999994,\"Annual\":951.25000000000011,\"AnnualIncreaseDecrease\":951.25000000000011,\"Subs\":10}},{\"payments\":[{\"Id\":803,\"TransactionId\":\"KVJDnr930DTvLa44QVwkuzZr46jvOLSAXyOxO\",\"Amount\":8.33,\"Date\":\"2016-10-02T11:00:00\"},{\"Id\":804,\"TransactionId\":\"8KbyrX15AyfY4EDD5xRnTv3wVRN0YdSxkxO7L\",\"Amount\":8.33,\"Date\":\"2016-09-01T10:00:00\"},{\"Id\":805,\"TransactionId\":\"BJL6gBNdP6skLV66qDyNS1B9XdZywOt3RDaoP\",\"Amount\":8.33,\"Date\":\"2016-08-03T10:00:00\"},{\"Id\":806,\"TransactionId\":\"VLd0B7ADn0cd5bggDjkQsR9PN5wxK6s4Jp8yQ\",\"Amount\":8.33,\"Date\":\"2016-07-02T10:00:00\"},{\"Id\":807,\"TransactionId\":\"ydgmDZ7q8ms9BQ44JZRgh43zXodQDahpPz7Va\",\"Amount\":8.33,\"Date\":\"2016-06-03T10:00:00\"}],\"Id\":208,\"Name\":\"Google Apps for Work\",\"IsActive\":true,\"Period\":1,\"NextBillingDate\":null,\"PreviousBillingDate\":\"2016-10-02T11:00:00\",\"DateCreated\":\"2016-06-03T10:00:00\",\"MotherSubscriptionName\":\"Google Apps for Work\",\"LastPaymentAmount\":8.33,\"MotherSubscriptionLogoUrl\":\"https://www.filepicker.io/api/file/lHhuap8CRQqiCye5VpBK\",\"motherSubscriptionId\":123,\"accountid\":2,\"account\":{\"Id\":2,\"_id\":\"1DkAyOzRPAhYKvjj5NP4TvE9nzEeZjFjKRPm8\",\"_item\":\"638E0nB4KEHYjpggeRE6TJv65Nv4KVFy5v8ap\",\"balance_available\":\"0\",\"balance_current\":\"1997.76\",\"institution_type\":\"wells\",\"meta_name\":\"PLATINUM CARD\",\"meta_number\":\"7154\",\"subtype\":\"credit\",\"type\":\"credit\",\"lastdateprocessed\":\"2006-09-11T00:00:00\",\"lastdateadded\":\"2016-05-20T00:00:00\",\"enabled\":true,\"Monthly\":214.59000000000003,\"MonthlyIncreaseDecrease\":-20.099999999999994,\"Annual\":951.25000000000011,\"AnnualIncreaseDecrease\":951.25000000000011,\"Subs\":10}},{\"payments\":[{\"Id\":808,\"TransactionId\":\"k5zr93wMArsk6aooq3K5c07jbXAEwJu0jbkVn\",\"Amount\":14.34,\"Date\":\"2016-10-02T11:00:00\"},{\"Id\":809,\"TransactionId\":\"AxwvbqmY1vspmj996Va8TgLo8AnXMDHMxMvaD\",\"Amount\":14.34,\"Date\":\"2016-09-02T10:00:00\"},{\"Id\":810,\"TransactionId\":\"PQOx68ZvnxuRnDbbayP1SZOk5ew0DMso5yrkn\",\"Amount\":14.34,\"Date\":\"2016-08-02T10:00:00\"},{\"Id\":811,\"TransactionId\":\"OR9bwJ0AnbSnAVyya3gxFbR6Mkdmn1cxPADno\",\"Amount\":14.34,\"Date\":\"2016-07-02T10:00:00\"},{\"Id\":812,\"TransactionId\":\"BJL6gBNdP6skLV66qDyNS1B9XdZywOt3RDap8\",\"Amount\":14.34,\"Date\":\"2016-06-02T10:00:00\"}],\"Id\":209,\"Name\":\"HostGator\",\"IsActive\":true,\"Period\":1,\"NextBillingDate\":null,\"PreviousBillingDate\":\"2016-10-02T11:00:00\",\"DateCreated\":\"2016-06-02T10:00:00\",\"MotherSubscriptionName\":\"HostGator\",\"LastPaymentAmount\":14.34,\"MotherSubscriptionLogoUrl\":\"https://www.filepicker.io/api/file/4GuzShtWSyy3edRcMdXW\",\"motherSubscriptionId\":294,\"accountid\":2,\"account\":{\"Id\":2,\"_id\":\"1DkAyOzRPAhYKvjj5NP4TvE9nzEeZjFjKRPm8\",\"_item\":\"638E0nB4KEHYjpggeRE6TJv65Nv4KVFy5v8ap\",\"balance_available\":\"0\",\"balance_current\":\"1997.76\",\"institution_type\":\"wells\",\"meta_name\":\"PLATINUM CARD\",\"meta_number\":\"7154\",\"subtype\":\"credit\",\"type\":\"credit\",\"lastdateprocessed\":\"2006-09-11T00:00:00\",\"lastdateadded\":\"2016-05-20T00:00:00\",\"enabled\":true,\"Monthly\":214.59000000000003,\"MonthlyIncreaseDecrease\":-20.099999999999994,\"Annual\":951.25000000000011,\"AnnualIncreaseDecrease\":951.25000000000011,\"Subs\":10}},{\"payments\":[{\"Id\":826,\"TransactionId\":\"xpoRL89P7RiEqKkkx8g4ikEbqnMXQDuOpO51N\",\"Amount\":11.99,\"Date\":\"2016-09-07T10:00:00\"},{\"Id\":827,\"TransactionId\":\"ydgmDZ7q8ms9BQ44JZRgh43zXodQDahpPz76V\",\"Amount\":11.99,\"Date\":\"2016-08-07T10:00:00\"},{\"Id\":828,\"TransactionId\":\"qVKaQ0wE8aTjdzZZQ0pBiZXEp5qQ73sD5joje\",\"Amount\":11.99,\"Date\":\"2016-07-07T10:00:00\"},{\"Id\":829,\"TransactionId\":\"vQJRwxoD8Ruk1300QxojS0ZaJExp6nSVPyDLD\",\"Amount\":11.99,\"Date\":\"2016-06-07T10:00:00\"},{\"Id\":858,\"TransactionId\":\"rOA6nj7d86sEn5KKQj3NTOvvx1NRA8IegKmba\",\"Amount\":11.99,\"Date\":\"2016-10-07T00:00:00\"}],\"Id\":212,\"Name\":\"Hulu\",\"IsActive\":true,\"Period\":1,\"NextBillingDate\":null,\"PreviousBillingDate\":\"2016-10-07T00:00:00\",\"DateCreated\":\"2016-06-07T10:00:00\",\"MotherSubscriptionName\":\"Hulu\",\"LastPaymentAmount\":11.99,\"MotherSubscriptionLogoUrl\":\"https://www.filepicker.io/api/file/SuMOCYZ2RGKnPTOB8H6p\",\"motherSubscriptionId\":33,\"accountid\":2,\"account\":{\"Id\":2,\"_id\":\"1DkAyOzRPAhYKvjj5NP4TvE9nzEeZjFjKRPm8\",\"_item\":\"638E0nB4KEHYjpggeRE6TJv65Nv4KVFy5v8ap\",\"balance_available\":\"0\",\"balance_current\":\"1997.76\",\"institution_type\":\"wells\",\"meta_name\":\"PLATINUM CARD\",\"meta_number\":\"7154\",\"subtype\":\"credit\",\"type\":\"credit\",\"lastdateprocessed\":\"2006-09-11T00:00:00\",\"lastdateadded\":\"2016-05-20T00:00:00\",\"enabled\":true,\"Monthly\":214.59000000000003,\"MonthlyIncreaseDecrease\":-20.099999999999994,\"Annual\":951.25000000000011,\"AnnualIncreaseDecrease\":951.25000000000011,\"Subs\":10}},{\"payments\":[{\"Id\":813,\"TransactionId\":\"ydgmDZ7q8ms9BQ44JZROS1nDRVZB5Yu9MRwjE\",\"Amount\":26.95,\"Date\":\"2016-10-02T11:00:00\"},{\"Id\":814,\"TransactionId\":\"b054xdKRD4S6jqAAed1MFnbP6jqY7JiJaJV4n\",\"Amount\":26.95,\"Date\":\"2016-09-02T10:00:00\"},{\"Id\":815,\"TransactionId\":\"1DkAyOzRPAhYKvjj5NP4TvQKB4xMV9unrVQKr\",\"Amount\":26.95,\"Date\":\"2016-08-02T10:00:00\"},{\"Id\":816,\"TransactionId\":\"gxRpo1A73ps38VxxL1mns0eDmYVBARS8EpO0r\",\"Amount\":26.95,\"Date\":\"2016-07-01T10:00:00\"},{\"Id\":817,\"TransactionId\":\"qVKaQ0wE8aTjdzZZQ0pBiZXEp5qQ73sD5joYB\",\"Amount\":26.95,\"Date\":\"2016-06-02T10:00:00\"}],\"Id\":210,\"Name\":\"Intuit Quickbooks Online\",\"IsActive\":true,\"Period\":1,\"NextBillingDate\":null,\"PreviousBillingDate\":\"2016-10-02T11:00:00\",\"DateCreated\":\"2016-06-02T10:00:00\",\"MotherSubscriptionName\":\"Intuit Quickbooks Online\",\"LastPaymentAmount\":26.95,\"MotherSubscriptionLogoUrl\":\"https://www.filepicker.io/api/file/3GDf3NtjSQyeGwAf3ti4\",\"motherSubscriptionId\":46,\"accountid\":2,\"account\":{\"Id\":2,\"_id\":\"1DkAyOzRPAhYKvjj5NP4TvE9nzEeZjFjKRPm8\",\"_item\":\"638E0nB4KEHYjpggeRE6TJv65Nv4KVFy5v8ap\",\"balance_available\":\"0\",\"balance_current\":\"1997.76\",\"institution_type\":\"wells\",\"meta_name\":\"PLATINUM CARD\",\"meta_number\":\"7154\",\"subtype\":\"credit\",\"type\":\"credit\",\"lastdateprocessed\":\"2006-09-11T00:00:00\",\"lastdateadded\":\"2016-05-20T00:00:00\",\"enabled\":true,\"Monthly\":214.59000000000003,\"MonthlyIncreaseDecrease\":-20.099999999999994,\"Annual\":951.25000000000011,\"AnnualIncreaseDecrease\":951.25000000000011,\"Subs\":10}},{\"payments\":[{\"Id\":762,\"TransactionId\":\"J9aNQjOBvNF6xX55LDg4HAwjQod0rxum3OqeD\",\"Amount\":347.29,\"Date\":\"2016-09-19T10:00:00\"},{\"Id\":763,\"TransactionId\":\"gxRpo1A73ps38VxxL1mns0eDmYVBARS8EpOQp\",\"Amount\":347.29,\"Date\":\"2016-08-17T10:00:00\"},{\"Id\":764,\"TransactionId\":\"8KbyrX15AyfY4EDD5xRnTvk4nMqre8uxk6zXJ\",\"Amount\":347.29,\"Date\":\"2016-07-19T10:00:00\"},{\"Id\":765,\"TransactionId\":\"E53BVKJLmBsAYzkkxMLXsdwNQgeAb1uQqAvqp\",\"Amount\":347.29,\"Date\":\"2016-06-17T10:00:00\"},{\"Id\":766,\"TransactionId\":\"wnZ76Jj187uNn3AAaeoYfA7wxM7E7MiKJEDOe\",\"Amount\":347.29,\"Date\":\"2016-05-17T10:00:00\"},{\"Id\":767,\"TransactionId\":\"X1DRyVMPnRiMkm55aevxsokMn9kRk9sdjVKQo\",\"Amount\":347.29,\"Date\":\"2016-04-19T10:00:00\"},{\"Id\":768,\"TransactionId\":\"ZqB9kMvmn9iLvn660xjNiPoLdJoaoJsbPZrL6\",\"Amount\":347.29,\"Date\":\"2016-03-17T11:00:00\"},{\"Id\":769,\"TransactionId\":\"X1DRyVMPnRiMkm55aevxsokMn9kRk9sdjVK6D\",\"Amount\":347.29,\"Date\":\"2016-02-17T11:00:00\"},{\"Id\":987,\"TransactionId\":\"vQJRwxoD8Ruk1300Qxo6S4mXZYvKpqf0D173R\",\"Amount\":347.29,\"Date\":\"2016-10-18T00:00:00\"}],\"Id\":200,\"Name\":\"Nissan Motor Acceptance Corp Bill Payment\",\"IsActive\":true,\"Period\":1,\"NextBillingDate\":null,\"PreviousBillingDate\":\"2016-10-18T00:00:00\",\"DateCreated\":\"2016-02-17T11:00:00\",\"MotherSubscriptionName\":\"Nissan Motor Acceptance Corp Bill Payment\",\"LastPaymentAmount\":347.29,\"MotherSubscriptionLogoUrl\":\"https://www.filepicker.io/api/file/AvPbRq2TS674QITSZBlg\",\"motherSubscriptionId\":366,\"accountid\":1,\"account\":{\"Id\":1,\"_id\":\"LOZ1p4myj1sqEQaaLP9bTmPgzZP5KotmwAq0D\",\"_item\":\"638E0nB4KEHYjpggeRE6TJv65Nv4KVFy5v8ap\",\"balance_available\":\"1435.41\",\"balance_current\":\"1924.1\",\"institution_type\":\"wells\",\"meta_name\":\"COMPLETE ADVANTAGERM\",\"meta_number\":\"1530\",\"subtype\":\"checking\",\"type\":\"depository\",\"lastdateprocessed\":\"2006-09-11T00:00:00\",\"lastdateadded\":\"2015-03-18T00:00:00\",\"enabled\":true,\"Monthly\":828.22,\"MonthlyIncreaseDecrease\":-277.57999999999993,\"Annual\":7463.5099999999993,\"AnnualIncreaseDecrease\":6981.8099999999995,\"Subs\":6}},{\"payments\":[{\"Id\":853,\"TransactionId\":\"DZqE9yQ6BEsKOmnnb3YqtE6MQ8r816twQ1Ery\",\"Amount\":9.99,\"Date\":\"2016-09-14T10:00:00\"},{\"Id\":854,\"TransactionId\":\"o7q9EA1XO9u96P00KAXzh0JzXZ6Aw7S40zMxe\",\"Amount\":9.99,\"Date\":\"2016-08-15T10:00:00\"},{\"Id\":855,\"TransactionId\":\"VLd0B7ADn0cd5bggDjkQsR9PN5wxK6s4Jp8pQ\",\"Amount\":9.99,\"Date\":\"2016-07-15T10:00:00\"},{\"Id\":856,\"TransactionId\":\"8KbyrX15AyfY4EDD5xRnTvk4nMqre8uxk6zwX\",\"Amount\":9.99,\"Date\":\"2016-06-14T10:00:00\"},{\"Id\":981,\"TransactionId\":\"ydgmDZ7q8ms9BQ44JZR6tyxDN6X6QxspP6n8g\",\"Amount\":40.12,\"Date\":\"2016-09-17T00:00:00\"},{\"Id\":984,\"TransactionId\":\"0LyZ3VER1Zcom3YY6xRkiE6LdKOR0buvXn5NN\",\"Amount\":9.99,\"Date\":\"2016-10-15T00:00:00\"}],\"Id\":216,\"Name\":\"Skype\",\"IsActive\":true,\"Period\":1,\"NextBillingDate\":null,\"PreviousBillingDate\":\"2016-10-15T00:00:00\",\"DateCreated\":\"2016-06-14T10:00:00\",\"MotherSubscriptionName\":\"Skype\",\"LastPaymentAmount\":9.99,\"MotherSubscriptionLogoUrl\":\"https://www.filepicker.io/api/file/uRRNEeq9RXWGbZngZt0K\",\"motherSubscriptionId\":1,\"accountid\":2,\"account\":{\"Id\":2,\"_id\":\"1DkAyOzRPAhYKvjj5NP4TvE9nzEeZjFjKRPm8\",\"_item\":\"638E0nB4KEHYjpggeRE6TJv65Nv4KVFy5v8ap\",\"balance_available\":\"0\",\"balance_current\":\"1997.76\",\"institution_type\":\"wells\",\"meta_name\":\"PLATINUM CARD\",\"meta_number\":\"7154\",\"subtype\":\"credit\",\"type\":\"credit\",\"lastdateprocessed\":\"2006-09-11T00:00:00\",\"lastdateadded\":\"2016-05-20T00:00:00\",\"enabled\":true,\"Monthly\":214.59000000000003,\"MonthlyIncreaseDecrease\":-20.099999999999994,\"Annual\":951.25000000000011,\"AnnualIncreaseDecrease\":951.25000000000011,\"Subs\":10}},{\"payments\":[{\"Id\":770,\"TransactionId\":\"av8PaNb0LPcjqzYYZVbASrek3jExZaS73dbmM\",\"Amount\":199.98,\"Date\":\"2016-09-19T10:00:00\"},{\"Id\":771,\"TransactionId\":\"MMo5XaAKn5iJBK11wNL0fNRgZy6qdrsXgNqAo\",\"Amount\":199.98,\"Date\":\"2016-08-18T10:00:00\"},{\"Id\":772,\"TransactionId\":\"AxwvbqmY1vspmj996Va8TgeQOKkRzrtMxpJdm\",\"Amount\":199.98,\"Date\":\"2016-07-18T10:00:00\"},{\"Id\":773,\"TransactionId\":\"QnEOLZQ0XOubRX00mKqeTwK8Yp6xe1COoKeo9\",\"Amount\":199.98,\"Date\":\"2016-06-20T10:00:00\"},{\"Id\":774,\"TransactionId\":\"DZqE9yQ6BEsKOmnnb3YjhpdO1PdxdPswQNOM0\",\"Amount\":199.98,\"Date\":\"2016-05-18T10:00:00\"},{\"Id\":775,\"TransactionId\":\"o7q9EA1XO9u96P00KAXzh0JnANJQJNu40zLMp\",\"Amount\":199.98,\"Date\":\"2016-04-18T10:00:00\"},{\"Id\":776,\"TransactionId\":\"gxRpo1A73ps38VxxL1mns0e5Byepeyu8Epm4V\",\"Amount\":199.98,\"Date\":\"2016-03-18T11:00:00\"},{\"Id\":986,\"TransactionId\":\"AxwvbqmY1vspmj996VavTmn3eY9BRycnJ1Bqp\",\"Amount\":199.98,\"Date\":\"2016-10-18T00:00:00\"}],\"Id\":201,\"Name\":\"Toyota Motor Credit Corp\",\"IsActive\":true,\"Period\":1,\"NextBillingDate\":null,\"PreviousBillingDate\":\"2016-10-18T00:00:00\",\"DateCreated\":\"2016-03-18T11:00:00\",\"MotherSubscriptionName\":\"Toyota Motor Credit Corp\",\"LastPaymentAmount\":199.98,\"MotherSubscriptionLogoUrl\":\"https://www.filepicker.io/api/file/8TztzWSdQOWnf7AV9LbQ\",\"motherSubscriptionId\":454,\"accountid\":1,\"account\":{\"Id\":1,\"_id\":\"LOZ1p4myj1sqEQaaLP9bTmPgzZP5KotmwAq0D\",\"_item\":\"638E0nB4KEHYjpggeRE6TJv65Nv4KVFy5v8ap\",\"balance_available\":\"1435.41\",\"balance_current\":\"1924.1\",\"institution_type\":\"wells\",\"meta_name\":\"COMPLETE ADVANTAGERM\",\"meta_number\":\"1530\",\"subtype\":\"checking\",\"type\":\"depository\",\"lastdateprocessed\":\"2006-09-11T00:00:00\",\"lastdateadded\":\"2015-03-18T00:00:00\",\"enabled\":true,\"Monthly\":828.22,\"MonthlyIncreaseDecrease\":-277.57999999999993,\"Annual\":7463.5099999999993,\"AnnualIncreaseDecrease\":6981.8099999999995,\"Subs\":6}},{\"payments\":[{\"Id\":839,\"TransactionId\":\"mbLPOKg70PidvLgg3KkOtLyEjq3qdbh4ReXPr\",\"Amount\":5.0,\"Date\":\"2016-10-01T10:00:00\"},{\"Id\":840,\"TransactionId\":\"5BxjvQEOyjsY0MAAr8gOTEQXLgZbmkIwmw7Eq\",\"Amount\":5.0,\"Date\":\"2016-09-01T10:00:00\"},{\"Id\":841,\"TransactionId\":\"638E0nB4KEHYjpggeRE6TJRj91bQg6cwZekeq\",\"Amount\":5.0,\"Date\":\"2016-08-01T10:00:00\"},{\"Id\":842,\"TransactionId\":\"7y5Djpg4kDierLBBKmg9TVApLv074zi0Z9Rdo\",\"Amount\":5.0,\"Date\":\"2016-07-01T10:00:00\"},{\"Id\":843,\"TransactionId\":\"DZqE9yQ6BEsKOmnnb3YjhpdNrw91MVTwQNpaw\",\"Amount\":5.0,\"Date\":\"2016-06-01T10:00:00\"},{\"Id\":844,\"TransactionId\":\"ydgmDZ7q8ms9BQ44JZRoIKMAkwRRxLCOgqx5L\",\"Amount\":5.0,\"Date\":\"2016-09-27T10:00:00\"},{\"Id\":845,\"TransactionId\":\"0LyZ3VER1Zcom3YY6xReix9Aa46Vn8sABAk1P\",\"Amount\":5.0,\"Date\":\"2016-08-27T10:00:00\"},{\"Id\":846,\"TransactionId\":\"mbLPOKg70PidvLgg3KkXsgOmvzE59AtBE0J0J\",\"Amount\":5.0,\"Date\":\"2016-07-27T10:00:00\"},{\"Id\":847,\"TransactionId\":\"5BxjvQEOyjsY0MAAr8gOTEn05d6eYMhwmpRZo\",\"Amount\":5.0,\"Date\":\"2016-06-27T10:00:00\"},{\"Id\":848,\"TransactionId\":\"mbLPOKg70PidvLgg3KkXsgOmvzE59AtBE0J5L\",\"Amount\":5.0,\"Date\":\"2016-05-27T10:00:00\"},{\"Id\":849,\"TransactionId\":\"3PjgkEObBguYA7338ZLzU9Ejdpbp6EToM8jkP\",\"Amount\":20.0,\"Date\":\"2016-09-16T10:00:00\"},{\"Id\":850,\"TransactionId\":\"gxRpo1A73ps38VxxL1mns0eDmYVBARS8EpO7r\",\"Amount\":20.0,\"Date\":\"2016-08-16T10:00:00\"},{\"Id\":851,\"TransactionId\":\"OR9bwJ0AnbSnAVyya3gxFbR6Mkdmn1cxPADAo\",\"Amount\":20.0,\"Date\":\"2016-07-16T10:00:00\"},{\"Id\":852,\"TransactionId\":\"5BxjvQEOyjsY0MAAr8gOTEn05d6eYMhwmpRZM\",\"Amount\":20.0,\"Date\":\"2016-06-16T10:00:00\"},{\"Id\":985,\"TransactionId\":\"jZe87Bdnk8sbpwggPeVBfLDP98vyxkUjBPYNL\",\"Amount\":20.0,\"Date\":\"2016-10-16T00:00:00\"}],\"Id\":215,\"Name\":\"Web Flow\",\"IsActive\":true,\"Period\":1,\"NextBillingDate\":null,\"PreviousBillingDate\":\"2016-10-16T00:00:00\",\"DateCreated\":\"2016-05-27T10:00:00\",\"MotherSubscriptionName\":\"Web Flow\",\"LastPaymentAmount\":5.0,\"MotherSubscriptionLogoUrl\":\"https://www.filepicker.io/api/file/N0MjvDfRYewhUFFtfSXD\",\"motherSubscriptionId\":477,\"accountid\":2,\"account\":{\"Id\":2,\"_id\":\"1DkAyOzRPAhYKvjj5NP4TvE9nzEeZjFjKRPm8\",\"_item\":\"638E0nB4KEHYjpggeRE6TJv65Nv4KVFy5v8ap\",\"balance_available\":\"0\",\"balance_current\":\"1997.76\",\"institution_type\":\"wells\",\"meta_name\":\"PLATINUM CARD\",\"meta_number\":\"7154\",\"subtype\":\"credit\",\"type\":\"credit\",\"lastdateprocessed\":\"2006-09-11T00:00:00\",\"lastdateadded\":\"2016-05-20T00:00:00\",\"enabled\":true,\"Monthly\":214.59000000000003,\"MonthlyIncreaseDecrease\":-20.099999999999994,\"Annual\":951.25000000000011,\"AnnualIncreaseDecrease\":951.25000000000011,\"Subs\":10}}]";
        private static string UserDashboard = "{\"Id\":1,\"Monthly\":1042.8100000000002,\"MonthlyIncreaseDecrease\":-297.68000000000029,\"Annual\":8414.7599999999948,\"AnnualIncreaseDecrease\":7933.0599999999949,\"Subs\":16}";

        public static List<UserSubscription> DummyListOfSubscriptions()
        {
            return JsonConvert.DeserializeObject<List<UserSubscription>>(ListOfSubscriptions);
        }

        public static UserDashboard DummyDashboard()
        {
            return JsonConvert.DeserializeObject<UserDashboard>(UserDashboard);
        }
    }
}
