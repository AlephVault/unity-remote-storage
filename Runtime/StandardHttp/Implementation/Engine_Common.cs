using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AlephVault.Unity.RemoteStorage.Types.Results;
using AlephVault.Unity.Support.Types.Async;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlasticPipe.Server;
using UnityEngine;
using UnityEngine.Networking;


namespace AlephVault.Unity.RemoteStorage.StandardHttp
{
    namespace Implementation
    {
        public static partial class Engine
        {
            /// <summary>
            ///   An exception to be raised on http queries. The
            ///   validation errors are given, when the case.
            /// </summary>
            public class Exception : System.Exception
            {
                /// <summary>
                ///   The result code.
                /// </summary>
                public readonly ResultCode Code;

                /// <summary>
                ///   The result code. This only applies for custom
                ///   400 errors.
                /// </summary>
                public readonly string RequestErrorCode;

                /// <summary>
                ///   The validation errors.
                /// </summary>
                public readonly JObject ValidationErrors;
                
                public Exception(ResultCode code, JObject errors = null) : base($"Storage access failure ({code})")
                {
                    Code = code;
                    ValidationErrors = errors;
                }

                public Exception(ResultCode code, string requestErrorCode) : base($"Storage access failure ({code})")
                {
                    Code = code;
                    RequestErrorCode = requestErrorCode;
                }
            }

            // Sends a request, waits for it, and captures some errors.
            private static async Task SendRequest(UnityWebRequest request)
            {
                await request.SendWebRequest();
                // Check whether the request was done successfully.
                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    throw new Exception(ResultCode.Unreachable);
                }
            }

            /// <summary>
            ///   Deserialize an object from a JSON byte array.
            /// </summary>
            /// <param name="data">The bytes to deserialize from</param>
            /// <typeparam name="ElementType">The element type</typeparam>
            /// <returns>The deserialized element</returns>
            /// <exception cref="Exception">A JSON deserialization error occurred</exception>
            private static ElementType Deserialize<ElementType>(byte[] data)
            {
                return Deserialize<ElementType>(data, ResultCode.FormatError);
            }

            /// <summary>
            ///   Deserialize an object from a JSON byte array.
            /// </summary>
            /// <param name="data">The bytes to deserialize from</param>
            /// <param name="resultCode">The error to raise</param>
            /// <typeparam name="ElementType">The element type</typeparam>
            /// <returns>The deserialized element</returns>
            /// <exception cref="Exception">A JSON deserialization error occurred</exception>
            private static ElementType Deserialize<ElementType>(byte[] data, ResultCode resultCode)
            {
                try
                {
                    return JsonSerializer.Create().Deserialize<ElementType>(
                        new JsonTextReader(new StreamReader(new MemoryStream(data)))
                    );
                }
                catch (System.Exception)
                {
                    throw new Exception(resultCode);
                }
            }

            /// <summary>
            ///   Deserialize a JObject from a JSON byte array.
            /// </summary>
            /// <param name="data">The bytes to deserialize from</param>
            /// <returns>The deserialized element</returns>
            /// <exception cref="Exception">A JSON deserialization error occurred</exception>
            private static JObject DeserializeJObject(byte[] data)
            {
                try
                {
                    MemoryStream stream = new MemoryStream(data);
                    StreamReader reader = new StreamReader(stream);
                    return JObject.Parse(reader.ReadToEnd());
                }
                catch (System.Exception e)
                {
                    throw new Exception(ResultCode.FormatError);
                }
            }

            /// <summary>
            ///   Deserialize a JArray from a JSON byte array.
            /// </summary>
            /// <param name="data">The bytes to deserialize from</param>
            /// <returns>The deserialized element</returns>
            /// <exception cref="Exception">A JSON deserialization error occurred</exception>
            private static JArray DeserializeJArray(byte[] data)
            {
                try
                {
                    MemoryStream stream = new MemoryStream(data);
                    StreamReader reader = new StreamReader(stream);
                    return JArray.Parse(reader.ReadToEnd());
                }
                catch (System.Exception e)
                {
                    throw new Exception(ResultCode.FormatError);
                }
            }

            // Serializes content using Newtonsoft.Json.
            private static byte[] Serialize<ElementType>(ElementType data, ResultCode errorCode = ResultCode.FormatError)
            {
                try
                {
                    MemoryStream stream = new MemoryStream();
                    using (StreamWriter streamWriter = new StreamWriter(stream))
                    using (JsonTextWriter jsonWriter = new JsonTextWriter(streamWriter))
                    {
                        JsonSerializer.Create().Serialize(jsonWriter, data);
                    }

                    return stream.ToArray();
                }
                catch (System.Exception e)
                {
                    throw new Exception(errorCode);
                }
            }
        }
    }
}