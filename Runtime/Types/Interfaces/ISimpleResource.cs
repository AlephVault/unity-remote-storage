using System.Collections.Generic;
using System.Threading.Tasks;
using AlephVault.Unity.RemoteStorage.Types.Results;
using Newtonsoft.Json.Linq;


namespace AlephVault.Unity.RemoteStorage
{
    namespace Types
    {
        namespace Interfaces
        {
            /// <summary>
            ///   A reference to a simple resource. Simple resources can be
            ///   created, updated, replaced, deleted and read. Also, methods
            ///   can be invoked (if available), setup either as a read-only
            ///   operation or a write-enabled operation.
            /// </summary>
            /// <typeparam name="AuthType">The type to marshal authentication details</typeparam>
            /// <typeparam name="ElementType">The type to marshal the related resource instances</typeparam>
            /// <typeparam name="IDType">The type to marshal the related resource ids</typeparam>
            public interface ISimpleResource<AuthType, ElementType, IDType>
            {
                /// <summary>
                ///   Creates the resource. It is an error if the resource is
                ///   already created. It may also incur in validation errors
                ///   (or even key conflict errors on soft-deleted instances).
                /// </summary>
                /// <param name="body">The resource body</param>
                /// <returns>A result of the operation</returns>
                public Task<Result<ElementType, IDType>> Create(ElementType body);
                
                /// <summary>
                ///   Reads the resource. It is an error if the resource is not
                ///   already created.
                /// </summary>
                /// <returns>A result of the operation</returns>
                public Task<Result<ElementType, IDType>> Read();
                
                /// <summary>
                ///   Updates the resource. It is an error if the resource is
                ///   not already created. It may also incur in validation errors
                ///   (or even key conflict errors on soft-deleted instances).
                /// </summary>
                /// <param name="changes">The map of changes to apply</param>
                /// <returns>A result of the operation</returns>
                public Task<Result<ElementType, IDType>> Update(JObject changes);
                
                /// <summary>
                ///   Replaces the resource with a new one. It is an error if the
                ///   resource is not already created. It may also incur in validation
                ///   errors (or even key conflict errors on soft-deleted instances).
                /// </summary>
                /// <param name="replacement">The new resource body</param>
                /// <returns>A result of the operation</returns>
                public Task<Result<ElementType, IDType>> Replace(ElementType replacement);
                
                /// <summary>
                ///   Deletes the resource. It is an error if the resource is not
                ///   already created.
                /// </summary>
                /// <returns>A result of the operation</returns>
                public Task<Result<ElementType, IDType>> Delete();
                
                /// <summary>
                ///   Queries a view method from the only item in the
                ///   collection.
                /// </summary>
                /// <param name="method">The method to query</param>
                /// <param name="args">The arguments to pass</param>
                /// <returns>A result of the view</returns>
                public Task<Result<JObject, IDType>> ViewToJson(string method, Dictionary<string, string> args);

                /// <summary>
                ///   Runs an operation method from the only item in
                ///   the collection. It also provides a custom body.
                /// </summary>
                /// <param name="method">The method to run</param>
                /// <param name="args">The arguments to pass</param>
                /// <param name="body">The body to send</param>
                /// <typeparam name="E">The body type</typeparam>
                /// <returns>A result of the operation</returns>
                public Task<Result<JObject, IDType>> OperationToJson<E>(string method, Dictionary<string, string> args,
                    E body);

                /// <summary>
                ///   Runs an operation method from the only item in
                ///   the collection.
                /// </summary>
                /// <param name="method">The method to run</param>
                /// <param name="args">The arguments to pass</param>
                /// <returns>A result of the operation</returns>
                public Task<Result<JObject, IDType>> OperationToJson(string method, Dictionary<string, string> args);
                
                /// <summary>
                ///   Queries a view method from the only item in the
                ///   collection.
                /// </summary>
                /// <param name="method">The method to query</param>
                /// <param name="args">The arguments to pass</param>
                /// <returns>A result of the view</returns>
                public Task<Result<JArray, IDType>> ViewToJsonArray(string method, Dictionary<string, string> args);

                /// <summary>
                ///   Runs an operation method from the only item in
                ///   the collection. It also provides a custom body.
                /// </summary>
                /// <param name="method">The method to run</param>
                /// <param name="args">The arguments to pass</param>
                /// <param name="body">The body to send</param>
                /// <typeparam name="E">The body type</typeparam>
                /// <returns>A result of the operation</returns>
                public Task<Result<JArray, IDType>> OperationToJsonArray<E>(string method, Dictionary<string, string> args,
                    E body);

                /// <summary>
                ///   Runs an operation method from the only item in
                ///   the collection.
                /// </summary>
                /// <param name="method">The method to run</param>
                /// <param name="args">The arguments to pass</param>
                /// <returns>A result of the operation</returns>
                public Task<Result<JArray, IDType>> OperationToJsonArray(string method, Dictionary<string, string> args);
                
                /// <summary>
                ///   Queries a view method from the only item in the
                ///   collection.
                /// </summary>
                /// <param name="method">The method to query</param>
                /// <param name="args">The arguments to pass</param>
                /// <typeparam name="ResponseType">The response type</typeparam>
                /// <returns>A result of the view</returns>
                public Task<Result<ResponseType, IDType>> ViewTo<ResponseType>(string method, Dictionary<string, string> args);

                /// <summary>
                ///   Runs an operation method from the only item in
                ///   the collection. It also provides a custom body.
                /// </summary>
                /// <param name="method">The method to run</param>
                /// <param name="args">The arguments to pass</param>
                /// <param name="body">The body to send</param>
                /// <typeparam name="ResponseType">The response type</typeparam>
                /// <typeparam name="E">The body type</typeparam>
                /// <returns>A result of the operation</returns>
                public Task<Result<ResponseType, IDType>> OperationTo<E, ResponseType>(string method, Dictionary<string, string> args,
                    E body);

                /// <summary>
                ///   Runs an operation method from the only item in
                ///   the collection.
                /// </summary>
                /// <param name="method">The method to run</param>
                /// <param name="args">The arguments to pass</param>
                /// <typeparam name="ResponseType">The response type</typeparam>
                /// <returns>A result of the operation</returns>
                public Task<Result<ResponseType, IDType>> OperationTo<ResponseType>(string method, Dictionary<string, string> args);
            }
        }
    }
}