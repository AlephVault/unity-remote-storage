using System.Collections.Generic;
using System.Threading.Tasks;
using AlephVault.Unity.RemoteStorage.StandardHttp.Implementation;
using AlephVault.Unity.RemoteStorage.Types.Interfaces;
using AlephVault.Unity.RemoteStorage.Types.Results;
using Newtonsoft.Json.Linq;


namespace AlephVault.Unity.RemoteStorage
{
    namespace StandardHttp
    {
        namespace Types
        {
            /// <summary>
            ///   A Standard HTTP MongoDB Storage simple resource.
            /// </summary>
            public class SimpleResource<ElementType> :
                Resource, ISimpleResource<Authorization, ElementType, string>
            {
                /// <summary>
                ///   Creating a simple resource requires both
                ///   the name and authorization header, as well
                ///   as the base endpoint to hit.
                /// </summary>
                /// <param name="name">The resource name</param>
                /// <param name="baseEndpoint">The base endpoint</param>
                /// <param name="authorization">The authorization header</param>
                public SimpleResource(string name, string baseEndpoint, Authorization authorization) :
                    base(name, baseEndpoint, authorization) {}

                public Task<Result<ElementType, string>> Create(ElementType body)
                {
                    return WrapException(async () =>
                    {
                        string id = await Engine.Create($"{BaseEndpoint}/{Name}", Authorization, body);
                        return new Result<ElementType, string>
                        {
                            Code = ResultCode.Created,
                            CreatedID = id
                        };
                    });
                }

                public Task<Result<ElementType, string>> Read()
                {
                    return WrapException(async () =>
                    {
                        ElementType result = await Engine.One<ElementType, Authorization>(
                            $"{BaseEndpoint}/{Name}", Authorization
                        );
                        return new Result<ElementType, string>
                        {
                            Code = ResultCode.Ok,
                            Element = result
                        };
                    });
                }

                public Task<Result<ElementType, string>> Update(JObject changes)
                {
                    return WrapException(async () =>
                    {
                        await Engine.Update(
                            $"{BaseEndpoint}/{Name}", Authorization, changes
                        );
                        return new Result<ElementType, string>
                        {
                            Code = ResultCode.Ok
                        };
                    });
                }

                public Task<Result<ElementType, string>> Replace(ElementType replacement)
                {
                    return WrapException(async () =>
                    {
                        await Engine.Replace(
                            $"{BaseEndpoint}/{Name}", Authorization, replacement
                        );
                        return new Result<ElementType, string>
                        {
                            Code = ResultCode.Ok
                        };
                    });
                }

                public Task<Result<ElementType, string>> Delete()
                {
                    return WrapException(async () =>
                    {
                        await Engine.Delete($"{BaseEndpoint}/{Name}", Authorization);
                        return new Result<ElementType, string>
                        {
                            Code = ResultCode.Ok
                        };
                    });
                }

                public Task<Result<JObject, string>> ViewToJson(string method, Dictionary<string, string> args)
                {
                    return WrapException(async () =>
                    {
                        JObject obj = await Engine.ViewToJson($"{BaseEndpoint}/{Name}/~{method}", Authorization, args);
                        return new Result<JObject, string>
                        {
                            Element = obj,
                            Code = ResultCode.Ok
                        };
                    });
                }

                public Task<Result<JObject, string>> OperationToJson<E>(string method, Dictionary<string, string> args, E body)
                {
                    return WrapException(async () =>
                    {
                        JObject obj = await Engine.OperationToJson($"{BaseEndpoint}/{Name}/~{method}", Authorization, args, body);
                        return new Result<JObject, string>
                        {
                            Element = obj,
                            Code = ResultCode.Ok
                        };
                    });
                }

                public Task<Result<JObject, string>> OperationToJson(string method, Dictionary<string, string> args)
                {
                    return WrapException(async () =>
                    {
                        JObject obj = await Engine.OperationToJson($"{BaseEndpoint}/{Name}/~{method}", Authorization, args);
                        return new Result<JObject, string>
                        {
                            Element = obj,
                            Code = ResultCode.Ok
                        };
                    });
                }

                public Task<Result<JArray, string>> ViewToJsonArray(string method, Dictionary<string, string> args)
                {
                    return WrapException(async () =>
                    {
                        JArray obj =
                            await Engine.ViewToJsonArray($"{BaseEndpoint}/{Name}/~{method}", Authorization, args);
                        return new Result<JArray, string>
                        {
                            Element = obj,
                            Code = ResultCode.Ok
                        };
                    });
                }

                public Task<Result<JArray, string>> OperationToJsonArray<E>(string method, Dictionary<string, string> args, E body)
                {
                    return WrapException(async () =>
                    {
                        JArray obj = await Engine.OperationToJsonArray($"{BaseEndpoint}/{Name}/~{method}", Authorization, args, body);
                        return new Result<JArray, string>
                        {
                            Element = obj,
                            Code = ResultCode.Ok
                        };
                    });
                }

                public Task<Result<JArray, string>> OperationToJsonArray(string method, Dictionary<string, string> args)
                {
                    return WrapException(async () =>
                    {
                        JArray obj = await Engine.OperationToJsonArray($"{BaseEndpoint}/{Name}/~{method}", Authorization, args);
                        return new Result<JArray, string>
                        {
                            Element = obj,
                            Code = ResultCode.Ok
                        };
                    });
                }

                public Task<Result<ResponseType, string>> ViewTo<ResponseType>(string method, Dictionary<string, string> args)
                {
                    return WrapException(async () =>
                    {
                        ResponseType obj = await Engine.ViewTo<Authorization, ResponseType>($"{BaseEndpoint}/{Name}/~{method}", Authorization, args);
                        return new Result<ResponseType, string>
                        {
                            Element = obj,
                            Code = ResultCode.Ok
                        };
                    });
                }

                public Task<Result<ResponseType, string>> OperationTo<E, ResponseType>(string method, Dictionary<string, string> args, E body)
                {
                    return WrapException(async () =>
                    {
                        ResponseType obj = await Engine.OperationTo<E, Authorization, ResponseType>($"{BaseEndpoint}/{Name}/~{method}", Authorization, args, body);
                        return new Result<ResponseType, string>
                        {
                            Element = obj,
                            Code = ResultCode.Ok
                        };
                    });
                }

                public Task<Result<ResponseType, string>> OperationTo<ResponseType>(string method, Dictionary<string, string> args)
                {
                    return WrapException(async () =>
                    {
                        ResponseType obj = await Engine.OperationTo<Authorization, ResponseType>($"{BaseEndpoint}/{Name}/~{method}", Authorization, args);
                        return new Result<ResponseType, string>
                        {
                            Element = obj,
                            Code = ResultCode.Ok
                        };
                    });
                }
            }
        }
    }
}