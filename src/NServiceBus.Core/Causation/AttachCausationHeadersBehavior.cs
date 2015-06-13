namespace NServiceBus
{
    using System;
    using NServiceBus.Pipeline.Contexts;
    using NServiceBus.TransportDispatch;

    class AttachCausationHeadersBehavior :PhysicalOutgoingContextStageBehavior
    {

        public override void Invoke(Context context, Action next)
        {
            ApplyHeaders(context);

            next();
         }

        void ApplyHeaders(Context context)
        {
            var conversationId = CombGuid.Generate().ToString();

            TransportMessage incomingMessage;

            if (context.TryGet(TransportReceiveContext.IncomingPhysicalMessageKey,out incomingMessage))
            {
                context.SetHeader(Headers.RelatedTo,incomingMessage.Id);

                string conversationIdFromCurrentMessageContext;
                if (incomingMessage.Headers.TryGetValue(Headers.ConversationId, out conversationIdFromCurrentMessageContext))
                {
                    conversationId = conversationIdFromCurrentMessageContext;
                }
            }

            context.SetHeader(Headers.ConversationId,conversationId);
        }
    }
}