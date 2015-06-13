namespace NServiceBus
{
    using System;
    using NServiceBus.Extensibility;

    /// <summary>
    ///     Allows the users to control how the send is performed
    /// </summary>
    public class SendOptions:ExtendableOptions
    {

        /// <summary>
        ///     Creates an instance of <see cref="SendOptions" />.
        /// </summary>
        /// <param name="destination">Specifies a specific destination for the message.</param>
        /// <param name="deliverAt">Tells the bus to deliver the message at the given time.</param>
        /// <param name="delayDeliveryFor">Tells the bus to wait the specified amount of time before delivering the message.</param>
        public SendOptions(string destination = null,DateTime? deliverAt = null, TimeSpan? delayDeliveryFor = null)
        {
            if (deliverAt != null && delayDeliveryFor != null)
            {
                throw new ArgumentException("Ensure you either set `deliverAt` or `delayDeliveryFor`, but not both.");
            }

            Delay = delayDeliveryFor;
            At = deliverAt;
            Destination = destination;
        }

        internal string Destination { get; private set; }

        internal TimeSpan? Delay { get; private set; }

        internal DateTime? At { get; private set; }
    }
}