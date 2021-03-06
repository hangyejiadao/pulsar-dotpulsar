﻿/*
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace DotPulsar
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;

    /// <summary>
    /// The message received by consumers and readers.
    /// </summary>
    public sealed class Message
    {
        internal Message(
            MessageId messageId,
            ReadOnlySequence<byte> data,
            string producerName,
            ulong sequenceId,
            uint redeliveryCount,
            ulong eventTime,
            ulong publishTime,
            IReadOnlyDictionary<string, string> properties,
            bool hasBase64EncodedKey,
            string? key,
            byte[]? orderingKey)
        {
            MessageId = messageId;
            Data = data;
            ProducerName = producerName;
            SequenceId = sequenceId;
            RedeliveryCount = redeliveryCount;
            EventTime = eventTime;
            PublishTime = publishTime;
            Properties = properties;
            HasBase64EncodedKey = hasBase64EncodedKey;
            Key = key;
            OrderingKey = orderingKey;
        }

        /// <summary>
        /// The id of the message.
        /// </summary>
        public MessageId MessageId { get; }

        /// <summary>
        /// The raw payload of the message.
        /// </summary>
        public ReadOnlySequence<byte> Data { get; }

        /// <summary>
        /// The name of the producer who produced the message.
        /// </summary>
        public string ProducerName { get; }

        /// <summary>
        /// The sequence id of the message.
        /// </summary>
        public ulong SequenceId { get; }

        /// <summary>
        /// The redelivery count (maintained by the broker) of the message.
        /// </summary>
        public uint RedeliveryCount { get; }

        /// <summary>
        /// Check whether the message has an event time.
        /// </summary>
        public bool HasEventTime => EventTime != 0;

        /// <summary>
        /// The event time of the message as unix time in milliseconds.
        /// </summary>
        public ulong EventTime { get; }

        /// <summary>
        /// The event time of the message as an UTC DateTime.
        /// </summary>
        public DateTime EventTimeAsDateTime => EventTimeAsDateTimeOffset.UtcDateTime;

        /// <summary>
        /// The event time of the message as a DateTimeOffset with an offset of 0.
        /// </summary>
        public DateTimeOffset EventTimeAsDateTimeOffset => DateTimeOffset.FromUnixTimeMilliseconds((long) EventTime);

        /// <summary>
        /// Check whether the key been base64 encoded.
        /// </summary>
        public bool HasBase64EncodedKey { get; }

        /// <summary>
        /// Check whether the message has a key.
        /// </summary>
        public bool HasKey => Key is not null;

        /// <summary>
        /// The key as a string.
        /// </summary>
        public string? Key { get; }

        /// <summary>
        /// The key as bytes.
        /// </summary>
        public byte[]? KeyBytes => Key is not null ? Convert.FromBase64String(Key) : null;

        /// <summary>
        /// Check whether the message has an ordering key.
        /// </summary>
        public bool HasOrderingKey => OrderingKey is not null;

        /// <summary>
        /// The ordering key of the message.
        /// </summary>
        public byte[]? OrderingKey { get; }

        /// <summary>
        /// The publish time of the message as unix time in milliseconds.
        /// </summary>
        public ulong PublishTime { get; }

        /// <summary>
        /// The publish time of the message as an UTC DateTime.
        /// </summary>
        public DateTime PublishTimeAsDateTime => PublishTimeAsDateTimeOffset.UtcDateTime;

        /// <summary>
        /// The publish time of the message as a DateTimeOffset with an offset of 0.
        /// </summary>
        public DateTimeOffset PublishTimeAsDateTimeOffset => DateTimeOffset.FromUnixTimeMilliseconds((long) PublishTime);

        /// <summary>
        /// The properties of the message.
        /// </summary>
        public IReadOnlyDictionary<string, string> Properties { get; }
    }
}
