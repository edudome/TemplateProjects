// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: greet.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Template.gRPC.Service {
  public static partial class GreetService
  {
    static readonly string __ServiceName = "greet.GreetService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Template.gRPC.Service.GreetRequest> __Marshaller_greet_GreetRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Template.gRPC.Service.GreetRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Template.gRPC.Service.GreetResponse> __Marshaller_greet_GreetResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Template.gRPC.Service.GreetResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Template.gRPC.Service.GreetManyTimesRequest> __Marshaller_greet_GreetManyTimesRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Template.gRPC.Service.GreetManyTimesRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Template.gRPC.Service.GreetManytimesResponse> __Marshaller_greet_GreetManytimesResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Template.gRPC.Service.GreetManytimesResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Template.gRPC.Service.LongGreetRequest> __Marshaller_greet_LongGreetRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Template.gRPC.Service.LongGreetRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Template.gRPC.Service.LongGreetResponse> __Marshaller_greet_LongGreetResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Template.gRPC.Service.LongGreetResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Template.gRPC.Service.GreetEveryoneRequest> __Marshaller_greet_GreetEveryoneRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Template.gRPC.Service.GreetEveryoneRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Template.gRPC.Service.GreetEveryoneResponse> __Marshaller_greet_GreetEveryoneResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Template.gRPC.Service.GreetEveryoneResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Template.gRPC.Service.GreetWithDeadlineRequest> __Marshaller_greet_GreetWithDeadlineRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Template.gRPC.Service.GreetWithDeadlineRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Template.gRPC.Service.GreetWithDeadlineResponse> __Marshaller_greet_GreetWithDeadlineResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Template.gRPC.Service.GreetWithDeadlineResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Template.gRPC.Service.GreetRequest, global::Template.gRPC.Service.GreetResponse> __Method_Greet = new grpc::Method<global::Template.gRPC.Service.GreetRequest, global::Template.gRPC.Service.GreetResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Greet",
        __Marshaller_greet_GreetRequest,
        __Marshaller_greet_GreetResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Template.gRPC.Service.GreetManyTimesRequest, global::Template.gRPC.Service.GreetManytimesResponse> __Method_GreetManyTimes = new grpc::Method<global::Template.gRPC.Service.GreetManyTimesRequest, global::Template.gRPC.Service.GreetManytimesResponse>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "GreetManyTimes",
        __Marshaller_greet_GreetManyTimesRequest,
        __Marshaller_greet_GreetManytimesResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Template.gRPC.Service.LongGreetRequest, global::Template.gRPC.Service.LongGreetResponse> __Method_LongGreet = new grpc::Method<global::Template.gRPC.Service.LongGreetRequest, global::Template.gRPC.Service.LongGreetResponse>(
        grpc::MethodType.ClientStreaming,
        __ServiceName,
        "LongGreet",
        __Marshaller_greet_LongGreetRequest,
        __Marshaller_greet_LongGreetResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Template.gRPC.Service.GreetEveryoneRequest, global::Template.gRPC.Service.GreetEveryoneResponse> __Method_GreetEveryone = new grpc::Method<global::Template.gRPC.Service.GreetEveryoneRequest, global::Template.gRPC.Service.GreetEveryoneResponse>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "GreetEveryone",
        __Marshaller_greet_GreetEveryoneRequest,
        __Marshaller_greet_GreetEveryoneResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Template.gRPC.Service.GreetWithDeadlineRequest, global::Template.gRPC.Service.GreetWithDeadlineResponse> __Method_GreetWithDeadline = new grpc::Method<global::Template.gRPC.Service.GreetWithDeadlineRequest, global::Template.gRPC.Service.GreetWithDeadlineResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GreetWithDeadline",
        __Marshaller_greet_GreetWithDeadlineRequest,
        __Marshaller_greet_GreetWithDeadlineResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Template.gRPC.Service.GreetReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for GreetService</summary>
    public partial class GreetServiceClient : grpc::ClientBase<GreetServiceClient>
    {
      /// <summary>Creates a new client for GreetService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GreetServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GreetService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GreetServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GreetServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GreetServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Template.gRPC.Service.GreetResponse Greet(global::Template.gRPC.Service.GreetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Greet(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Template.gRPC.Service.GreetResponse Greet(global::Template.gRPC.Service.GreetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Greet, null, options, request);
      }
      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Template.gRPC.Service.GreetResponse> GreetAsync(global::Template.gRPC.Service.GreetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GreetAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Template.gRPC.Service.GreetResponse> GreetAsync(global::Template.gRPC.Service.GreetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Greet, null, options, request);
      }
      /// <summary>
      /// Server Streaming
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncServerStreamingCall<global::Template.gRPC.Service.GreetManytimesResponse> GreetManyTimes(global::Template.gRPC.Service.GreetManyTimesRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GreetManyTimes(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Server Streaming
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncServerStreamingCall<global::Template.gRPC.Service.GreetManytimesResponse> GreetManyTimes(global::Template.gRPC.Service.GreetManyTimesRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_GreetManyTimes, null, options, request);
      }
      /// <summary>
      /// Client Streaming
      /// </summary>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncClientStreamingCall<global::Template.gRPC.Service.LongGreetRequest, global::Template.gRPC.Service.LongGreetResponse> LongGreet(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return LongGreet(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Client Streaming
      /// </summary>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncClientStreamingCall<global::Template.gRPC.Service.LongGreetRequest, global::Template.gRPC.Service.LongGreetResponse> LongGreet(grpc::CallOptions options)
      {
        return CallInvoker.AsyncClientStreamingCall(__Method_LongGreet, null, options);
      }
      /// <summary>
      /// BiDi Streaming
      /// </summary>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::Template.gRPC.Service.GreetEveryoneRequest, global::Template.gRPC.Service.GreetEveryoneResponse> GreetEveryone(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GreetEveryone(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// BiDi Streaming
      /// </summary>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::Template.gRPC.Service.GreetEveryoneRequest, global::Template.gRPC.Service.GreetEveryoneResponse> GreetEveryone(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_GreetEveryone, null, options);
      }
      /// <summary>
      /// Unary With Deadline
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Template.gRPC.Service.GreetWithDeadlineResponse GreetWithDeadline(global::Template.gRPC.Service.GreetWithDeadlineRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GreetWithDeadline(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Unary With Deadline
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Template.gRPC.Service.GreetWithDeadlineResponse GreetWithDeadline(global::Template.gRPC.Service.GreetWithDeadlineRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GreetWithDeadline, null, options, request);
      }
      /// <summary>
      /// Unary With Deadline
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Template.gRPC.Service.GreetWithDeadlineResponse> GreetWithDeadlineAsync(global::Template.gRPC.Service.GreetWithDeadlineRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GreetWithDeadlineAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Unary With Deadline
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Template.gRPC.Service.GreetWithDeadlineResponse> GreetWithDeadlineAsync(global::Template.gRPC.Service.GreetWithDeadlineRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GreetWithDeadline, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override GreetServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GreetServiceClient(configuration);
      }
    }

  }
}
#endregion
